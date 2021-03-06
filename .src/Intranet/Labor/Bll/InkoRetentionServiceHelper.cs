﻿#region Usings

using System;
using System.Linq;
using Extend;
using Intranet.Common;
using Intranet.Labor.Definition;
using Intranet.Labor.Model;
using Intranet.Labor.ViewModel;

#endregion

namespace Intranet.Labor.Bll
{
    /// <summary>
    ///     Class representing the inko retention service helper
    /// </summary>
    public class InkoRetentionServiceHelper : ServiceBase, IInkoRetentionServiceHelper
    {
        #region Properties

        /// <summary>
        ///     Gets or sets the bll for the all testvalues.
        /// </summary>
        public ITestBll TestBll { get; set; }

        /// <summary>
        ///     Gets or sets the test service helper.
        /// </summary>
        public ITestServiceHelper TestServiceHelper { get; set; }

        #endregion

        #region Ctor

        /// <summary>
        ///     Initialize a new instance of the <see cref="ServiceBase" /> class.
        /// </summary>
        /// <param name="loggerFactory">A <see cref="ILoggerFactory" />.</param>
        public InkoRetentionServiceHelper( ILoggerFactory loggerFactory )
            : base( loggerFactory.CreateLogger( typeof(InkoRetentionServiceHelper) ) )
        {
            Logger.Trace( "Enter Ctor - Exit." );
        }

        #endregion

        #region Implementation of IInkoRetentionServiceHelper

        /// <summary>
        ///     Creates an new TestValue from the view model
        /// </summary>
        /// <param name="viewModel">the data from the view</param>
        /// <returns>The created test value</returns>
        public TestValue SaveNewRetentionTest( InkoRetentionEditViewModel viewModel )
        {
            var testValue = TestServiceHelper.CreateNewTestValue( viewModel.TestSheetId, viewModel.TestPerson, viewModel.ProductionCodeDay, viewModel.Notes );
            testValue.ArticleTestType = ArticleType.IncontinencePad;

            var incontinencePadTestValue = new IncontinencePadTestValue
            {
                IncontinencePadTime = viewModel.ProductionCodeTime,
                ExpireMonth = viewModel.ExpireMonth,
                ExpireYear = viewModel.ExpireYear,
                RetentionWeight = viewModel.InkoWeight,
                RetentionWetValue = viewModel.InkoWeightWet,
                RetentionAfterZentrifuge = viewModel.InkoWeightAfterZentrifuge,
                TestType = TestTypeIncontinencePad.Retention
            };
            incontinencePadTestValue = CalculateInkoRetentionValues( incontinencePadTestValue, viewModel.TestSheetId );
            testValue.IncontinencePadTestValue = incontinencePadTestValue;

            TestBll.SaveNewTestValue( testValue );
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
            var inkoRetentionTestAvg =
                testSheet.TestValues.FirstOrDefault(
                             tv =>
                                 ( tv.ArticleTestType == ArticleType.IncontinencePad )
                                 && ( tv.IncontinencePadTestValue.TestType == TestTypeIncontinencePad.Retention )
                                 && ( tv.TestValueType == TestValueType.Average ) );
            var inkoRetentionTestStDev =
                testSheet.TestValues.FirstOrDefault(
                             tv =>
                                 ( tv.ArticleTestType == ArticleType.IncontinencePad )
                                 && ( tv.IncontinencePadTestValue.TestType == TestTypeIncontinencePad.Retention )
                                 && ( tv.TestValueType == TestValueType.StandardDeviation ) );
            UpdateInkoRetentionAvg( testSheet, inkoRetentionTestAvg );
            UpdateInkoRetentionStDev( testSheet, inkoRetentionTestAvg, inkoRetentionTestStDev );

            TestBll.UpdateTestSheet();
            return testSheet;
        }

