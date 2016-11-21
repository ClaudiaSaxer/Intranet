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
    ///     Class representing the babydiaper rewet service helper
    /// </summary>
    public class BabyDiaperRewetServiceHelper : ServiceBase, IBabyDiaperRewetServiceHelper
    {
        #region Properties

        /// <summary>
        ///     Gets or sets the bll for the baby diapers retention test.
        /// </summary>
        public ITestBll TestBll { get; set; }

        /// <summary>
        ///     Gets or sets the baby diaper service helper.
        /// </summary>
        public ITestServiceHelper TestServiceHelper { get; set; }

        #endregion

        #region Ctor

        /// <summary>
        ///     Initialize a new instance of the <see cref="ServiceBase" /> class.
        /// </summary>
        /// <param name="loggerFactory">A <see cref="ILoggerFactory" />.</param>
        public BabyDiaperRewetServiceHelper( ILoggerFactory loggerFactory )
            : base( loggerFactory.CreateLogger( typeof(BabyDiaperRewetServiceHelper) ) )
        {
            Logger.Trace( "Enter Ctor - Exit." );
        }

        #endregion

        #region Implementation of IBabyDiaperRewetServiceHelper

        /// <summary>
        ///     Creates an new TestValue from the view model
        /// </summary>
        /// <param name="viewModel">the data from the view</param>
        /// <returns>The created test value</returns>
        public TestValue SaveNewRewetTest( BabyDiaperRewetEditViewModel viewModel )
        {
            var testValue = TestServiceHelper.CreateNewTestValue( viewModel.TestSheetId, viewModel.TestPerson, viewModel.ProductionCodeDay, viewModel.Notes );
            testValue.ArticleTestType = ArticleType.BabyDiaper;

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
            if ( babyDiaperTestValue.TestType == TestTypeBabyDiaper.Rewet )
            {
                babyDiaperTestValue.PenetrationTimeAdditionFirst = 0;
                babyDiaperTestValue.PenetrationTimeAdditionSecond = 0;
                babyDiaperTestValue.PenetrationTimeAdditionThird = 0;
                babyDiaperTestValue.PenetrationTimeAdditionFourth = 0;
            }
            babyDiaperTestValue = CalculateBabyDiaperRewetValues( babyDiaperTestValue, viewModel.TestSheetId );
            testValue.BabyDiaperTestValue = babyDiaperTestValue;

            TestBll.SaveNewTestValue( testValue );
            return testValue;
        }

        /// <summary>
        ///     Updates the Average and standard deviation values of the testsheet for retention values
        /// </summary>
        /// <param name="testSheetId">id of the test sheet</param>
        /// <returns>the updated test sheet</returns>
        public TestSheet UpdateRewetAverageAndStv( Int32 testSheetId )
        {
            var testSheet = TestBll.GetTestSheetInfo( testSheetId );
            // REWET
            var rewetTestAvg =
                testSheet.TestValues.FirstOrDefault(
                             tv =>
                                 tv.ArticleTestType == ArticleType.BabyDiaper
                                 && ( tv.BabyDiaperTestValue.TestType == TestTypeBabyDiaper.Rewet)
                                 && tv.TestValueType == TestValueType.Average );
            var rewetTestStDev =
                testSheet.TestValues.FirstOrDefault(
                             tv =>
                                 tv.ArticleTestType == ArticleType.BabyDiaper
                                 && ( tv.BabyDiaperTestValue.TestType == TestTypeBabyDiaper.Rewet)
                                 && tv.TestValueType == TestValueType.StandardDeviation );
            UpdateRewetAvg( testSheet, rewetTestAvg );
            UpdateRewetStDev( testSheet, rewetTestAvg, rewetTestStDev );

            // Penetration
            var penetrationTestAvg =
                testSheet.TestValues.FirstOrDefault(
                             tv =>
                                 tv.ArticleTestType == ArticleType.BabyDiaper && tv.BabyDiaperTestValue.TestType == TestTypeBabyDiaper.RewetAndPenetrationTime
                                 && tv.TestValueType == TestValueType.Average );
            var penetrationStDev =
                testSheet.TestValues.FirstOrDefault(
                             tv =>
                                 tv.ArticleTestType == ArticleType.BabyDiaper && tv.BabyDiaperTestValue.TestType == TestTypeBabyDiaper.RewetAndPenetrationTime
                                 && tv.TestValueType == TestValueType.StandardDeviation );
            UpdatePenetrationAvg( testSheet, penetrationTestAvg );
            UpdatePenetrationStDev( testSheet, penetrationTestAvg, penetrationStDev );

            TestBll.UpdateTestSheet();
            return testSheet;
        }

        /// <summary>
        ///     Updates an given Testvalue from the view model
        /// </summary>
        /// <param name="viewModel">the data from the view</param>
        /// <returns>the updated test value</returns>
        public TestValue UpdateRewetTest( BabyDiaperRewetEditViewModel viewModel )
        {
            var testValue = TestBll.GetTestValue( viewModel.TestValueId );
            if (testValue.IsNull() || testValue.ArticleTestType != ArticleType.BabyDiaper || (testValue.BabyDiaperTestValue.TestType != TestTypeBabyDiaper.Rewet && testValue.BabyDiaperTestValue.TestType != TestTypeBabyDiaper.RewetAndPenetrationTime))
            {
                Logger.Error("Old Test not found in DB");
                return null;
            }
            testValue.LastEditedDateTime = DateTime.Now;
            testValue.LastEditedPerson = viewModel.TestPerson;
            testValue.DayInYearOfArticleCreation = viewModel.ProductionCodeDay;
            testValue.BabyDiaperTestValue.DiaperCreatedTime = viewModel.ProductionCodeTime;
            testValue.BabyDiaperTestValue.WeightDiaperDry = viewModel.DiaperWeight;
            testValue.BabyDiaperTestValue.Rewet140Value = viewModel.RewetAfter140;
            testValue.BabyDiaperTestValue.Rewet210Value = viewModel.RewetAfter210;
            testValue.BabyDiaperTestValue.StrikeTroughValue = viewModel.StrikeThrough;
            testValue.BabyDiaperTestValue.DistributionOfTheStrikeTrough = viewModel.Distribution;
            if ( viewModel.TestType == TestTypeBabyDiaper.RewetAndPenetrationTime )
            {
                testValue.BabyDiaperTestValue.PenetrationTimeAdditionFirst = viewModel.PenetrationTime1;
                testValue.BabyDiaperTestValue.PenetrationTimeAdditionSecond = viewModel.PenetrationTime2;
                testValue.BabyDiaperTestValue.PenetrationTimeAdditionThird = viewModel.PenetrationTime3;
                testValue.BabyDiaperTestValue.PenetrationTimeAdditionFourth = viewModel.PenetrationTime4;
            }
            else
            {
                testValue.BabyDiaperTestValue.PenetrationTimeAdditionFirst = 0;
                testValue.BabyDiaperTestValue.PenetrationTimeAdditionSecond = 0;
                testValue.BabyDiaperTestValue.PenetrationTimeAdditionThird = 0;
                testValue.BabyDiaperTestValue.PenetrationTimeAdditionFourth = 0;
            }
            testValue.BabyDiaperTestValue.TestType = viewModel.TestType;

            if ( viewModel.Notes.IsNull() )
                viewModel.Notes = new List<TestNote>();

            foreach ( var note in testValue.TestValueNote )
                foreach ( var vmNote in viewModel.Notes.Where( vmNote => note.TestValueNoteId == vmNote.Id ) )
                {
                    note.Message = vmNote.Message;
                    note.ErrorRefId = vmNote.ErrorCodeId;
                }
            foreach ( var vmNote in viewModel.Notes.Where( n => n.Id == 0 ) )
                testValue.TestValueNote.Add( new TestValueNote { ErrorRefId = vmNote.ErrorCodeId, Message = vmNote.Message, TestValue = testValue } );

            testValue.BabyDiaperTestValue = CalculateBabyDiaperRewetValues( testValue.BabyDiaperTestValue,
                                                                            viewModel.TestSheetId );

            TestBll.UpdateTestValue( testValue );
            return testValue;
        }

        #endregion

        #region Private Methods

        /// <summary>
        ///     Calculates all values for the baby diaper rewet test
        /// </summary>
        /// <param name="babyDiaperTestValue">the test value</param>
        /// <param name="testSheetId">the test sheet id</param>
        /// <returns></returns>
        private BabyDiaperTestValue CalculateBabyDiaperRewetValues( BabyDiaperTestValue babyDiaperTestValue,
                                                                    Int32 testSheetId )
        {
            var testSheet = TestBll.GetTestSheetInfo( testSheetId );
            var productionOrder = TestBll.GetProductionOrder( testSheet.FaNr );

            babyDiaperTestValue.Rewet140Rw = GetRewet140RwType( babyDiaperTestValue.Rewet140Value, productionOrder );
            babyDiaperTestValue.Rewet210Rw = GetRewet210RwType( babyDiaperTestValue.Rewet210Value, productionOrder );
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
        private static RwType GetRewet210RwType( Double value, ProductionOrder productOrder ) => productOrder.Article.Rewet210Max >= value ? RwType.Ok : RwType.Worse;

        /// <summary>
        ///     returns the RwType for the penetration test
        /// </summary>
        /// <param name="value">the tested Value</param>
        /// <param name="productOrder">the Production order</param>
        /// <returns>The RwType</returns>
        private static RwType GetPenetrationRwType( Double value, ProductionOrder productOrder )
            => productOrder.Article.MaxPenetrationAfter4Time >= value ? RwType.Ok : RwType.Worse;

        private TestValue UpdateRewetAvg( TestSheet testSheet, TestValue rewetTestAvg )
        {
            var productionOrder = TestBll.GetProductionOrder(testSheet.FaNr);
            var tempBabyDiaper = new BabyDiaperTestValue { Rewet140Rw = RwType.Ok, Rewet210Rw = RwType.Ok };
            var counter = 0;
            foreach (
                var testValue in
                testSheet.TestValues.Where(
                             testValue =>
                                 testValue.TestValueType == TestValueType.Single
                                 && ( testValue.BabyDiaperTestValue.TestType == TestTypeBabyDiaper.Rewet
                                      || testValue.BabyDiaperTestValue.TestType == TestTypeBabyDiaper.RewetAndPenetrationTime ) )
            )
            {
                tempBabyDiaper.WeightDiaperDry += testValue.BabyDiaperTestValue.WeightDiaperDry;
                tempBabyDiaper.Rewet140Value += testValue.BabyDiaperTestValue.Rewet140Value;
                tempBabyDiaper.Rewet210Value += testValue.BabyDiaperTestValue.Rewet210Value;
                tempBabyDiaper.StrikeTroughValue += testValue.BabyDiaperTestValue.StrikeTroughValue;
                tempBabyDiaper.DistributionOfTheStrikeTrough += testValue.BabyDiaperTestValue.DistributionOfTheStrikeTrough;
                if ( testValue.BabyDiaperTestValue.Rewet140Rw == RwType.Worse )
                    tempBabyDiaper.Rewet140Rw = RwType.SomethingWorse;
                if ( testValue.BabyDiaperTestValue.Rewet210Rw == RwType.Worse )
                    tempBabyDiaper.Rewet210Rw = RwType.SomethingWorse;
                if ( testValue.BabyDiaperTestValue.PenetrationRwType == RwType.Worse )
                    tempBabyDiaper.PenetrationRwType = RwType.SomethingWorse;
                counter++;
            }
            if ( counter == 0 )
                counter = 1;
            rewetTestAvg.BabyDiaperTestValue.WeightDiaperDry = tempBabyDiaper.WeightDiaperDry / counter;
            rewetTestAvg.BabyDiaperTestValue.Rewet140Value = tempBabyDiaper.Rewet140Value / counter;
            rewetTestAvg.BabyDiaperTestValue.Rewet210Value = tempBabyDiaper.Rewet210Value / counter;
            rewetTestAvg.BabyDiaperTestValue.StrikeTroughValue = tempBabyDiaper.StrikeTroughValue / counter;
            rewetTestAvg.BabyDiaperTestValue.DistributionOfTheStrikeTrough = tempBabyDiaper.DistributionOfTheStrikeTrough / counter;
            if (GetRewet140RwType(rewetTestAvg.BabyDiaperTestValue.Rewet140Value, productionOrder) == RwType.Worse)
                tempBabyDiaper.Rewet140Rw = RwType.Worse;
            if (GetRewet210RwType(rewetTestAvg.BabyDiaperTestValue.Rewet210Value, productionOrder) == RwType.Worse)
                tempBabyDiaper.Rewet210Rw = RwType.Worse;
            if (GetPenetrationRwType(rewetTestAvg.BabyDiaperTestValue.PenetrationTimeAdditionFourth, productionOrder) == RwType.Worse)
                tempBabyDiaper.PenetrationRwType = RwType.Worse;

            rewetTestAvg.BabyDiaperTestValue.Rewet140Rw = tempBabyDiaper.Rewet140Rw;
            rewetTestAvg.BabyDiaperTestValue.Rewet210Rw = tempBabyDiaper.Rewet210Rw;
            rewetTestAvg.BabyDiaperTestValue.PenetrationRwType = tempBabyDiaper.PenetrationRwType;
            return rewetTestAvg;
        }

        private static TestValue UpdateRewetStDev( TestSheet testSheet, TestValue rewetTestAvg, TestValue rewetTestStDev )
        {
            var tempBabyDiaper = new BabyDiaperTestValue();
            var counter = 0;
            foreach (
                var testValue in
                testSheet.TestValues.Where(
                             testValue =>
                                 testValue.TestValueType == TestValueType.Single
                                 && ( testValue.BabyDiaperTestValue.TestType == TestTypeBabyDiaper.Rewet
                                      || testValue.BabyDiaperTestValue.TestType == TestTypeBabyDiaper.RewetAndPenetrationTime ) )
            )
            {
                tempBabyDiaper.WeightDiaperDry += Math.Pow( testValue.BabyDiaperTestValue.WeightDiaperDry - rewetTestAvg.BabyDiaperTestValue.WeightDiaperDry, 2 );
                tempBabyDiaper.Rewet140Value += Math.Pow( testValue.BabyDiaperTestValue.Rewet140Value - rewetTestAvg.BabyDiaperTestValue.Rewet140Value, 2 );
                tempBabyDiaper.Rewet210Value += Math.Pow( testValue.BabyDiaperTestValue.Rewet210Value - rewetTestAvg.BabyDiaperTestValue.Rewet210Value, 2 );
                tempBabyDiaper.StrikeTroughValue += Math.Pow( testValue.BabyDiaperTestValue.StrikeTroughValue - rewetTestAvg.BabyDiaperTestValue.StrikeTroughValue, 2 );
                tempBabyDiaper.DistributionOfTheStrikeTrough +=
                    Math.Pow( testValue.BabyDiaperTestValue.DistributionOfTheStrikeTrough - rewetTestAvg.BabyDiaperTestValue.DistributionOfTheStrikeTrough, 2 );
                counter++;
            }
            if ( counter < 2 )
            {
                rewetTestStDev.BabyDiaperTestValue.WeightDiaperDry = 0;
                rewetTestStDev.BabyDiaperTestValue.Rewet140Value = 0;
                rewetTestStDev.BabyDiaperTestValue.Rewet210Value = 0;
                rewetTestStDev.BabyDiaperTestValue.StrikeTroughValue = 0;
                rewetTestStDev.BabyDiaperTestValue.DistributionOfTheStrikeTrough = 0;
            }
            else
            {
                counter--;
                rewetTestStDev.BabyDiaperTestValue.WeightDiaperDry = Math.Sqrt(tempBabyDiaper.WeightDiaperDry / counter);
                rewetTestStDev.BabyDiaperTestValue.Rewet140Value = Math.Sqrt(tempBabyDiaper.Rewet140Value / counter);
                rewetTestStDev.BabyDiaperTestValue.Rewet210Value = Math.Sqrt(tempBabyDiaper.Rewet210Value / counter);
                rewetTestStDev.BabyDiaperTestValue.StrikeTroughValue = Math.Sqrt(tempBabyDiaper.StrikeTroughValue / counter);
                rewetTestStDev.BabyDiaperTestValue.DistributionOfTheStrikeTrough = Math.Sqrt(tempBabyDiaper.DistributionOfTheStrikeTrough / counter);

            }
            return rewetTestStDev;
        }

        private TestValue UpdatePenetrationAvg( TestSheet testSheet, TestValue penetrationTestAvg )
        {
            var productionOrder = TestBll.GetProductionOrder(testSheet.FaNr);
            var tempBabyDiaper = new BabyDiaperTestValue { PenetrationRwType = RwType.Ok };
            var counter = 0;
            foreach (
                var testValue in
                testSheet.TestValues.Where(
                             testValue => testValue.TestValueType == TestValueType.Single && testValue.BabyDiaperTestValue.TestType == TestTypeBabyDiaper.RewetAndPenetrationTime )
            )
            {
                tempBabyDiaper.WeightDiaperDry += testValue.BabyDiaperTestValue.WeightDiaperDry;
                tempBabyDiaper.PenetrationTimeAdditionFirst += testValue.BabyDiaperTestValue.PenetrationTimeAdditionFirst;
                tempBabyDiaper.PenetrationTimeAdditionSecond += testValue.BabyDiaperTestValue.PenetrationTimeAdditionSecond;
                tempBabyDiaper.PenetrationTimeAdditionThird += testValue.BabyDiaperTestValue.PenetrationTimeAdditionThird;
                tempBabyDiaper.PenetrationTimeAdditionFourth += testValue.BabyDiaperTestValue.PenetrationTimeAdditionFourth;
                if ( testValue.BabyDiaperTestValue.PenetrationRwType == RwType.Worse )
                    tempBabyDiaper.PenetrationRwType = RwType.SomethingWorse;
                counter++;
            }
            if ( counter == 0 )
                counter = 1;
            penetrationTestAvg.BabyDiaperTestValue.WeightDiaperDry = tempBabyDiaper.WeightDiaperDry / counter;
            penetrationTestAvg.BabyDiaperTestValue.PenetrationTimeAdditionFirst = tempBabyDiaper.PenetrationTimeAdditionFirst / counter;
            penetrationTestAvg.BabyDiaperTestValue.PenetrationTimeAdditionSecond = tempBabyDiaper.PenetrationTimeAdditionSecond / counter;
            penetrationTestAvg.BabyDiaperTestValue.PenetrationTimeAdditionThird = tempBabyDiaper.PenetrationTimeAdditionThird / counter;
            penetrationTestAvg.BabyDiaperTestValue.PenetrationTimeAdditionFourth = tempBabyDiaper.PenetrationTimeAdditionFourth / counter;
            if (GetPenetrationRwType(penetrationTestAvg.BabyDiaperTestValue.PenetrationTimeAdditionFourth, productionOrder) == RwType.Worse)
                tempBabyDiaper.PenetrationRwType = RwType.Worse;
            penetrationTestAvg.BabyDiaperTestValue.PenetrationRwType = tempBabyDiaper.PenetrationRwType;
            return penetrationTestAvg;
        }

        private static TestValue UpdatePenetrationStDev( TestSheet testSheet, TestValue penetrationTestAvg, TestValue penetrationTestStDev )
        {
            var tempBabyDiaper = new BabyDiaperTestValue();
            var counter = 0;
            foreach (
                var testValue in
                testSheet.TestValues.Where( testValue => testValue.TestValueType == TestValueType.Single && testValue.BabyDiaperTestValue.TestType == TestTypeBabyDiaper.RewetAndPenetrationTime)
            )
            {
                tempBabyDiaper.WeightDiaperDry += Math.Pow( testValue.BabyDiaperTestValue.WeightDiaperDry - penetrationTestAvg.BabyDiaperTestValue.WeightDiaperDry, 2 );
                tempBabyDiaper.PenetrationTimeAdditionFirst +=
                    Math.Pow( testValue.BabyDiaperTestValue.PenetrationTimeAdditionFirst - penetrationTestAvg.BabyDiaperTestValue.PenetrationTimeAdditionFirst, 2 );
                tempBabyDiaper.PenetrationTimeAdditionSecond +=
                    Math.Pow( testValue.BabyDiaperTestValue.PenetrationTimeAdditionSecond - penetrationTestAvg.BabyDiaperTestValue.PenetrationTimeAdditionSecond, 2 );
                tempBabyDiaper.PenetrationTimeAdditionThird +=
                    Math.Pow( testValue.BabyDiaperTestValue.PenetrationTimeAdditionThird - penetrationTestAvg.BabyDiaperTestValue.PenetrationTimeAdditionThird, 2 );
                tempBabyDiaper.PenetrationTimeAdditionFourth +=
                    Math.Pow( testValue.BabyDiaperTestValue.PenetrationTimeAdditionFourth - penetrationTestAvg.BabyDiaperTestValue.PenetrationTimeAdditionFourth, 2 );
                counter++;
            }
            if ( counter < 2 )
            {
                penetrationTestStDev.BabyDiaperTestValue.WeightDiaperDry = 0;
                penetrationTestStDev.BabyDiaperTestValue.PenetrationTimeAdditionFirst = 0;
                penetrationTestStDev.BabyDiaperTestValue.PenetrationTimeAdditionSecond = 0;
                penetrationTestStDev.BabyDiaperTestValue.PenetrationTimeAdditionThird = 0;
                penetrationTestStDev.BabyDiaperTestValue.PenetrationTimeAdditionFourth = 0;
            }
            else
            {
                counter--;
                penetrationTestStDev.BabyDiaperTestValue.WeightDiaperDry = Math.Sqrt(tempBabyDiaper.WeightDiaperDry / counter);
                penetrationTestStDev.BabyDiaperTestValue.PenetrationTimeAdditionFirst = Math.Sqrt(tempBabyDiaper.PenetrationTimeAdditionFirst / counter);
                penetrationTestStDev.BabyDiaperTestValue.PenetrationTimeAdditionSecond = Math.Sqrt(tempBabyDiaper.PenetrationTimeAdditionSecond / counter);
                penetrationTestStDev.BabyDiaperTestValue.PenetrationTimeAdditionThird = Math.Sqrt(tempBabyDiaper.PenetrationTimeAdditionThird / counter);
                penetrationTestStDev.BabyDiaperTestValue.PenetrationTimeAdditionFourth = Math.Sqrt(tempBabyDiaper.PenetrationTimeAdditionFourth / counter);
            }
            return penetrationTestStDev;
        }

        #endregion
    }
}