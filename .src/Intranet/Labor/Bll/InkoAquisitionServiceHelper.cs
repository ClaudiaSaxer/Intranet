using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Extend;
using Intranet.Common;
using Intranet.Labor.Definition;
using Intranet.Labor.Model;
using Intranet.Labor.Model.labor;
using Intranet.Labor.ViewModel;

namespace Intranet.Labor.Bll
{
    /// <summary>
    ///     Class representing the inko aquisition service helper
    /// </summary>
    public class InkoAquisitionServiceHelper : ServiceBase, IInkoAquisitionServiceHelper
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
        public InkoAquisitionServiceHelper(ILoggerFactory loggerFactory)
            : base( loggerFactory.CreateLogger( typeof(InkoAquisitionServiceHelper) ) )
        {
            Logger.Trace("Enter Ctor - Exit.");
        }

        #endregion

        #region Implementation of IInkoAquisitionServiceHelper

        /// <summary>
        ///     Creates an new TestValue from the view model
        /// </summary>
        /// <param name="viewModel">the data from the view</param>
        /// <returns>The created test value</returns>
        public TestValue SaveNewAquisitionTest( InkoAquisitionEditViewModel viewModel )
        {
            var testValue = TestServiceHelper.CreateNewTestValue(viewModel.TestSheetId, viewModel.TestPerson, viewModel.ProductionCodeDay, viewModel.Notes);
            testValue.ArticleTestType = ArticleType.IncontinencePad;

            var incontinencePadTestValue = new IncontinencePadTestValue
            {
                IncontinencePadTime = viewModel.ProductionCodeTime,
                AcquisitionTimeFirst = viewModel.AquisitionAddition1,
                AcquisitionTimeSecond = viewModel.AquisitionAddition2,
                AcquisitionTimeThird = viewModel.AquisitionAddition3,
                AcquisitionWeight = viewModel.InkoWeight,
                RewetAfterAcquisitionTimeDryWeight = viewModel.FPDry,
                RewetAfterAcquisitionTimeWetWeight = viewModel.FPWet,
                TestType = TestTypeIncontinencePad.AcquisitionTimeAndRewet
            };
            incontinencePadTestValue = CalculateInkoAquisitionValues(incontinencePadTestValue, viewModel.TestSheetId);
            testValue.IncontinencePadTestValue = incontinencePadTestValue;

            TestBll.SaveNewTestValue(testValue);
            return testValue;
        }

        /// <summary>
        ///     Updates the Average and standard deviation values of the testsheet for aquisition values
        /// </summary>
        /// <param name="testSheetId">id of the test sheet</param>
        /// <returns>the updated test sheet</returns>
        public TestSheet UpdateAquisitionAverageAndStv( Int32 testSheetId )
        {
            var testSheet = TestBll.GetTestSheetInfo(testSheetId);
            var inkoAquisitionTestAvg =
                testSheet.TestValues.FirstOrDefault(
                             tv =>
                                 tv.ArticleTestType == ArticleType.IncontinencePad
                                 && (tv.IncontinencePadTestValue.TestType == TestTypeIncontinencePad.AcquisitionTimeAndRewet)
                                 && tv.TestValueType == TestValueType.Average);
            var inkoAquisitionTestStDev =
                testSheet.TestValues.FirstOrDefault(
                             tv =>
                                 tv.ArticleTestType == ArticleType.IncontinencePad
                                 && (tv.IncontinencePadTestValue.TestType == TestTypeIncontinencePad.AcquisitionTimeAndRewet)
                                 && tv.TestValueType == TestValueType.StandardDeviation);
            UpdateInkoAquisitionAvg(testSheet, inkoAquisitionTestAvg);
            UpdateInkoAquisitionStDev(testSheet, inkoAquisitionTestAvg, inkoAquisitionTestStDev);

            TestBll.UpdateTestSheet();
            return testSheet;
        }

