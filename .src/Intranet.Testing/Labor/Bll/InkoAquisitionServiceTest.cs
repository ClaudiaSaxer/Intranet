#region Usings

using System;
using System.Collections.Generic;
using System.Linq;
using Intranet.Common;
using Intranet.Labor.Model;
using Intranet.Labor.Model.labor;
using Intranet.Labor.TestEnvironment;
using Intranet.Labor.ViewModel;
using Xunit;

#endregion

namespace Intranet.Labor.Bll.Test
{
    /// <summary>
    ///     Class representing Tests for InkoAquisitionService
    /// </summary>
    public class InkoAquisitionServiceTest
    {
        /// <summary>
        ///     Test Delete
        /// </summary>
        [Fact]
        public void DeleteTest()
        {
            var deletedTestValue = new TestValue { TestValueId = 1 };

            var inkoAquisitionServiceHelper =
                MockHelperTestServiceHelper.GetInkoAquisitionServiceHelper(
                    null
                );
            var testBll =
                MockHelperBll.GetBabyDiaperBllForDelete(
                    deletedTestValue
                );

            var target = new InkoAquisitionService( new NLogLoggerFactory() )
            {
                InkoAquisitionServiceHelper = inkoAquisitionServiceHelper,
                TestBll = testBll
            };

            var actual = target.Delete( 1 );
            Assert.Equal( 1, actual.TestValueId );
        }

        /// <summary>
        ///     Tests if it get null if the testValue doesnt exist in the db
        /// </summary>
        [Fact]
        public void GetInkoAquisitionEditViewModelFromNotExistingTestValueTest()
        {
            var listOfTestValues = new List<TestValue>
            {
                new TestValue { TestValueId = 1, TestSheetRefId = 1, IncontinencePadTestValue = new IncontinencePadTestValue(), ArticleTestType = ArticleType.IncontinencePad }
            };
            var testSheetInDb = new TestSheet
            {
                TestSheetId = 2,
                MachineNr = "M49",
                CreatedDateTime = new DateTime( 2016, 5, 5 ),
                TestValues = listOfTestValues
            };
            foreach ( var testValue in listOfTestValues )
                testValue.TestSheet = testSheetInDb;

            var testBll =
                MockHelperBll.GetTestBll(
                    testSheetInDb
                );

            var target = new InkoAquisitionService( new NLogLoggerFactory() )
            {
                TestBll = testBll
            };

            var actual = target.GetInkoAquisitionEditViewModel( 2 );

            Assert.Equal( null, actual );
        }

        /// <summary>
        ///     Tests if it get a correct viewModel if everything for the testvalue exists in the db for inkoaquisitiontest
        /// </summary>
        [Fact]
        public void GetInkoAquisitionEditViewModelTest()
        {
            var listOfTestValues = new List<TestValue>
            {
                new TestValue
                {
                    TestValueId = 1,
                    TestSheetRefId = 1,
                    LastEditedPerson = "Hans",
                    DayInYearOfArticleCreation = 123,
                    IncontinencePadTestValue =
                        new IncontinencePadTestValue
                        {
                            IncontinencePadTime = new TimeSpan( 5, 10, 0 ),
                            AcquisitionTimeFirst = 10,
                            AcquisitionTimeFirstRw = RwType.Ok,
                            AcquisitionTimeSecond = 20,
                            AcquisitionTimeSecondRw = RwType.Ok,
                            AcquisitionTimeThird = 30,
                            AcquisitionTimeThirdRw = RwType.Ok,
                            AcquisitionWeight = 20.2,
                            RewetAfterAcquisitionTimeDryWeight = 12.5,
                            RewetAfterAcquisitionTimeWetWeight = 13,
                            RewetAfterAcquisitionTimeWeightDifference = 0.5,
                            RewetAfterAcquisitionTimeRw = RwType.Ok,
                            TestType = TestTypeIncontinencePad.AcquisitionTimeAndRewet
                        }
                }
            };
            var testSheetInDb = new TestSheet
            {
                TestSheetId = 1,
                MachineNr = "M49",
                CreatedDateTime = new DateTime( 2016, 5, 5 ),
                TestValues = listOfTestValues
            };
            foreach ( var testValue in listOfTestValues )
                testValue.TestSheet = testSheetInDb;

            var testBll =
                MockHelperBll.GetTestBll(
                    testSheetInDb
                );

            var testServiceHelper = MockHelperTestServiceHelper.GetTestServiceHelper( "IT/49/16/" );

            var target = new InkoAquisitionService( new NLogLoggerFactory() )
            {
                TestBll = testBll,
                TestServiceHelper = testServiceHelper
            };

            var actual = target.GetInkoAquisitionEditViewModel( 1 );

            Assert.Equal( testSheetInDb.TestSheetId, actual.TestSheetId );
            Assert.Equal( 1, actual.TestValueId );
            Assert.Equal( "IT/49/16/", actual.ProductionCode );
            Assert.Equal( "Hans", actual.TestPerson );
            Assert.Equal( 123, actual.ProductionCodeDay );
            Assert.Equal( new TimeSpan( 5, 10, 0 ), actual.ProductionCodeTime );
            Assert.Equal( 20.2, actual.InkoWeight );
            Assert.Equal( 10, actual.AquisitionAddition1 );
            Assert.Equal( 20, actual.AquisitionAddition2 );
            Assert.Equal( 30, actual.AquisitionAddition3 );
            Assert.Equal( 12.5, actual.FPDry );
            Assert.Equal( 13, actual.FPWet );
            Assert.Equal( 2,
                          actual.NoteCodes.ToList()
                                .Count );
        }

