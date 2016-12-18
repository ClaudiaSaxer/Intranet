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
    ///     Class representing Tests for InkoRewetServiceHelper
    /// </summary>
    public class InkoRewetServiceHelperTest
    {
        #region BaseTestData

        private TestSheet GetTestSheetTestData() => new TestSheet
        {
            FaNr = "FA123456"
        };

        private ProductionOrder GetProductionOrderTestData() => new ProductionOrder
        {
            Article = new Article
            {
                MaxInkoRewet = 0.5
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
                        TestType = TestTypeIncontinencePad.RewetFree
                    }
                },
                new TestValue
                {
                    TestValueType = TestValueType.StandardDeviation,
                    ArticleTestType = ArticleType.IncontinencePad,
                    IncontinencePadTestValue = new IncontinencePadTestValue
                    {
                        TestType = TestTypeIncontinencePad.RewetFree
                    }
                }
            }
        };

        #endregion

        #region SaveNewRewetTest Tests

        /// <summary>
        ///     Tests if Saving a new InkoRewet Test works
        /// </summary>
        [Fact]
        public void SaveNewRewetTestBaseTest()
        {
            var viewModel = new InkoRewetEditViewModel
            {
                TestPerson = "Hans",
                TestValueId = -1,
                TestSheetId = 1,
                ProductionCodeTime = new TimeSpan( 12, 34, 0 ),
                ProductionCodeDay = 123,
                FPDry = 20.0,
                FPWet = 20.2,
                Notes = new List<TestNote> { new TestNote { ErrorCodeId = 1, Id = 1, Message = "Testnote" } }
            };
            var testValueReturnedFromHelper = new TestValue
            {
                TestSheetId = 1,
                CreatedDateTime = new DateTime( 2016, 1, 2 ),
                LastEditedDateTime = new DateTime( 2016, 1, 2 ),
                CreatedPerson = "Hans",
                LastEditedPerson = "Hans",
                DayInYearOfArticleCreation = 123,
                ArticleTestType = ArticleType.IncontinencePad,
                TestValueNote = new List<TestValueNote> { new TestValueNote { ErrorId = 1, Message = "Testnote" } }
            };
            var testSheetDataFromDb = GetTestSheetTestData();
            var productionOrderDataFromDb = GetProductionOrderTestData();

            var testBll = MockHelperBll.GetTestBllForSavingAndUpdating( testSheetDataFromDb, productionOrderDataFromDb, null );
            var testServiceHelper = MockHelperTestServiceHelper.GetTestServiceHelperCreateNewTestValue( testValueReturnedFromHelper );

            var target = new InkoRewetServiceHelper( new NLogLoggerFactory() )
            {
                TestBll = testBll,
                TestServiceHelper = testServiceHelper
            };

            var actual = target.SaveNewRewetTest( viewModel );

            Assert.Equal( testValueReturnedFromHelper, actual );
        }

        /// <summary>
        ///     Tests if while saving the correct RwValue choosen
        /// </summary>
        [Fact]
        public void SaveNewRewetTestCalculationRwOkTest()
        {
            var viewModel = new InkoRewetEditViewModel
            {
                TestPerson = "Hans",
                TestValueId = -1,
                TestSheetId = 1,
                ProductionCodeTime = new TimeSpan( 12, 34, 0 ),
                ProductionCodeDay = 123,
                FPDry = 20.0,
                FPWet = 20.2,
                Notes = new List<TestNote> { new TestNote { ErrorCodeId = 1, Id = 1, Message = "Testnote" } }
            };
            var testValueReturnedFromHelper = new TestValue
            {
                TestSheetId = 1,
                CreatedDateTime = new DateTime( 2016, 1, 2 ),
                LastEditedDateTime = new DateTime( 2016, 1, 2 ),
                CreatedPerson = "Hans",
                LastEditedPerson = "Hans",
                DayInYearOfArticleCreation = 123,
                ArticleTestType = ArticleType.IncontinencePad,
                TestValueNote = new List<TestValueNote> { new TestValueNote { ErrorId = 1, Message = "Testnote" } }
            };
            var testSheetDataFromDb = GetTestSheetTestData();
            var productionOrderDataFromDb = GetProductionOrderTestData();

            var testBll = MockHelperBll.GetTestBllForSavingAndUpdating( testSheetDataFromDb, productionOrderDataFromDb, null );
            var testServiceHelper = MockHelperTestServiceHelper.GetTestServiceHelperCreateNewTestValue( testValueReturnedFromHelper );

            var target = new InkoRewetServiceHelper( new NLogLoggerFactory() )
            {
                TestBll = testBll,
                TestServiceHelper = testServiceHelper
            };

            var actual = target.SaveNewRewetTest( viewModel );

            Assert.Equal( RwType.Ok, actual.IncontinencePadTestValue.RewetFreeRw );
        }

        /// <summary>
        ///     Tests if while saving the correct RwValue choosen
        /// </summary>
        [Fact]
        public void SaveNewRewetTestCalculationRwWorseTest()
        {
            var viewModel = new InkoRewetEditViewModel
            {
                TestPerson = "Hans",
                TestValueId = -1,
                TestSheetId = 1,
                ProductionCodeTime = new TimeSpan( 12, 34, 0 ),
                ProductionCodeDay = 123,
                FPDry = 20.0,
                FPWet = 21.0,
                Notes = new List<TestNote> { new TestNote { ErrorCodeId = 1, Id = 1, Message = "Testnote" } }
            };
            var testValueReturnedFromHelper = new TestValue
            {
                TestSheetId = 1,
                CreatedDateTime = new DateTime( 2016, 1, 2 ),
                LastEditedDateTime = new DateTime( 2016, 1, 2 ),
                CreatedPerson = "Hans",
                LastEditedPerson = "Hans",
                DayInYearOfArticleCreation = 123,
                ArticleTestType = ArticleType.IncontinencePad,
                TestValueNote = new List<TestValueNote> { new TestValueNote { ErrorId = 1, Message = "Testnote" } }
            };
            var testSheetDataFromDb = GetTestSheetTestData();
            var productionOrderDataFromDb = GetProductionOrderTestData();

            var testBll = MockHelperBll.GetTestBllForSavingAndUpdating( testSheetDataFromDb, productionOrderDataFromDb, null );
            var testServiceHelper = MockHelperTestServiceHelper.GetTestServiceHelperCreateNewTestValue( testValueReturnedFromHelper );

            var target = new InkoRewetServiceHelper( new NLogLoggerFactory() )
            {
                TestBll = testBll,
                TestServiceHelper = testServiceHelper
            };

            var actual = target.SaveNewRewetTest( viewModel );

            Assert.Equal( RwType.Worse, actual.IncontinencePadTestValue.RewetFreeRw );
        }

        #endregion

        #region UpdateRewetTest Tests

        /// <summary>
        ///     Tests if Updating an Test returns null if old test not exist
        /// </summary>
        [Fact]
        public void UpdateRewetTestFailTest()
        {
            var testBll = MockHelperBll.GetTestBllForSavingAndUpdating( null, null, null );
            var target = new InkoRewetServiceHelper( new NLogLoggerFactory() )
            {
                TestBll = testBll
            };

            var actual = target.UpdateRewetTest( new InkoRewetEditViewModel() );

            Assert.Equal( null, actual );
        }

        /// <summary>
        ///     Tests if Updating an existing Inko Rewet Test works
        /// </summary>
        [Fact]
        public void UpdateRewetTestBaseTest()
        {
            var viewModel = new InkoRewetEditViewModel
            {
                TestPerson = "Hans",
                TestValueId = -1,
                TestSheetId = 1,
                ProductionCodeTime = new TimeSpan( 12, 34, 0 ),
                ProductionCodeDay = 123,
                FPDry = 20.0,
                FPWet = 20.2,
                Notes = new List<TestNote> { new TestNote { ErrorCodeId = 1, Id = 1, Message = "Testnote" } }
            };
            var testValueReturnedFromDb = new TestValue
            {
                TestSheetId = 1,
                CreatedDateTime = new DateTime( 2016, 1, 2 ),
                LastEditedDateTime = new DateTime( 2016, 1, 2 ),
                CreatedPerson = "Fritz",
                LastEditedPerson = "Fritz",
                DayInYearOfArticleCreation = 123,
                ArticleTestType = ArticleType.IncontinencePad,
                TestValueNote = new List<TestValueNote> { new TestValueNote { ErrorId = 1, Message = "Testnote" } },
                IncontinencePadTestValue = new IncontinencePadTestValue
                {
                    IncontinencePadTime = new TimeSpan( 11, 11, 0 ),
                    TestType = TestTypeIncontinencePad.RewetFree
                }
            };
            var testSheetDataFromDb = GetTestSheetTestData();
            var productionOrderDataFromDb = GetProductionOrderTestData();

            var babyDiaperBll = MockHelperBll.GetTestBllForSavingAndUpdating( testSheetDataFromDb, productionOrderDataFromDb, testValueReturnedFromDb );

            var testServiceHelper = MockHelperTestServiceHelper.GetTestServiceHelperForUpdating();

            var target = new InkoRewetServiceHelper( new NLogLoggerFactory() )
            {
                TestBll = babyDiaperBll,
                TestServiceHelper = testServiceHelper
            };

            var actual = target.UpdateRewetTest( viewModel );

            Assert.Equal( testValueReturnedFromDb, actual );
            Assert.Equal( 20, actual.IncontinencePadTestValue.RewetFreeDryValue );
            Assert.Equal( 20.2, actual.IncontinencePadTestValue.RewetFreeWetValue );
            Assert.Equal( 0.2, actual.IncontinencePadTestValue.RewetFreeDifference, 2 );
            Assert.Equal( "Hans", actual.LastEditedPerson );
            Assert.Equal( "Fritz", actual.CreatedPerson );
            Assert.NotEqual( new DateTime( 2016, 1, 2 ), actual.LastEditedDateTime );
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

            var babyDiaperBll = MockHelperBll.GetTestBllForSavingAndUpdating( testSheetDataFromDb, productionOrderDataFromDb, null );

            var target = new InkoRewetServiceHelper( new NLogLoggerFactory() )
            {
                TestBll = babyDiaperBll
            };

            var actual = target.UpdateRewetAverageAndStv( 1 );

            var actualRewetAvg =
                actual.TestValues.FirstOrDefault(
                          tv => ( tv.TestValueType == TestValueType.Average ) && ( tv.IncontinencePadTestValue.TestType == TestTypeIncontinencePad.RewetFree ) );
            Assert.NotNull( actualRewetAvg );
            Assert.Equal( 0, actualRewetAvg.IncontinencePadTestValue.RewetFreeDryValue );
            Assert.Equal( 0, actualRewetAvg.IncontinencePadTestValue.RewetFreeWetValue );
            Assert.Equal( 0, actualRewetAvg.IncontinencePadTestValue.RewetFreeDifference );
            Assert.Equal( RwType.Ok, actualRewetAvg.IncontinencePadTestValue.RewetFreeRw );

            var actualRewetStDev =
                actual.TestValues.FirstOrDefault(
                          tv => ( tv.TestValueType == TestValueType.StandardDeviation ) && ( tv.IncontinencePadTestValue.TestType == TestTypeIncontinencePad.RewetFree ) );
            Assert.NotNull( actualRewetStDev );
            Assert.Equal( 0, actualRewetStDev.IncontinencePadTestValue.RewetFreeDryValue );
            Assert.Equal( 0, actualRewetStDev.IncontinencePadTestValue.RewetFreeWetValue );
            Assert.Equal( 0, actualRewetStDev.IncontinencePadTestValue.RewetFreeDifference );
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
                IncontinencePadTestValue = new IncontinencePadTestValue
                {
                    TestType = TestTypeIncontinencePad.RewetFree,
                    RewetFreeDryValue = 20,
                    RewetFreeWetValue = 20.2,
                    RewetFreeDifference = 0.2,
                    RewetFreeRw = RwType.Ok
                }
            };
            var testSheetDataFromDb = GetTestSheetTestDataWithAvgAndStDev();
            testSheetDataFromDb.TestValues.Add( onlyTestValue );
            var productionOrderDataFromDb = GetProductionOrderTestData();

            var testBll = MockHelperBll.GetTestBllForSavingAndUpdating( testSheetDataFromDb, productionOrderDataFromDb, null );

            var target = new InkoRewetServiceHelper( new NLogLoggerFactory() )
            {
                TestBll = testBll
            };

            var actual = target.UpdateRewetAverageAndStv( 1 );

            var actualRewetAvg =
                actual.TestValues.FirstOrDefault(
                          tv => ( tv.TestValueType == TestValueType.Average ) && ( tv.IncontinencePadTestValue.TestType == TestTypeIncontinencePad.RewetFree ) );
            Assert.NotNull( actualRewetAvg );
            Assert.Equal( 20.0, actualRewetAvg.IncontinencePadTestValue.RewetFreeDryValue );
            Assert.Equal( 20.2, actualRewetAvg.IncontinencePadTestValue.RewetFreeWetValue );
            Assert.Equal( 0.2, actualRewetAvg.IncontinencePadTestValue.RewetFreeDifference, 2 );
            Assert.Equal( RwType.Ok, actualRewetAvg.IncontinencePadTestValue.RewetFreeRw );

            var actualRewetStDev =
                actual.TestValues.FirstOrDefault(
                          tv => ( tv.TestValueType == TestValueType.StandardDeviation ) && ( tv.IncontinencePadTestValue.TestType == TestTypeIncontinencePad.RewetFree ) );
            Assert.NotNull( actualRewetStDev );
            Assert.Equal( 0, actualRewetStDev.IncontinencePadTestValue.RewetFreeDryValue );
            Assert.Equal( 0, actualRewetStDev.IncontinencePadTestValue.RewetFreeWetValue );
            Assert.Equal( 0, actualRewetStDev.IncontinencePadTestValue.RewetFreeDifference );
        }

        #endregion
    }
}