        /// <summary>
        ///     Updates an given Testvalue from the view model
        /// </summary>
        /// <param name="viewModel">the data from the view</param>
        /// <returns>the updated test value</returns>
        public TestValue UpdateAquisitionTest( InkoAquisitionEditViewModel viewModel )
        {
            var testValue = TestBll.GetTestValue(viewModel.TestValueId);
            if (testValue.IsNull() || testValue.ArticleTestType != ArticleType.IncontinencePad || testValue.IncontinencePadTestValue.TestType != TestTypeIncontinencePad.AcquisitionTimeAndRewet)
            {
                Logger.Error("Old Test not found in DB");
                return null;
            }
            testValue.LastEditedDateTime = DateTime.Now;
            testValue.LastEditedPerson = viewModel.TestPerson;
            testValue.DayInYearOfArticleCreation = viewModel.ProductionCodeDay;
            testValue.IncontinencePadTestValue.IncontinencePadTime = viewModel.ProductionCodeTime;
            testValue.IncontinencePadTestValue.AcquisitionTimeFirst = viewModel.AquisitionAddition1;
            testValue.IncontinencePadTestValue.AcquisitionTimeSecond = viewModel.AquisitionAddition2;
            testValue.IncontinencePadTestValue.AcquisitionTimeThird = viewModel.AquisitionAddition3;
            testValue.IncontinencePadTestValue.AcquisitionWeight = viewModel.InkoWeight;
            testValue.IncontinencePadTestValue.RewetAfterAcquisitionTimeDryWeight = viewModel.FPDry;
            testValue.IncontinencePadTestValue.RewetAfterAcquisitionTimeWetWeight = viewModel.FPWet;
            testValue.IncontinencePadTestValue.TestType = TestTypeIncontinencePad.AcquisitionTimeAndRewet;

            if (viewModel.Notes.IsNull())
                viewModel.Notes = new List<TestNote>();

            foreach (var note in testValue.TestValueNote)
                foreach (var vmNote in viewModel.Notes.Where(vmNote => note.TestValueNoteId == vmNote.Id))
                {
                    note.Message = vmNote.Message;
                    note.ErrorRefId = vmNote.ErrorCodeId;
                }
            foreach (var vmNote in viewModel.Notes.Where(n => n.Id == 0))
                testValue.TestValueNote.Add(new TestValueNote { ErrorRefId = vmNote.ErrorCodeId, Message = vmNote.Message, TestValue = testValue });

            testValue.IncontinencePadTestValue = CalculateInkoAquisitionValues(testValue.IncontinencePadTestValue, viewModel.TestSheetId);

            TestBll.UpdateTestValue(testValue);
            return testValue;
        }

        #endregion

        #region Private Methods

        /// <summary>
        ///     Calculates all values for the incontinence acquisition test
        /// </summary>
        /// <param name="incontinencePadTestValue">the test value</param>
        /// <param name="testSheetId">the test sheet id</param>
        /// <returns></returns>
        private IncontinencePadTestValue CalculateInkoAquisitionValues(IncontinencePadTestValue incontinencePadTestValue,
                                                                   Int32 testSheetId)
        {
            var testSheet = TestBll.GetTestSheetInfo(testSheetId);
            var productionOrder = TestBll.GetProductionOrder(testSheet.FaNr);

            incontinencePadTestValue.AcquisitionTimeFirstRw = GetMaxRw( incontinencePadTestValue.AcquisitionTimeFirst, productionOrder.Article.MaxHyTec1 );
            incontinencePadTestValue.AcquisitionTimeSecondRw = GetMaxRw(incontinencePadTestValue.AcquisitionTimeSecond, productionOrder.Article.MaxHyTec2 );
            incontinencePadTestValue.AcquisitionTimeThirdRw = GetMaxRw(incontinencePadTestValue.AcquisitionTimeThird, productionOrder.Article.MaxHyTec3 );

            incontinencePadTestValue.RewetAfterAcquisitionTimeWeightDifference = incontinencePadTestValue.RewetAfterAcquisitionTimeWetWeight - incontinencePadTestValue.RewetAfterAcquisitionTimeDryWeight;
            incontinencePadTestValue.RewetAfterAcquisitionTimeRw = GetMaxRw(incontinencePadTestValue.RewetAfterAcquisitionTimeWeightDifference, productionOrder.Article.MaxInkoRewetAfterAquisition);
            return incontinencePadTestValue;
        }

        /// <summary>
        ///     returns the RwType for a max testcase
        /// </summary>
        /// <param name="value">the tested Value</param>
        /// <param name="articleMaxValue">the Article max value</param>
        /// <returns>The RwType</returns>
        private static RwType GetMaxRw(Double value, Double articleMaxValue) => articleMaxValue > value ? RwType.Ok : RwType.Worse;

