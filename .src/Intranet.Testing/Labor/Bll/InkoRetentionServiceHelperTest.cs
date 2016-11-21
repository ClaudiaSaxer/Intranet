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

        private InkoRetentionEditViewModel GetViewModelTestData() => new InkoRetentionEditViewModel
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

        private TestValue GetTestValueTestData() => new TestValue
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

        #endregion

        #region SaveNewRetentionTest Tests

        /// <summary>
        ///     Tests if Saving a new InkoRewet Test works
        /// </summary>
        [Fact]
        public void SaveNewRetentionTestBaseTest()
        {
            var viewModel = GetViewModelTestData();
            var testValueReturnedFromHelper = GetTestValueTestData();
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
            var viewModel = GetViewModelTestData();
            var testValueReturnedFromHelper = GetTestValueTestData();
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
            var viewModel = GetViewModelTestData();
            viewModel.InkoWeightAfterZentrifuge = 202.11;
            var testValueReturnedFromHelper = GetTestValueTestData();
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

        /// <summary>
        ///     Tests if Updating an Test returns null if old test not exist
        /// </summary>
        [Fact]
        public void UpdateRetentionTestFailTest()
        {
            var testBll = MockHelperBll.GetTestBllForSavingAndUpdating(null, null, null);
            var target = new InkoRetentionServiceHelper(new NLogLoggerFactory())
            {
                TestBll = testBll
            };

            var actual = target.UpdateRetentionTest(new InkoRetentionEditViewModel());

            Assert.Equal(null, actual);
        }

        /// <summary>
        ///     Tests if Updating an existing Inko Retention Test works
        /// </summary>
        [Fact]
        public void UpdateRetentionTestBaseTest()
        {
            var viewModel = GetViewModelTestData();
            var testValueReturnedFromDb = GetTestValueTestData();
            testValueReturnedFromDb.CreatedPerson = "Fritz";
            testValueReturnedFromDb.LastEditedPerson = "Fritz";
            testValueReturnedFromDb.IncontinencePadTestValue = new IncontinencePadTestValue
            {
                IncontinencePadTime = new TimeSpan( 11, 11, 0 ),
                TestType = TestTypeIncontinencePad.Retention
            };

            var testSheetDataFromDb = GetTestSheetTestData();
            var productionOrderDataFromDb = GetProductionOrderTestData();

            var babyDiaperBll = MockHelperBll.GetTestBllForSavingAndUpdating(testSheetDataFromDb, productionOrderDataFromDb, testValueReturnedFromDb);

            var testServiceHelper = MockHelperTestServiceHelper.GetTestServiceHelperForUpdating();

            var target = new InkoRetentionServiceHelper(new NLogLoggerFactory())
            {
                TestBll = babyDiaperBll,
                TestServiceHelper = testServiceHelper
            };

            var actual = target.UpdateRetentionTest(viewModel);

            Assert.Equal(testValueReturnedFromDb, actual);
            Assert.Equal(30.21, actual.IncontinencePadTestValue.RetentionWeight);
            Assert.Equal(430.15, actual.IncontinencePadTestValue.RetentionWetValue);
            Assert.Equal(212.11, actual.IncontinencePadTestValue.RetentionAfterZentrifuge);
            Assert.Equal(399.94, actual.IncontinencePadTestValue.RetentionAbsorbtion, 2);
            Assert.Equal(181.9, actual.IncontinencePadTestValue.RetentionEndValue, 2);
            Assert.Equal("Hans", actual.LastEditedPerson);
            Assert.Equal("Fritz", actual.CreatedPerson);
            Assert.NotEqual(new DateTime(2016, 1, 2), actual.LastEditedDateTime);
        }

        #endregion

        #region UpdateRetentionAverageAndStv Tests

        /// <summary>
        ///     Tests when there are no tests, the avg and dev values are default
        /// </summary>
        [Fact]
        public void UpdateRetentionAverageAndStvNoTestsTest()
        {
            var testSheetDataFromDb = GetTestSheetTestDataWithAvgAndStDev();
            var productionOrderDataFromDb = GetProductionOrderTestData();

            var babyDiaperBll = MockHelperBll.GetTestBllForSavingAndUpdating(testSheetDataFromDb, productionOrderDataFromDb, null);

            var target = new InkoRetentionServiceHelper(new NLogLoggerFactory())
            {
                TestBll = babyDiaperBll
            };

            var actual = target.UpdateRetentionAverageAndStv(1);

            var actualRetentionAvg =
                actual.TestValues.FirstOrDefault(tv => tv.TestValueType == TestValueType.Average && tv.IncontinencePadTestValue.TestType == TestTypeIncontinencePad.Retention);
            Assert.NotNull(actualRetentionAvg);
            Assert.Equal(0, actualRetentionAvg.IncontinencePadTestValue.RetentionWeight);
            Assert.Equal(0, actualRetentionAvg.IncontinencePadTestValue.RetentionWetValue);
            Assert.Equal(0, actualRetentionAvg.IncontinencePadTestValue.RetentionAfterZentrifuge);
            Assert.Equal(0, actualRetentionAvg.IncontinencePadTestValue.RetentionAbsorbtion);
            Assert.Equal(0, actualRetentionAvg.IncontinencePadTestValue.RetentionEndValue);
            Assert.Equal(RwType.Ok, actualRetentionAvg.IncontinencePadTestValue.RetentionRw);

            var actualRetentionStDev =
                actual.TestValues.FirstOrDefault(
                          tv => tv.TestValueType == TestValueType.StandardDeviation && tv.IncontinencePadTestValue.TestType == TestTypeIncontinencePad.Retention);
            Assert.NotNull(actualRetentionStDev);
            Assert.Equal(0, actualRetentionStDev.IncontinencePadTestValue.RetentionWeight);
            Assert.Equal(0, actualRetentionStDev.IncontinencePadTestValue.RetentionWetValue);
            Assert.Equal(0, actualRetentionStDev.IncontinencePadTestValue.RetentionAfterZentrifuge);
            Assert.Equal(0, actualRetentionStDev.IncontinencePadTestValue.RetentionAbsorbtion);
            Assert.Equal(0, actualRetentionStDev.IncontinencePadTestValue.RetentionEndValue);
        }

        /// <summary>
        ///     Tests when there is only one test, the avg values are equal the test values
        /// </summary>
        [Fact]
        public void UpdateRetentionAverageAndStvOneTestAvgTest()
        {
            var onlyTestValue = new TestValue
            {
                TestValueType = TestValueType.Single,
                ArticleTestType = ArticleType.BabyDiaper,
                IncontinencePadTestValue = new IncontinencePadTestValue
                {
                    TestType = TestTypeIncontinencePad.Retention,
                    RetentionWeight = 30.21,
                    RetentionWetValue = 430.15,
                    RetentionAfterZentrifuge = 212.11,
                    RetentionAbsorbtion = 399.94,
                    RetentionEndValue = 181.9,
                    RetentionRw = RwType.Ok
                }
            };
            var testSheetDataFromDb = GetTestSheetTestDataWithAvgAndStDev();
            testSheetDataFromDb.TestValues.Add(onlyTestValue);
            var productionOrderDataFromDb = GetProductionOrderTestData();

            var testBll = MockHelperBll.GetTestBllForSavingAndUpdating(testSheetDataFromDb, productionOrderDataFromDb, null);

            var target = new InkoRetentionServiceHelper(new NLogLoggerFactory())
            {
                TestBll = testBll
            };

            var actual = target.UpdateRetentionAverageAndStv(1);

            var actualRetentionAvg =
                actual.TestValues.FirstOrDefault(tv => tv.TestValueType == TestValueType.Average && tv.IncontinencePadTestValue.TestType == TestTypeIncontinencePad.Retention);
            Assert.NotNull(actualRetentionAvg);
            Assert.Equal(30.21, actualRetentionAvg.IncontinencePadTestValue.RetentionWeight);
            Assert.Equal(430.15, actualRetentionAvg.IncontinencePadTestValue.RetentionWetValue);
            Assert.Equal(212.11, actualRetentionAvg.IncontinencePadTestValue.RetentionAfterZentrifuge);
            Assert.Equal(399.94, actualRetentionAvg.IncontinencePadTestValue.RetentionAbsorbtion);
            Assert.Equal(181.9, actualRetentionAvg.IncontinencePadTestValue.RetentionEndValue);
            Assert.Equal(RwType.Ok, actualRetentionAvg.IncontinencePadTestValue.RetentionRw);

            var actualRetentionStDev =
                actual.TestValues.FirstOrDefault(
                          tv => tv.TestValueType == TestValueType.StandardDeviation && tv.IncontinencePadTestValue.TestType == TestTypeIncontinencePad.Retention);
            Assert.NotNull(actualRetentionStDev);
            Assert.Equal(0, actualRetentionStDev.IncontinencePadTestValue.RetentionWeight);
            Assert.Equal(0, actualRetentionStDev.IncontinencePadTestValue.RetentionWetValue);
            Assert.Equal(0, actualRetentionStDev.IncontinencePadTestValue.RetentionAfterZentrifuge);
            Assert.Equal(0, actualRetentionStDev.IncontinencePadTestValue.RetentionAbsorbtion);
            Assert.Equal(0, actualRetentionStDev.IncontinencePadTestValue.RetentionEndValue);
        }

        #endregion
    }
}
