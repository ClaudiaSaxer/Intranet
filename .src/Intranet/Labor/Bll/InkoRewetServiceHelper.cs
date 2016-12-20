#region Usings

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
    ///     Class representing the inko rewet service helper
    /// </summary>
    public class InkoRewetServiceHelper : ServiceBase, IInkoRewetServiceHelper
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
        public InkoRewetServiceHelper( ILoggerFactory loggerFactory )
            : base( loggerFactory.CreateLogger( typeof(InkoRewetServiceHelper) ) )
        {
            Logger.Trace( "Enter Ctor - Exit." );
        }

        #endregion

        #region Implementation of IInkoRewetServiceHelper

        /// <summary>
        ///     Creates an new TestValue from the view model
        /// </summary>
        /// <param name="viewModel">the data from the view</param>
        /// <returns>The created test value</returns>
        public TestValue SaveNewRewetTest( InkoRewetEditViewModel viewModel )
        {
            var testValue = TestServiceHelper.CreateNewTestValue( viewModel.TestSheetId, viewModel.TestPerson, viewModel.ProductionCodeDay, viewModel.Notes );
            testValue.ArticleTestType = ArticleType.IncontinencePad;

            var incontinencePadTestValue = new IncontinencePadTestValue
            {
                IncontinencePadTime = viewModel.ProductionCodeTime,
                ExpireMonth = viewModel.ExpireMonth,
                ExpireYear = viewModel.ExpireYear,
                RewetFreeDryValue = viewModel.FPDry,
                RewetFreeWetValue = viewModel.FPWet,
                TestType = TestTypeIncontinencePad.RewetFree
            };
            incontinencePadTestValue = CalculateInkoRewetValues( incontinencePadTestValue, viewModel.TestSheetId );
            testValue.IncontinencePadTestValue = incontinencePadTestValue;

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
            var inkoRewetTestAvg =
                testSheet.TestValues.FirstOrDefault(
                             tv =>
                                 ( tv.ArticleTestType == ArticleType.IncontinencePad )
                                 && ( tv.IncontinencePadTestValue.TestType == TestTypeIncontinencePad.RewetFree )
                                 && ( tv.TestValueType == TestValueType.Average ) );
            var inkoRewetTestStDev =
                testSheet.TestValues.FirstOrDefault(
                             tv =>
                                 ( tv.ArticleTestType == ArticleType.IncontinencePad )
                                 && ( tv.IncontinencePadTestValue.TestType == TestTypeIncontinencePad.RewetFree )
                                 && ( tv.TestValueType == TestValueType.StandardDeviation ) );
            UpdateInkoRewetAvg( testSheet, inkoRewetTestAvg );
            UpdateInkoRewetStDev( testSheet, inkoRewetTestAvg, inkoRewetTestStDev );

            TestBll.UpdateTestSheet();
            return testSheet;
        }

        /// <summary>
        ///     Updates an given Testvalue from the view model
        /// </summary>
        /// <param name="viewModel">the data from the view</param>
        /// <returns>the updated test value</returns>
        public TestValue UpdateRewetTest( InkoRewetEditViewModel viewModel )
        {
            var testValue = TestBll.GetTestValue( viewModel.TestValueId );
            if ( testValue.IsNull() || ( testValue.ArticleTestType != ArticleType.IncontinencePad )
                 || ( testValue.IncontinencePadTestValue.TestType != TestTypeIncontinencePad.RewetFree ) )
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
            testValue.IncontinencePadTestValue.RewetFreeDryValue = viewModel.FPDry;
            testValue.IncontinencePadTestValue.RewetFreeWetValue = viewModel.FPWet;
            testValue.IncontinencePadTestValue.TestType = TestTypeIncontinencePad.RewetFree;

            TestServiceHelper.UpdateNotes( viewModel.Notes, testValue );

            testValue.IncontinencePadTestValue = CalculateInkoRewetValues( testValue.IncontinencePadTestValue, viewModel.TestSheetId );

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
        private IncontinencePadTestValue CalculateInkoRewetValues( IncontinencePadTestValue incontinencePadTestValue,
                                                                   Int32 testSheetId )
        {
            var testSheet = TestBll.GetTestSheetInfo( testSheetId );
            var productionOrder = TestBll.GetProductionOrder( testSheet.FaNr );

            incontinencePadTestValue.RewetFreeDifference = incontinencePadTestValue.RewetFreeWetValue - incontinencePadTestValue.RewetFreeDryValue;
            incontinencePadTestValue.RewetFreeRw = GetRewetFreeRwType( incontinencePadTestValue.RewetFreeDifference, productionOrder );
            return incontinencePadTestValue;
        }

        /// <summary>
        ///     returns the RwType for the Rewet140 test
        /// </summary>
        /// <param name="value">the tested Value</param>
        /// <param name="productOrder">the Production order</param>
        /// <returns>The RwType</returns>
        private static RwType GetRewetFreeRwType( Double value, ProductionOrder productOrder ) => productOrder.Article.MaxInkoRewet > value ? RwType.Ok : RwType.Worse;

        private void UpdateInkoRewetAvg( TestSheet testSheet, TestValue rewetTestAvg )
        {
            var productionOrder = TestBll.GetProductionOrder( testSheet.FaNr );
            var tempInko = new IncontinencePadTestValue { RewetFreeRw = RwType.Ok };
            var counter = 0;
            foreach (
                var testValue in
                testSheet.TestValues.Where(
                             testValue =>
                                 ( testValue.TestValueType == TestValueType.Single )
                                 && ( testValue.IncontinencePadTestValue.TestType == TestTypeIncontinencePad.RewetFree ) )
            )
            {
                tempInko.RewetFreeDryValue += testValue.IncontinencePadTestValue.RewetFreeDryValue;
                tempInko.RewetFreeWetValue += testValue.IncontinencePadTestValue.RewetFreeWetValue;
                tempInko.RewetFreeDifference += testValue.IncontinencePadTestValue.RewetFreeDifference;
                if ( testValue.IncontinencePadTestValue.RewetFreeRw == RwType.Worse )
                    tempInko.RewetFreeRw = RwType.SomethingWorse;
                counter++;
            }
            if ( counter == 0 )
                counter = 1;
            rewetTestAvg.IncontinencePadTestValue.RewetFreeDryValue = tempInko.RewetFreeDryValue / counter;
            rewetTestAvg.IncontinencePadTestValue.RewetFreeWetValue = tempInko.RewetFreeWetValue / counter;
            rewetTestAvg.IncontinencePadTestValue.RewetFreeDifference = tempInko.RewetFreeDifference / counter;
            if ( GetRewetFreeRwType( rewetTestAvg.IncontinencePadTestValue.RewetFreeDifference, productionOrder ) == RwType.Worse )
                tempInko.RewetFreeRw = RwType.Worse;
            rewetTestAvg.IncontinencePadTestValue.RewetFreeRw = tempInko.RewetFreeRw;
        }

        private static void UpdateInkoRewetStDev( TestSheet testSheet, TestValue rewetTestAvg, TestValue rewetTestStDev )
        {
            var tempInko = new IncontinencePadTestValue();
            var counter = 0;
            foreach (
                var testValue in
                testSheet.TestValues.Where(
                             testValue =>
                                 ( testValue.TestValueType == TestValueType.Single )
                                 && ( testValue.IncontinencePadTestValue.TestType == TestTypeIncontinencePad.RewetFree ) )
            )
            {
                tempInko.RewetFreeDryValue += Math.Pow( testValue.IncontinencePadTestValue.RewetFreeDryValue - rewetTestAvg.IncontinencePadTestValue.RewetFreeDryValue, 2 );
                tempInko.RewetFreeWetValue += Math.Pow( testValue.IncontinencePadTestValue.RewetFreeWetValue - rewetTestAvg.IncontinencePadTestValue.RewetFreeWetValue, 2 );
                tempInko.RewetFreeDifference += Math.Pow( testValue.IncontinencePadTestValue.RewetFreeDifference - rewetTestAvg.IncontinencePadTestValue.RewetFreeDifference, 2 );
                counter++;
            }
            if ( counter < 2 )
            {
                rewetTestStDev.IncontinencePadTestValue.RewetFreeDryValue = 0;
                rewetTestStDev.IncontinencePadTestValue.RewetFreeWetValue = 0;
                rewetTestStDev.IncontinencePadTestValue.RewetFreeDifference = 0;
            }
            else
            {
                counter--;
                rewetTestStDev.IncontinencePadTestValue.RewetFreeDryValue = Math.Sqrt( tempInko.RewetFreeDryValue / counter );
                rewetTestStDev.IncontinencePadTestValue.RewetFreeWetValue = Math.Sqrt( tempInko.RewetFreeWetValue / counter );
                rewetTestStDev.IncontinencePadTestValue.RewetFreeDifference = Math.Sqrt( tempInko.RewetFreeDifference / counter );
            }
        }

        #endregion
    }
}