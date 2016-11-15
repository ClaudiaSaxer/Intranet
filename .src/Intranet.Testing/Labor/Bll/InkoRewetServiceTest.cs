using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Intranet.Common;
using Intranet.Labor.Model;
using Intranet.Labor.Model.labor;
using Intranet.Labor.TestEnvironment;
using Intranet.Labor.ViewModel;
using Xunit;

namespace Intranet.Labor.Bll.Test
{
    /// <summary>
    ///     Class representing Tests for InkoRewetService
    /// </summary>
    public class InkoRewetServiceTest
    {
        /// <summary>
        ///     Tests if it get a new correct viewModel if the testSheet exists in the db
        /// </summary>
        [Fact]
        public void GetNewInkoRewetEditViewModelFromExistingTestSheetTest()
        {
            var testSheetInDb = new TestSheet
            {
                TestSheetId = 2,
                MachineNr = "M49",
                CreatedDateTime = new DateTime(2016, 5, 5),
                ArticleType = ArticleType.IncontinencePad
            };
            var testBll =
                MockHelperBll.GetTestBll(
                    testSheetInDb
                );

            var testServiceHelper = MockHelperTestServiceHelper.GetTestServiceHelper("IT/11/16/");

            var target = new InkoRewetService(new NLogLoggerFactory())
            {
                TestBll = testBll,
                TestServiceHelper = testServiceHelper
            };

            var actual = target.GetNewInkoRewetEditViewModel(2);

            Assert.Equal(testSheetInDb.TestSheetId, actual.TestSheetId);
            Assert.Equal(-1, actual.TestValueId);
            Assert.Equal("IT/11/16/", actual.ProductionCode);
        }

        /// <summary>
        ///     Tests if it get null if the testSheet doesnt exist in the db
        /// </summary>
        [Fact]
        public void GetNewInkoRewetEditViewModelFromNotExistingTestSheetTest()
        {
            var testBll =
                MockHelperBll.GetTestBll(
                    new TestSheet { TestSheetId = 1 }
                );

            var target = new InkoRewetService(new NLogLoggerFactory())
            {
                TestBll = testBll
            };

            var actual = target.GetNewInkoRewetEditViewModel(2);

            Assert.Equal(null, actual);
        }

        /// <summary>
        ///     Tests if it get null if the testValue doesnt exist in the db
        /// </summary>
        [Fact]
        public void GetInkoRewetEditViewModelFromNotExistingTestValueTest()
        {
            var listOfTestValues = new List<TestValue>
            {
                new TestValue { TestValueId = 1, TestSheetRefId = 1, IncontinencePadTestValue = new IncontinencePadTestValue(), ArticleTestType = ArticleType.IncontinencePad}
            };
            var testSheetInDb = new TestSheet
            {
                TestSheetId = 2,
                MachineNr = "M49",
                CreatedDateTime = new DateTime(2016, 5, 5),
                TestValues = listOfTestValues
            };
            foreach (var testValue in listOfTestValues)
                testValue.TestSheet = testSheetInDb;

            var testBll =
                MockHelperBll.GetTestBll(
                    testSheetInDb
                );

            var target = new InkoRewetService(new NLogLoggerFactory())
            {
                TestBll = testBll
            };

            var actual = target.GetInkoRewetEditViewModel(2);

            Assert.Equal(null, actual);
        }

        /// <summary>
        ///     Tests if it get null if the testsheet for the testvalue doesnt exist in the db
        /// </summary>
        [Fact]
        public void GetInkoRewetEditViewModelWithNotExistingTestSheetTest()
        {
            var listOfTestValues = new List<TestValue>
            {
                new TestValue { TestValueId = 1, TestSheetRefId = 1, IncontinencePadTestValue = new IncontinencePadTestValue(), ArticleTestType = ArticleType.IncontinencePad}
            };
            var testSheetInDb = new TestSheet
            {
                TestSheetId = 2,
                MachineNr = "M11",
                CreatedDateTime = new DateTime(2016, 5, 5),
                TestValues = listOfTestValues
            };

            var testBll =
                MockHelperBll.GetTestBll(
                    testSheetInDb
                );

            var target = new InkoRewetService(new NLogLoggerFactory())
            {
                TestBll = testBll
            };

            var actual = target.GetInkoRewetEditViewModel(1);

            Assert.Equal(null, actual);
        }

