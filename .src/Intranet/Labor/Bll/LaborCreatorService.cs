using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using Extend;
using Intranet.Common;
using Intranet.Labor.Bll;
using Intranet.Labor.Model;
using Intranet.Labor.Model.labor;
using Intranet.Labor.ViewModel;

namespace Intranet.Web.Areas.Labor.Controllers
{
    /// <summary>
    ///     Class representing the labor creator service
    /// </summary>
    public class LaborCreatorService : ServiceBase, ILaborCreatorService
    {
        #region Properties

        /// <summary>
        ///     Gets or sets the Labor creator bll
        /// </summary>
        /// <value>the labor creator bll</value>
        public ILaborCreatorBll LaborCreatorBll { get; set; }

        #endregion

        #region Ctor

        /// <summary>
        ///     Initialize a new instance of the <see cref="ServiceBase" /> class.
        /// </summary>
        /// <param name="loggerFactory">A <see cref="ILoggerFactory" />.</param>
        public LaborCreatorService( ILoggerFactory loggerFactory )
            : base( loggerFactory.CreateLogger( typeof(LaborCreatorService) ) )
        {
        }

        #endregion

        #region Implementation of ILaborCreatorService

        /// <summary>
        ///     Get the labor creator view model for a specific id.
        /// </summary>
        /// <returns>the labor creator view model</returns>
        public LaborCreatorViewModel GetLaborCreatorViewModel( Int32 testSheetId )
        {
            var testSheet = LaborCreatorBll.getTestSheetForId( testSheetId );
            var babydiaper = testSheet.TestValues.ToList()
                                      .Where( x => x.ArticleTestType == ArticleType.BabyDiaper )
                                      .ToList();
            var rewets = ToRewetTestValuesCollection( babydiaper );
            var rewetAverage = ToRewetAverage( babydiaper );
            var rewetStandardDeviation = ToRewetStandardDeviation( babydiaper );

            var retentions = ToRetentionTestValuesCollection( babydiaper );
            var retentionAverage = ToRetentionAverage( babydiaper );
            var retentionStandardDeviation = ToRetentionStandardDeviation( babydiaper );

            var penetrationTimes = ToPenetrationTimeTestValuesCollection( babydiaper );
            var penetrationTimeAverage = ToPenetrationTimeAverage( babydiaper );
            var penetrationTimeStandardDeviation = ToPenetrationTimeStandardDeviation( babydiaper );
            var vm = new LaborCreatorViewModel
            {
                Producer = "Intigena",
                Shift = testSheet.ShiftType.ToFriendlyString(),
                FaNr = testSheet.FaNr,
                ProductName = testSheet.ProductName,
                SizeName = testSheet.SizeName,
                CreatedDate = testSheet.CreatedDateTime.ToShortDateString(),
                Rewets = rewets,
                RewetAverage = rewetAverage,
                RewetStandardDeviation = rewetStandardDeviation,
                Retentions = retentions,
                RetentionAverage = retentionAverage,
                RetentionStandardDeviation = retentionStandardDeviation,
                PenetrationTimes = penetrationTimes,
                PenetrationTimeStandardDeviation = penetrationTimeStandardDeviation,
                PenetrationTimeAverage = penetrationTimeAverage
            };
            return vm;
        }

        private TestValue TestTestValueOnlyExactlyOneHasToExist( ICollection<TestValue> testValue, String testType, String valueType )
        {
            if (testValue.Count > 1)
            {
                Logger.Error("Only one Average for "+ testType + " per Testsheet allowed");
                throw new InvalidDataException("Only one "+ valueType + " for " + testType + " per Testsheet allowed");
            }

            var item = testValue.FirstOrDefault();
            if (item == null)
            {
                Logger.Error("No  " + valueType +"  for " + testType + " per Testsheet existing");
                throw new InvalidDataException("No  " + valueType +"  for " + testType + " per Testsheet existing");
            }
            return item;
        }
        private Rewet ToRewetAverage( IEnumerable<TestValue> testValue )
        {
            var average = testValue.ToList()
                                   .Where(
                                       x => ( x.TestValueType == TestValueType.Average ) && ( x.BabyDiaperTestValue.TestType == TestTypeBabyDiaper.Rewet ) )
                                   .ToList();
            var item = TestTestValueOnlyExactlyOneHasToExist( average, "Rewet", "Average" );
            return ToRewet( item.BabyDiaperTestValue );
        }

