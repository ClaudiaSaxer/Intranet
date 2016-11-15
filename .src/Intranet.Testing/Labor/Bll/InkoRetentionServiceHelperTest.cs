using System;
using System.Collections.Generic;
using System.Linq;
using Intranet.Common;
using Intranet.Labor.Model;
using Intranet.Labor.Model.labor;
using Intranet.Labor.TestEnvironment;
using Intranet.Labor.ViewModel;
using Xunit;

namespace Intranet.Labor.Bll.Test
{
    /// <summary>
    ///     Class representing Tests for InkoRetentionServiceHelper
    /// </summary>
    public class InkoRetentionServiceHelperTest
    {
        #region BaseTestData

        private TestSheet GetTestSheetTestData() => new TestSheet
        {
            FaNr = "FA654321"
        };

        private ProductionOrder GetProductionOrderTestData() => new ProductionOrder
        {
            Article = new Article
            {
                MinInkoRetention = 180
            }
        };

        private TestSheet GetTestSheetTestDataWithAvgAndStDev() => new TestSheet
        {
            TestValues = new List<TestValue>
            {
                new TestValue
                {
                    TestValueType = TestValueType.Average,
                    ArticleTestType = ArticleType.IncontinencePad,
                    IncontinencePadTestValue = new IncontinencePadTestValue
                    {
                        TestType = TestTypeIncontinencePad.Retention
                    }
                },
                new TestValue
                {
                    TestValueType = TestValueType.StandardDeviation,
                    ArticleTestType = ArticleType.IncontinencePad,
                    IncontinencePadTestValue = new IncontinencePadTestValue
                    {
                        TestType = TestTypeIncontinencePad.Retention
                    }
                }
            }
        };

        #endregion

        #region SaveNewRetentionTest Tests

        /// <summary>
        ///     Tests if Saving a new InkoRewet Test works
        /// </summary>
        [Fact]
        public void SaveNewRetentionTestBaseTest()
        {
            var viewModel = new InkoRetentionEditViewModel
            {
                TestPerson = "Hans",
                TestValueId = -1,
                TestSheetId = 1,
                ProductionCodeTime = new TimeSpan(12, 34, 0),
                ProductionCodeDay = 123,
                InkoWeight = 30.21,
                InkoWeightWet = 430.15,
                InkoWeightAfterZentrifuge = 212.11,
                Notes = new List<TestNote> { new TestNote { ErrorCodeId = 1, Id = 1, Message = "Testnote" } }
            };
            var testValueReturnedFromHelper = new TestValue
            {
                TestSheetRefId = 1,
                CreatedDateTime = new DateTime(2016, 1, 2),
                LastEditedDateTime = new DateTime(2016, 1, 2),
                CreatedPerson = "Hans",
                LastEditedPerson = "Hans",
                DayInYearOfArticleCreation = 123,
                ArticleTestType = ArticleType.IncontinencePad,
                TestValueNote = new List<TestValueNote> { new TestValueNote { ErrorRefId = 1, Message = "Testnote" } }
            };
            var testSheetDataFromDb = GetTestSheetTestData();
            var productionOrderDataFromDb = GetProductionOrderTestData();

            var testBll = MockHelperBll.GetTestBllForSavingAndUpdating(testSheetDataFromDb, productionOrderDataFromDb, null);
            var testServiceHelper = MockHelperTestServiceHelper.GetTestServiceHelperCreateNewTestValue(testValueReturnedFromHelper);

            var target = new InkoRetentionServiceHelper(new NLogLoggerFactory())
            {
                TestBll = testBll,
                TestServiceHelper = testServiceHelper
            };

            var actual = target.SaveNewRetentionTest(viewModel);

            Assert.Equal(testValueReturnedFromHelper, actual);
        }

