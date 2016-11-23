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
    ///     Class representing Tests for InkoRetentionService
    /// </summary>
    public class InkoRetentionServiceTest
    {
        /// <summary>
        ///     Test Delete
        /// </summary>
        [Fact]
        public void DeleteTest()
        {
            var deletedTestValue = new TestValue { TestValueId = 1 };

            var inkoRetentionServiceHelper =
                MockHelperTestServiceHelper.GetInkoRetentionServiceHelper(
                    null
                );
            var testBll =
                MockHelperBll.GetBabyDiaperBllForDelete(
                    deletedTestValue
                );

            var target = new InkoRetentionService( new NLogLoggerFactory() )
            {
                InkoRetentionServiceHelper = inkoRetentionServiceHelper,
                TestBll = testBll
            };

            var actual = target.Delete( 1 );
            Assert.Equal( 1, actual.TestValueId );
        }

        /// <summary>
        ///     Tests if it get null if the testValue doesnt exist in the db
        /// </summary>
        [Fact]
        public void GetInkoRetentionEditViewModelFromNotExistingTestValueTest()
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

            var target = new InkoRetentionService( new NLogLoggerFactory() )
            {
                TestBll = testBll
            };

            var actual = target.GetInkoRetentionEditViewModel( 2 );

            Assert.Equal( null, actual );
        }

        /// <summary>
        ///     Tests if it get a correct viewModel if everything for the testvalue exists in the db for inkorewettest
        /// </summary>
        [Fact]
        public void GetInkoRetentionEditViewModelTest()
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
                            RetentionWeight = 30,
                            RetentionWetValue = 400.2,
                            RetentionAfterZentrifuge = 200,
                            RetentionAbsorbtion = 380.2,
                            RetentionEndValue = 190,
                            RetentionRw = RwType.Ok,
                            TestType = TestTypeIncontinencePad.Retention
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

            var target = new InkoRetentionService( new NLogLoggerFactory() )
            {
                TestBll = testBll,
                TestServiceHelper = testServiceHelper
            };

            var actual = target.GetInkoRetentionEditViewModel( 1 );

            Assert.Equal( testSheetInDb.TestSheetId, actual.TestSheetId );
            Assert.Equal( 1, actual.TestValueId );
            Assert.Equal( "IT/49/16/", actual.ProductionCode );
            Assert.Equal( "Hans", actual.TestPerson );
            Assert.Equal( 123, actual.ProductionCodeDay );
            Assert.Equal( new TimeSpan( 5, 10, 0 ), actual.ProductionCodeTime );
            Assert.Equal( 30, actual.InkoWeight );
            Assert.Equal( 400.2, actual.InkoWeightWet );
            Assert.Equal( 200, actual.InkoWeightAfterZentrifuge );
            Assert.Equal( 2,
                          actual.NoteCodes.ToList()
                                .Count );
        }

        /// <summary>
        ///     Tests if it get null if the IncontinenceTestValue for the testvalue doesnt exist in the db
        /// </summary>
        [Fact]
        public void GetInkoRetentionEditViewModelWithNoIncontinenceTestVauleTest()
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

            var target = new InkoRetentionService( new NLogLoggerFactory() )
            {
                TestBll = testBll
            };

            var actual = target.GetInkoRetentionEditViewModel( 1 );

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

            var target = new InkoRetentionService( new NLogLoggerFactory() )
            {
                TestBll = testBll
            };

            var actual = target.GetInkoRetentionEditViewModel( 1 );

            Assert.Equal( null, actual );
        }

        /// <summary>
        ///     Tests if it get a new correct viewModel if the testSheet exists in the db
        /// </summary>
        [Fact]
        public void GetNewInkoRetentionEditViewModelFromExistingTestSheetTest()
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

            var target = new InkoRetentionService( new NLogLoggerFactory() )
            {
                TestBll = testBll,
                TestServiceHelper = testServiceHelper
            };

            var actual = target.GetNewInkoRetentionEditViewModel( 2 );

            Assert.Equal( testSheetInDb.TestSheetId, actual.TestSheetId );
            Assert.Equal( -1, actual.TestValueId );
            Assert.Equal( "IT/49/16/", actual.ProductionCode );
        }

        /// <summary>
        ///     Tests if it get null if the testSheet doesnt exist in the db
        /// </summary>
        [Fact]
        public void GetNewInkoRetentionEditViewModelFromNotExistingTestSheetTest()
        {
            var testBll =
                MockHelperBll.GetTestBll(
                    new TestSheet { TestSheetId = 1 }
                );

            var target = new InkoRetentionService( new NLogLoggerFactory() )
            {
                TestBll = testBll
            };

            var actual = target.GetNewInkoRetentionEditViewModel( 2 );

            Assert.Equal( null, actual );
        }

        /// <summary>
        ///     Test the failure of saving
        /// </summary>
        [Fact]
        public void SaveFailTest()
        {
            var testValue = new TestValue();

            var inkoRetentionServiceHelper =
                MockHelperTestServiceHelper.GetInkoRetentionServiceHelper(
                    testValue
                );

            var target = new InkoRetentionService( new NLogLoggerFactory() )
            {
                InkoRetentionServiceHelper = inkoRetentionServiceHelper
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

            var inkoRetentionServiceHelper =
                MockHelperTestServiceHelper.GetInkoRetentionServiceHelper(
                    testValue
                );

            var target = new InkoRetentionService( new NLogLoggerFactory() )
            {
                InkoRetentionServiceHelper = inkoRetentionServiceHelper
            };

            var actual = target.Save( new InkoRetentionEditViewModel() );
            Assert.Equal( testValue, actual );
        }
    }
}