        private Retention ToRetentionAverage( IEnumerable<TestValue> testValue )
        {
            var average = testValue.ToList()
                                   .Where(
                                       x => ( x.TestValueType == TestValueType.Average ) && ( x.BabyDiaperTestValue.TestType == TestTypeBabyDiaper.Retention ) )
                                   .ToList();
            var item = TestTestValueOnlyExactlyOneHasToExist(average, "Retention", "Average");

            return ToRetention( item.BabyDiaperTestValue );
        }

        private PenetrationTime ToPenetrationTimeAverage( IEnumerable<TestValue> testValue )
        {
            var average = testValue.ToList()
                                   .Where(
                                       x => ( x.TestValueType == TestValueType.Average ) && ( x.BabyDiaperTestValue.TestType == TestTypeBabyDiaper.PenetrationTime ) )
                                   .ToList();
            var item = TestTestValueOnlyExactlyOneHasToExist(average, "Penetration Time", "Average");

            return ToPenetrationTime( item.BabyDiaperTestValue );
        }

        private Rewet ToRewetStandardDeviation( IEnumerable<TestValue> testValue )
        {
            var standardDeviation = testValue.ToList()
                                             .Where(
                                                 x => ( x.TestValueType == TestValueType.StandardDeviation ) && ( x.BabyDiaperTestValue.TestType == TestTypeBabyDiaper.Rewet ) )
                                             .ToList();
            var item = TestTestValueOnlyExactlyOneHasToExist(standardDeviation, "Rewet", "Standard Deviation");

            return ToRewet( item.BabyDiaperTestValue );
        }

        private Retention ToRetentionStandardDeviation( IEnumerable<TestValue> testValue )
        {
            var standardDeviation = testValue.ToList()
                                             .Where(
                                                 x => ( x.TestValueType == TestValueType.StandardDeviation ) && ( x.BabyDiaperTestValue.TestType == TestTypeBabyDiaper.Retention ) )
                                             .ToList();
            var item = TestTestValueOnlyExactlyOneHasToExist(standardDeviation, "Rewet", "Standard Deviation");

            return ToRetention( item.BabyDiaperTestValue );
        }

        private PenetrationTime ToPenetrationTimeStandardDeviation( IEnumerable<TestValue> testValue )
        {
            var standardDeviation = testValue.ToList()
                                             .Where(
                                                 x =>
                                                     ( x.TestValueType == TestValueType.StandardDeviation )
                                                     && ( x.BabyDiaperTestValue.TestType == TestTypeBabyDiaper.PenetrationTime ) )
                                             .ToList();
            var item = TestTestValueOnlyExactlyOneHasToExist(standardDeviation, "Penetration Time", "Standard Deviation");

            return ToPenetrationTime( item.BabyDiaperTestValue );
        }

        private ICollection<RewetTestValue> ToRewetTestValuesCollection( IEnumerable<TestValue> testValue )
        {
            var rewets = new Collection<RewetTestValue>();
            var values = testValue.ToList()
                                  .Where(
                                      x =>
                                          ( x.TestValueType == TestValueType.Single )
                                          && ( ( x.BabyDiaperTestValue.TestType == TestTypeBabyDiaper.Rewet )
                                               || ( x.BabyDiaperTestValue.TestType == TestTypeBabyDiaper.PenetrationTime ) ) )
                                  .ForEach(
                                      x =>
                                          rewets.Add( ToRewetTestValue( x.BabyDiaperTestValue,
                                                                        x.LastEditedPerson,
                                                                        GenerateProdCode( x.TestSheet.MachineNr,
                                                                                          x.TestSheet.CreatedDateTime.Year,
                                                                                          x.DayInYearOfArticleCreation,
                                                                                          x.BabyDiaperTestValue.DiaperCreatedTime ) ) ) );
            return rewets;
        }

        private ICollection<RetentionTestValue> ToRetentionTestValuesCollection( IEnumerable<TestValue> testValue )
        {
            var retention = new Collection<RetentionTestValue>();
            var values = testValue.ToList()
                                  .Where(
                                      x => ( x.TestValueType == TestValueType.Single ) && ( x.BabyDiaperTestValue.TestType == TestTypeBabyDiaper.Retention ) )
                                  .ForEach(
                                      x =>
                                          retention.Add( ToRetentionTestValue( x.BabyDiaperTestValue,
                                                                               x.LastEditedPerson,
                                                                               GenerateProdCode( x.TestSheet.MachineNr,
                                                                                                 x.TestSheet.CreatedDateTime.Year,
                                                                                                 x.DayInYearOfArticleCreation,
                                                                                                 x.BabyDiaperTestValue.DiaperCreatedTime ) ) ) );
            return retention;
        }

