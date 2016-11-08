#region Usings

using System;
using System.Linq;
using Extend;
using Intranet.Common;
using Intranet.Labor.Definition;
using Intranet.Labor.Model;
using Intranet.Labor.Model.labor;
using Intranet.Labor.ViewModel;

#endregion

namespace Intranet.Labor.Bll
{
    /// <summary>
    /// </summary>
    public class BabyDiaperServiceHelper : ServiceBase, IBabyDiaperServiceHelper
    {
        #region Properties

        /// <summary>
        ///     Gets or sets the bll for the baby diapers retention test.
        /// </summary>
        public IBabyDiaperRetentionBll BabyDiaperRetentionBll { get; set; }

        #endregion

        #region Ctor

        /// <summary>
        ///     Initialize a new instance of the <see cref="ServiceBase" /> class.
        /// </summary>
        /// <param name="loggerFactory">A <see cref="ILoggerFactory" />.</param>
        public BabyDiaperServiceHelper( ILoggerFactory loggerFactory )
            : base( loggerFactory.CreateLogger( typeof(BabyDiaperServiceHelper) ) )
        {
            Logger.Trace( "Enter Ctor - Exit." );
        }

        #endregion

        /// <summary>
        ///     Calculates all values for the baby diaper retention test
        /// </summary>
        /// <param name="babyDiaperTestValue">the test value</param>
        /// <param name="testSheetId">the test sheet id</param>
        /// <returns></returns>
        public BabyDiaperTestValue CalculateBabyDiaperRetentionValues( BabyDiaperTestValue babyDiaperTestValue,
                                                                       int testSheetId )
        {
            var testSheet = BabyDiaperRetentionBll.GetTestSheetInfo( testSheetId );
            var productionOrder = BabyDiaperRetentionBll.GetProductionOrder( testSheet.FaNr );

            babyDiaperTestValue.RetentionAfterZentrifugeValue = babyDiaperTestValue.RetentionWetWeight -
                                                                babyDiaperTestValue.WeightDiaperDry;
            if ( Math.Abs( babyDiaperTestValue.WeightDiaperDry ) > 0.1 )
                babyDiaperTestValue.RetentionAfterZentrifugePercent = ( babyDiaperTestValue.RetentionWetWeight -
                                                                        babyDiaperTestValue.WeightDiaperDry ) * 100.0 /
                                                                      babyDiaperTestValue.WeightDiaperDry;
            babyDiaperTestValue.RetentionRw = GetRetentionRwType( babyDiaperTestValue.RetentionAfterZentrifugeValue, productionOrder );
            babyDiaperTestValue.SapType = testSheet.SAPType;
            babyDiaperTestValue.SapNr = testSheet.SAPNr;
            babyDiaperTestValue.SapGHoewiValue = ( babyDiaperTestValue.RetentionWetWeight - babyDiaperTestValue.WeightDiaperDry - productionOrder.Component.PillowRetentWithoutSAP )
                                                 / productionOrder.Component.SAP;
            return babyDiaperTestValue;
        }

        /// <summary>
        ///     Calculates all values for the baby diaper rewet test
        /// </summary>
        /// <param name="babyDiaperTestValue">the test value</param>
        /// <param name="testSheetId">the test sheet id</param>
        /// <returns></returns>
        public BabyDiaperTestValue CalculateBabyDiaperRewetValues(BabyDiaperTestValue babyDiaperTestValue,
                                                                       Int32 testSheetId)
        {
            var testSheet = BabyDiaperRetentionBll.GetTestSheetInfo(testSheetId);
            var productionOrder = BabyDiaperRetentionBll.GetProductionOrder(testSheet.FaNr);

            babyDiaperTestValue.Rewet140Rw = GetRewet140RwType( babyDiaperTestValue.Rewet140Value, productionOrder );
            babyDiaperTestValue.Rewet210Rw = GetRewet210RwType(babyDiaperTestValue.Rewet210Value, productionOrder);
            //TODO PENETRATION
            return babyDiaperTestValue;
        }

        /// <summary>
        ///     returns the RwType for the Rewet140 test
        /// </summary>
        /// <param name="value">the tested Value</param>
        /// <param name="productOrder">the Production order</param>
        /// <returns>The RwType</returns>
        private static RwType GetRewet140RwType( Double value, ProductionOrder productOrder ) => productOrder.Article.Rewet140Max >= value ? RwType.Worse : RwType.Ok;