        /// <summary>
        ///     Updates an given Testvalue from the view model
        /// </summary>
        /// <param name="viewModel">the data from the view</param>
        /// <returns>the updated test value</returns>
        public TestValue UpdateRetentionTest( InkoRetentionEditViewModel viewModel )
        {
            var testValue = TestBll.GetTestValue( viewModel.TestValueId );
            if ( testValue.IsNull() || ( testValue.ArticleTestType != ArticleType.IncontinencePad )
                 || ( testValue.IncontinencePadTestValue.TestType != TestTypeIncontinencePad.Retention ) )
            {
                Logger.Error( "Old Test not found in DB" );
                return null;
            }
            testValue.LastEditedDateTime = DateTime.Now;
            testValue.LastEditedPerson = viewModel.TestPerson;
            testValue.DayInYearOfArticleCreation = viewModel.ProductionCodeDay;
            testValue.IncontinencePadTestValue.IncontinencePadTime = viewModel.ProductionCodeTime;
            testValue.IncontinencePadTestValue.ExpireMonth = viewModel.ExpireMonth;
            testValue.IncontinencePadTestValue.ExpireYear = viewModel.ExpireYear;
            testValue.IncontinencePadTestValue.RetentionWeight = viewModel.InkoWeight;
            testValue.IncontinencePadTestValue.RetentionWetValue = viewModel.InkoWeightWet;
            testValue.IncontinencePadTestValue.RetentionAfterZentrifuge = viewModel.InkoWeightAfterZentrifuge;
            testValue.IncontinencePadTestValue.TestType = TestTypeIncontinencePad.Retention;

            TestServiceHelper.UpdateNotes( viewModel.Notes, testValue );

            testValue.IncontinencePadTestValue = CalculateInkoRetentionValues( testValue.IncontinencePadTestValue, viewModel.TestSheetId );

            TestBll.UpdateTestValue( testValue );
            return testValue;
        }

        #endregion

        #region Private Methods

        /// <summary>
        ///     Calculates all values for the incontinence rewet test
        /// </summary>
        /// <param name="incontinencePadTestValue">the test value</param>
        /// <param name="testSheetId">the test sheet id</param>
        /// <returns></returns>
        private IncontinencePadTestValue CalculateInkoRetentionValues( IncontinencePadTestValue incontinencePadTestValue,
                                                                       Int32 testSheetId )
        {
            var testSheet = TestBll.GetTestSheetInfo( testSheetId );
            var productionOrder = TestBll.GetProductionOrder( testSheet.FaNr );

            incontinencePadTestValue.RetentionAbsorbtion = incontinencePadTestValue.RetentionWetValue - incontinencePadTestValue.RetentionWeight;
            incontinencePadTestValue.RetentionEndValue = incontinencePadTestValue.RetentionAfterZentrifuge - incontinencePadTestValue.RetentionWeight;
            incontinencePadTestValue.RetentionRw = GetRetentionRwType( incontinencePadTestValue.RetentionEndValue, productionOrder );
            return incontinencePadTestValue;
        }

        /// <summary>
        ///     returns the RwType for the RetentionRw test
        /// </summary>
        /// <param name="value">the tested Value</param>
        /// <param name="productOrder">the Production order</param>
        /// <returns>The RwType</returns>
        private static RwType GetRetentionRwType( Double value, ProductionOrder productOrder ) => productOrder.Article.MinInkoRetention < value ? RwType.Ok : RwType.Worse;

