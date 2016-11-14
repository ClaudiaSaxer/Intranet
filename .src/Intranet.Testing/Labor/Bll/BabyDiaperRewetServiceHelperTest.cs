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
    ///     Class representing Tests for BabyDiaperRewetServiceHelper
    /// </summary>
    public class BabyDiaperRewetServiceHelperTest
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
            Article = new Article
            {
                Rewet140Max = 0.4,
                Rewet210Max = 0.5,
                MaxPenetrationAfter4Time = 250
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
                        TestType = TestTypeBabyDiaper.Rewet
                    }
                },
                new TestValue
                {
                    TestValueType = TestValueType.StandardDeviation,
                    ArticleTestType = ArticleType.BabyDiaper,
                    BabyDiaperTestValue = new BabyDiaperTestValue
                    {
                        TestType = TestTypeBabyDiaper.Rewet
                    }
                },
                new TestValue
                {
                    TestValueType = TestValueType.Average,
                    ArticleTestType = ArticleType.BabyDiaper,
                    BabyDiaperTestValue = new BabyDiaperTestValue
                    {
                        TestType = TestTypeBabyDiaper.RewetAndPenetrationTime
                    }
                },
                new TestValue
                {
                    TestValueType = TestValueType.StandardDeviation,
                    ArticleTestType = ArticleType.BabyDiaper,
                    BabyDiaperTestValue = new BabyDiaperTestValue
                    {
                        TestType = TestTypeBabyDiaper.RewetAndPenetrationTime
                    }
                }
            }
        };

        #endregion

        #region SaveNewRewetTest Tests

        /// <summary>
        ///     Tests if Saving a new BabyDiaperRetention Test works
        /// </summary>
        [Fact]
        public void SaveNewRewetTestBaseTest()
        {
            var viewModel = new BabyDiaperRewetEditViewModel()
            {
                TestPerson = "Hans",
                TestValueId = -1,
                TestSheetId = 1,
                ProductionCodeTime = new TimeSpan(12, 34, 0),
                ProductionCodeDay = 123,
                DiaperWeight = 30.1,
                RewetAfter140 = 0,
                RewetAfter210 = 0.1,
                StrikeThrough = 0.3,
                Distribution = 250,
                PenetrationTime1 = 1,
                PenetrationTime2 = 2,
                PenetrationTime3 = 3,
                PenetrationTime4 = 4,
                TestType = TestTypeBabyDiaper.RewetAndPenetrationTime,
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

            var babyDiaperBll = MockHelperBll.GetBabyDiaperBllForSavingAndUpdating(testSheetDataFromDb, productionOrderDataFromDb, null);
            var babyDiaperServiceHelper = MockHelperBabyDiaperServiceHelper.GetBabyDiaperServiceHelperCreateNewTestValue(testValueReturnedFromHelper);

            var target = new BabyDiaperRewetServiceHelper(new NLogLoggerFactory())
            {
                BabyDiaperBll = babyDiaperBll,
                BabyDiaperServiceHelper = babyDiaperServiceHelper
            };

            var actual = target.SaveNewRewetTest(viewModel);

            Assert.Equal(testValueReturnedFromHelper, actual);
        }

        /// <summary>
        ///     Tests if while saving the correct RwValue choosen
        /// </summary>
        [Fact]
        public void SaveNewRewetTestCalculationRwOkTest()
        {
            var viewModel = new BabyDiaperRewetEditViewModel()
            {
                TestPerson = "Hans",
                TestValueId = -1,
                TestSheetId = 1,
                ProductionCodeTime = new TimeSpan(12, 34, 0),
                ProductionCodeDay = 123,
                DiaperWeight = 30.1,
                RewetAfter140 = 0,
                RewetAfter210 = 0.1,
                StrikeThrough = 0.3,
                Distribution = 250,
                PenetrationTime1 = 1,
                PenetrationTime2 = 2,
                PenetrationTime3 = 3,
                PenetrationTime4 = 4,
                TestType = TestTypeBabyDiaper.RewetAndPenetrationTime,
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

            var babyDiaperBll = MockHelperBll.GetBabyDiaperBllForSavingAndUpdating(testSheetDataFromDb, productionOrderDataFromDb, null);
            var babyDiaperServiceHelper = MockHelperBabyDiaperServiceHelper.GetBabyDiaperServiceHelperCreateNewTestValue(testValueReturnedFromHelper);

            var target = new BabyDiaperRewetServiceHelper(new NLogLoggerFactory())
            {
                BabyDiaperBll = babyDiaperBll,
                BabyDiaperServiceHelper = babyDiaperServiceHelper
            };

            var actual = target.SaveNewRewetTest(viewModel);

            Assert.Equal(RwType.Ok, actual.BabyDiaperTestValue.Rewet140Rw);
            Assert.Equal(RwType.Ok, actual.BabyDiaperTestValue.Rewet140Rw);
            Assert.Equal(RwType.Ok, actual.BabyDiaperTestValue.PenetrationRwType);
        }

        /// <summary>
        ///     Tests if while saving the correct RwValue choosen
        /// </summary>
        [Fact]
        public void SaveNewRewetTestCalculationRwWorseTest()
        {
            var viewModel = new BabyDiaperRewetEditViewModel()
            {
                TestPerson = "Hans",
                TestValueId = -1,
                TestSheetId = 1,
                ProductionCodeTime = new TimeSpan(12, 34, 0),
                ProductionCodeDay = 123,
                DiaperWeight = 30.1,
                RewetAfter140 = 0.5,
                RewetAfter210 = 0.6,
                StrikeThrough = 0.3,
                Distribution = 250,
                PenetrationTime1 = 1,
                PenetrationTime2 = 2,
                PenetrationTime3 = 3,
                PenetrationTime4 = 260,
                TestType = TestTypeBabyDiaper.RewetAndPenetrationTime,
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

            var babyDiaperRetentionBll = MockHelperBll.GetBabyDiaperBllForSavingAndUpdating(testSheetDataFromDb, productionOrderDataFromDb, null);
            var babyDiaperServiceHelper = MockHelperBabyDiaperServiceHelper.GetBabyDiaperServiceHelperCreateNewTestValue(testValueReturnedFromHelper);

            var target = new BabyDiaperRewetServiceHelper(new NLogLoggerFactory())
            {
                BabyDiaperBll = babyDiaperRetentionBll,
                BabyDiaperServiceHelper = babyDiaperServiceHelper
            };

            var actual = target.SaveNewRewetTest(viewModel);

            Assert.Equal(RwType.Worse, actual.BabyDiaperTestValue.Rewet140Rw);
            Assert.Equal(RwType.Worse, actual.BabyDiaperTestValue.Rewet140Rw);
            Assert.Equal(RwType.Worse, actual.BabyDiaperTestValue.PenetrationRwType);
        }

        #endregion

        #region UpdateRewetTest Tests

        /// <summary>
        ///     Tests if Updating an Test returns null if old test not exist
        /// </summary>
        [Fact]
        public void UpdateRetentionTestFailTest()
        {
            var babyDiaperRetentionBll = MockHelperBll.GetBabyDiaperBllForSavingAndUpdating(null, null, null);
            var target = new BabyDiaperRewetServiceHelper(new NLogLoggerFactory())
            {
                BabyDiaperBll = babyDiaperRetentionBll,
            };

            var actual = target.UpdateRewetTest(new BabyDiaperRewetEditViewModel());

            Assert.Equal(null, actual);
        }

        /// <summary>
        ///     Tests if Updating an existing BabyDiaperRetention Test works
        /// </summary>
        [Fact]
        public void UpdateRewetTestBaseTest()
        {
            var viewModel = new BabyDiaperRewetEditViewModel()
            {
                TestPerson = "Hans",
                TestValueId = -1,
                TestSheetId = 1,
                ProductionCodeTime = new TimeSpan(12, 34, 0),
                ProductionCodeDay = 123,
                DiaperWeight = 30.1,
                RewetAfter140 = 0,
                RewetAfter210 = 0.1,
                StrikeThrough = 0.3,
                Distribution = 250,
                PenetrationTime1 = 1,
                PenetrationTime2 = 2,
                PenetrationTime3 = 3,
                PenetrationTime4 = 4,
                TestType = TestTypeBabyDiaper.RewetAndPenetrationTime,
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
                    DiaperCreatedTime = new TimeSpan(11, 11, 0),
                    WeightDiaperDry = 15,
                    TestType = TestTypeBabyDiaper.RewetAndPenetrationTime
                }
            };
            var testSheetDataFromDb = GetTestSheetTestData();
            var productionOrderDataFromDb = GetProductionOrderTestData();

            var babyDiaperBll = MockHelperBll.GetBabyDiaperBllForSavingAndUpdating(testSheetDataFromDb, productionOrderDataFromDb, testValueReturnedFromDb);

            var target = new BabyDiaperRewetServiceHelper(new NLogLoggerFactory())
            {
                BabyDiaperBll = babyDiaperBll,
            };

            var actual = target.UpdateRewetTest(viewModel);

            Assert.Equal(testValueReturnedFromDb, actual);
            Assert.Equal(30.1, actual.BabyDiaperTestValue.WeightDiaperDry);
            Assert.Equal(0.3, actual.BabyDiaperTestValue.StrikeTroughValue);
            Assert.Equal("Hans", actual.LastEditedPerson);
            Assert.Equal("Fritz", actual.CreatedPerson);
            Assert.NotEqual(new DateTime(2016, 1, 2), actual.LastEditedDateTime);
        }

        #endregion

        #region UpdateRewetAverageAndStv Tests

        /// <summary>
        ///     Tests when there are no tests, the avg and dev values are default
        /// </summary>
        [Fact]
        public void UpdateRewetAverageAndStvNoTestsTest()
        {
            var testSheetDataFromDb = GetTestSheetTestDataWithAvgAndStDev();
            var productionOrderDataFromDb = GetProductionOrderTestData();

            var babyDiaperBll = MockHelperBll.GetBabyDiaperBllForSavingAndUpdating(testSheetDataFromDb, productionOrderDataFromDb, null);

            var target = new BabyDiaperRewetServiceHelper(new NLogLoggerFactory())
            {
                BabyDiaperBll = babyDiaperBll
            };

            var actual = target.UpdateRewetAverageAndStv(1);

            var actualRewetAvg = actual.TestValues.FirstOrDefault(tv => tv.TestValueType == TestValueType.Average && tv.BabyDiaperTestValue.TestType == TestTypeBabyDiaper.Rewet);
            Assert.NotNull(actualRewetAvg);
            Assert.Equal(0, actualRewetAvg.BabyDiaperTestValue.WeightDiaperDry);
            Assert.Equal(0, actualRewetAvg.BabyDiaperTestValue.Rewet140Value);
            Assert.Equal(0, actualRewetAvg.BabyDiaperTestValue.Rewet210Value);
            Assert.Equal(0, actualRewetAvg.BabyDiaperTestValue.StrikeTroughValue);
            Assert.Equal(0, actualRewetAvg.BabyDiaperTestValue.DistributionOfTheStrikeTrough);
            Assert.Equal(RwType.Ok, actualRewetAvg.BabyDiaperTestValue.Rewet140Rw);
            Assert.Equal(RwType.Ok, actualRewetAvg.BabyDiaperTestValue.Rewet210Rw);

            var actualRewetStDev = actual.TestValues.FirstOrDefault(tv => tv.TestValueType == TestValueType.StandardDeviation && tv.BabyDiaperTestValue.TestType == TestTypeBabyDiaper.Rewet);
            Assert.NotNull(actualRewetStDev);
            Assert.Equal(0, actualRewetStDev.BabyDiaperTestValue.WeightDiaperDry);
            Assert.Equal(0, actualRewetStDev.BabyDiaperTestValue.Rewet140Value);
            Assert.Equal(0, actualRewetStDev.BabyDiaperTestValue.Rewet210Value);
            Assert.Equal(0, actualRewetStDev.BabyDiaperTestValue.StrikeTroughValue);
            Assert.Equal(0, actualRewetStDev.BabyDiaperTestValue.DistributionOfTheStrikeTrough);

            var actualRewetAndPenetrationAvg = actual.TestValues.FirstOrDefault(tv => tv.TestValueType == TestValueType.Average && tv.BabyDiaperTestValue.TestType == TestTypeBabyDiaper.RewetAndPenetrationTime);
            Assert.NotNull(actualRewetAndPenetrationAvg);
            Assert.Equal(0, actualRewetAndPenetrationAvg.BabyDiaperTestValue.WeightDiaperDry);
            Assert.Equal(0, actualRewetAndPenetrationAvg.BabyDiaperTestValue.PenetrationTimeAdditionFirst);
            Assert.Equal(0, actualRewetAndPenetrationAvg.BabyDiaperTestValue.PenetrationTimeAdditionSecond);
            Assert.Equal(0, actualRewetAndPenetrationAvg.BabyDiaperTestValue.PenetrationTimeAdditionThird);
            Assert.Equal(0, actualRewetAndPenetrationAvg.BabyDiaperTestValue.PenetrationTimeAdditionFourth);
            Assert.Equal(RwType.Ok, actualRewetAndPenetrationAvg.BabyDiaperTestValue.PenetrationRwType);

            var actualRewetAndPenetrationStDev = actual.TestValues.FirstOrDefault(tv => tv.TestValueType == TestValueType.StandardDeviation && tv.BabyDiaperTestValue.TestType == TestTypeBabyDiaper.RewetAndPenetrationTime);
            Assert.NotNull(actualRewetAndPenetrationStDev);
            Assert.Equal(0, actualRewetAndPenetrationStDev.BabyDiaperTestValue.WeightDiaperDry);
            Assert.Equal(0, actualRewetAndPenetrationStDev.BabyDiaperTestValue.PenetrationTimeAdditionFirst);
            Assert.Equal(0, actualRewetAndPenetrationStDev.BabyDiaperTestValue.PenetrationTimeAdditionSecond);
            Assert.Equal(0, actualRewetAndPenetrationStDev.BabyDiaperTestValue.PenetrationTimeAdditionThird);
            Assert.Equal(0, actualRewetAndPenetrationStDev.BabyDiaperTestValue.PenetrationTimeAdditionFourth);
        }

        /// <summary>
        ///     Tests when there is only one test, the avg values are equal the test values
        /// </summary>
        [Fact]
        public void UpdateRewetAverageAndStvOneTestAvgTest()
        {
            var onlyTestValue = new TestValue
            {
                TestValueType = TestValueType.Single,
                ArticleTestType = ArticleType.BabyDiaper,
                BabyDiaperTestValue = new BabyDiaperTestValue
                {
                    TestType = TestTypeBabyDiaper.RewetAndPenetrationTime,
                    WeightDiaperDry = 30.1,
                    Rewet140Value = 0,
                    Rewet210Value = 0.1,
                    StrikeTroughValue = 0.3,
                    DistributionOfTheStrikeTrough = 250,
                    PenetrationTimeAdditionFirst = 1,
                    PenetrationTimeAdditionSecond = 2,
                    PenetrationTimeAdditionThird = 3,
                    PenetrationTimeAdditionFourth = 4,
                    Rewet140Rw = RwType.Ok,
                    Rewet210Rw = RwType.Ok,
                    PenetrationRwType = RwType.Ok
                }
            };
            var testSheetDataFromDb = GetTestSheetTestDataWithAvgAndStDev();
            testSheetDataFromDb.TestValues.Add(onlyTestValue);
            var productionOrderDataFromDb = GetProductionOrderTestData();

            var babyDiaperBll = MockHelperBll.GetBabyDiaperBllForSavingAndUpdating(testSheetDataFromDb, productionOrderDataFromDb, null);

            var target = new BabyDiaperRewetServiceHelper(new NLogLoggerFactory())
            {
                BabyDiaperBll = babyDiaperBll
            };

            var actual = target.UpdateRewetAverageAndStv(1);

            var actualRewetAvg = actual.TestValues.FirstOrDefault(tv => tv.TestValueType == TestValueType.Average && tv.BabyDiaperTestValue.TestType == TestTypeBabyDiaper.Rewet);
            Assert.NotNull(actualRewetAvg);
            Assert.Equal(onlyTestValue.BabyDiaperTestValue.WeightDiaperDry, actualRewetAvg.BabyDiaperTestValue.WeightDiaperDry);
            Assert.Equal(onlyTestValue.BabyDiaperTestValue.Rewet140Value, actualRewetAvg.BabyDiaperTestValue.Rewet140Value);
            Assert.Equal(onlyTestValue.BabyDiaperTestValue.Rewet210Value, actualRewetAvg.BabyDiaperTestValue.Rewet210Value);
            Assert.Equal(onlyTestValue.BabyDiaperTestValue.StrikeTroughValue, actualRewetAvg.BabyDiaperTestValue.StrikeTroughValue);
            Assert.Equal(onlyTestValue.BabyDiaperTestValue.DistributionOfTheStrikeTrough, actualRewetAvg.BabyDiaperTestValue.DistributionOfTheStrikeTrough);
            Assert.Equal(onlyTestValue.BabyDiaperTestValue.Rewet140Rw, actualRewetAvg.BabyDiaperTestValue.Rewet140Rw);
            Assert.Equal(onlyTestValue.BabyDiaperTestValue.Rewet210Rw, actualRewetAvg.BabyDiaperTestValue.Rewet210Rw);

            var actualRewetAndPenetrationStDev = actual.TestValues.FirstOrDefault(tv => tv.TestValueType == TestValueType.Average && tv.BabyDiaperTestValue.TestType == TestTypeBabyDiaper.RewetAndPenetrationTime);
            Assert.NotNull(actualRewetAndPenetrationStDev);
            Assert.Equal(onlyTestValue.BabyDiaperTestValue.WeightDiaperDry, actualRewetAndPenetrationStDev.BabyDiaperTestValue.WeightDiaperDry);
            Assert.Equal(onlyTestValue.BabyDiaperTestValue.PenetrationTimeAdditionFirst, actualRewetAndPenetrationStDev.BabyDiaperTestValue.PenetrationTimeAdditionFirst);
            Assert.Equal(onlyTestValue.BabyDiaperTestValue.PenetrationTimeAdditionSecond, actualRewetAndPenetrationStDev.BabyDiaperTestValue.PenetrationTimeAdditionSecond);
            Assert.Equal(onlyTestValue.BabyDiaperTestValue.PenetrationTimeAdditionThird, actualRewetAndPenetrationStDev.BabyDiaperTestValue.PenetrationTimeAdditionThird);
            Assert.Equal(onlyTestValue.BabyDiaperTestValue.PenetrationTimeAdditionFourth, actualRewetAndPenetrationStDev.BabyDiaperTestValue.PenetrationTimeAdditionFourth);
            Assert.Equal(onlyTestValue.BabyDiaperTestValue.PenetrationRwType, actualRewetAndPenetrationStDev.BabyDiaperTestValue.PenetrationRwType);
        }

        /// <summary>
        ///     Tests when there is only one test, the stdev values are the default values
        /// </summary>
        [Fact]
        public void UpdateRewetAverageAndStvOneTestStDevTest()
        {
            var onlyTestValue = new TestValue
            {
                TestValueType = TestValueType.Single,
                ArticleTestType = ArticleType.BabyDiaper,
                BabyDiaperTestValue = new BabyDiaperTestValue
                {
                    TestType = TestTypeBabyDiaper.RewetAndPenetrationTime,
                    WeightDiaperDry = 30.1,
                    Rewet140Value = 0,
                    Rewet210Value = 0.1,
                    StrikeTroughValue = 0.3,
                    DistributionOfTheStrikeTrough = 250,
                    PenetrationTimeAdditionFirst = 1,
                    PenetrationTimeAdditionSecond = 2,
                    PenetrationTimeAdditionThird = 3,
                    PenetrationTimeAdditionFourth = 4,
                    Rewet140Rw = RwType.Ok,
                    Rewet210Rw = RwType.Ok,
                    PenetrationRwType = RwType.Ok
                }
            };
            var testSheetDataFromDb = GetTestSheetTestDataWithAvgAndStDev();
            testSheetDataFromDb.TestValues.Add(onlyTestValue);
            var productionOrderDataFromDb = GetProductionOrderTestData();

            var babyDiaperBll = MockHelperBll.GetBabyDiaperBllForSavingAndUpdating(testSheetDataFromDb, productionOrderDataFromDb, null);

            var target = new BabyDiaperRewetServiceHelper(new NLogLoggerFactory())
            {
                BabyDiaperBll = babyDiaperBll
            };

            var actual = target.UpdateRewetAverageAndStv(1);

            var actualRewetStDev = actual.TestValues.FirstOrDefault(tv => tv.TestValueType == TestValueType.StandardDeviation && tv.BabyDiaperTestValue.TestType == TestTypeBabyDiaper.Rewet);
            Assert.NotNull(actualRewetStDev);
            Assert.Equal(0, actualRewetStDev.BabyDiaperTestValue.WeightDiaperDry);
            Assert.Equal(0, actualRewetStDev.BabyDiaperTestValue.Rewet140Value);
            Assert.Equal(0, actualRewetStDev.BabyDiaperTestValue.Rewet210Value);
            Assert.Equal(0, actualRewetStDev.BabyDiaperTestValue.StrikeTroughValue);
            Assert.Equal(0, actualRewetStDev.BabyDiaperTestValue.DistributionOfTheStrikeTrough);

            var actualRewetAndPenetrationAvg = actual.TestValues.FirstOrDefault(tv => tv.TestValueType == TestValueType.StandardDeviation && tv.BabyDiaperTestValue.TestType == TestTypeBabyDiaper.RewetAndPenetrationTime);
            Assert.NotNull(actualRewetAndPenetrationAvg);
            Assert.Equal(0, actualRewetAndPenetrationAvg.BabyDiaperTestValue.WeightDiaperDry);
            Assert.Equal(0, actualRewetAndPenetrationAvg.BabyDiaperTestValue.PenetrationTimeAdditionFirst);
            Assert.Equal(0, actualRewetAndPenetrationAvg.BabyDiaperTestValue.PenetrationTimeAdditionSecond);
            Assert.Equal(0, actualRewetAndPenetrationAvg.BabyDiaperTestValue.PenetrationTimeAdditionThird);
            Assert.Equal(0, actualRewetAndPenetrationAvg.BabyDiaperTestValue.PenetrationTimeAdditionFourth);
        }

        #endregion
    }
}