        /// <summary>
        ///     Tests if while saving the correct RwValue choosen
        /// </summary>
        [Fact]
        public void SaveNewRetentionTestCalculationRwOkTest()
        {
            var viewModel = new InkoRetentionEditViewModel
            {
                TestPerson = "Hans",
                TestValueId = -1,
                TestSheetId = 1,
                ProductionCodeTime = new TimeSpan(12, 34, 0),
                ProductionCodeDay = 123,
                InkoWeight = 30.21,
                InkoWeightWet = 430.15,
                InkoWeightAfterZentrifuge = 212.11,
                Notes = new List<TestNote> { new TestNote { ErrorCodeId = 1, Id = 1, Message = "Testnote" } }
            };
            var testValueReturnedFromHelper = new TestValue
            {
                TestSheetRefId = 1,
                CreatedDateTime = new DateTime(2016, 1, 2),
                LastEditedDateTime = new DateTime(2016, 1, 2),
                CreatedPerson = "Hans",
                LastEditedPerson = "Hans",
                DayInYearOfArticleCreation = 123,
                ArticleTestType = ArticleType.IncontinencePad,
                TestValueNote = new List<TestValueNote> { new TestValueNote { ErrorRefId = 1, Message = "Testnote" } }
            };
            var testSheetDataFromDb = GetTestSheetTestData();
            var productionOrderDataFromDb = GetProductionOrderTestData();

            var testBll = MockHelperBll.GetTestBllForSavingAndUpdating(testSheetDataFromDb, productionOrderDataFromDb, null);
            var testServiceHelper = MockHelperTestServiceHelper.GetTestServiceHelperCreateNewTestValue(testValueReturnedFromHelper);

            var target = new InkoRetentionServiceHelper(new NLogLoggerFactory())
            {
                TestBll = testBll,
                TestServiceHelper = testServiceHelper
            };

            var actual = target.SaveNewRetentionTest(viewModel);

            Assert.Equal(RwType.Ok, actual.IncontinencePadTestValue.RetentionRw);
        }

        /// <summary>
        ///     Tests if while saving the correct RwValue choosen
        /// </summary>
        [Fact]
        public void SaveNewRetentionTestCalculationRwWorseTest()
        {
            var viewModel = new InkoRetentionEditViewModel
            {
                TestPerson = "Hans",
                TestValueId = -1,
                TestSheetId = 1,
                ProductionCodeTime = new TimeSpan(12, 34, 0),
                ProductionCodeDay = 123,
                InkoWeight = 30.21,
                InkoWeightWet = 430.15,
                InkoWeightAfterZentrifuge = 202.11,
                Notes = new List<TestNote> { new TestNote { ErrorCodeId = 1, Id = 1, Message = "Testnote" } }
            };
            var testValueReturnedFromHelper = new TestValue
            {
                TestSheetRefId = 1,
                CreatedDateTime = new DateTime(2016, 1, 2),
                LastEditedDateTime = new DateTime(2016, 1, 2),
                CreatedPerson = "Hans",
                LastEditedPerson = "Hans",
                DayInYearOfArticleCreation = 123,
                ArticleTestType = ArticleType.IncontinencePad,
                TestValueNote = new List<TestValueNote> { new TestValueNote { ErrorRefId = 1, Message = "Testnote" } }
            };
            var testSheetDataFromDb = GetTestSheetTestData();
            var productionOrderDataFromDb = GetProductionOrderTestData();

            var testBll = MockHelperBll.GetTestBllForSavingAndUpdating(testSheetDataFromDb, productionOrderDataFromDb, null);
            var testServiceHelper = MockHelperTestServiceHelper.GetTestServiceHelperCreateNewTestValue(testValueReturnedFromHelper);

            var target = new InkoRetentionServiceHelper(new NLogLoggerFactory())
            {
                TestBll = testBll,
                TestServiceHelper = testServiceHelper
            };

            var actual = target.SaveNewRetentionTest(viewModel);

            Assert.Equal(RwType.Worse, actual.IncontinencePadTestValue.RetentionRw);
        }

        #endregion

        #region UpdateRetentionTest Tests



        #endregion

        #region UpdateRetentionAverageAndStv Tests



        #endregion
    }
}
