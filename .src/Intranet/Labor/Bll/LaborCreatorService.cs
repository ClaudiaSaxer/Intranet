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

        /// <summary>
        /// Validates the test value where only one item is allowed to exists for the given input parameter. Throws a <see cref="InvalidDataException"/> if more than one or none is found.
        /// Used for example as average or standard deviation.
        /// </summary>
        /// <param name="testValue">the testvalue collection to validate.</param>
        /// <param name="testType">the testtype that is validate</param>
        /// <param name="valueType">the valuetype that is validated</param>
        /// <returns></returns>
        private TestValue ValidateTestValueOnlyExactlyOneHasToExist( ICollection<TestValue> testValue, String testType, String valueType )
        {
            if ( testValue.Count > 1 )
            {
                Logger.Error( "Only one Average for " + testType + " per Testsheet allowed" );
                throw new InvalidDataException( "Only one " + valueType + " for " + testType + " per Testsheet allowed" );
            }

            var item = testValue.FirstOrDefault();
            if ( item == null )
            {
                Logger.Error( "No  " + valueType + "  for " + testType + " per Testsheet existing" );
                throw new InvalidDataException( "No  " + valueType + "  for " + testType + " per Testsheet existing" );
            }
            return item;
        }

        /// <summary>
        /// Gets the BabyDiaperTestValue out of a list of testvalues for the correct <see cref="TestTypeBabyDiaper"/> and <see cref="TestValueType"/>
        /// </summary>
        /// <param name="testValues">the test values containing the wanted item</param>
        /// <param name="testTypeBabyDiaper">the type of the baby diaper. <see cref="TestTypeBabyDiaper"/></param>
        /// <param name="testValueType">the value type of the baby diaper. <see cref="TestValueType"/></param>
        /// <returns></returns>
        private BabyDiaperTestValue GetBabyDiaperTestValueForType( IEnumerable<TestValue> testValues, TestTypeBabyDiaper testTypeBabyDiaper, TestValueType testValueType )
        {
            var values = testValues.ToList()
                                   .Where(
                                       x => ( x.TestValueType == testValueType ) && ( x.BabyDiaperTestValue.TestType == testTypeBabyDiaper ) )
                                   .ToList();
            var item = ValidateTestValueOnlyExactlyOneHasToExist( values, testTypeBabyDiaper.ToString(), testValueType.ToString() );
            return item.BabyDiaperTestValue;
        }
        /// <summary>
        /// Creates a retention Average with the data from the test values
        /// </summary>
        /// <param name="testValues">testvalues containing a item representing data for the average</param>
        /// <returns>a class representing the retention with the average</returns>
        private Rewet ToRewetAverage( IEnumerable<TestValue> testValues )
            => ToRewet( GetBabyDiaperTestValueForType( testValues, TestTypeBabyDiaper.Rewet, TestValueType.Average ) );
        /// <summary>
        /// Creates a retention Average with the data from the test values
        /// </summary>
        /// <param name="testValues">testvalues containing a item representing data for the average</param>
        /// <returns>a class representing the retention with the average</returns>
        private Retention ToRetentionAverage( IEnumerable<TestValue> testValues )
            => ToRetention( GetBabyDiaperTestValueForType( testValues, TestTypeBabyDiaper.Retention, TestValueType.Average ) );

        /// <summary>
        /// Creates a PenetrationTime Average with the data from the test values
        /// </summary>
        /// <param name="testValues">testvalues containing a item representing data for the average</param>
        /// <returns>a class representing the penetration time with the average</returns>
        private PenetrationTime ToPenetrationTimeAverage( IEnumerable<TestValue> testValues )
            => ToPenetrationTime( GetBabyDiaperTestValueForType( testValues, TestTypeBabyDiaper.PenetrationTime, TestValueType.Average ) );
        /// <summary>
        /// Creates a Rewet Standard Deviation with the data from the test values
        /// </summary>
        /// <param name="testValues">testvalues containing a item representing data for the standard deviation</param>
        /// <returns>a class representing the rewet with the standard deviation</returns>
        private Rewet ToRewetStandardDeviation( IEnumerable<TestValue> testValues )
            => ToRewet( GetBabyDiaperTestValueForType( testValues, TestTypeBabyDiaper.Rewet, TestValueType.StandardDeviation ) );
        /// <summary>
        /// Creates a Retention Standard Deviation with the data from the test values
        /// </summary>
        /// <param name="testValues">testvalues containing a item representing data for the standard deviation</param>
        /// <returns>a class representing the retention with the standard deviation</returns>
        private Retention ToRetentionStandardDeviation( IEnumerable<TestValue> testValues )
            => ToRetention( GetBabyDiaperTestValueForType( testValues, TestTypeBabyDiaper.Retention, TestValueType.StandardDeviation ) );

        /// <summary>
        /// Creates a PenetrationTime Standard Deviation with the data from the test values
        /// </summary>
        /// <param name="testValues">testvalues containing a item representing data for the standard deviation</param>
        /// <returns>a class representing the penetration time with the standard deviation</returns>
        private PenetrationTime ToPenetrationTimeStandardDeviation( IEnumerable<TestValue> testValues )
            => ToPenetrationTime( GetBabyDiaperTestValueForType( testValues, TestTypeBabyDiaper.PenetrationTime, TestValueType.StandardDeviation ) );

        /// <summary>
        /// Creates a collection of a TestValue Type and selects only the needed items from a Collection with help of the input parameter
        /// </summary>
        /// <typeparam name="T">the collection type. For example: <see cref="RewetTestValue"/>, <see cref="PenetrationTimeTestValue"/>,<see cref="RetentionTestValue"/></typeparam>
        /// <param name="testValue">the testvalues that contain the data</param>
        /// <param name="testValueType">the value type of the test. <see cref="TestValueType"/></param>
        /// <param name="testTypeBabyDiaper">the type of the baby diaper. <see cref="TestTypeBabyDiaper"/></param>
        /// <param name="toTestTypeTestValueAction">the action the get the correct data for a collection item. For example: <see cref="ToRewetTestValue"/>, <see cref="ToRetentionTestValue"/>, <see cref="ToPenetrationTimeTestValue"/></param>
        /// <returns>A Collection with the Type of the output from the <paramref name="toTestTypeTestValueAction"/> given as a input action </returns>
        private Collection<T> ToTestValuesCollectionByTestType<T>( IEnumerable<TestValue> testValue,
                                                                   TestValueType testValueType,
                                                                   ICollection<TestTypeBabyDiaper> testTypeBabyDiaper,
                                                                   Func<BabyDiaperTestValue, String, String, T> toTestTypeTestValueAction )
        {
            var tests = new Collection<T>();
            var values = testValue.ToList()
                                  .Where(
                                      x =>
                                          ( x.TestValueType == testValueType )
                                          && x.BabyDiaperTestValue.TestType.IsIn( testTypeBabyDiaper ) )
                                  .ForEach(
                                      x =>
                                          tests.Add( toTestTypeTestValueAction( x.BabyDiaperTestValue,
                                                                                x.LastEditedPerson,
                                                                                GenerateProdCode( x.TestSheet.MachineNr,
                                                                                                  x.TestSheet.CreatedDateTime.Year,
                                                                                                  x.DayInYearOfArticleCreation,
                                                                                                  x.BabyDiaperTestValue.DiaperCreatedTime ) ) ) );
            return tests;
        }

        /// <summary>
        /// Creates the rewet test value collection for all singe tests
        /// </summary>
        /// <param name="testValues">the testvalues containing the rewet tests</param>
        /// <returns>a collection for all rewet tests</returns>
        private ICollection<RewetTestValue> ToRewetTestValuesCollection( IEnumerable<TestValue> testValues )
            => ToTestValuesCollectionByTestType( testValues,
                                                 TestValueType.Single,
                                                 new List<TestTypeBabyDiaper> { TestTypeBabyDiaper.Rewet, TestTypeBabyDiaper.PenetrationTime },
                                                 ToRewetTestValue );

        /// <summary>
        /// Creates the retention  test value collection for all singe tests
        /// </summary>
        /// <param name="testValues">the testvalues containing the retention tests</param>
        /// <returns>a collection for all retention tests</returns>
        private ICollection<RetentionTestValue> ToRetentionTestValuesCollection( IEnumerable<TestValue> testValues )
            => ToTestValuesCollectionByTestType( testValues,
                                                 TestValueType.Single,
                                                 new List<TestTypeBabyDiaper> { TestTypeBabyDiaper.Retention },
                                                 ToRetentionTestValue );
        /// <summary>
        /// Creates the penetration time test value collection for all singe tests
        /// </summary>
        /// <param name="testValues">the testvalues containing the penetration time tests</param>
        /// <returns>a collection for all penetration time tests</returns>
        private ICollection<PenetrationTimeTestValue> ToPenetrationTimeTestValuesCollection( IEnumerable<TestValue> testValues )
            => ToTestValuesCollectionByTestType( testValues,
                                                 TestValueType.Single,
                                                 new List<TestTypeBabyDiaper> { TestTypeBabyDiaper.PenetrationTime },
                                                 ToPenetrationTimeTestValue );

        /// <summary>
        /// Creates to TestInfo from diffrent input data
        /// </summary>
        /// <param name="testPerson">the person who did the test</param>
        /// <param name="prodCode">the prodcution code from the diaper</param>
        /// <param name="weightDiaperDry">the weight of the dry diaper</param>
        /// <returns></returns>
        private TestInfo toTestInfo( String testPerson, String prodCode, Double weightDiaperDry )
            => new TestInfo
            {
                TestPerson = testPerson,
                ProductionCode = prodCode,
                WeightyDiaperDry = weightDiaperDry
            };

        /// <summary>
        /// Creates the Rewet TestValue from diffrent input data
        /// </summary>
        /// <param name="rewet">the baby diaper test value containing the rewet data</param>
        /// <param name="testPerson">the person who did the test</param>
        /// <param name="prodCode">the diaper production code</param>
        /// <returns></returns>
        private RewetTestValue ToRewetTestValue( BabyDiaperTestValue rewet, String testPerson, String prodCode )
        {
            var vm = new RewetTestValue
            {
                TestInfo = toTestInfo( testPerson, prodCode, rewet.WeightDiaperDry ),
                Rewet = ToRewet( rewet )
            };
            return vm;
        }
        /// <summary>
        /// Creates the PenetrationTime TestValue from diffrent input data
        /// </summary>
        /// <param name="penetrationTime">the baby diaper test value containing the penetration time data</param>
        /// <param name="testPerson">the person who did the test</param>
        /// <param name="prodCode">the diaper production code</param>
        /// <returns></returns>
        private PenetrationTimeTestValue ToPenetrationTimeTestValue( BabyDiaperTestValue penetrationTime, String testPerson, String prodCode )
        {
            var vm = new PenetrationTimeTestValue
            {
                TestInfo = toTestInfo( testPerson, prodCode, penetrationTime.WeightDiaperDry ),
                PenetrationTime = ToPenetrationTime( penetrationTime )
            };
            return vm;
        }

        /// <summary>
        /// Creates the RetentionTestValues from diffrent input data
        /// </summary>
        /// <param name="retention">the baby diaper test value containing the retention data</param>
        /// <param name="testPerson">the person who did the test</param>
        /// <param name="prodCode">the diaper production code</param>
        /// <returns></returns>
        private RetentionTestValue ToRetentionTestValue( BabyDiaperTestValue retention, String testPerson, String prodCode )
        {
            var vm = new RetentionTestValue
            {
                TestInfo = toTestInfo( testPerson, prodCode, retention.WeightDiaperDry ),
                Retention = ToRetention( retention )
            };
            return vm;
        }

        /// <summary>
        /// Sets the values for the Rewet View Model out of the BabyDiaperTestValue Model
        /// </summary>
        /// <param name="rewet">the Baby Diaper Test value with the rewet data</param>
        /// <returns>The rewet View Model with the data collected from the model</returns>
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

        /// <summary>
        /// Sets the values for the Retention View Model out of the BabyDiaperTestValue Model
        /// </summary>
        /// <param name="retention">the Baby Diaper Test value with the retention data</param>
        /// <returns>The Retention View Model with the data collected from the model</returns>
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
        /// <summary>
        /// Sets the values for the Penetration Time View Model out of the BabyDiaperTestValue Model
        /// </summary>
        /// <param name="penetrationTime">the Baby Diaper Test value with the penetration time data</param>
        /// <returns>The Penetration Time View Model with the data collected from the model</returns>
        private PenetrationTime ToPenetrationTime( BabyDiaperTestValue penetrationTime )
            => new PenetrationTime
            {
                PenetrationTimeAdditionFourth = penetrationTime.PenetrationTimeAdditionFourth,
                PenetrationTimeAdditionSecond = penetrationTime.PenetrationTimeAdditionSecond,
                PenetrationTimeAdditionFirst = penetrationTime.PenetrationTimeAdditionFirst,
                PenetrationTimeAdditionThird = penetrationTime.PenetrationTimeAdditionThird
            };

        /// <summary>
        /// Generates the Production Code for the Diaper
        /// </summary>
        /// <param name="machine">The machine Nr</param>
        /// <param name="year">the year of the production of the diaper</param>
        /// <param name="dayOfyear">the day of the year of the production of the diaper</param>
        /// <param name="time">the time od the production of the diaper</param>
        /// <returns>A Production code for a single diaper</returns>
        private String GenerateProdCode( String machine, Int32 year, Int32 dayOfyear, TimeSpan time )
            => "IT/" + machine + "/" + year + "/" + dayOfyear + "/" + dayOfyear + "/" + time.Minutes + ":" + time.Seconds;

        #endregion

        #region Implementation of ILaborCreatorService

        #endregion
    }
}