        /// <summary>
        ///     returns the RwType for the Rewet210 test
        /// </summary>
        /// <param name="value">the tested Value</param>
        /// <param name="productOrder">the Production order</param>
        /// <returns>The RwType</returns>
        private static RwType GetRewet210RwType(Double value, ProductionOrder productOrder) => productOrder.Article.Rewet210Max >= value ? RwType.Worse : RwType.Ok;

        /// <summary>
        ///     returns the RwType for the Retention test
        /// </summary>
        /// <param name="value">the tested Value</param>
        /// <param name="productOrder">the Production order</param>
        /// <returns></returns>
        public RwType GetRetentionRwType(Double value, ProductionOrder productOrder)
        {
            if (value <= productOrder.Article.MinRetention)
                return RwType.Worse;
            return value >= productOrder.Article.MaxRetention ? RwType.Better : RwType.Ok;
        }

        #region Implementation of IBabyDiaperRetentionBll

        /// <summary>
        ///     Creates an new TestValue from the view model
        /// </summary>
        /// <param name="viewModel">the data from the view</param>
        /// <returns>The created test value</returns>
        public TestValue SaveNewRetentionTest( BabyDiaperRetentionEditViewModel viewModel )
        {
            var testValue = new TestValue
            {
                TestSheetRefId = viewModel.TestSheetId,
                CreatedDateTime = DateTime.Now,
                LastEditedDateTime = DateTime.Now,
                CreatedPerson = viewModel.TestPerson,
                LastEditedPerson = viewModel.TestPerson,
                DayInYearOfArticleCreation = viewModel.ProductionCodeDay,
                ArticleTestType = ArticleType.BabyDiaper
            };
            if ( viewModel.Notes.IsNotNull() )
                testValue.TestValueNote = viewModel.Notes.Select( error => new TestValueNote { ErrorRefId = error.ErrorCodeId, Message = error.Message, TestValue = testValue } )
                                                   .ToList();
            var babyDiaperTestValue = new BabyDiaperTestValue
            {
                DiaperCreatedTime = viewModel.ProductionCodeTime,
                WeightDiaperDry = viewModel.DiaperWeight,
                RetentionWetWeight = viewModel.WeightRetentionWet,
                TestType = TestTypeBabyDiaper.Retention
            };
            babyDiaperTestValue = CalculateBabyDiaperRetentionValues( babyDiaperTestValue, viewModel.TestSheetId );
            testValue.BabyDiaperTestValue = babyDiaperTestValue;

            BabyDiaperRetentionBll.SaveNewTestValue( testValue );
            return testValue;
        }

        /// <summary>
        ///     Updates an given Testvalue from the view model
        /// </summary>
        /// <param name="viewModel">the data from the view</param>
        /// <returns>the updated test value</returns>
        public TestValue UpdateRetentionTest( BabyDiaperRetentionEditViewModel viewModel )
        {
            var testValue = BabyDiaperRetentionBll.GetTestValue( viewModel.TestValueId );
            testValue.LastEditedDateTime = DateTime.Now;
            testValue.LastEditedPerson = viewModel.TestPerson;
            testValue.DayInYearOfArticleCreation = viewModel.ProductionCodeDay;
            testValue.BabyDiaperTestValue.DiaperCreatedTime = viewModel.ProductionCodeTime;
            testValue.BabyDiaperTestValue.WeightDiaperDry = viewModel.DiaperWeight;
            testValue.BabyDiaperTestValue.RetentionWetWeight = viewModel.WeightRetentionWet;

            /*var result = testValue.TestValueNote.Where(p => viewModel.Notes.All( p2 => p2.ErrorCodeId != p.ErrorRefId ) );
            foreach ( var n in result )
                testValue.TestValueNote.Remove( n );*/ //remove if not exist anymore
            foreach ( var note in testValue.TestValueNote )
                foreach ( var vmNote in viewModel.Notes.Where( vmNote => note.TestValueNoteId == vmNote.Id ) )
                {
                    note.Message = vmNote.Message;
                    note.ErrorRefId = vmNote.ErrorCodeId;
                }
            foreach ( var vmNote in viewModel.Notes.Where( n => n.Id == 0 ) )
                testValue.TestValueNote.Add( new TestValueNote { ErrorRefId = vmNote.ErrorCodeId, Message = vmNote.Message, TestValue = testValue } );

            testValue.BabyDiaperTestValue = CalculateBabyDiaperRetentionValues( testValue.BabyDiaperTestValue,
                                                                                viewModel.TestSheetId );

            BabyDiaperRetentionBll.UpdateTestValue( testValue );
            return testValue;
        }