        private TestValue UpdateInkoAquisitionAvg(TestSheet testSheet, TestValue aquisitionTestAvg)
        {
            var productionOrder = TestBll.GetProductionOrder(testSheet.FaNr);
            var tempInko = new IncontinencePadTestValue { RewetFreeRw = RwType.Ok };
            var counter = 0;
            foreach (
                var testValue in
                testSheet.TestValues.Where(
                             testValue =>
                                 testValue.TestValueType == TestValueType.Single
                                 && testValue.IncontinencePadTestValue.TestType == TestTypeIncontinencePad.AcquisitionTimeAndRewet)
            )
            {
                tempInko.AcquisitionTimeFirst += testValue.IncontinencePadTestValue.AcquisitionTimeFirst;
                tempInko.AcquisitionTimeSecond += testValue.IncontinencePadTestValue.AcquisitionTimeSecond;
                tempInko.AcquisitionTimeThird += testValue.IncontinencePadTestValue.AcquisitionTimeThird;
                tempInko.AcquisitionWeight += testValue.IncontinencePadTestValue.AcquisitionWeight;

                tempInko.RewetAfterAcquisitionTimeDryWeight += testValue.IncontinencePadTestValue.RewetAfterAcquisitionTimeDryWeight;
                tempInko.RewetAfterAcquisitionTimeWetWeight += testValue.IncontinencePadTestValue.RewetAfterAcquisitionTimeWetWeight;
                tempInko.RewetAfterAcquisitionTimeWeightDifference += testValue.IncontinencePadTestValue.RewetAfterAcquisitionTimeWeightDifference;
                if (testValue.IncontinencePadTestValue.AcquisitionTimeFirstRw == RwType.Worse)
                    tempInko.AcquisitionTimeFirstRw = RwType.SomethingWorse;
                if (testValue.IncontinencePadTestValue.AcquisitionTimeSecondRw == RwType.Worse)
                    tempInko.AcquisitionTimeSecondRw = RwType.SomethingWorse;
                if (testValue.IncontinencePadTestValue.AcquisitionTimeThirdRw == RwType.Worse)
                    tempInko.AcquisitionTimeThirdRw = RwType.SomethingWorse;
                if (testValue.IncontinencePadTestValue.RewetAfterAcquisitionTimeRw == RwType.Worse)
                    tempInko.RewetAfterAcquisitionTimeRw = RwType.SomethingWorse;
                counter++;
            }
            if (counter == 0)
                counter = 1;
            aquisitionTestAvg.IncontinencePadTestValue.AcquisitionTimeFirst = tempInko.AcquisitionTimeFirst / counter;
            aquisitionTestAvg.IncontinencePadTestValue.AcquisitionTimeSecond = tempInko.AcquisitionTimeSecond / counter;
            aquisitionTestAvg.IncontinencePadTestValue.AcquisitionTimeThird = tempInko.AcquisitionTimeThird / counter;
            aquisitionTestAvg.IncontinencePadTestValue.AcquisitionWeight = tempInko.AcquisitionWeight / counter;
            aquisitionTestAvg.IncontinencePadTestValue.RewetAfterAcquisitionTimeDryWeight = tempInko.RewetAfterAcquisitionTimeDryWeight / counter;
            aquisitionTestAvg.IncontinencePadTestValue.RewetAfterAcquisitionTimeWetWeight = tempInko.RewetAfterAcquisitionTimeWetWeight / counter;
            aquisitionTestAvg.IncontinencePadTestValue.RewetAfterAcquisitionTimeWeightDifference = tempInko.RewetAfterAcquisitionTimeWeightDifference / counter;

            if (GetMaxRw(aquisitionTestAvg.IncontinencePadTestValue.AcquisitionTimeFirst, productionOrder.Article.MaxHyTec1) == RwType.Worse)
                tempInko.AcquisitionTimeFirstRw = RwType.Worse;
            aquisitionTestAvg.IncontinencePadTestValue.AcquisitionTimeFirstRw = tempInko.AcquisitionTimeFirstRw;

            if (GetMaxRw(aquisitionTestAvg.IncontinencePadTestValue.AcquisitionTimeSecond, productionOrder.Article.MaxHyTec2) == RwType.Worse)
                tempInko.AcquisitionTimeSecondRw = RwType.Worse;
            aquisitionTestAvg.IncontinencePadTestValue.AcquisitionTimeSecondRw = tempInko.AcquisitionTimeSecondRw;

            if (GetMaxRw(aquisitionTestAvg.IncontinencePadTestValue.AcquisitionTimeThird, productionOrder.Article.MaxHyTec3) == RwType.Worse)
                tempInko.AcquisitionTimeThirdRw = RwType.Worse;
            aquisitionTestAvg.IncontinencePadTestValue.AcquisitionTimeThirdRw = tempInko.AcquisitionTimeThirdRw;

            if (GetMaxRw(aquisitionTestAvg.IncontinencePadTestValue.RewetAfterAcquisitionTimeWeightDifference, productionOrder.Article.MaxInkoRewetAfterAquisition) == RwType.Worse)
                tempInko.RewetAfterAcquisitionTimeRw = RwType.Worse;
            aquisitionTestAvg.IncontinencePadTestValue.RewetAfterAcquisitionTimeRw = tempInko.RewetAfterAcquisitionTimeRw;
            return aquisitionTestAvg;
        }