        /// <summary>
        ///     Tests if it get null if the IncontinenceTestValue for the testvalue doesnt exist in the db
        /// </summary>
        [Fact]
        public void GetInkoRewetEditViewModelWithNoBabyDiaperTestVauleTest()
        {
            var listOfTestValues = new List<TestValue>
            {
                new TestValue { TestValueId = 1, TestSheetRefId = 1}
            };
            var testSheetInDb = new TestSheet
            {
                TestSheetId = 1,
                MachineNr = "M49",
                CreatedDateTime = new DateTime(2016, 5, 5),
                TestValues = listOfTestValues
            };
            foreach (var testValue in listOfTestValues)
                testValue.TestSheet = testSheetInDb;

            var testBll =
                MockHelperBll.GetTestBll(
                    testSheetInDb
                );

            var target = new InkoRewetService(new NLogLoggerFactory())
            {
                TestBll = testBll
            };

            var actual = target.GetInkoRewetEditViewModel(1);

            Assert.Equal(null, actual);
        }

        /// <summary>
        ///     Tests if it get a correct viewModel if everything for the testvalue exists in the db for inkorewettest
        /// </summary>
        [Fact]
        public void GetInkoRewetEditViewModelRewetOnlyTest()
        {
            var listOfTestValues = new List<TestValue>
            {
                new TestValue
                {
                    TestValueId = 1,
                    TestSheetRefId = 1,
                    LastEditedPerson = "Hans",
                    DayInYearOfArticleCreation = 123,
                    IncontinencePadTestValue = new IncontinencePadTestValue {IncontinencePadTime = new TimeSpan(5,10,0), RewetFreeDryValue = 20, RewetFreeWetValue = 20.5, RewetFreeDifference = 0.5,RewetFreeRw = RwType.Ok, TestType = TestTypeIncontinencePad.RewetFree}
                }
            };
            var testSheetInDb = new TestSheet
            {
                TestSheetId = 1,
                MachineNr = "M49",
                CreatedDateTime = new DateTime(2016, 5, 5),
                TestValues = listOfTestValues
            };
            foreach (var testValue in listOfTestValues)
                testValue.TestSheet = testSheetInDb;

            var testBll =
                MockHelperBll.GetTestBll(
                    testSheetInDb
                );

            var testServiceHelper = MockHelperTestServiceHelper.GetTestServiceHelper("IT/49/16/");

            var target = new InkoRewetService(new NLogLoggerFactory())
            {
                TestBll = testBll,
                TestServiceHelper = testServiceHelper
            };

            var actual = target.GetInkoRewetEditViewModel(1);

            Assert.Equal(testSheetInDb.TestSheetId, actual.TestSheetId);
            Assert.Equal(1, actual.TestValueId);
            Assert.Equal("IT/49/16/", actual.ProductionCode);
            Assert.Equal("Hans", actual.TestPerson);
            Assert.Equal(123, actual.ProductionCodeDay);
            Assert.Equal(new TimeSpan(5, 10, 0), actual.ProductionCodeTime);
            Assert.Equal(20, actual.FPDry);
            Assert.Equal(20.5, actual.FPWet);
            Assert.Equal(2, actual.NoteCodes.ToList().Count);
        }

        /// <summary>
        ///     Test the success of saving
        /// </summary>
        [Fact]
        public void SaveSuccessTest()
        {
            var testValue = new TestValue();

            var inkoRewetServiceHelper =
                MockHelperTestServiceHelper.GetInkoRewetServiceHelper(
                    testValue
                );

            var target = new InkoRewetService(new NLogLoggerFactory())
            {
                InkoRewetServiceHelper = inkoRewetServiceHelper
            };

            var actual = target.Save(new InkoRewetEditViewModel());
            Assert.Equal(testValue, actual);
        }

        /// <summary>
        ///     Test the failure of saving
        /// </summary>
        [Fact]
        public void SaveFailTest()
        {
            var testValue = new TestValue();

            var inkoRewetServiceHelper =
                MockHelperTestServiceHelper.GetInkoRewetServiceHelper(
                    testValue
                );

            var target = new InkoRewetService(new NLogLoggerFactory())
            {
                InkoRewetServiceHelper = inkoRewetServiceHelper
            };

            var actual = target.Save(null);
            Assert.Equal(null, actual);
        }

        /// <summary>
        ///     Test Delete
        /// </summary>
        [Fact]
        public void DeleteTest()
        {
            var deletedTestValue = new TestValue { TestValueId = 1 };

            var inkoRewetServiceHelper =
                MockHelperTestServiceHelper.GetInkoRewetServiceHelper(
                    null
                );
            var testBll =
                MockHelperBll.GetBabyDiaperBllForDelete(
                    deletedTestValue
                );

            var target = new InkoRewetService(new NLogLoggerFactory())
            {
                InkoRewetServiceHelper = inkoRewetServiceHelper,
                TestBll = testBll
            };

            var actual = target.Delete(1);
            Assert.Equal(1, actual.TestValueId);
        }
    }
}
