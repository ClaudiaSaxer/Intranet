#region Usings

using System;
using System.Collections.Generic;
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
        private BabyDiaperTestValue CalculateBabyDiaperRetentionValues( BabyDiaperTestValue babyDiaperTestValue,
                                                                       Int32 testSheetId )
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
        private BabyDiaperTestValue CalculateBabyDiaperRewetValues(BabyDiaperTestValue babyDiaperTestValue,
                                                                       Int32 testSheetId)
        {
            var testSheet = BabyDiaperRetentionBll.GetTestSheetInfo(testSheetId);
            var productionOrder = BabyDiaperRetentionBll.GetProductionOrder(testSheet.FaNr);

            babyDiaperTestValue.Rewet140Rw = GetRewet140RwType( babyDiaperTestValue.Rewet140Value, productionOrder );
            babyDiaperTestValue.Rewet210Rw = GetRewet210RwType(babyDiaperTestValue.Rewet210Value, productionOrder);
            babyDiaperTestValue.PenetrationRwType = GetPenetrationRwType( babyDiaperTestValue.PenetrationTimeAdditionFourth, productionOrder );
            return babyDiaperTestValue;
        }

        /// <summary>
        ///     returns the RwType for the Rewet140 test
        /// </summary>
        /// <param name="value">the tested Value</param>
        /// <param name="productOrder">the Production order</param>
        /// <returns>The RwType</returns>
        private static RwType GetRewet140RwType( Double value, ProductionOrder productOrder ) => productOrder.Article.Rewet140Max >= value ? RwType.Ok : RwType.Worse;

        /// <summary>
        ///     returns the RwType for the Rewet210 test
        /// </summary>
        /// <param name="value">the tested Value</param>
        /// <param name="productOrder">the Production order</param>
        /// <returns>The RwType</returns>
        private static RwType GetRewet210RwType(Double value, ProductionOrder productOrder) => productOrder.Article.Rewet210Max >= value ? RwType.Ok : RwType.Worse;

        /// <summary>
        ///     returns the RwType for the penetration test
        /// </summary>
        /// <param name="value">the tested Value</param>
        /// <param name="productOrder">the Production order</param>
        /// <returns>The RwType</returns>
        private static RwType GetPenetrationRwType(Double value, ProductionOrder productOrder) => productOrder.Article.MaxPenetrationAfter4Time >= value ? RwType.Ok : RwType.Worse;

        /// <summary>
        ///     returns the RwType for the Retention test
        /// </summary>
        /// <param name="value">the tested Value</param>
        /// <param name="productOrder">the Production order</param>
        /// <returns></returns>
        private static RwType GetRetentionRwType(Double value, ProductionOrder productOrder)
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

            if(viewModel.Notes.IsNull()) viewModel.Notes = new List<TestNote>();
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
            // Retention
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

            // REWET
            var rewetTestAvg =
                testSheet.TestValues.FirstOrDefault(
                             tv =>
                                 tv.ArticleTestType == ArticleType.BabyDiaper && (tv.BabyDiaperTestValue.TestType == TestTypeBabyDiaper.Rewet || tv.BabyDiaperTestValue.TestType == TestTypeBabyDiaper.RewetAndPenetrationTime)
                                 && tv.TestValueType == TestValueType.Average);
            var rewetTestStDev =
                testSheet.TestValues.FirstOrDefault(
                             tv =>
                                 tv.ArticleTestType == ArticleType.BabyDiaper && (tv.BabyDiaperTestValue.TestType == TestTypeBabyDiaper.Rewet || tv.BabyDiaperTestValue.TestType == TestTypeBabyDiaper.RewetAndPenetrationTime)
                                 && tv.TestValueType == TestValueType.StandardDeviation);
            UpdateRewetAvg(testSheet, rewetTestAvg);
            UpdateRewetStDev(testSheet, rewetTestAvg, rewetTestStDev);

            // Penetration
            var penetrationTestAvg =
                testSheet.TestValues.FirstOrDefault(
                             tv =>
                                 tv.ArticleTestType == ArticleType.BabyDiaper && tv.BabyDiaperTestValue.TestType == TestTypeBabyDiaper.RewetAndPenetrationTime
                                 && tv.TestValueType == TestValueType.Average);
            var penetrationStDev =
                testSheet.TestValues.FirstOrDefault(
                             tv =>
                                 tv.ArticleTestType == ArticleType.BabyDiaper && tv.BabyDiaperTestValue.TestType == TestTypeBabyDiaper.RewetAndPenetrationTime
                                 && tv.TestValueType == TestValueType.StandardDeviation);
            UpdatePenetrationAvg(testSheet, penetrationTestAvg);
            UpdatePenetrationStDev(testSheet, penetrationTestAvg, penetrationStDev);

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

            if (viewModel.Notes.IsNull()) viewModel.Notes = new List<TestNote>();

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
                //TODO IF AVG IS WORSE
                if ( testValue.BabyDiaperTestValue.RetentionRw == RwType.Worse )
                    tempBabyDiaper.RetentionRw = RwType.SomethingWorse;
                tempBabyDiaper.SapGHoewiValue += testValue.BabyDiaperTestValue.SapGHoewiValue;
                counter++;
            }
            if ( counter == 0 )
                counter = 1;
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
                counter++;
            }
            if (counter == 0)
                counter = 1;
            retentionTestStDev.BabyDiaperTestValue.WeightDiaperDry = Math.Sqrt( tempBabyDiaper.WeightDiaperDry / counter );
            retentionTestStDev.BabyDiaperTestValue.RetentionWetWeight = Math.Sqrt( tempBabyDiaper.RetentionWetWeight / counter );
            retentionTestStDev.BabyDiaperTestValue.RetentionAfterZentrifugeValue = Math.Sqrt( tempBabyDiaper.RetentionAfterZentrifugeValue / counter );
            retentionTestStDev.BabyDiaperTestValue.RetentionAfterZentrifugePercent = Math.Sqrt( tempBabyDiaper.RetentionAfterZentrifugePercent / counter );
            retentionTestStDev.BabyDiaperTestValue.SapGHoewiValue = Math.Sqrt( tempBabyDiaper.SapGHoewiValue / counter );
            return retentionTestStDev;
        }

        private static TestValue UpdateRewetAvg(TestSheet testSheet, TestValue rewetTestAvg)
        {
            var tempBabyDiaper = new BabyDiaperTestValue { Rewet140Rw = RwType.Ok, Rewet210Rw = RwType.Ok};
            var counter = 0;
            foreach (
                var testValue in
                testSheet.TestValues.Where(testValue => testValue.TestValueType == TestValueType.Single && (testValue.BabyDiaperTestValue.TestType == TestTypeBabyDiaper.Rewet || testValue.BabyDiaperTestValue.TestType == TestTypeBabyDiaper.RewetAndPenetrationTime))
            )
            {
                tempBabyDiaper.WeightDiaperDry += testValue.BabyDiaperTestValue.WeightDiaperDry;
                tempBabyDiaper.Rewet140Value += testValue.BabyDiaperTestValue.Rewet140Value;
                tempBabyDiaper.Rewet210Value += testValue.BabyDiaperTestValue.Rewet210Value;
                tempBabyDiaper.StrikeTroughValue += testValue.BabyDiaperTestValue.StrikeTroughValue;
                tempBabyDiaper.DistributionOfTheStrikeTrough += testValue.BabyDiaperTestValue.DistributionOfTheStrikeTrough;
                //TODO IF AVG IS WORSE
                if (testValue.BabyDiaperTestValue.Rewet140Rw == RwType.Worse)
                    tempBabyDiaper.Rewet140Rw = RwType.SomethingWorse;
                if (testValue.BabyDiaperTestValue.Rewet210Rw == RwType.Worse)
                    tempBabyDiaper.Rewet210Rw = RwType.SomethingWorse;
                if (testValue.BabyDiaperTestValue.PenetrationRwType == RwType.Worse)
                    tempBabyDiaper.PenetrationRwType = RwType.SomethingWorse;
                counter++;
            }
            if (counter == 0)
                counter = 1;
            rewetTestAvg.BabyDiaperTestValue.WeightDiaperDry = tempBabyDiaper.WeightDiaperDry / counter;
            rewetTestAvg.BabyDiaperTestValue.Rewet140Value = tempBabyDiaper.Rewet140Value / counter;
            rewetTestAvg.BabyDiaperTestValue.Rewet210Value = tempBabyDiaper.Rewet210Value / counter;
            rewetTestAvg.BabyDiaperTestValue.StrikeTroughValue = tempBabyDiaper.StrikeTroughValue / counter;
            rewetTestAvg.BabyDiaperTestValue.DistributionOfTheStrikeTrough = tempBabyDiaper.DistributionOfTheStrikeTrough / counter;
            rewetTestAvg.BabyDiaperTestValue.Rewet140Rw = tempBabyDiaper.Rewet140Rw;
            rewetTestAvg.BabyDiaperTestValue.Rewet210Rw = tempBabyDiaper.Rewet210Rw;
            rewetTestAvg.BabyDiaperTestValue.PenetrationRwType = tempBabyDiaper.PenetrationRwType;
            return rewetTestAvg;
        }

        private static TestValue UpdateRewetStDev(TestSheet testSheet, TestValue rewetTestAvg, TestValue rewetTestStDev)
        {
            var tempBabyDiaper = new BabyDiaperTestValue();
            var counter = 0;
            foreach (
                var testValue in
                testSheet.TestValues.Where(testValue => testValue.TestValueType == TestValueType.Single && (testValue.BabyDiaperTestValue.TestType == TestTypeBabyDiaper.Rewet || testValue.BabyDiaperTestValue.TestType == TestTypeBabyDiaper.RewetAndPenetrationTime))
            )
            {
                tempBabyDiaper.WeightDiaperDry += Math.Pow(testValue.BabyDiaperTestValue.WeightDiaperDry - rewetTestAvg.BabyDiaperTestValue.WeightDiaperDry, 2);
                tempBabyDiaper.Rewet140Value += Math.Pow(testValue.BabyDiaperTestValue.Rewet140Value - rewetTestAvg.BabyDiaperTestValue.Rewet140Value, 2);
                tempBabyDiaper.Rewet210Value += Math.Pow(testValue.BabyDiaperTestValue.Rewet210Value - rewetTestAvg.BabyDiaperTestValue.Rewet210Value, 2);
                tempBabyDiaper.StrikeTroughValue += Math.Pow(testValue.BabyDiaperTestValue.StrikeTroughValue - rewetTestAvg.BabyDiaperTestValue.StrikeTroughValue, 2);
                tempBabyDiaper.DistributionOfTheStrikeTrough += Math.Pow(testValue.BabyDiaperTestValue.DistributionOfTheStrikeTrough - rewetTestAvg.BabyDiaperTestValue.DistributionOfTheStrikeTrough, 2);
                counter++;
            }
            if (counter == 0)
                counter = 1;
            rewetTestStDev.BabyDiaperTestValue.WeightDiaperDry = Math.Sqrt(tempBabyDiaper.WeightDiaperDry / counter);
            rewetTestStDev.BabyDiaperTestValue.Rewet140Value = Math.Sqrt(tempBabyDiaper.Rewet140Value / counter);
            rewetTestStDev.BabyDiaperTestValue.Rewet210Value = Math.Sqrt(tempBabyDiaper.Rewet210Value / counter);
            rewetTestStDev.BabyDiaperTestValue.StrikeTroughValue = Math.Sqrt(tempBabyDiaper.StrikeTroughValue / counter);
            rewetTestStDev.BabyDiaperTestValue.DistributionOfTheStrikeTrough = Math.Sqrt(tempBabyDiaper.DistributionOfTheStrikeTrough / counter);
            return rewetTestStDev;
        }

        private static TestValue UpdatePenetrationAvg(TestSheet testSheet, TestValue penetrationTestAvg)
        {
            var tempBabyDiaper = new BabyDiaperTestValue { PenetrationRwType = RwType.Ok };
            var counter = 0;
            foreach (
                var testValue in
                testSheet.TestValues.Where(testValue => testValue.TestValueType == TestValueType.Single && testValue.BabyDiaperTestValue.TestType == TestTypeBabyDiaper.RewetAndPenetrationTime)
            )
            {
                tempBabyDiaper.WeightDiaperDry += testValue.BabyDiaperTestValue.WeightDiaperDry;
                tempBabyDiaper.PenetrationTimeAdditionFirst += testValue.BabyDiaperTestValue.PenetrationTimeAdditionFirst;
                tempBabyDiaper.PenetrationTimeAdditionSecond += testValue.BabyDiaperTestValue.PenetrationTimeAdditionSecond;
                tempBabyDiaper.PenetrationTimeAdditionThird += testValue.BabyDiaperTestValue.PenetrationTimeAdditionThird;
                tempBabyDiaper.PenetrationTimeAdditionFourth += testValue.BabyDiaperTestValue.PenetrationTimeAdditionFourth;
                //TODO IF AVG IS WORSE
                if (testValue.BabyDiaperTestValue.PenetrationRwType == RwType.Worse)
                    tempBabyDiaper.PenetrationRwType = RwType.SomethingWorse;
                counter++;
            }
            if (counter == 0)
                counter = 1;
            penetrationTestAvg.BabyDiaperTestValue.WeightDiaperDry = tempBabyDiaper.WeightDiaperDry / counter;
            penetrationTestAvg.BabyDiaperTestValue.PenetrationTimeAdditionFirst = tempBabyDiaper.PenetrationTimeAdditionFirst / counter;
            penetrationTestAvg.BabyDiaperTestValue.PenetrationTimeAdditionSecond = tempBabyDiaper.PenetrationTimeAdditionSecond / counter;
            penetrationTestAvg.BabyDiaperTestValue.PenetrationTimeAdditionThird = tempBabyDiaper.PenetrationTimeAdditionThird / counter;
            penetrationTestAvg.BabyDiaperTestValue.PenetrationTimeAdditionFourth = tempBabyDiaper.PenetrationTimeAdditionFourth / counter;
            penetrationTestAvg.BabyDiaperTestValue.PenetrationRwType = tempBabyDiaper.PenetrationRwType;
            return penetrationTestAvg;
        }

        private static TestValue UpdatePenetrationStDev(TestSheet testSheet, TestValue penetrationTestAvg, TestValue penetrationTestStDev)
        {
            var tempBabyDiaper = new BabyDiaperTestValue();
            var counter = 0;
            foreach (
                var testValue in
                testSheet.TestValues.Where(testValue => testValue.TestValueType == TestValueType.Single && testValue.BabyDiaperTestValue.TestType == TestTypeBabyDiaper.Retention)
            )
            {
                tempBabyDiaper.WeightDiaperDry += Math.Pow(testValue.BabyDiaperTestValue.WeightDiaperDry - penetrationTestAvg.BabyDiaperTestValue.WeightDiaperDry, 2);
                tempBabyDiaper.PenetrationTimeAdditionFirst += Math.Pow(testValue.BabyDiaperTestValue.PenetrationTimeAdditionFirst - penetrationTestAvg.BabyDiaperTestValue.PenetrationTimeAdditionFirst, 2);
                tempBabyDiaper.PenetrationTimeAdditionSecond += Math.Pow(testValue.BabyDiaperTestValue.PenetrationTimeAdditionSecond - penetrationTestAvg.BabyDiaperTestValue.PenetrationTimeAdditionSecond, 2);
                tempBabyDiaper.PenetrationTimeAdditionThird += Math.Pow(testValue.BabyDiaperTestValue.PenetrationTimeAdditionThird - penetrationTestAvg.BabyDiaperTestValue.PenetrationTimeAdditionThird, 2);
                tempBabyDiaper.PenetrationTimeAdditionFourth += Math.Pow(testValue.BabyDiaperTestValue.PenetrationTimeAdditionFourth - penetrationTestAvg.BabyDiaperTestValue.PenetrationTimeAdditionFourth, 2);
                counter++;
            }
            if (counter == 0)
                counter = 1;
            penetrationTestStDev.BabyDiaperTestValue.WeightDiaperDry = Math.Sqrt(tempBabyDiaper.WeightDiaperDry / counter);
            penetrationTestStDev.BabyDiaperTestValue.PenetrationTimeAdditionFirst = Math.Sqrt(tempBabyDiaper.PenetrationTimeAdditionFirst / counter);
            penetrationTestStDev.BabyDiaperTestValue.PenetrationTimeAdditionSecond = Math.Sqrt(tempBabyDiaper.PenetrationTimeAdditionSecond / counter);
            penetrationTestStDev.BabyDiaperTestValue.PenetrationTimeAdditionThird = Math.Sqrt(tempBabyDiaper.PenetrationTimeAdditionThird / counter);
            penetrationTestStDev.BabyDiaperTestValue.PenetrationTimeAdditionFourth = Math.Sqrt(tempBabyDiaper.PenetrationTimeAdditionFourth / counter);
            return penetrationTestStDev;
        }

        #endregion
    }
}