        private static TestValue UpdateInkoAquisitionStDev(TestSheet testSheet, TestValue aquisitionTestAvg, TestValue aquisitionTestStDev)
        {
            var tempInko = new IncontinencePadTestValue();
            var counter = 0;
            foreach (
                var testValue in
                testSheet.TestValues.Where(
                             testValue =>
                                 testValue.TestValueType == TestValueType.Single
                                 && testValue.IncontinencePadTestValue.TestType == TestTypeIncontinencePad.AcquisitionTimeAndRewet)
            )
            {
                tempInko.AcquisitionTimeFirst += Math.Pow(testValue.IncontinencePadTestValue.AcquisitionTimeFirst - aquisitionTestAvg.IncontinencePadTestValue.AcquisitionTimeFirst, 2);
                tempInko.AcquisitionTimeSecond += Math.Pow(testValue.IncontinencePadTestValue.AcquisitionTimeSecond - aquisitionTestAvg.IncontinencePadTestValue.AcquisitionTimeSecond, 2);
                tempInko.AcquisitionTimeThird += Math.Pow(testValue.IncontinencePadTestValue.AcquisitionTimeThird - aquisitionTestAvg.IncontinencePadTestValue.AcquisitionTimeThird, 2);
                tempInko.AcquisitionWeight += Math.Pow(testValue.IncontinencePadTestValue.AcquisitionWeight - aquisitionTestAvg.IncontinencePadTestValue.AcquisitionWeight, 2);
                tempInko.RewetAfterAcquisitionTimeDryWeight += Math.Pow(testValue.IncontinencePadTestValue.RewetAfterAcquisitionTimeDryWeight - aquisitionTestAvg.IncontinencePadTestValue.RewetAfterAcquisitionTimeDryWeight, 2);
                tempInko.RewetAfterAcquisitionTimeWetWeight += Math.Pow(testValue.IncontinencePadTestValue.RewetAfterAcquisitionTimeWetWeight - aquisitionTestAvg.IncontinencePadTestValue.RewetAfterAcquisitionTimeWetWeight, 2);
                tempInko.RewetAfterAcquisitionTimeWeightDifference += Math.Pow(testValue.IncontinencePadTestValue.RewetAfterAcquisitionTimeWeightDifference - aquisitionTestAvg.IncontinencePadTestValue.RewetAfterAcquisitionTimeWeightDifference, 2);
                counter++;
            }
            if ( counter < 2 )
            {
                aquisitionTestStDev.IncontinencePadTestValue.AcquisitionTimeFirst = 0;
                aquisitionTestStDev.IncontinencePadTestValue.AcquisitionTimeSecond = 0;
                aquisitionTestStDev.IncontinencePadTestValue.AcquisitionTimeThird = 0;
                aquisitionTestStDev.IncontinencePadTestValue.AcquisitionWeight = 0;
                aquisitionTestStDev.IncontinencePadTestValue.RewetAfterAcquisitionTimeDryWeight = 0;
                aquisitionTestStDev.IncontinencePadTestValue.RewetAfterAcquisitionTimeWetWeight = 0;
                aquisitionTestStDev.IncontinencePadTestValue.RewetAfterAcquisitionTimeWeightDifference = 0;
            }
            else
            {
                counter--;
                aquisitionTestStDev.IncontinencePadTestValue.AcquisitionTimeFirst = Math.Sqrt(tempInko.AcquisitionTimeFirst / counter);
                aquisitionTestStDev.IncontinencePadTestValue.AcquisitionTimeSecond = Math.Sqrt(tempInko.AcquisitionTimeSecond / counter);
                aquisitionTestStDev.IncontinencePadTestValue.AcquisitionTimeThird = Math.Sqrt(tempInko.AcquisitionTimeThird / counter);
                aquisitionTestStDev.IncontinencePadTestValue.AcquisitionWeight = Math.Sqrt(tempInko.AcquisitionWeight / counter);
                aquisitionTestStDev.IncontinencePadTestValue.RewetAfterAcquisitionTimeDryWeight = Math.Sqrt(tempInko.RewetAfterAcquisitionTimeDryWeight / counter);
                aquisitionTestStDev.IncontinencePadTestValue.RewetAfterAcquisitionTimeWetWeight = Math.Sqrt(tempInko.RewetAfterAcquisitionTimeWetWeight / counter);
                aquisitionTestStDev.IncontinencePadTestValue.RewetAfterAcquisitionTimeWeightDifference = Math.Sqrt(tempInko.RewetAfterAcquisitionTimeWeightDifference / counter);
            }
            return aquisitionTestStDev;
        }

        #endregion
    }
}