        /// <summary>
        ///     Updates the Average and standard deviation values of the testsheet
        /// </summary>
        /// <param name="testSheetId">id of the test sheet</param>
        /// <returns>the updated test sheet</returns>
        public TestSheet UpdateAverageAndStv( Int32 testSheetId )
        {
            var testSheet = BabyDiaperRetentionBll.GetTestSheetInfo( testSheetId );
            var retentionTestAvg =
                testSheet.TestValues.FirstOrDefault(
                             tv =>
                                 tv.ArticleTestType == ArticleType.BabyDiaper && tv.BabyDiaperTestValue.TestType == TestTypeBabyDiaper.Retention
                                 && tv.TestValueType == TestValueType.Average );
            var retentionTestStDev =
                testSheet.TestValues.FirstOrDefault(
                             tv =>
                                 tv.ArticleTestType == ArticleType.BabyDiaper && tv.BabyDiaperTestValue.TestType == TestTypeBabyDiaper.Retention
                                 && tv.TestValueType == TestValueType.StandardDeviation );
            UpdateRetentionAvg( testSheet, retentionTestAvg );
            UpdateRetentionStDev( testSheet, retentionTestAvg, retentionTestStDev );

            /*
            var rewetAndPenetrationTestAvg = 
                testSheet.TestValues.FirstOrDefault(
                             tv =>
                                 tv.ArticleTestType == ArticleType.BabyDiaper && tv.BabyDiaperTestValue.TestType == TestTypeBabyDiaper.RewetAndPenetrationTime
                                 && tv.TestValueType == TestValueType.Average);
            var rewetTestAvg =
                testSheet.TestValues.FirstOrDefault(
                             tv =>
                                 tv.ArticleTestType == ArticleType.BabyDiaper && tv.BabyDiaperTestValue.TestType == TestTypeBabyDiaper.Rewet
                                 && tv.TestValueType == TestValueType.Average);
            */
            BabyDiaperRetentionBll.UpdateTestSheet();
            return testSheet;
        }

        /// <summary>
        ///     Creates the Production code string from the testsheet
        /// </summary>
        /// <param name="testSheet">the testSheet</param>
        /// <returns>the production code</returns>
        public String CreateProductionCode( TestSheet testSheet ) => "IT/" + testSheet.MachineNr.Substring(1) + "/" + testSheet.CreatedDateTime.Year.ToString().Substring(2) + "/";

        /// <summary>
        ///     Creates an new TestValue from the view model
        /// </summary>
        /// <param name="viewModel">the data from the view</param>
        /// <returns>The created test value</returns>
        public TestValue SaveNewRewetTest( BabyDiaperRewetEditViewModel viewModel )
        {
            var testValue = new TestValue
            {
                TestSheetRefId = viewModel.TestSheetId,
                CreatedDateTime = DateTime.Now,
                LastEditedDateTime = DateTime.Now,
                CreatedPerson = viewModel.TestPerson,
                LastEditedPerson = viewModel.TestPerson,
                DayInYearOfArticleCreation = viewModel.ProductionCodeDay,
                ArticleTestType = ArticleType.BabyDiaper
            };
            if (viewModel.Notes.IsNotNull())
                testValue.TestValueNote = viewModel.Notes.Select(error => new TestValueNote { ErrorRefId = error.ErrorCodeId, Message = error.Message, TestValue = testValue })
                                                   .ToList();
            var babyDiaperTestValue = new BabyDiaperTestValue
            {
                DiaperCreatedTime = viewModel.ProductionCodeTime,
                WeightDiaperDry = viewModel.DiaperWeight,
                Rewet140Value = viewModel.RewetAfter140,
                Rewet210Value = viewModel.RewetAfter210,
                StrikeTroughValue = viewModel.StrikeThrough,
                DistributionOfTheStrikeTrough = viewModel.Distribution,
                PenetrationTimeAdditionFirst = viewModel.PenetrationTime1,
                PenetrationTimeAdditionSecond = viewModel.PenetrationTime2,
                PenetrationTimeAdditionThird = viewModel.PenetrationTime3,
                PenetrationTimeAdditionFourth = viewModel.PenetrationTime4,
                TestType = viewModel.TestType
            };
            babyDiaperTestValue = CalculateBabyDiaperRewetValues(babyDiaperTestValue, viewModel.TestSheetId);
            testValue.BabyDiaperTestValue = babyDiaperTestValue;

            BabyDiaperRetentionBll.SaveNewTestValue(testValue);
            return testValue;
        }

