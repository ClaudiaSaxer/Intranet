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
    ///     Class representing Tests for InkoAquisitionServiceHelper
    /// </summary>
    public class InkoAquisitionServiceHelperTest
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
                MaxHyTec1 = 20,
                MaxHyTec2 = 60,
                MaxHyTec3 = 85,
                MaxInkoRewetAfterAquisition = 2
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
                        TestType = TestTypeIncontinencePad.AcquisitionTimeAndRewet
                    }
                },
                new TestValue
                {
                    TestValueType = TestValueType.StandardDeviation,
                    ArticleTestType = ArticleType.IncontinencePad,
                    IncontinencePadTestValue = new IncontinencePadTestValue
                    {
                        TestType = TestTypeIncontinencePad.AcquisitionTimeAndRewet
                    }
                }
            }
        };

        private InkoAquisitionEditViewModel GetViewModelTestData() => new InkoAquisitionEditViewModel
        {
            TestPerson = "Hans",
            TestValueId = -1,
            TestSheetId = 1,
            ProductionCodeTime = new TimeSpan( 12, 34, 0 ),
            ProductionCodeDay = 123,
            InkoWeight = 21.15,
            AquisitionAddition1 = 17.12,
            AquisitionAddition2 = 54.06,
            AquisitionAddition3 = 67.85,
            FPDry = 21.73,
            FPWet = 21.79,
            Notes = new List<TestNote> { new TestNote { ErrorCodeId = 1, Id = 1, Message = "Testnote" } }
        };

        private TestValue GetTestValueTestData() => new TestValue
        {
            TestSheetRefId = 1,
            CreatedDateTime = new DateTime( 2016, 1, 2 ),
            LastEditedDateTime = new DateTime( 2016, 1, 2 ),
            CreatedPerson = "Hans",
            LastEditedPerson = "Hans",
            DayInYearOfArticleCreation = 123,
            ArticleTestType = ArticleType.IncontinencePad,
            TestValueNote = new List<TestValueNote> { new TestValueNote { ErrorRefId = 1, Message = "Testnote" } }
        };

        #endregion

        #region SaveNewAquisitionTest Tests

        /// <summary>
        ///     Tests if Saving a new InkoAquisition Test works
        /// </summary>
        [Fact]
        public void SaveNewAquisitionTestBaseTest()
        {
            var viewModel = GetViewModelTestData();
            var testValueReturnedFromHelper = GetTestValueTestData();
            var testSheetDataFromDb = GetTestSheetTestData();
            var productionOrderDataFromDb = GetProductionOrderTestData();

            var testBll = MockHelperBll.GetTestBllForSavingAndUpdating( testSheetDataFromDb, productionOrderDataFromDb, null );
            var testServiceHelper = MockHelperTestServiceHelper.GetTestServiceHelperCreateNewTestValue( testValueReturnedFromHelper );

            var target = new InkoAquisitionServiceHelper( new NLogLoggerFactory() )
            {
                TestBll = testBll,
                TestServiceHelper = testServiceHelper
            };

            var actual = target.SaveNewAquisitionTest( viewModel );

            Assert.Equal( testValueReturnedFromHelper, actual );
        }

        /// <summary>
        ///     Tests if while saving the correct RwValue choosen
        /// </summary>
        [Fact]
        public void SaveNewAquisitionTestCalculationRwOkTest()
        {
            var viewModel = GetViewModelTestData();
            var testValueReturnedFromHelper = GetTestValueTestData();
            var testSheetDataFromDb = GetTestSheetTestData();
            var productionOrderDataFromDb = GetProductionOrderTestData();

            var testBll = MockHelperBll.GetTestBllForSavingAndUpdating( testSheetDataFromDb, productionOrderDataFromDb, null );
            var testServiceHelper = MockHelperTestServiceHelper.GetTestServiceHelperCreateNewTestValue( testValueReturnedFromHelper );

            var target = new InkoAquisitionServiceHelper( new NLogLoggerFactory() )
            {
                TestBll = testBll,
                TestServiceHelper = testServiceHelper
            };

            var actual = target.SaveNewAquisitionTest( viewModel );

            Assert.Equal( RwType.Ok, actual.IncontinencePadTestValue.AcquisitionTimeFirstRw );
            Assert.Equal( RwType.Ok, actual.IncontinencePadTestValue.AcquisitionTimeSecondRw );
            Assert.Equal( RwType.Ok, actual.IncontinencePadTestValue.AcquisitionTimeThirdRw );
            Assert.Equal( RwType.Ok, actual.IncontinencePadTestValue.RewetAfterAcquisitionTimeRw );
        }

        /// <summary>
        ///     Tests if while saving the correct RwValue choosen
        /// </summary>
        [Fact]
        public void SaveNewAquisitionTestCalculationRwWorseTest()
        {
            var viewModel = GetViewModelTestData();
            viewModel.AquisitionAddition1 = 20.1;
            viewModel.AquisitionAddition2 = 60.1;
            viewModel.AquisitionAddition3 = 85.1;
            viewModel.FPWet = 25;
            var testValueReturnedFromHelper = GetTestValueTestData();
            var testSheetDataFromDb = GetTestSheetTestData();
            var productionOrderDataFromDb = GetProductionOrderTestData();

            var testBll = MockHelperBll.GetTestBllForSavingAndUpdating( testSheetDataFromDb, productionOrderDataFromDb, null );
            var testServiceHelper = MockHelperTestServiceHelper.GetTestServiceHelperCreateNewTestValue( testValueReturnedFromHelper );

            var target = new InkoAquisitionServiceHelper( new NLogLoggerFactory() )
            {
                TestBll = testBll,
                TestServiceHelper = testServiceHelper
            };

            var actual = target.SaveNewAquisitionTest( viewModel );

            Assert.Equal( RwType.Worse, actual.IncontinencePadTestValue.AcquisitionTimeFirstRw );
            Assert.Equal( RwType.Worse, actual.IncontinencePadTestValue.AcquisitionTimeSecondRw );
            Assert.Equal( RwType.Worse, actual.IncontinencePadTestValue.AcquisitionTimeThirdRw );
            Assert.Equal( RwType.Worse, actual.IncontinencePadTestValue.RewetAfterAcquisitionTimeRw );
        }

        #endregion

        #region UpdateAquisitionTest Tests

        /// <summary>
        ///     Tests if Updating an Test returns null if old test not exist
        /// </summary>
        [Fact]
        public void UpdateAquisitionTestFailTest()
        {
            var testBll = MockHelperBll.GetTestBllForSavingAndUpdating( null, null, null );
            var target = new InkoAquisitionServiceHelper( new NLogLoggerFactory() )
            {
                TestBll = testBll
            };

            var actual = target.UpdateAquisitionTest( new InkoAquisitionEditViewModel() );

            Assert.Equal( null, actual );
        }

        /// <summary>
        ///     Tests if Updating an existing Inko Retention Test works
        /// </summary>
        [Fact]
        public void UpdateAquisitionTestBaseTest()
        {
            var viewModel = GetViewModelTestData();
            var testValueReturnedFromDb = GetTestValueTestData();
            testValueReturnedFromDb.CreatedPerson = "Fritz";
            testValueReturnedFromDb.LastEditedPerson = "Fritz";
            testValueReturnedFromDb.IncontinencePadTestValue = new IncontinencePadTestValue
            {
                IncontinencePadTime = new TimeSpan( 11, 11, 0 ),
                TestType = TestTypeIncontinencePad.AcquisitionTimeAndRewet
            };

            var testSheetDataFromDb = GetTestSheetTestData();
            var productionOrderDataFromDb = GetProductionOrderTestData();

            var babyDiaperBll = MockHelperBll.GetTestBllForSavingAndUpdating( testSheetDataFromDb, productionOrderDataFromDb, testValueReturnedFromDb );

            var testServiceHelper = MockHelperTestServiceHelper.GetTestServiceHelperForUpdating();

            var target = new InkoAquisitionServiceHelper( new NLogLoggerFactory() )
            {
                TestBll = babyDiaperBll,
                TestServiceHelper = testServiceHelper
            };

            var actual = target.UpdateAquisitionTest( viewModel );

            Assert.Equal( testValueReturnedFromDb, actual );
            Assert.Equal( 21.15, actual.IncontinencePadTestValue.AcquisitionWeight );
            Assert.Equal( 17.12, actual.IncontinencePadTestValue.AcquisitionTimeFirst );
            Assert.Equal( 54.06, actual.IncontinencePadTestValue.AcquisitionTimeSecond );
            Assert.Equal( 67.85, actual.IncontinencePadTestValue.AcquisitionTimeThird, 2 );
            Assert.Equal( 21.73, actual.IncontinencePadTestValue.RewetAfterAcquisitionTimeDryWeight, 2 );
            Assert.Equal( 21.79, actual.IncontinencePadTestValue.RewetAfterAcquisitionTimeWetWeight, 2 );
            Assert.Equal( 0.06, actual.IncontinencePadTestValue.RewetAfterAcquisitionTimeWeightDifference, 2 );
            Assert.Equal( RwType.Ok, actual.IncontinencePadTestValue.AcquisitionTimeFirstRw );
            Assert.Equal( RwType.Ok, actual.IncontinencePadTestValue.AcquisitionTimeSecondRw );
            Assert.Equal( RwType.Ok, actual.IncontinencePadTestValue.AcquisitionTimeThirdRw );
            Assert.Equal( RwType.Ok, actual.IncontinencePadTestValue.RewetAfterAcquisitionTimeRw );
            Assert.Equal( "Hans", actual.LastEditedPerson );
            Assert.Equal( "Fritz", actual.CreatedPerson );
            Assert.NotEqual( new DateTime( 2016, 1, 2 ), actual.LastEditedDateTime );
        }

        #endregion

        #region UpdateAquisitionAverageAndStv Tests

        /// <summary>
        ///     Tests when there are no tests, the avg and dev values are default
        /// </summary>
        [Fact]
        public void UpdateAquisitionAverageAndStvNoTestsTest()
        {
            var testSheetDataFromDb = GetTestSheetTestDataWithAvgAndStDev();
            var productionOrderDataFromDb = GetProductionOrderTestData();

            var babyDiaperBll = MockHelperBll.GetTestBllForSavingAndUpdating( testSheetDataFromDb, productionOrderDataFromDb, null );

            var target = new InkoAquisitionServiceHelper( new NLogLoggerFactory() )
            {
                TestBll = babyDiaperBll
            };

            var actual = target.UpdateAquisitionAverageAndStv( 1 );

            var actualAquisitionAvg =
                actual.TestValues.FirstOrDefault(
                          tv => tv.TestValueType == TestValueType.Average && tv.IncontinencePadTestValue.TestType == TestTypeIncontinencePad.AcquisitionTimeAndRewet );
            Assert.NotNull( actualAquisitionAvg );
            Assert.Equal( 0, actualAquisitionAvg.IncontinencePadTestValue.AcquisitionWeight );
            Assert.Equal( 0, actualAquisitionAvg.IncontinencePadTestValue.AcquisitionTimeFirst );
            Assert.Equal( 0, actualAquisitionAvg.IncontinencePadTestValue.AcquisitionTimeSecond );
            Assert.Equal( 0, actualAquisitionAvg.IncontinencePadTestValue.AcquisitionTimeThird );
            Assert.Equal( 0, actualAquisitionAvg.IncontinencePadTestValue.RewetAfterAcquisitionTimeDryWeight );
            Assert.Equal( 0, actualAquisitionAvg.IncontinencePadTestValue.RewetAfterAcquisitionTimeWetWeight );
            Assert.Equal( 0, actualAquisitionAvg.IncontinencePadTestValue.RewetAfterAcquisitionTimeWeightDifference );
            Assert.Equal( RwType.Ok, actualAquisitionAvg.IncontinencePadTestValue.AcquisitionTimeFirstRw );
            Assert.Equal( RwType.Ok, actualAquisitionAvg.IncontinencePadTestValue.AcquisitionTimeSecondRw );
            Assert.Equal( RwType.Ok, actualAquisitionAvg.IncontinencePadTestValue.AcquisitionTimeThirdRw );
            Assert.Equal( RwType.Ok, actualAquisitionAvg.IncontinencePadTestValue.RewetAfterAcquisitionTimeRw );

            var actualAquisitionStDev =
                actual.TestValues.FirstOrDefault(
                          tv => tv.TestValueType == TestValueType.StandardDeviation && tv.IncontinencePadTestValue.TestType == TestTypeIncontinencePad.AcquisitionTimeAndRewet );
            Assert.NotNull( actualAquisitionStDev );
            Assert.Equal( 0, actualAquisitionStDev.IncontinencePadTestValue.AcquisitionWeight );
            Assert.Equal( 0, actualAquisitionStDev.IncontinencePadTestValue.AcquisitionTimeFirst );
            Assert.Equal( 0, actualAquisitionStDev.IncontinencePadTestValue.AcquisitionTimeSecond );
            Assert.Equal( 0, actualAquisitionStDev.IncontinencePadTestValue.AcquisitionTimeThird );
            Assert.Equal( 0, actualAquisitionStDev.IncontinencePadTestValue.RewetAfterAcquisitionTimeDryWeight );
            Assert.Equal( 0, actualAquisitionStDev.IncontinencePadTestValue.RewetAfterAcquisitionTimeWetWeight );
            Assert.Equal( 0, actualAquisitionStDev.IncontinencePadTestValue.RewetAfterAcquisitionTimeWeightDifference );
        }

        /// <summary>
        ///     Tests when there is only one test, the avg values are equal the test values
        /// </summary>
        [Fact]
        public void UpdateAquisitionAverageAndStvOneTestAvgTest()
        {
            var onlyTestValue = new TestValue
            {
                TestValueType = TestValueType.Single,
                ArticleTestType = ArticleType.BabyDiaper,
                IncontinencePadTestValue = new IncontinencePadTestValue
                {
                    TestType = TestTypeIncontinencePad.AcquisitionTimeAndRewet,
                    AcquisitionWeight = 21.15,
                    AcquisitionTimeFirst = 17.12,
                    AcquisitionTimeSecond = 54.06,
                    AcquisitionTimeThird = 67.85,
                    RewetAfterAcquisitionTimeDryWeight = 21.73,
                    RewetAfterAcquisitionTimeWetWeight = 21.79,
                    RewetAfterAcquisitionTimeWeightDifference = 0.06,
                    AcquisitionTimeFirstRw = RwType.Ok,
                    AcquisitionTimeSecondRw = RwType.Ok,
                    AcquisitionTimeThirdRw = RwType.Ok,
                    RewetAfterAcquisitionTimeRw = RwType.Ok
                }
            };
            var testSheetDataFromDb = GetTestSheetTestDataWithAvgAndStDev();
            testSheetDataFromDb.TestValues.Add( onlyTestValue );
            var productionOrderDataFromDb = GetProductionOrderTestData();

            var testBll = MockHelperBll.GetTestBllForSavingAndUpdating( testSheetDataFromDb, productionOrderDataFromDb, null );

            var target = new InkoAquisitionServiceHelper( new NLogLoggerFactory() )
            {
                TestBll = testBll
            };

            var actual = target.UpdateAquisitionAverageAndStv( 1 );

            var actualAquisitionAvg =
                actual.TestValues.FirstOrDefault(
                          tv => tv.TestValueType == TestValueType.Average && tv.IncontinencePadTestValue.TestType == TestTypeIncontinencePad.AcquisitionTimeAndRewet );
            Assert.NotNull( actualAquisitionAvg );
            Assert.Equal( 21.15, actualAquisitionAvg.IncontinencePadTestValue.AcquisitionWeight );
            Assert.Equal( 17.12, actualAquisitionAvg.IncontinencePadTestValue.AcquisitionTimeFirst );
            Assert.Equal( 54.06, actualAquisitionAvg.IncontinencePadTestValue.AcquisitionTimeSecond );
            Assert.Equal( 67.85, actualAquisitionAvg.IncontinencePadTestValue.AcquisitionTimeThird );
            Assert.Equal( 21.73, actualAquisitionAvg.IncontinencePadTestValue.RewetAfterAcquisitionTimeDryWeight );
            Assert.Equal( 21.79, actualAquisitionAvg.IncontinencePadTestValue.RewetAfterAcquisitionTimeWetWeight );
            Assert.Equal( 0.06, actualAquisitionAvg.IncontinencePadTestValue.RewetAfterAcquisitionTimeWeightDifference );
            Assert.Equal( RwType.Ok, actualAquisitionAvg.IncontinencePadTestValue.AcquisitionTimeFirstRw );
            Assert.Equal( RwType.Ok, actualAquisitionAvg.IncontinencePadTestValue.AcquisitionTimeSecondRw );
            Assert.Equal( RwType.Ok, actualAquisitionAvg.IncontinencePadTestValue.AcquisitionTimeThirdRw );
            Assert.Equal( RwType.Ok, actualAquisitionAvg.IncontinencePadTestValue.RewetAfterAcquisitionTimeRw );

            var actualAquisitionStDev =
                actual.TestValues.FirstOrDefault(
                          tv => tv.TestValueType == TestValueType.StandardDeviation && tv.IncontinencePadTestValue.TestType == TestTypeIncontinencePad.AcquisitionTimeAndRewet );
            Assert.NotNull( actualAquisitionStDev );
            Assert.Equal( 0, actualAquisitionStDev.IncontinencePadTestValue.AcquisitionWeight );
            Assert.Equal( 0, actualAquisitionStDev.IncontinencePadTestValue.AcquisitionTimeFirst );
            Assert.Equal( 0, actualAquisitionStDev.IncontinencePadTestValue.AcquisitionTimeSecond );
            Assert.Equal( 0, actualAquisitionStDev.IncontinencePadTestValue.AcquisitionTimeThird );
            Assert.Equal( 0, actualAquisitionStDev.IncontinencePadTestValue.RewetAfterAcquisitionTimeDryWeight );
            Assert.Equal( 0, actualAquisitionStDev.IncontinencePadTestValue.RewetAfterAcquisitionTimeWetWeight );
            Assert.Equal( 0, actualAquisitionStDev.IncontinencePadTestValue.RewetAfterAcquisitionTimeWeightDifference );
        }

        #endregion
    }
}