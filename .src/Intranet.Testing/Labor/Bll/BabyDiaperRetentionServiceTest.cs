#region Usings

using System;
using System.Collections.Generic;
using System.Linq;
using Intranet.Common;
using Intranet.Labor.Model;
using Intranet.Labor.TestEnvironment;
using Intranet.Labor.ViewModel;
using Xunit;

#endregion

namespace Intranet.Labor.Bll.Test
{
    /// <summary>
    ///     Class representing Tests for BabyDiaperRetentionService
    /// </summary>
    public class BabyDiaperRetentionServiceTest
    {
        /// <summary>
        ///     Test Delete
        /// </summary>
        [Fact]
        public void DeleteTest()
        {
            var deletedTestValue = new TestValue { TestValueId = 1 };

            var babyDiaperRetentionServiceHelper =
                MockHelperTestServiceHelper.GetBabyDiaperRetentionServiceHelper(
                    null
                );
            var babyDiaperBll =
                MockHelperBll.GetBabyDiaperBllForDelete(
                    deletedTestValue
                );

            var target = new BabyDiaperRetentionService( new NLogLoggerFactory() )
            {
                BabyDiaperRetentionServiceHelper = babyDiaperRetentionServiceHelper,
                TestBll = babyDiaperBll
            };

            var actual = target.Delete( 1 );
            Assert.Equal( 1, actual.TestValueId );
        }

        /// <summary>
        ///     Tests if it get null if the testValue doesnt exist in the db
        /// </summary>
        [Fact]
        public void GetBabyDiaperRetentionEditViewModelFromNotExistingTestValueTest()
        {
            var listOfTestValues = new List<TestValue>
            {
                new TestValue { TestValueId = 1, TestSheetRefId = 1, BabyDiaperTestValue = new BabyDiaperTestValue() }
            };
            var testSheetInDb = new TestSheet
            {
                TestSheetId = 1,
                MachineNr = "M11",
                CreatedDateTime = new DateTime( 2016, 5, 5 ),
                TestValues = listOfTestValues
            };
            foreach ( var testValue in listOfTestValues )
                testValue.TestSheet = testSheetInDb;

            var babyDiaperRetentionBll =
                MockHelperBll.GetTestBll(
                    testSheetInDb
                );

            var target = new BabyDiaperRetentionService( new NLogLoggerFactory() )
            {
                TestBll = babyDiaperRetentionBll
            };

            var actual = target.GetBabyDiapersRetentionEditViewModel( 2 );

            Assert.Equal( null, actual );
        }

        /// <summary>
        ///     Tests if it get a correct viewModel if everything for the testvalue exists in the db
        /// </summary>
        [Fact]
        public void GetBabyDiaperRetentionEditViewModelTest()
        {
            var listOfTestValues = new List<TestValue>
            {
                new TestValue
                {
                    TestValueId = 1,
                    TestSheetRefId = 1,
                    LastEditedPerson = "Hans",
                    DayInYearOfArticleCreation = 123,
                    BabyDiaperTestValue =
                        new BabyDiaperTestValue
                        {
                            DiaperCreatedTime = new TimeSpan( 5, 10, 0 ),
                            WeightDiaperDry = 32.2,
                            RetentionWetWeight = 398.1,
                            TestType = TestTypeBabyDiaper.Retention
                        }
                }
            };
            var testSheetInDb = new TestSheet
            {
                TestSheetId = 1,
                MachineNr = "M11",
                CreatedDateTime = new DateTime( 2016, 5, 5 ),
                TestValues = listOfTestValues
            };
            foreach ( var testValue in listOfTestValues )
                testValue.TestSheet = testSheetInDb;

            var babyDiaperRetentionBll =
                MockHelperBll.GetTestBll(
                    testSheetInDb
                );

            var babyDiaperServiceHelper = MockHelperTestServiceHelper.GetTestServiceHelper( "IT/11/16/" );

            var target = new BabyDiaperRetentionService( new NLogLoggerFactory() )
            {
                TestBll = babyDiaperRetentionBll,
                TestServiceHelper = babyDiaperServiceHelper
            };

            var actual = target.GetBabyDiapersRetentionEditViewModel( 1 );

            Assert.Equal( testSheetInDb.TestSheetId, actual.TestSheetId );
            Assert.Equal( 1, actual.TestValueId );
            Assert.Equal( "IT/11/16/", actual.ProductionCode );
            Assert.Equal( "Hans", actual.TestPerson );
            Assert.Equal( 123, actual.ProductionCodeDay );
            Assert.Equal( new TimeSpan( 5, 10, 0 ), actual.ProductionCodeTime );
            Assert.Equal( 32.2, actual.DiaperWeight );
            Assert.Equal( 398.1, actual.WeightRetentionWet );
            Assert.Equal( 2,
                          actual.NoteCodes.ToList()
                                .Count );
        }

