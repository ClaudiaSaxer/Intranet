using System;
using Intranet.Common;
using Intranet.Labor.Definition;
using Intranet.Labor.Model;
using Intranet.Labor.Model.labor;
using Intranet.Labor.ViewModel;

namespace Intranet.Labor.Bll
{
    /// <summary>
    /// </summary>
    public class BabyDiaperServiceHelper : ServiceBase, IBabyDiaperServiceHelper
    {
        #region Ctor

        /// <summary>
        ///     Initialize a new instance of the <see cref="ServiceBase" /> class.
        /// </summary>
        /// <param name="loggerFactory">A <see cref="ILoggerFactory" />.</param>
        public BabyDiaperServiceHelper(ILoggerFactory loggerFactory)
            : base(loggerFactory.CreateLogger(typeof(BabyDiaperServiceHelper)))
        {
            Logger.Trace("Enter Ctor - Exit.");
        }

        #endregion

        #region Properties

        /// <summary>
        ///     Gets or sets the bll for the baby diapers retention test.
        /// </summary>
        public IBabyDiaperRetentionBll BabyDiaperRetentionBll { get; set; }

        #endregion

        /// <summary>
        ///     Calculates all values for the baby diaper retention test
        /// </summary>
        /// <param name="babyDiaperTestValue">the test value</param>
        /// <param name="testSheetId">the test sheet id</param>
        /// <returns></returns>
        public BabyDiaperTestValue CalculateBabyDiaperRetentionValues(BabyDiaperTestValue babyDiaperTestValue,
            int testSheetId)
        {
            var testSheet = BabyDiaperRetentionBll.GetTestSheetInfo(testSheetId);
            var productionOrder = BabyDiaperRetentionBll.GetProductionOrder(testSheet.FaNr);

            babyDiaperTestValue.RetentionAfterZentrifugeValue = babyDiaperTestValue.RetentionWetWeight -
                                                                babyDiaperTestValue.WeightDiaperDry;
            if (Math.Abs(babyDiaperTestValue.WeightDiaperDry) > 0.1)
                babyDiaperTestValue.RetentionAfterZentrifugePercent = (babyDiaperTestValue.RetentionWetWeight -
                                                                       babyDiaperTestValue.WeightDiaperDry)*100.0/
                                                                      babyDiaperTestValue.WeightDiaperDry;
            babyDiaperTestValue.RetentionRw = GetRetentionRwType( babyDiaperTestValue.RetentionAfterZentrifugeValue, productionOrder);
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
        public RwType GetRetentionRwType( Double value, ProductionOrder productOrder)
        {
            if(value <= productOrder.Article.MinRetention) return RwType.Worse;
            return value >= productOrder.Article.MaxRetention ? RwType.Better : RwType.Ok;
        }

        #region Implementation of IBabyDiaperRetentionBll

        /// <summary>
        ///     Creates an new TestValue from the view model
        /// </summary>
        /// <param name="viewModel">the data from the view</param>
        /// <returns>The created test value</returns>
        public TestValue SaveNewRetentionTest(BabyDiaperRetentionEditViewModel viewModel)
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
            var babyDiaperTestValue = new BabyDiaperTestValue
            {
                DiaperCreatedTime = viewModel.ProductionCodeTime,
                WeightDiaperDry = viewModel.DiaperWeight,
                RetentionWetWeight = viewModel.WeightRetentionWet,
                TestType = TestTypeBabyDiaper.Retention
            };
            babyDiaperTestValue = CalculateBabyDiaperRetentionValues(babyDiaperTestValue, viewModel.TestSheetId);
            testValue.BabyDiaperTestValue = babyDiaperTestValue;

            BabyDiaperRetentionBll.SaveNewTestValue(testValue);
            return testValue;
        }

        /// <summary>
        ///     Updates an given Testvalue from the view model
        /// </summary>
        /// <param name="viewModel">the data from the view</param>
        /// <returns>the updated test value</returns>
        public TestValue UpdateRetentionTest(BabyDiaperRetentionEditViewModel viewModel)
        {
            var testValue = BabyDiaperRetentionBll.GetTestValue(viewModel.TestValueId);
            testValue.LastEditedDateTime = DateTime.Now;
            testValue.LastEditedPerson = viewModel.TestPerson;
            testValue.DayInYearOfArticleCreation = viewModel.ProductionCodeDay;
            testValue.BabyDiaperTestValue.DiaperCreatedTime = viewModel.ProductionCodeTime;
            testValue.BabyDiaperTestValue.WeightDiaperDry = viewModel.DiaperWeight;
            testValue.BabyDiaperTestValue.RetentionWetWeight = viewModel.WeightRetentionWet;
            testValue.BabyDiaperTestValue = CalculateBabyDiaperRetentionValues(testValue.BabyDiaperTestValue,
                viewModel.TestSheetId);

            BabyDiaperRetentionBll.UpdateTestValue(testValue);
            return testValue;
        }

        /// <summary>
        ///     Updates the Average and standard deviation values of the testsheet
        /// </summary>
        /// <param name="testSheetId">id of the test sheet</param>
        /// <returns>the updated test sheet</returns>
        public TestSheet UpdateAverageAndStv(Int32 testSheetId)
        {
            // TODO
            return null;
        }

        #endregion
    }
}