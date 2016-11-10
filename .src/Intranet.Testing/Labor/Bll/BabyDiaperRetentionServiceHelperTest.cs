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
    ///     Class representing Tests for BabyDiaperRetentionServiceHelper
    /// </summary>
    public class BabyDiaperRetentionServiceHelperTest
    {
        #region BaseTestData

        private TestSheet GetTestSheetTestData() => new TestSheet
        {
            FaNr = "FA123456",
            SAPType = "EKX",
            SAPNr = "EN67"
        };

        private ProductionOrder GetProductionOrderTestData() => new ProductionOrder
        {
            Component = new ProductionOrderComponent
            {
                SAP = 32.7,
                PillowRetentWithoutSAP = 31.2,
                PillowWeightWithoutSAP = 26.0,
                CelluloseRetention = 1.2,
                ComponentType = "EKX",
                ComponentNr = "EN67"
            },
            Article = new Article
            {
                MinRetention = 350,
                MaxRetention = 380
            }
        };

        private TestSheet GetTestSheetTestDataWithAvgAndStDev() => new TestSheet
        {
            TestValues = new List<TestValue>
            {
                new TestValue
                {
                    TestValueType = TestValueType.Average,
                    ArticleTestType = ArticleType.BabyDiaper,
                    BabyDiaperTestValue = new BabyDiaperTestValue
                    {
                        TestType = TestTypeBabyDiaper.Retention
                    }
                },
                new TestValue
                {
                    TestValueType = TestValueType.StandardDeviation,
                    ArticleTestType = ArticleType.BabyDiaper,
                    BabyDiaperTestValue = new BabyDiaperTestValue
                    {
                        TestType = TestTypeBabyDiaper.Retention
                    }
                },
            }
        };

        #endregion

        #region SaveNewRetentionTest Tests

        /// <summary>
        ///     Tests if Saving a new Retention Test works
        /// </summary>
        [Fact]
        public void SaveNewRetentionTestBaseTest()
        {
            var viewModel = new BabyDiaperRetentionEditViewModel
            {
                TestPerson = "Hans",
                TestValueId = -1,
                TestSheetId = 1,
                ProductionCodeTime = new TimeSpan(12,34,0),
                ProductionCodeDay = 123,
                DiaperWeight = 30.1,
                WeightRetentionWet = 399.8,
                Notes = new List<TestNote> { new TestNote { ErrorCodeId = 1, Id = 1, Message = "Testnote"} }
            };
            var testValueReturnedFromHelper = new TestValue
            {
                TestSheetRefId = 1,
                CreatedDateTime = new DateTime(2016,1,2),
                LastEditedDateTime = new DateTime(2016, 1, 2),
                CreatedPerson = "Hans",
                LastEditedPerson = "Hans",
                DayInYearOfArticleCreation = 123,
                ArticleTestType = ArticleType.BabyDiaper,
                TestValueNote = new List<TestValueNote> { new TestValueNote { ErrorRefId = 1, Message = "Testnote" } }
            };
            var testSheetDataFromDb = GetTestSheetTestData();
            var productionOrderDataFromDb = GetProductionOrderTestData();

            var babyDiaperRetentionBll = MockHelperBll.GetBabyDiaperBllForSavingAndUpdating(testSheetDataFromDb, productionOrderDataFromDb,null);
            var babyDiaperServiceHelper = MockHelperBabyDiaperServiceHelper.GetBabyDiaperServiceHelperCreateNewTestValue( testValueReturnedFromHelper );

            var target = new BabyDiaperRetentionServiceHelper( new NLogLoggerFactory() )
            {
                BabyDiaperBll = babyDiaperRetentionBll,
                BabyDiaperServiceHelper = babyDiaperServiceHelper
            };

            var actual = target.SaveNewRetentionTest( viewModel );

            Assert.Equal(testValueReturnedFromHelper,actual);
        }

        /// <summary>
        ///     Tests if all calculations while saving are correct
        /// </summary>
        [Fact]
        public void SaveNewRetentionTestCalculationTest()
        {
            var viewModel = new BabyDiaperRetentionEditViewModel
            {
                TestPerson = "Hans",
                TestValueId = -1,
                TestSheetId = 1,
                ProductionCodeTime = new TimeSpan(12, 34, 0),
                ProductionCodeDay = 123,
                DiaperWeight = 30.1,
                WeightRetentionWet = 399.8,
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
                ArticleTestType = ArticleType.BabyDiaper,
                TestValueNote = new List<TestValueNote> { new TestValueNote { ErrorRefId = 1, Message = "Testnote" } }
            };
            var testSheetDataFromDb = GetTestSheetTestData();
            var productionOrderDataFromDb = GetProductionOrderTestData();

            var babyDiaperRetentionBll = MockHelperBll.GetBabyDiaperBllForSavingAndUpdating(testSheetDataFromDb, productionOrderDataFromDb,null);
            var babyDiaperServiceHelper = MockHelperBabyDiaperServiceHelper.GetBabyDiaperServiceHelperCreateNewTestValue(testValueReturnedFromHelper);

            var target = new BabyDiaperRetentionServiceHelper(new NLogLoggerFactory())
            {
                BabyDiaperBll = babyDiaperRetentionBll,
                BabyDiaperServiceHelper = babyDiaperServiceHelper
            };

            var actual = target.SaveNewRetentionTest(viewModel);

            Assert.Equal(369.7, actual.BabyDiaperTestValue.RetentionAfterZentrifugeValue);
            Assert.Equal(1228.2392026578073, actual.BabyDiaperTestValue.RetentionAfterZentrifugePercent, 2);
            Assert.Equal("EKX", actual.BabyDiaperTestValue.SapType);
            Assert.Equal("EN67", actual.BabyDiaperTestValue.SapNr);
            Assert.Equal(10.351681957186543, actual.BabyDiaperTestValue.SapGHoewiValue,2);
        }

        /// <summary>
        ///     Tests if while saving the correct RwValue choosen
        /// </summary>
        [Fact]
        public void SaveNewRetentionTestCalculationRwBetterTest()
        {
            var viewModel = new BabyDiaperRetentionEditViewModel
            {
                TestPerson = "Hans",
                TestValueId = -1,
                TestSheetId = 1,
                ProductionCodeTime = new TimeSpan(12, 34, 0),
                ProductionCodeDay = 123,
                DiaperWeight = 30.1,
                WeightRetentionWet = 420.8,
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
                ArticleTestType = ArticleType.BabyDiaper,
                TestValueNote = new List<TestValueNote> { new TestValueNote { ErrorRefId = 1, Message = "Testnote" } }
            };
            var testSheetDataFromDb = GetTestSheetTestData();
            var productionOrderDataFromDb = GetProductionOrderTestData();

            var babyDiaperRetentionBll = MockHelperBll.GetBabyDiaperBllForSavingAndUpdating(testSheetDataFromDb, productionOrderDataFromDb,null);
            var babyDiaperServiceHelper = MockHelperBabyDiaperServiceHelper.GetBabyDiaperServiceHelperCreateNewTestValue(testValueReturnedFromHelper);

            var target = new BabyDiaperRetentionServiceHelper(new NLogLoggerFactory())
            {
                BabyDiaperBll = babyDiaperRetentionBll,
                BabyDiaperServiceHelper = babyDiaperServiceHelper
            };

            var actual = target.SaveNewRetentionTest(viewModel);

            Assert.Equal(RwType.Better, actual.BabyDiaperTestValue.RetentionRw);
        }

        /// <summary>
        ///     Tests if while saving the correct RwValue choosen
        /// </summary>
        [Fact]
        public void SaveNewRetentionTestCalculationRwOkTest()
        {
            var viewModel = new BabyDiaperRetentionEditViewModel
            {
                TestPerson = "Hans",
                TestValueId = -1,
                TestSheetId = 1,
                ProductionCodeTime = new TimeSpan(12, 34, 0),
                ProductionCodeDay = 123,
                DiaperWeight = 30.1,
                WeightRetentionWet = 399.8,
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
                ArticleTestType = ArticleType.BabyDiaper,
                TestValueNote = new List<TestValueNote> { new TestValueNote { ErrorRefId = 1, Message = "Testnote" } }
            };
            var testSheetDataFromDb = GetTestSheetTestData();
            var productionOrderDataFromDb = GetProductionOrderTestData();

            var babyDiaperRetentionBll = MockHelperBll.GetBabyDiaperBllForSavingAndUpdating(testSheetDataFromDb, productionOrderDataFromDb,null);
            var babyDiaperServiceHelper = MockHelperBabyDiaperServiceHelper.GetBabyDiaperServiceHelperCreateNewTestValue(testValueReturnedFromHelper);

            var target = new BabyDiaperRetentionServiceHelper(new NLogLoggerFactory())
            {
                BabyDiaperBll = babyDiaperRetentionBll,
                BabyDiaperServiceHelper = babyDiaperServiceHelper
            };

            var actual = target.SaveNewRetentionTest(viewModel);

            Assert.Equal(RwType.Ok, actual.BabyDiaperTestValue.RetentionRw);
        }

        /// <summary>
        ///     Tests if while saving the correct RwValue choosen
        /// </summary>
        [Fact]
        public void SaveNewRetentionTestCalculationRwWorseTest()
        {
            var viewModel = new BabyDiaperRetentionEditViewModel
            {
                TestPerson = "Hans",
                TestValueId = -1,
                TestSheetId = 1,
                ProductionCodeTime = new TimeSpan(12, 34, 0),
                ProductionCodeDay = 123,
                DiaperWeight = 30.1,
                WeightRetentionWet = 379.8,
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
                ArticleTestType = ArticleType.BabyDiaper,
                TestValueNote = new List<TestValueNote> { new TestValueNote { ErrorRefId = 1, Message = "Testnote" } }
            };
            var testSheetDataFromDb = GetTestSheetTestData();
            var productionOrderDataFromDb = GetProductionOrderTestData();

            var babyDiaperRetentionBll = MockHelperBll.GetBabyDiaperBllForSavingAndUpdating(testSheetDataFromDb, productionOrderDataFromDb,null);
            var babyDiaperServiceHelper = MockHelperBabyDiaperServiceHelper.GetBabyDiaperServiceHelperCreateNewTestValue(testValueReturnedFromHelper);

            var target = new BabyDiaperRetentionServiceHelper(new NLogLoggerFactory())
            {
                BabyDiaperBll = babyDiaperRetentionBll,
                BabyDiaperServiceHelper = babyDiaperServiceHelper
            };

            var actual = target.SaveNewRetentionTest(viewModel);

            Assert.Equal(RwType.Worse, actual.BabyDiaperTestValue.RetentionRw);
        }

        #endregion

        #region UpdateRetentionTest Tests

        /// <summary>
        ///     Tests if Updating an Test returns null if old test not exist
        /// </summary>
        [Fact]
        public void UpdateRetentionTestFailTest()
        {
            var babyDiaperRetentionBll = MockHelperBll.GetBabyDiaperBllForSavingAndUpdating(null, null, null);
            var target = new BabyDiaperRetentionServiceHelper(new NLogLoggerFactory())
            {
                BabyDiaperBll = babyDiaperRetentionBll,
            };

            var actual = target.UpdateRetentionTest(new BabyDiaperRetentionEditViewModel());

            Assert.Equal(null, actual);
        }

        /// <summary>
        ///     Tests if Updating an existing Retention Test works
        /// </summary>
        [Fact]
        public void UpdateRetentionTestBaseTest()
        {
            var viewModel = new BabyDiaperRetentionEditViewModel
            {
                TestPerson = "Hans",
                TestValueId = 2,
                TestSheetId = 1,
                ProductionCodeTime = new TimeSpan(12, 34, 0),
                ProductionCodeDay = 123,
                DiaperWeight = 30.1,
                WeightRetentionWet = 399.8,
                Notes = new List<TestNote> { new TestNote { ErrorCodeId = 1, Id = 1, Message = "Testnote" } }
            };
            var testValueReturnedFromDb = new TestValue
            {
                TestSheetRefId = 1,
                TestValueId = 2,
                CreatedDateTime = new DateTime(2016, 1, 2),
                LastEditedDateTime = new DateTime(2016, 1, 2),
                CreatedPerson = "Fritz",
                LastEditedPerson = "Fritz",
                DayInYearOfArticleCreation = 123,
                ArticleTestType = ArticleType.BabyDiaper,
                TestValueNote = new List<TestValueNote> { new TestValueNote { ErrorRefId = 1, Message = "Testnote" } },
                BabyDiaperTestValue = new BabyDiaperTestValue
                {
                    DiaperCreatedTime = new TimeSpan(11,11,0),
                    WeightDiaperDry = 15,
                    RetentionWetWeight = 2,
                    TestType = TestTypeBabyDiaper.Retention
                }
            };
            var testSheetDataFromDb = GetTestSheetTestData();
            var productionOrderDataFromDb = GetProductionOrderTestData();

            var babyDiaperRetentionBll = MockHelperBll.GetBabyDiaperBllForSavingAndUpdating(testSheetDataFromDb, productionOrderDataFromDb, testValueReturnedFromDb);

            var target = new BabyDiaperRetentionServiceHelper(new NLogLoggerFactory())
            {
                BabyDiaperBll = babyDiaperRetentionBll,
            };

            var actual = target.UpdateRetentionTest(viewModel);

            Assert.Equal(testValueReturnedFromDb, actual);
            Assert.Equal(30.1,actual.BabyDiaperTestValue.WeightDiaperDry);
            Assert.Equal(399.8, actual.BabyDiaperTestValue.RetentionWetWeight);
            Assert.Equal("Hans",actual.LastEditedPerson);
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

            var babyDiaperRetentionBll = MockHelperBll.GetBabyDiaperBllForSavingAndUpdating(testSheetDataFromDb, productionOrderDataFromDb, null);

            var target = new BabyDiaperRetentionServiceHelper(new NLogLoggerFactory())
            {
                BabyDiaperBll = babyDiaperRetentionBll
            };

            var actual = target.UpdateRetentionAverageAndStv(1);

            var actualAvg = actual.TestValues.FirstOrDefault(tv => tv.TestValueType == TestValueType.Average && tv.BabyDiaperTestValue.TestType == TestTypeBabyDiaper.Retention);
            Assert.NotNull( actualAvg );
            Assert.Equal(0, actualAvg.BabyDiaperTestValue.WeightDiaperDry);
            Assert.Equal(0, actualAvg.BabyDiaperTestValue.RetentionWetWeight);
            Assert.Equal(0, actualAvg.BabyDiaperTestValue.RetentionAfterZentrifugeValue);
            Assert.Equal(0, actualAvg.BabyDiaperTestValue.RetentionAfterZentrifugePercent);
            Assert.Equal(0, actualAvg.BabyDiaperTestValue.SapGHoewiValue);
            Assert.Equal(RwType.Ok, actualAvg.BabyDiaperTestValue.RetentionRw);

            var actualStDev = actual.TestValues.FirstOrDefault(tv => tv.TestValueType == TestValueType.StandardDeviation && tv.BabyDiaperTestValue.TestType == TestTypeBabyDiaper.Retention);
            Assert.NotNull(actualStDev);
            Assert.Equal(0, actualStDev.BabyDiaperTestValue.WeightDiaperDry);
            Assert.Equal(0, actualStDev.BabyDiaperTestValue.RetentionWetWeight);
            Assert.Equal(0, actualStDev.BabyDiaperTestValue.RetentionAfterZentrifugeValue);
            Assert.Equal(0, actualStDev.BabyDiaperTestValue.RetentionAfterZentrifugePercent);
            Assert.Equal(0, actualStDev.BabyDiaperTestValue.SapGHoewiValue);
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
                BabyDiaperTestValue = new BabyDiaperTestValue
                {
                    TestType = TestTypeBabyDiaper.Retention,
                    WeightDiaperDry = 30,
                    RetentionWetWeight = 400,
                    RetentionAfterZentrifugeValue = 370,
                    RetentionAfterZentrifugePercent = 1100,
                    SapGHoewiValue = 10.98,
                    RetentionRw = RwType.Ok
                }
            };
            var testSheetDataFromDb = GetTestSheetTestDataWithAvgAndStDev();
            testSheetDataFromDb.TestValues.Add(onlyTestValue);

            var productionOrderDataFromDb = GetProductionOrderTestData();

            var babyDiaperRetentionBll = MockHelperBll.GetBabyDiaperBllForSavingAndUpdating(testSheetDataFromDb, productionOrderDataFromDb, null);

            var target = new BabyDiaperRetentionServiceHelper(new NLogLoggerFactory())
            {
                BabyDiaperBll = babyDiaperRetentionBll
            };

            var actual = target.UpdateRetentionAverageAndStv(1);

            var actualAvg = actual.TestValues.FirstOrDefault(tv => tv.TestValueType == TestValueType.Average && tv.BabyDiaperTestValue.TestType == TestTypeBabyDiaper.Retention);
            Assert.NotNull(actualAvg);
            Assert.Equal(onlyTestValue.BabyDiaperTestValue.WeightDiaperDry, actualAvg.BabyDiaperTestValue.WeightDiaperDry);
            Assert.Equal(onlyTestValue.BabyDiaperTestValue.RetentionWetWeight, actualAvg.BabyDiaperTestValue.RetentionWetWeight);
            Assert.Equal(onlyTestValue.BabyDiaperTestValue.RetentionAfterZentrifugeValue, actualAvg.BabyDiaperTestValue.RetentionAfterZentrifugeValue);
            Assert.Equal(onlyTestValue.BabyDiaperTestValue.RetentionAfterZentrifugePercent, actualAvg.BabyDiaperTestValue.RetentionAfterZentrifugePercent);
            Assert.Equal(onlyTestValue.BabyDiaperTestValue.SapGHoewiValue, actualAvg.BabyDiaperTestValue.SapGHoewiValue);
            Assert.Equal(onlyTestValue.BabyDiaperTestValue.RetentionRw, actualAvg.BabyDiaperTestValue.RetentionRw);
        }

        /// <summary>
        ///     Tests when there is only one test, the stdev values are the default values
        /// </summary>
        [Fact]
        public void UpdateRetentionAverageAndStvOneTestStDevTest()
        {
            var onlyTestValue = new TestValue
            {
                TestValueType = TestValueType.Single,
                ArticleTestType = ArticleType.BabyDiaper,
                BabyDiaperTestValue = new BabyDiaperTestValue
                {
                    TestType = TestTypeBabyDiaper.Retention,
                    WeightDiaperDry = 30,
                    RetentionWetWeight = 400,
                    RetentionAfterZentrifugeValue = 370,
                    RetentionAfterZentrifugePercent = 1100,
                    SapGHoewiValue = 10.98,
                    RetentionRw = RwType.Ok
                }
            };
            var testSheetDataFromDb = GetTestSheetTestDataWithAvgAndStDev();
            testSheetDataFromDb.TestValues.Add(onlyTestValue);

            var productionOrderDataFromDb = GetProductionOrderTestData();

            var babyDiaperRetentionBll = MockHelperBll.GetBabyDiaperBllForSavingAndUpdating(testSheetDataFromDb, productionOrderDataFromDb, null);

            var target = new BabyDiaperRetentionServiceHelper(new NLogLoggerFactory())
            {
                BabyDiaperBll = babyDiaperRetentionBll
            };

            var actual = target.UpdateRetentionAverageAndStv(1);

            var actualStDev = actual.TestValues.FirstOrDefault(tv => tv.TestValueType == TestValueType.StandardDeviation && tv.BabyDiaperTestValue.TestType == TestTypeBabyDiaper.Retention);
            Assert.NotNull(actualStDev);
            Assert.Equal(0, actualStDev.BabyDiaperTestValue.WeightDiaperDry);
            Assert.Equal(0, actualStDev.BabyDiaperTestValue.RetentionWetWeight);
            Assert.Equal(0, actualStDev.BabyDiaperTestValue.RetentionAfterZentrifugeValue);
            Assert.Equal(0, actualStDev.BabyDiaperTestValue.RetentionAfterZentrifugePercent);
            Assert.Equal(0, actualStDev.BabyDiaperTestValue.SapGHoewiValue);
        }

        /// <summary>
        ///     Tests when there are more than one test, the avg values are correct calculated
        /// </summary>
        [Fact]
        public void UpdateRetentionAverageAndStvTwoTestsAvgTest()
        {
            var testValue1 = new TestValue
            {
                TestValueType = TestValueType.Single,
                ArticleTestType = ArticleType.BabyDiaper,
                BabyDiaperTestValue = new BabyDiaperTestValue
                {
                    TestType = TestTypeBabyDiaper.Retention,
                    WeightDiaperDry = 30,
                    RetentionWetWeight = 400,
                    RetentionAfterZentrifugeValue = 370,
                    RetentionAfterZentrifugePercent = 1100,
                    SapGHoewiValue = 10.98,
                    RetentionRw = RwType.Ok
                }
            };
            var testValue2 = new TestValue
            {
                TestValueType = TestValueType.Single,
                ArticleTestType = ArticleType.BabyDiaper,
                BabyDiaperTestValue = new BabyDiaperTestValue
                {
                    TestType = TestTypeBabyDiaper.Retention,
                    WeightDiaperDry = 31,
                    RetentionWetWeight = 395.5,
                    RetentionAfterZentrifugeValue = 364.5,
                    RetentionAfterZentrifugePercent = 1200,
                    SapGHoewiValue = 10.3,
                    RetentionRw = RwType.Ok
                }
            };
            var testSheetDataFromDb = GetTestSheetTestDataWithAvgAndStDev();
            testSheetDataFromDb.TestValues.Add(testValue1);
            testSheetDataFromDb.TestValues.Add(testValue2);

            var productionOrderDataFromDb = GetProductionOrderTestData();

            var babyDiaperRetentionBll = MockHelperBll.GetBabyDiaperBllForSavingAndUpdating(testSheetDataFromDb, productionOrderDataFromDb, null);

            var target = new BabyDiaperRetentionServiceHelper(new NLogLoggerFactory())
            {
                BabyDiaperBll = babyDiaperRetentionBll
            };

            var actual = target.UpdateRetentionAverageAndStv(1);

            var actualAvg = actual.TestValues.FirstOrDefault(tv => tv.TestValueType == TestValueType.Average && tv.BabyDiaperTestValue.TestType == TestTypeBabyDiaper.Retention);
            Assert.NotNull(actualAvg);
            Assert.Equal(30.5, actualAvg.BabyDiaperTestValue.WeightDiaperDry);
            Assert.Equal(397.75, actualAvg.BabyDiaperTestValue.RetentionWetWeight);
            Assert.Equal(367.25, actualAvg.BabyDiaperTestValue.RetentionAfterZentrifugeValue);
            Assert.Equal(1150, actualAvg.BabyDiaperTestValue.RetentionAfterZentrifugePercent);
            Assert.Equal(10.64, actualAvg.BabyDiaperTestValue.SapGHoewiValue);
            Assert.Equal(RwType.Ok, actualAvg.BabyDiaperTestValue.RetentionRw);
        }

        /// <summary>
        ///     Tests when there are more than one test, the StDev values are correct calculated
        /// </summary>
        [Fact]
        public void UpdateRetentionAverageAndStvTwoTestsStDevTest()
        {
            var testValue1 = new TestValue
            {
                TestValueType = TestValueType.Single,
                ArticleTestType = ArticleType.BabyDiaper,
                BabyDiaperTestValue = new BabyDiaperTestValue
                {
                    TestType = TestTypeBabyDiaper.Retention,
                    WeightDiaperDry = 30,
                    RetentionWetWeight = 400,
                    RetentionAfterZentrifugeValue = 370,
                    RetentionAfterZentrifugePercent = 1100,
                    SapGHoewiValue = 10.98,
                    RetentionRw = RwType.Ok
                }
            };
            var testValue2 = new TestValue
            {
                TestValueType = TestValueType.Single,
                ArticleTestType = ArticleType.BabyDiaper,
                BabyDiaperTestValue = new BabyDiaperTestValue
                {
                    TestType = TestTypeBabyDiaper.Retention,
                    WeightDiaperDry = 31,
                    RetentionWetWeight = 395.5,
                    RetentionAfterZentrifugeValue = 364.5,
                    RetentionAfterZentrifugePercent = 1200,
                    SapGHoewiValue = 10.3,
                    RetentionRw = RwType.Ok
                }
            };
            var testSheetDataFromDb = GetTestSheetTestDataWithAvgAndStDev();
            testSheetDataFromDb.TestValues.Add(testValue1);
            testSheetDataFromDb.TestValues.Add(testValue2);

            var productionOrderDataFromDb = GetProductionOrderTestData();

            var babyDiaperRetentionBll = MockHelperBll.GetBabyDiaperBllForSavingAndUpdating(testSheetDataFromDb, productionOrderDataFromDb, null);

            var target = new BabyDiaperRetentionServiceHelper(new NLogLoggerFactory())
            {
                BabyDiaperBll = babyDiaperRetentionBll
            };

            var actual = target.UpdateRetentionAverageAndStv(1);

            var actualStDev = actual.TestValues.FirstOrDefault(tv => tv.TestValueType == TestValueType.StandardDeviation && tv.BabyDiaperTestValue.TestType == TestTypeBabyDiaper.Retention);
            Assert.NotNull(actualStDev);
            Assert.Equal(0.5, actualStDev.BabyDiaperTestValue.WeightDiaperDry);
            Assert.Equal(2.25, actualStDev.BabyDiaperTestValue.RetentionWetWeight);
            Assert.Equal(2.75, actualStDev.BabyDiaperTestValue.RetentionAfterZentrifugeValue);
            Assert.Equal(50, actualStDev.BabyDiaperTestValue.RetentionAfterZentrifugePercent);
            Assert.Equal(0.34, actualStDev.BabyDiaperTestValue.SapGHoewiValue,2);
        }

        #endregion
    }
}
