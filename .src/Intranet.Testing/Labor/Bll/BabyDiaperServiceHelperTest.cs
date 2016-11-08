using System;
using Intranet.Common;
using Intranet.Labor.Model;
using Intranet.Labor.Model.labor;
using Xunit;

namespace Intranet.Labor.Bll.Test
{
    /// <summary>
    ///     Class representing Tests for BabyDiaperServiceHelper
    /// </summary>
    public class BabyDiaperServiceHelperTest
    {
        /// <summary>
        ///     Tests if CreateProductionCode returns the right string
        /// </summary>
        [Fact]
        public void CreateProductionCodeTest1()
        {
            const String expectedResult = "IT/11/16/";
            var testSheet = new TestSheet
            {
                MachineNr = "M11",
                CreatedDateTime = new DateTime(2016,1,1)
            };
            var babyDiaperServiceHelper = new BabyDiaperServiceHelper( new NLogLoggerFactory() );

            var actual = babyDiaperServiceHelper.CreateProductionCode( testSheet );

            Assert.Equal( expectedResult, actual);
        }

        /// <summary>
        ///     Tests if CreateProductionCode returns the right string
        /// </summary>
        [Fact]
        public void CreateProductionCodeTest2()
        {
            const String expectedResult = "IT/10/17/";
            var testSheet = new TestSheet
            {
                MachineNr = "M10",
                CreatedDateTime = new DateTime(2017, 1, 7)
            };
            var babyDiaperServiceHelper = new BabyDiaperServiceHelper(new NLogLoggerFactory());

            var actual = babyDiaperServiceHelper.CreateProductionCode(testSheet);

            Assert.Equal(expectedResult, actual);
        }


        /// <summary>
        ///     Tests if CreateNewTestValue returns the correct TestSheetRefId
        /// </summary>
        [Fact]
        public void CreateNewTestValueTestSheetRefIdTest()
        {
            var expectedTestValue = new TestValue
            {
                TestSheetRefId = 1,
                CreatedPerson = "Hans",
                DayInYearOfArticleCreation = 123
            };
            var babyDiaperServiceHelper = new BabyDiaperServiceHelper(new NLogLoggerFactory());

            var actual = babyDiaperServiceHelper.CreateNewTestValue( expectedTestValue.TestSheetRefId,expectedTestValue.CreatedPerson,expectedTestValue.DayInYearOfArticleCreation,null );

            Assert.Equal(expectedTestValue.TestSheetRefId, actual.TestSheetRefId);
        }

        /// <summary>
        ///     Tests if CreateNewTestValue returns the correct CreatedPerson
        /// </summary>
        [Fact]
        public void CreateNewTestValueTestCreatedPersonTest()
        {
            var expectedTestValue = new TestValue
            {
                TestSheetRefId = 1,
                CreatedPerson = "Hans",
                DayInYearOfArticleCreation = 123
            };
            var babyDiaperServiceHelper = new BabyDiaperServiceHelper(new NLogLoggerFactory());

            var actual = babyDiaperServiceHelper.CreateNewTestValue(expectedTestValue.TestSheetRefId, expectedTestValue.CreatedPerson, expectedTestValue.DayInYearOfArticleCreation, null);

            Assert.Equal(expectedTestValue.CreatedPerson, actual.CreatedPerson);
        }

        /// <summary>
        ///     Tests if CreateNewTestValue returns the correct LastEditedPerson
        /// </summary>
        [Fact]
        public void CreateNewTestValueLastEditedPersonTest()
        {
            var expectedTestValue = new TestValue
            {
                TestSheetRefId = 1,
                CreatedPerson = "Hans",
                DayInYearOfArticleCreation = 123
            };
            var babyDiaperServiceHelper = new BabyDiaperServiceHelper(new NLogLoggerFactory());

            var actual = babyDiaperServiceHelper.CreateNewTestValue(expectedTestValue.TestSheetRefId, expectedTestValue.CreatedPerson, expectedTestValue.DayInYearOfArticleCreation, null);

            Assert.Equal(expectedTestValue.CreatedPerson, actual.LastEditedPerson);
        }

        /// <summary>
        ///     Tests if CreateNewTestValue returns the correct DayInYearOfArticleCreation
        /// </summary>
        [Fact]
        public void CreateNewTestValueTestDayInYearOfArticleCreationTest()
        {
            var expectedTestValue = new TestValue
            {
                TestSheetRefId = 1,
                CreatedPerson = "Hans",
                DayInYearOfArticleCreation = 123
            };
            var babyDiaperServiceHelper = new BabyDiaperServiceHelper(new NLogLoggerFactory());

            var actual = babyDiaperServiceHelper.CreateNewTestValue(expectedTestValue.TestSheetRefId, expectedTestValue.CreatedPerson, expectedTestValue.DayInYearOfArticleCreation, null);

            Assert.Equal(expectedTestValue.DayInYearOfArticleCreation, actual.DayInYearOfArticleCreation);
        }

        /// <summary>
        ///     Tests if CreateNewTestValue returns the correct ArticleTestType
        /// </summary>
        [Fact]
        public void CreateNewTestValueTestArticleTestTypeTest()
        {
            var expectedTestValue = new TestValue
            {
                TestSheetRefId = 1,
                CreatedPerson = "Hans",
                DayInYearOfArticleCreation = 123
            };
            var babyDiaperServiceHelper = new BabyDiaperServiceHelper(new NLogLoggerFactory());

            var actual = babyDiaperServiceHelper.CreateNewTestValue(expectedTestValue.TestSheetRefId, expectedTestValue.CreatedPerson, expectedTestValue.DayInYearOfArticleCreation, null);

            Assert.Equal(ArticleType.BabyDiaper, actual.ArticleTestType);
        }

        /// <summary>
        ///     Tests if CreateNewTestValue returns an TestValue where CreatedPerson equals LastEditedPerson
        /// </summary>
        [Fact]
        public void CreateNewTestValueTestCreatedAndLastEditedPersonTest()
        {
            var expectedTestValue = new TestValue
            {
                TestSheetRefId = 1,
                CreatedPerson = "Hans",
                DayInYearOfArticleCreation = 123
            };
            var babyDiaperServiceHelper = new BabyDiaperServiceHelper(new NLogLoggerFactory());

            var actual = babyDiaperServiceHelper.CreateNewTestValue(expectedTestValue.TestSheetRefId, expectedTestValue.CreatedPerson, expectedTestValue.DayInYearOfArticleCreation, null);

            Assert.Equal(actual.CreatedPerson, actual.LastEditedPerson);
        }

        /// <summary>
        ///     Tests if CreateNewTestValue returns an TestValue where CreatedPerson equals LastEditedPerson
        /// </summary>
        [Fact]
        public void CreateNewTestValueTestNullNotesTest()
        {
            var expectedTestValue = new TestValue
            {
                TestSheetRefId = 1,
                CreatedPerson = "Hans",
                DayInYearOfArticleCreation = 123
            };
            var babyDiaperServiceHelper = new BabyDiaperServiceHelper(new NLogLoggerFactory());

            var actual = babyDiaperServiceHelper.CreateNewTestValue(expectedTestValue.TestSheetRefId, expectedTestValue.CreatedPerson, expectedTestValue.DayInYearOfArticleCreation, null);

            Assert.Equal(null, actual.TestValueNote);
        }

        //TODO, TEST TESTVALUENOTES
    }
}
