using System;
using System.Collections.Generic;
using System.Linq;
using Intranet.Common;
using Intranet.Labor.Model.labor;
using Intranet.Labor.TestEnvironment;
using Intranet.Labor.ViewModel;
using Xunit;

namespace Intranet.Labor.Bll.Test
{
    /// <summary>
    ///     Class representing Tests for BabyDiaperRetentionService
    /// </summary>
    public class BabyDiaperRewetServiceTest
    {
        /// <summary>
        ///     Tests if it get a new correct viewModel if the testSheet exists in the db
        /// </summary>
        [Fact]
        public void GetNewBabyDiaperRewetEditViewModelFromExistingTestSheetTest()
        {
            var testSheetInDb = new TestSheet
            {
                TestSheetId = 1,
                MachineNr = "M11",
                CreatedDateTime = new DateTime(2016, 5, 5)
            };
            var babyDiaperRetentionBll =
                MockHelperBll.GetBabyDiaperBll(
                    testSheetInDb
                );

            var babyDiaperServiceHelper = MockHelperBabyDiaperServiceHelper.GetBabyDiaperServiceHelper("IT/11/16/");

            var target = new BabyDiaperRewetService(new NLogLoggerFactory())
            {
                BabyDiaperBll = babyDiaperRetentionBll,
                BabyDiaperServiceHelper = babyDiaperServiceHelper
            };

            var actual = target.GetNewBabyDiaperRewetEditViewModel(1);

            Assert.Equal(testSheetInDb.TestSheetId, actual.TestSheetId);
            Assert.Equal(-1, actual.TestValueId);
            Assert.Equal("IT/11/16/", actual.ProductionCode);
        }

        /// <summary>
        ///     Tests if it get null if the testSheet doesnt exist in the db
        /// </summary>
        [Fact]
        public void GetNewBabyDiaperRewetEditViewModelFromNotExistingTestSheetTest()
        {
            var babyDiaperRetentionBll =
                MockHelperBll.GetBabyDiaperBll(
                    new TestSheet { TestSheetId = 1 }
                );

            var target = new BabyDiaperRewetService(new NLogLoggerFactory())
            {
                BabyDiaperBll = babyDiaperRetentionBll
            };

            var actual = target.GetNewBabyDiaperRewetEditViewModel(2);

            Assert.Equal(null, actual);
        }

        /// <summary>
        ///     Tests if it get null if the testValue doesnt exist in the db
        /// </summary>
        [Fact]
        public void GetBabyDiaperRewetEditViewModelFromNotExistingTestValueTest()
        {
            var listOfTestValues = new List<TestValue>
            {
                new TestValue { TestValueId = 1, TestSheetRefId = 1, BabyDiaperTestValue = new BabyDiaperTestValue()}
            };
            var testSheetInDb = new TestSheet
            {
                TestSheetId = 1,
                MachineNr = "M11",
                CreatedDateTime = new DateTime(2016, 5, 5),
                TestValues = listOfTestValues
            };
            foreach (var testValue in listOfTestValues)
                testValue.TestSheet = testSheetInDb;

            var babyDiaperRetentionBll =
                MockHelperBll.GetBabyDiaperBll(
                    testSheetInDb
                );

            var target = new BabyDiaperRewetService(new NLogLoggerFactory())
            {
                BabyDiaperBll = babyDiaperRetentionBll
            };

            var actual = target.GetBabyDiaperRewetEditViewModel(2);

            Assert.Equal(null, actual);
        }

        /// <summary>
        ///     Tests if it get null if the testsheet for the testvalue doesnt exist in the db
        /// </summary>
        [Fact]
        public void GetBabyDiaperRewetEditViewModelWithNotExistingTestSheetTest()
        {
            var listOfTestValues = new List<TestValue>
            {
                new TestValue { TestValueId = 1, TestSheetRefId = 1, BabyDiaperTestValue = new BabyDiaperTestValue()}
            };
            var testSheetInDb = new TestSheet
            {
                TestSheetId = 1,
                MachineNr = "M11",
                CreatedDateTime = new DateTime(2016, 5, 5),
                TestValues = listOfTestValues
            };

            var babyDiaperRetentionBll =
                MockHelperBll.GetBabyDiaperBll(
                    testSheetInDb
                );

            var target = new BabyDiaperRewetService(new NLogLoggerFactory())
            {
                BabyDiaperBll = babyDiaperRetentionBll
            };

            var actual = target.GetBabyDiaperRewetEditViewModel(1);

            Assert.Equal(null, actual);
        }

        /// <summary>
        ///     Tests if it get null if the BabyDiaperTestValue for the testvalue doesnt exist in the db
        /// </summary>
        [Fact]
        public void GetBabyDiaperRewetEditViewModelWithNoBabyDiaperTestVauleTest()
        {
            var listOfTestValues = new List<TestValue>
            {
                new TestValue { TestValueId = 1, TestSheetRefId = 1}
            };
            var testSheetInDb = new TestSheet
            {
                TestSheetId = 1,
                MachineNr = "M11",
                CreatedDateTime = new DateTime(2016, 5, 5),
                TestValues = listOfTestValues
            };
            foreach (var testValue in listOfTestValues)
                testValue.TestSheet = testSheetInDb;

            var babyDiaperRetentionBll =
                MockHelperBll.GetBabyDiaperBll(
                    testSheetInDb
                );

            var target = new BabyDiaperRewetService(new NLogLoggerFactory())
            {
                BabyDiaperBll = babyDiaperRetentionBll
            };

            var actual = target.GetBabyDiaperRewetEditViewModel(1);

            Assert.Equal(null, actual);
        }