        /// <summary>
        ///     Tests if it get null if the IncontinenceTestValue for the testvalue doesnt exist in the db
        /// </summary>
        [Fact]
        public void GetInkoAquisitionEditViewModelWithNoIncontinenceTestVauleTest()
        {
            var listOfTestValues = new List<TestValue>
            {
                new TestValue { TestValueId = 1, TestSheetRefId = 1 }
            };
            var testSheetInDb = new TestSheet
            {
                TestSheetId = 1,
                MachineNr = "M49",
                CreatedDateTime = new DateTime( 2016, 5, 5 ),
                TestValues = listOfTestValues
            };
            foreach ( var testValue in listOfTestValues )
                testValue.TestSheet = testSheetInDb;

            var testBll =
                MockHelperBll.GetTestBll(
                    testSheetInDb
                );

            var target = new InkoAquisitionService( new NLogLoggerFactory() )
            {
                TestBll = testBll
            };

            var actual = target.GetInkoAquisitionEditViewModel( 1 );

            Assert.Equal( null, actual );
        }

        /// <summary>
        ///     Tests if it get null if the testsheet for the testvalue doesnt exist in the db
        /// </summary>
        [Fact]
        public void GetInkoRetentionEditViewModelWithNotExistingTestSheetTest()
        {
            var listOfTestValues = new List<TestValue>
            {
                new TestValue { TestValueId = 1, TestSheetRefId = 1, IncontinencePadTestValue = new IncontinencePadTestValue(), ArticleTestType = ArticleType.IncontinencePad }
            };
            var testSheetInDb = new TestSheet
            {
                TestSheetId = 2,
                MachineNr = "M49",
                CreatedDateTime = new DateTime( 2016, 5, 5 ),
                TestValues = listOfTestValues
            };

            var testBll =
                MockHelperBll.GetTestBll(
                    testSheetInDb
                );

            var target = new InkoAquisitionService( new NLogLoggerFactory() )
            {
                TestBll = testBll
            };

            var actual = target.GetInkoAquisitionEditViewModel( 1 );

            Assert.Equal( null, actual );
        }

        /// <summary>
        ///     Tests if it get a new correct viewModel if the testSheet exists in the db
        /// </summary>
        [Fact]
        public void GetNewInkoAquisitionEditViewModelFromExistingTestSheetTest()
        {
            var testSheetInDb = new TestSheet
            {
                TestSheetId = 2,
                MachineNr = "M49",
                CreatedDateTime = new DateTime( 2016, 5, 5 ),
                ArticleType = ArticleType.IncontinencePad
            };
            var testBll =
                MockHelperBll.GetTestBll(
                    testSheetInDb
                );

            var testServiceHelper = MockHelperTestServiceHelper.GetTestServiceHelper( "IT/49/16/" );

            var target = new InkoAquisitionService( new NLogLoggerFactory() )
            {
                TestBll = testBll,
                TestServiceHelper = testServiceHelper
            };

            var actual = target.GetNewInkoAquisitionEditViewModel( 2 );

            Assert.Equal( testSheetInDb.TestSheetId, actual.TestSheetId );
            Assert.Equal( -1, actual.TestValueId );
            Assert.Equal( "IT/49/16/", actual.ProductionCode );
        }

        /// <summary>
        ///     Tests if it get null if the testSheet doesnt exist in the db
        /// </summary>
        [Fact]
        public void GetNewInkoAquisitionEditViewModelFromNotExistingTestSheetTest()
        {
            var testBll =
                MockHelperBll.GetTestBll(
                    new TestSheet { TestSheetId = 1 }
                );

            var target = new InkoAquisitionService( new NLogLoggerFactory() )
            {
                TestBll = testBll
            };

            var actual = target.GetNewInkoAquisitionEditViewModel( 2 );

            Assert.Equal( null, actual );
        }

        /// <summary>
        ///     Test the failure of saving
        /// </summary>
        [Fact]
        public void SaveFailTest()
        {
            var testValue = new TestValue();

            var inkoAquisitionServiceHelper =
                MockHelperTestServiceHelper.GetInkoAquisitionServiceHelper(
                    testValue
                );

            var target = new InkoAquisitionService( new NLogLoggerFactory() )
            {
                InkoAquisitionServiceHelper = inkoAquisitionServiceHelper
            };

            var actual = target.Save( null );
            Assert.Equal( null, actual );
        }

        /// <summary>
        ///     Test the success of saving
        /// </summary>
        [Fact]
        public void SaveSuccessTest()
        {
            var testValue = new TestValue();

            var inkoAquisitionServiceHelper =
                MockHelperTestServiceHelper.GetInkoAquisitionServiceHelper(
                    testValue
                );

            var target = new InkoAquisitionService( new NLogLoggerFactory() )
            {
                InkoAquisitionServiceHelper = inkoAquisitionServiceHelper
            };

            var actual = target.Save( new InkoAquisitionEditViewModel() );
            Assert.Equal( testValue, actual );
        }
    }
}