        /// <summary>
        ///     Tests if it get null if the BabyDiaperTestValue for the testvalue doesnt exist in the db
        /// </summary>
        [Fact]
        public void GetBabyDiaperRetentionEditViewModelWithNoBabyDiaperTestVauleTest()
        {
            var listOfTestValues = new List<TestValue>
            {
                new TestValue { TestValueId = 1, TestSheetRefId = 1 }
            };
            var testSheetInDb = new TestSheet
            {
                TestSheetId = 1,
                MachineNr = "M11",
                CreatedDateTime = new DateTime( 2016, 5, 5 ),
                TestValues = listOfTestValues
            };
            foreach ( var testValue in listOfTestValues )
                testValue.TestSheet = testSheetInDb;

            var babyDiaperRetentionBll =
                MockHelperBll.GetTestBll(
                    testSheetInDb
                );

            var target = new BabyDiaperRetentionService( new NLogLoggerFactory() )
            {
                TestBll = babyDiaperRetentionBll
            };

            var actual = target.GetBabyDiapersRetentionEditViewModel( 1 );

            Assert.Equal( null, actual );
        }

        /// <summary>
        ///     Tests if it get null if the testsheet for the testvalue doesnt exist in the db
        /// </summary>
        [Fact]
        public void GetBabyDiaperRetentionEditViewModelWithNotExistingTestSheetTest()
        {
            var listOfTestValues = new List<TestValue>
            {
                new TestValue { TestValueId = 1, TestSheetRefId = 1, BabyDiaperTestValue = new BabyDiaperTestValue() }
            };
            var testSheetInDb = new TestSheet
            {
                TestSheetId = 1,
                MachineNr = "M11",
                CreatedDateTime = new DateTime( 2016, 5, 5 ),
                TestValues = listOfTestValues
            };

            var babyDiaperRetentionBll =
                MockHelperBll.GetTestBll(
                    testSheetInDb
                );

            var target = new BabyDiaperRetentionService( new NLogLoggerFactory() )
            {
                TestBll = babyDiaperRetentionBll
            };

            var actual = target.GetBabyDiapersRetentionEditViewModel( 1 );

            Assert.Equal( null, actual );
        }

        /// <summary>
        ///     Tests if it get a new correct viewModel if the testSheet exists in the db
        /// </summary>
        [Fact]
        public void GetNewBabyDiaperRetentionEditViewModelFromExistingTestSheetTest()
        {
            var testSheetInDb = new TestSheet
            {
                TestSheetId = 1,
                MachineNr = "M11",
                CreatedDateTime = new DateTime( 2016, 5, 5 )
            };
            var babyDiaperRetentionBll =
                MockHelperBll.GetTestBll(
                    testSheetInDb
                );

            var babyDiaperServiceHelper = MockHelperTestServiceHelper.GetTestServiceHelper( "IT/11/16/" );

            var target = new BabyDiaperRetentionService( new NLogLoggerFactory() )
            {
                TestBll = babyDiaperRetentionBll,
                TestServiceHelper = babyDiaperServiceHelper
            };

            var actual = target.GetNewBabyDiapersRetentionEditViewModel( 1 );

            Assert.Equal( testSheetInDb.TestSheetId, actual.TestSheetId );
            Assert.Equal( -1, actual.TestValueId );
            Assert.Equal( "IT/11/16/", actual.ProductionCode );
        }

        /// <summary>
        ///     Tests if it get null if the testSheet doesnt exist in the db
        /// </summary>
        [Fact]
        public void GetNewBabyDiaperRetentionEditViewModelFromNotExistingTestSheetTest()
        {
            var babyDiaperRetentionBll =
                MockHelperBll.GetTestBll(
                    new TestSheet { TestSheetId = 1 }
                );

            var target = new BabyDiaperRetentionService( new NLogLoggerFactory() )
            {
                TestBll = babyDiaperRetentionBll
            };

            var actual = target.GetNewBabyDiapersRetentionEditViewModel( 2 );

            Assert.Equal( null, actual );
        }

        /// <summary>
        ///     Test the failure of saving
        /// </summary>
        [Fact]
        public void SaveFailTest()
        {
            var testValue = new TestValue();

            var babyDiaperRetentionServiceHelper =
                MockHelperTestServiceHelper.GetBabyDiaperRetentionServiceHelper(
                    testValue
                );

            var target = new BabyDiaperRetentionService( new NLogLoggerFactory() )
            {
                BabyDiaperRetentionServiceHelper = babyDiaperRetentionServiceHelper
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

            var babyDiaperRetentionServiceHelper =
                MockHelperTestServiceHelper.GetBabyDiaperRetentionServiceHelper(
                    testValue
                );

            var target = new BabyDiaperRetentionService( new NLogLoggerFactory() )
            {
                BabyDiaperRetentionServiceHelper = babyDiaperRetentionServiceHelper
            };

            var actual = target.Save( new BabyDiaperRetentionEditViewModel() );
            Assert.Equal( testValue, actual );
        }
    }
}