        private ICollection<PenetrationTimeTestValue> ToPenetrationTimeTestValuesCollection( IEnumerable<TestValue> testValue )
        {
            var penetrationtime = new Collection<PenetrationTimeTestValue>();
            var values = testValue.ToList()
                                  .Where(
                                      x => ( x.TestValueType == TestValueType.Single ) && ( x.BabyDiaperTestValue.TestType == TestTypeBabyDiaper.PenetrationTime ) )
                                  .ForEach(
                                      x =>
                                          penetrationtime.Add( ToPenetrationTimeTestValue( x.BabyDiaperTestValue,
                                                                                           x.LastEditedPerson,
                                                                                           GenerateProdCode( x.TestSheet.MachineNr,
                                                                                                             x.TestSheet.CreatedDateTime.Year,
                                                                                                             x.DayInYearOfArticleCreation,
                                                                                                             x.BabyDiaperTestValue.DiaperCreatedTime ) ) ) );
            return penetrationtime;
        }

        private RewetTestValue ToRewetTestValue( BabyDiaperTestValue rewet, String testPerson, String prodCode )
        {
            var vm = new RewetTestValue
            {
                TestInfo = new TestInfo
                {
                    TestPerson = testPerson,
                    ProductionCode = prodCode,
                    WeightyDiaperDry = rewet.WeightDiaperDry
                },
                Rewet = ToRewet( rewet )
            };
            return vm;
        }

        private PenetrationTimeTestValue ToPenetrationTimeTestValue( BabyDiaperTestValue penetrationTime, String testPerson, String prodCode )
        {
            var vm = new PenetrationTimeTestValue
            {
                TestInfo = new TestInfo
                {
                    TestPerson = testPerson,
                    ProductionCode = prodCode,
                    WeightyDiaperDry = penetrationTime.WeightDiaperDry
                },
                PenetrationTime = ToPenetrationTime( penetrationTime )
            };
            return vm;
        }

        private RetentionTestValue ToRetentionTestValue( BabyDiaperTestValue retention, String testPerson, String prodCode )
        {
            var vm = new RetentionTestValue
            {
                TestInfo = new TestInfo
                {
                    TestPerson = testPerson,
                    ProductionCode = prodCode,
                    WeightyDiaperDry = retention.WeightDiaperDry
                },
                Retention = ToRetention( retention )
            };
            return vm;
        }

        private Rewet ToRewet( BabyDiaperTestValue rewet )
            => new Rewet
            {
                Revet210Rw = rewet.Revet210Rw,
                StrikeTroughValue = rewet.StrikeTroughValue,
                DistributionOfTheStrikeTrough = rewet.DistributionOfTheStrikeTrough,
                Revet210Value = rewet.Revet210Value,
                Revet140Rw = rewet.Revet140Rw,
                Revet140Value = rewet.Revert140Value
            };

        private Retention ToRetention( BabyDiaperTestValue retention )
            => new Retention
            {
                SapNr = retention.SapNr,
                RetentionAfterZentrifugeValue = retention.RetentionAfterZentrifugeValue,
                SapType = retention.SapType,
                RetentionRw = retention.RetentionRw,
                RetentionWetWeight = retention.RetentionWetWeight,
                RetentionAfterZentrifugePercent = retention.RetentionAfterZentrifugePercent,
                SapGHoewiValue = retention.SapGHoewiValue
            };

        private PenetrationTime ToPenetrationTime( BabyDiaperTestValue penetrationTime )
            => new PenetrationTime
            {
                PenetrationTimeAdditionFourth = penetrationTime.PenetrationTimeAdditionFourth,
                PenetrationTimeAdditionSecond = penetrationTime.PenetrationTimeAdditionSecond,
                PenetrationTimeAdditionFirst = penetrationTime.PenetrationTimeAdditionFirst,
                PenetrationTimeAdditionThird = penetrationTime.PenetrationTimeAdditionThird
            };

        private String GenerateProdCode( String machine, Int32 year, Int32 dayOfyear, TimeSpan time )
            => "IT/" + machine + "/" + year + "/" + dayOfyear + "/" + dayOfyear + "/" + time.Minutes + ":" + time.Seconds;

        #endregion

        #region Implementation of ILaborCreatorService

        #endregion
    }
}