        /// <summary>
        ///     Updates an given Testvalue from the view model
        /// </summary>
        /// <param name="viewModel">the data from the view</param>
        /// <returns>the updated test value</returns>
        public TestValue UpdateRewetTest( BabyDiaperRewetEditViewModel viewModel )
        {
            var testValue = BabyDiaperRetentionBll.GetTestValue(viewModel.TestValueId);
            testValue.LastEditedDateTime = DateTime.Now;
            testValue.LastEditedPerson = viewModel.TestPerson;
            testValue.DayInYearOfArticleCreation = viewModel.ProductionCodeDay;
            testValue.BabyDiaperTestValue.DiaperCreatedTime = viewModel.ProductionCodeTime;
            testValue.BabyDiaperTestValue.WeightDiaperDry = viewModel.DiaperWeight;
            testValue.BabyDiaperTestValue.Rewet140Value = viewModel.RewetAfter140;
            testValue.BabyDiaperTestValue.Rewet210Value = viewModel.RewetAfter210;
            testValue.BabyDiaperTestValue.StrikeTroughValue = viewModel.StrikeThrough;
            testValue.BabyDiaperTestValue.DistributionOfTheStrikeTrough = viewModel.Distribution;
            testValue.BabyDiaperTestValue.PenetrationTimeAdditionFirst = viewModel.PenetrationTime1;
            testValue.BabyDiaperTestValue.PenetrationTimeAdditionSecond = viewModel.PenetrationTime2;
            testValue.BabyDiaperTestValue.PenetrationTimeAdditionThird = viewModel.PenetrationTime3;
            testValue.BabyDiaperTestValue.PenetrationTimeAdditionFourth = viewModel.PenetrationTime4;
            testValue.BabyDiaperTestValue.TestType = viewModel.TestType;


            foreach (var note in testValue.TestValueNote)
                foreach (var vmNote in viewModel.Notes.Where(vmNote => note.TestValueNoteId == vmNote.Id))
                {
                    note.Message = vmNote.Message;
                    note.ErrorRefId = vmNote.ErrorCodeId;
                }
            foreach (var vmNote in viewModel.Notes.Where(n => n.Id == 0))
                testValue.TestValueNote.Add(new TestValueNote { ErrorRefId = vmNote.ErrorCodeId, Message = vmNote.Message, TestValue = testValue });

            testValue.BabyDiaperTestValue = CalculateBabyDiaperRewetValues(testValue.BabyDiaperTestValue,
                                                                                viewModel.TestSheetId);

            BabyDiaperRetentionBll.UpdateTestValue(testValue);
            return testValue;
        }



