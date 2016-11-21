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
    ///     Class representing the babydiaper retention service helper
    /// </summary>
    public class BabyDiaperRetentionServiceHelper : ServiceBase, IBabyDiaperRetentionServiceHelper
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
        public BabyDiaperRetentionServiceHelper( ILoggerFactory loggerFactory )
            : base( loggerFactory.CreateLogger( typeof(BabyDiaperRetentionServiceHelper) ) )
        {
            Logger.Trace( "Enter Ctor - Exit." );
        }

        #endregion

        #region Implementation of IBabyDiaperRetentionServiceHelper

        /// <summary>
        ///     Creates an new TestValue from the view model
        /// </summary>
        /// <param name="viewModel">the data from the view</param>
        /// <returns>The created test value</returns>
        public TestValue SaveNewRetentionTest( BabyDiaperRetentionEditViewModel viewModel )
        {
            var testValue = TestServiceHelper.CreateNewTestValue( viewModel.TestSheetId, viewModel.TestPerson, viewModel.ProductionCodeDay, viewModel.Notes );
            testValue.ArticleTestType = ArticleType.BabyDiaper;

            var babyDiaperTestValue = new BabyDiaperTestValue
            {
                DiaperCreatedTime = viewModel.ProductionCodeTime,
                WeightDiaperDry = viewModel.DiaperWeight,
                RetentionWetWeight = viewModel.WeightRetentionWet,
                TestType = TestTypeBabyDiaper.Retention
            };
            babyDiaperTestValue = CalculateBabyDiaperRetentionValues( babyDiaperTestValue, viewModel.TestSheetId );
            testValue.BabyDiaperTestValue = babyDiaperTestValue;

            testValue = TestBll.SaveNewTestValue( testValue );
            return testValue;
        }

        /// <summary>
        ///     Updates an given Testvalue from the view model
        /// </summary>
        /// <param name="viewModel">the data from the view</param>
        /// <returns>the updated test value</returns>
        public TestValue UpdateRetentionTest( BabyDiaperRetentionEditViewModel viewModel )
        {
            var testValue = TestBll.GetTestValue( viewModel.TestValueId );
            if ( testValue.IsNull() || testValue.ArticleTestType != ArticleType.BabyDiaper || testValue.BabyDiaperTestValue.TestType != TestTypeBabyDiaper.Retention)
            {
                Logger.Error("Old Test not found in DB");
                return null;
            }
            testValue.LastEditedDateTime = DateTime.Now;
            testValue.LastEditedPerson = viewModel.TestPerson;
            testValue.DayInYearOfArticleCreation = viewModel.ProductionCodeDay;
            testValue.BabyDiaperTestValue.DiaperCreatedTime = viewModel.ProductionCodeTime;
            testValue.BabyDiaperTestValue.WeightDiaperDry = viewModel.DiaperWeight;
            testValue.BabyDiaperTestValue.RetentionWetWeight = viewModel.WeightRetentionWet;

            if ( viewModel.Notes.IsNull() )
                viewModel.Notes = new List<TestNote>();
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

            TestBll.UpdateTestValue( testValue );
            return testValue;
        }

        /// <summary>
        ///     Updates the Average and standard deviation values of the testsheet for retention values
        /// </summary>
        /// <param name="testSheetId">id of the test sheet</param>
        /// <returns>the updated test sheet</returns>
        public TestSheet UpdateRetentionAverageAndStv( Int32 testSheetId )
        {
            var testSheet = TestBll.GetTestSheetInfo( testSheetId );
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
            TestBll.UpdateTestSheet();
            return testSheet;
        }

        #endregion

        #region Private Methods

        /// <summary>
        ///     Calculates all values for the baby diaper retention test
        /// </summary>
        /// <param name="babyDiaperTestValue">the test value</param>
        /// <param name="testSheetId">the test sheet id</param>
        /// <returns></returns>
        private BabyDiaperTestValue CalculateBabyDiaperRetentionValues( BabyDiaperTestValue babyDiaperTestValue,
                                                                        Int32 testSheetId )
        {
            var testSheet = TestBll.GetTestSheetInfo( testSheetId );
            var productionOrder = TestBll.GetProductionOrder( testSheet.FaNr );

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
        ///     returns the RwType for the BabyDiaperRetention test
        /// </summary>
        /// <param name="value">the tested Value</param>
        /// <param name="productOrder">the Production order</param>
        /// <returns></returns>
        private static RwType GetRetentionRwType( Double value, ProductionOrder productOrder )
        {
            if ( value <= productOrder.Article.MinRetention )
                return RwType.Worse;
            return value >= productOrder.Article.MaxRetention ? RwType.Better : RwType.Ok;
        }

        private TestValue UpdateRetentionAvg( TestSheet testSheet, TestValue retentionTestAvg )
        {
            var productionOrder = TestBll.GetProductionOrder(testSheet.FaNr);
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
            if (GetRetentionRwType(retentionTestAvg.BabyDiaperTestValue.RetentionAfterZentrifugeValue, productionOrder) == RwType.Worse && tempBabyDiaper.RetentionRw != RwType.Ok)
                tempBabyDiaper.RetentionRw = RwType.Worse;
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
            if ( counter == 0 )
            {
                retentionTestStDev.BabyDiaperTestValue.WeightDiaperDry = 0;
                retentionTestStDev.BabyDiaperTestValue.RetentionWetWeight = 0;
                retentionTestStDev.BabyDiaperTestValue.RetentionAfterZentrifugeValue = 0;
                retentionTestStDev.BabyDiaperTestValue.RetentionAfterZentrifugePercent = 0;
                retentionTestStDev.BabyDiaperTestValue.SapGHoewiValue = 0;
            }
            else
            {
                counter--;
                retentionTestStDev.BabyDiaperTestValue.WeightDiaperDry = Math.Sqrt(tempBabyDiaper.WeightDiaperDry / counter);
                retentionTestStDev.BabyDiaperTestValue.RetentionWetWeight = Math.Sqrt(tempBabyDiaper.RetentionWetWeight / counter);
                retentionTestStDev.BabyDiaperTestValue.RetentionAfterZentrifugeValue = Math.Sqrt(tempBabyDiaper.RetentionAfterZentrifugeValue / counter);
                retentionTestStDev.BabyDiaperTestValue.RetentionAfterZentrifugePercent = Math.Sqrt(tempBabyDiaper.RetentionAfterZentrifugePercent / counter);
                retentionTestStDev.BabyDiaperTestValue.SapGHoewiValue = Math.Sqrt(tempBabyDiaper.SapGHoewiValue / counter);
            }
            return retentionTestStDev;
        }

        #endregion
    }
}