        private void UpdateInkoRetentionAvg( TestSheet testSheet, TestValue retentionTestAvg )
        {
            var productionOrder = TestBll.GetProductionOrder( testSheet.FaNr );
            var tempInko = new IncontinencePadTestValue { RetentionRw = RwType.Ok };
            var counter = 0;
            foreach (
                var testValue in
                testSheet.TestValues.Where(
                             testValue =>
                                 ( testValue.TestValueType == TestValueType.Single )
                                 && ( testValue.IncontinencePadTestValue.TestType == TestTypeIncontinencePad.Retention ) )
            )
            {
                tempInko.RetentionWeight += testValue.IncontinencePadTestValue.RetentionWeight;
                tempInko.RetentionWetValue += testValue.IncontinencePadTestValue.RetentionWetValue;
                tempInko.RetentionAfterZentrifuge += testValue.IncontinencePadTestValue.RetentionAfterZentrifuge;
                tempInko.RetentionAbsorbtion += testValue.IncontinencePadTestValue.RetentionAbsorbtion;
                tempInko.RetentionEndValue += testValue.IncontinencePadTestValue.RetentionEndValue;
                if ( testValue.IncontinencePadTestValue.RetentionRw == RwType.Worse )
                    tempInko.RetentionRw = RwType.SomethingWorse;
                counter++;
            }
            if ( counter == 0 )
                counter = 1;
            retentionTestAvg.IncontinencePadTestValue.RetentionWeight = tempInko.RetentionWeight / counter;
            retentionTestAvg.IncontinencePadTestValue.RetentionWetValue = tempInko.RetentionWetValue / counter;
            retentionTestAvg.IncontinencePadTestValue.RetentionAfterZentrifuge = tempInko.RetentionAfterZentrifuge / counter;
            retentionTestAvg.IncontinencePadTestValue.RetentionAbsorbtion = tempInko.RetentionAbsorbtion / counter;
            retentionTestAvg.IncontinencePadTestValue.RetentionEndValue = tempInko.RetentionEndValue / counter;
            if ( ( GetRetentionRwType( retentionTestAvg.IncontinencePadTestValue.RetentionEndValue, productionOrder ) == RwType.Worse ) && ( tempInko.RetentionRw != RwType.Ok ) )
                tempInko.RetentionRw = RwType.Worse;
            retentionTestAvg.IncontinencePadTestValue.RetentionRw = tempInko.RetentionRw;
        }

        private static void UpdateInkoRetentionStDev( TestSheet testSheet, TestValue retentionTestAvg, TestValue retentionTestStDev )
        {
            var tempInko = new IncontinencePadTestValue();
            var counter = 0;
            foreach (
                var testValue in
                testSheet.TestValues.Where(
                             testValue =>
                                 ( testValue.TestValueType == TestValueType.Single )
                                 && ( testValue.IncontinencePadTestValue.TestType == TestTypeIncontinencePad.Retention ) )
            )
            {
                tempInko.RetentionWeight += Math.Pow( testValue.IncontinencePadTestValue.RetentionWeight - retentionTestAvg.IncontinencePadTestValue.RetentionWeight, 2 );
                tempInko.RetentionWetValue += Math.Pow( testValue.IncontinencePadTestValue.RetentionWetValue - retentionTestAvg.IncontinencePadTestValue.RetentionWetValue, 2 );
                tempInko.RetentionAfterZentrifuge +=
                    Math.Pow( testValue.IncontinencePadTestValue.RetentionAfterZentrifuge - retentionTestAvg.IncontinencePadTestValue.RetentionAfterZentrifuge, 2 );
                tempInko.RetentionAbsorbtion += Math.Pow( testValue.IncontinencePadTestValue.RetentionAbsorbtion - retentionTestAvg.IncontinencePadTestValue.RetentionAbsorbtion, 2 );
                tempInko.RetentionEndValue += Math.Pow( testValue.IncontinencePadTestValue.RetentionEndValue - retentionTestAvg.IncontinencePadTestValue.RetentionEndValue, 2 );
                counter++;
            }
            if ( counter < 2 )
            {
                retentionTestStDev.IncontinencePadTestValue.RetentionWeight = 0;
                retentionTestStDev.IncontinencePadTestValue.RetentionWetValue = 0;
                retentionTestStDev.IncontinencePadTestValue.RetentionAfterZentrifuge = 0;
                retentionTestStDev.IncontinencePadTestValue.RetentionAbsorbtion = 0;
                retentionTestStDev.IncontinencePadTestValue.RetentionEndValue = 0;
            }
            else
            {
                counter--;
                retentionTestStDev.IncontinencePadTestValue.RetentionWeight = Math.Sqrt( tempInko.RetentionWeight / counter );
                retentionTestStDev.IncontinencePadTestValue.RetentionWetValue = Math.Sqrt( tempInko.RetentionWetValue / counter );
                retentionTestStDev.IncontinencePadTestValue.RetentionAfterZentrifuge = Math.Sqrt( tempInko.RetentionAfterZentrifuge / counter );
                retentionTestStDev.IncontinencePadTestValue.RetentionAbsorbtion = Math.Sqrt( tempInko.RetentionAbsorbtion / counter );
                retentionTestStDev.IncontinencePadTestValue.RetentionEndValue = Math.Sqrt( tempInko.RetentionEndValue / counter );
            }

            #endregion
        }
    }
}