        private static TestValue UpdateRetentionAvg( TestSheet testSheet, TestValue retentionTestAvg )
        {
            var tempBabyDiaper = new BabyDiaperTestValue { RetentionRw = RwType.Ok };
            var counter = 0;
            foreach (
                var testValue in
                testSheet.TestValues.Where( testValue => testValue.TestValueType == TestValueType.Single && testValue.BabyDiaperTestValue.TestType == TestTypeBabyDiaper.Retention )
            )
            {
                tempBabyDiaper.WeightDiaperDry += testValue.BabyDiaperTestValue.WeightDiaperDry;
                tempBabyDiaper.RetentionWetWeight += testValue.BabyDiaperTestValue.RetentionWetWeight;
                tempBabyDiaper.RetentionAfterZentrifugeValue += testValue.BabyDiaperTestValue.RetentionAfterZentrifugeValue;
                tempBabyDiaper.RetentionAfterZentrifugePercent += testValue.BabyDiaperTestValue.RetentionAfterZentrifugePercent;
                if ( testValue.BabyDiaperTestValue.RetentionRw == RwType.Worse )
                    tempBabyDiaper.RetentionRw = RwType.Worse;
                tempBabyDiaper.SapGHoewiValue += testValue.BabyDiaperTestValue.SapGHoewiValue;
                counter++;
            }
            retentionTestAvg.BabyDiaperTestValue.WeightDiaperDry = tempBabyDiaper.WeightDiaperDry / counter;
            retentionTestAvg.BabyDiaperTestValue.RetentionWetWeight = tempBabyDiaper.RetentionWetWeight / counter;
            retentionTestAvg.BabyDiaperTestValue.RetentionAfterZentrifugeValue = tempBabyDiaper.RetentionAfterZentrifugeValue / counter;
            retentionTestAvg.BabyDiaperTestValue.RetentionAfterZentrifugePercent = tempBabyDiaper.RetentionAfterZentrifugePercent / counter;
            retentionTestAvg.BabyDiaperTestValue.RetentionRw = tempBabyDiaper.RetentionRw;
            retentionTestAvg.BabyDiaperTestValue.SapGHoewiValue = tempBabyDiaper.SapGHoewiValue / counter;
            return retentionTestAvg;
        }

        private static TestValue UpdateRetentionStDev( TestSheet testSheet, TestValue retentionTestAvg, TestValue retentionTestStDev )
        {
            var tempBabyDiaper = new BabyDiaperTestValue();
            var counter = 0;
            foreach (
                var testValue in
                testSheet.TestValues.Where( testValue => testValue.TestValueType == TestValueType.Single && testValue.BabyDiaperTestValue.TestType == TestTypeBabyDiaper.Retention )
            )
            {
                tempBabyDiaper.WeightDiaperDry += Math.Pow( testValue.BabyDiaperTestValue.WeightDiaperDry - retentionTestAvg.BabyDiaperTestValue.WeightDiaperDry, 2 );
                tempBabyDiaper.RetentionWetWeight += Math.Pow( testValue.BabyDiaperTestValue.RetentionWetWeight - retentionTestAvg.BabyDiaperTestValue.RetentionWetWeight, 2 );
                tempBabyDiaper.RetentionAfterZentrifugeValue +=
                    Math.Pow( testValue.BabyDiaperTestValue.RetentionAfterZentrifugeValue - retentionTestAvg.BabyDiaperTestValue.RetentionAfterZentrifugeValue, 2 );
                tempBabyDiaper.RetentionAfterZentrifugePercent +=
                    Math.Pow( testValue.BabyDiaperTestValue.RetentionAfterZentrifugePercent - retentionTestAvg.BabyDiaperTestValue.RetentionAfterZentrifugePercent, 2 );
                tempBabyDiaper.SapGHoewiValue += Math.Pow( testValue.BabyDiaperTestValue.SapGHoewiValue - retentionTestAvg.BabyDiaperTestValue.SapGHoewiValue, 2 );
                ;
                counter++;
            }
            retentionTestStDev.BabyDiaperTestValue.WeightDiaperDry = Math.Sqrt( tempBabyDiaper.WeightDiaperDry / counter );
            retentionTestStDev.BabyDiaperTestValue.RetentionWetWeight = Math.Sqrt( tempBabyDiaper.RetentionWetWeight / counter );
            retentionTestStDev.BabyDiaperTestValue.RetentionAfterZentrifugeValue = Math.Sqrt( tempBabyDiaper.RetentionAfterZentrifugeValue / counter );
            retentionTestStDev.BabyDiaperTestValue.RetentionAfterZentrifugePercent = Math.Sqrt( tempBabyDiaper.RetentionAfterZentrifugePercent / counter );
            retentionTestStDev.BabyDiaperTestValue.SapGHoewiValue = Math.Sqrt( tempBabyDiaper.SapGHoewiValue / counter );
            return retentionTestStDev;
        }

        #endregion
    }
}