        /// <summary>
        ///     Tests if it get a correct viewModel if everything for the testvalue exists in the db for rewettest
        /// </summary>
        [Fact]
        public void GetBabyDiaperRewetEditViewModelRewetOnlyTest()
        {
            var listOfTestValues = new List<TestValue>
            {
                new TestValue
                {
                    TestValueId = 1,
                    TestSheetRefId = 1,
                    LastEditedPerson = "Hans",
                    DayInYearOfArticleCreation = 123,
                    BabyDiaperTestValue = new BabyDiaperTestValue {DiaperCreatedTime = new TimeSpan(5,10,0), WeightDiaperDry = 32.2,Rewet140Value = 0.01,Rewet210Value = 0.1,StrikeTroughValue = 0.3,DistributionOfTheStrikeTrough = 250, TestType = TestTypeBabyDiaper.Rewet}
                }
            };
            var testSheetInDb = new TestSheet
            {
                TestSheetId = 1,
                MachineNr = "M11",
                CreatedDateTime = new DateTime(2016, 5, 5),
                TestValues = listOfTestValues
            };
            foreach (var testValue in listOfTestValues)
                testValue.TestSheet = testSheetInDb;

            var babyDiaperRetentionBll =
                MockHelperBll.GetBabyDiaperBll(
                    testSheetInDb
                );

            var babyDiaperServiceHelper = MockHelperBabyDiaperServiceHelper.GetBabyDiaperServiceHelper("IT/11/16/");

            var target = new BabyDiaperRewetService(new NLogLoggerFactory())
            {
                BabyDiaperBll = babyDiaperRetentionBll,
                BabyDiaperServiceHelper = babyDiaperServiceHelper
            };

            var actual = target.GetBabyDiaperRewetEditViewModel(1);

            Assert.Equal(testSheetInDb.TestSheetId, actual.TestSheetId);
            Assert.Equal(1, actual.TestValueId);
            Assert.Equal("IT/11/16/", actual.ProductionCode);
            Assert.Equal("Hans", actual.TestPerson);
            Assert.Equal(123, actual.ProductionCodeDay);
            Assert.Equal(new TimeSpan(5, 10, 0), actual.ProductionCodeTime);
            Assert.Equal(32.2, actual.DiaperWeight);
            Assert.Equal(0.01, actual.RewetAfter140);
            Assert.Equal(0.1, actual.RewetAfter210);
            Assert.Equal(0.3, actual.StrikeThrough);
            Assert.Equal(250, actual.Distribution);
            Assert.Equal(TestTypeBabyDiaper.Rewet, actual.TestType);
            Assert.Equal(2, actual.NoteCodes.ToList().Count);
        }

        /// <summary>
        ///     Tests if it get a correct viewModel if everything for the testvalue exists in the db for rewet and penetration tests
        /// </summary>
        [Fact]
        public void GetBabyDiaperRewetEditViewModelRewetAndPenetrationTest()
        {
            var listOfTestValues = new List<TestValue>
            {
                new TestValue
                {
                    TestValueId = 1,
                    TestSheetRefId = 1,
                    LastEditedPerson = "Hans",
                    DayInYearOfArticleCreation = 123,
                    BabyDiaperTestValue = new BabyDiaperTestValue {DiaperCreatedTime = new TimeSpan(5,10,0), WeightDiaperDry = 32.2,Rewet140Value = 0.01,Rewet210Value = 0.1,StrikeTroughValue = 0.3,DistributionOfTheStrikeTrough = 250, PenetrationTimeAdditionFirst = 1, PenetrationTimeAdditionSecond = 2, PenetrationTimeAdditionThird = 3, PenetrationTimeAdditionFourth = 4,TestType = TestTypeBabyDiaper.RewetAndPenetrationTime}
                }
            };
            var testSheetInDb = new TestSheet
            {
                TestSheetId = 1,
                MachineNr = "M11",
                CreatedDateTime = new DateTime(2016, 5, 5),
                TestValues = listOfTestValues
            };
            foreach (var testValue in listOfTestValues)
                testValue.TestSheet = testSheetInDb;

            var babyDiaperRetentionBll =
                MockHelperBll.GetBabyDiaperBll(
                    testSheetInDb
                );

            var babyDiaperServiceHelper = MockHelperBabyDiaperServiceHelper.GetBabyDiaperServiceHelper("IT/11/16/");

            var target = new BabyDiaperRewetService(new NLogLoggerFactory())
            {
                BabyDiaperBll = babyDiaperRetentionBll,
                BabyDiaperServiceHelper = babyDiaperServiceHelper
            };

            var actual = target.GetBabyDiaperRewetEditViewModel(1);

            Assert.Equal(testSheetInDb.TestSheetId, actual.TestSheetId);
            Assert.Equal(1, actual.TestValueId);
            Assert.Equal("IT/11/16/", actual.ProductionCode);
            Assert.Equal("Hans", actual.TestPerson);
            Assert.Equal(123, actual.ProductionCodeDay);
            Assert.Equal(new TimeSpan(5, 10, 0), actual.ProductionCodeTime);
            Assert.Equal(32.2, actual.DiaperWeight);
            Assert.Equal(0.01, actual.RewetAfter140);
            Assert.Equal(0.1, actual.RewetAfter210);
            Assert.Equal(0.3, actual.StrikeThrough);
            Assert.Equal(250, actual.Distribution);
            Assert.Equal(TestTypeBabyDiaper.RewetAndPenetrationTime, actual.TestType);
            Assert.Equal(1, actual.PenetrationTime1);
            Assert.Equal(2, actual.PenetrationTime2);
            Assert.Equal(3, actual.PenetrationTime3);
            Assert.Equal(4, actual.PenetrationTime4);
            Assert.Equal(2, actual.NoteCodes.ToList().Count);
        }
    }
}
