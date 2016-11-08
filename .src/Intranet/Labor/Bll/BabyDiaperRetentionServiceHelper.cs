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
        public IBabyDiaperRetentionBll BabyDiaperRetentionBll { get; set; }

        /// <summary>
        ///     Gets or sets the baby diaper service helper.
        /// </summary>
        public IBabyDiaperServiceHelper BabyDiaperServiceHelper { get; set; }

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

            BabyDiaperRetentionBll.UpdateTestValue( testValue );
            return testValue;
        }

        /// <summary>
        ///     Updates the Average and standard deviation values of the testsheet for retention values
        /// </summary>
        /// <param name="testSheetId">id of the test sheet</param>
        /// <returns>the updated test sheet</returns>
        public TestSheet UpdateRetentionAverageAndStv( Int32 testSheetId )
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
            BabyDiaperRetentionBll.UpdateTestSheet();
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
        ///     returns the RwType for the Retention test
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
            if ( counter == 0 )
                counter = 1;
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