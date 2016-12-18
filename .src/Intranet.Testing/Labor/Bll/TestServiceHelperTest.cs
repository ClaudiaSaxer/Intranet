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
    ///     Class representing Tests for TestServiceHelper
    /// </summary>
    public class TestServiceHelperTest
    {
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
            var testServiceHelper = new TestServiceHelper( new NLogLoggerFactory() );

            var actual = testServiceHelper.CreateNewTestValue( expectedTestValue.TestSheetRefId, expectedTestValue.CreatedPerson, expectedTestValue.DayInYearOfArticleCreation, null );

            Assert.Equal( expectedTestValue.CreatedPerson, actual.LastEditedPerson );
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
            var testServiceHelper = new TestServiceHelper( new NLogLoggerFactory() );

            var actual = testServiceHelper.CreateNewTestValue( expectedTestValue.TestSheetRefId, expectedTestValue.CreatedPerson, expectedTestValue.DayInYearOfArticleCreation, null );

            Assert.Equal( ArticleType.BabyDiaper, actual.ArticleTestType );
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
            var testServiceHelper = new TestServiceHelper( new NLogLoggerFactory() );

            var actual = testServiceHelper.CreateNewTestValue( expectedTestValue.TestSheetRefId, expectedTestValue.CreatedPerson, expectedTestValue.DayInYearOfArticleCreation, null );

            Assert.Equal( actual.CreatedPerson, actual.LastEditedPerson );
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
            var testServiceHelper = new TestServiceHelper( new NLogLoggerFactory() );

            var actual = testServiceHelper.CreateNewTestValue( expectedTestValue.TestSheetRefId, expectedTestValue.CreatedPerson, expectedTestValue.DayInYearOfArticleCreation, null );

            Assert.Equal( expectedTestValue.CreatedPerson, actual.CreatedPerson );
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
            var testServiceHelper = new TestServiceHelper( new NLogLoggerFactory() );

            var actual = testServiceHelper.CreateNewTestValue( expectedTestValue.TestSheetRefId, expectedTestValue.CreatedPerson, expectedTestValue.DayInYearOfArticleCreation, null );

            Assert.Equal( expectedTestValue.DayInYearOfArticleCreation, actual.DayInYearOfArticleCreation );
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
            var testServiceHelper = new TestServiceHelper( new NLogLoggerFactory() );

            var actual = testServiceHelper.CreateNewTestValue( expectedTestValue.TestSheetRefId, expectedTestValue.CreatedPerson, expectedTestValue.DayInYearOfArticleCreation, null );

            Assert.Equal( null, actual.TestValueNote );
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
            var testServiceHelper = new TestServiceHelper( new NLogLoggerFactory() );

            var actual = testServiceHelper.CreateNewTestValue( expectedTestValue.TestSheetRefId, expectedTestValue.CreatedPerson, expectedTestValue.DayInYearOfArticleCreation, null );

            Assert.Equal( expectedTestValue.TestSheetRefId, actual.TestSheetRefId );
        }

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
                CreatedDateTime = new DateTime( 2016, 1, 1 )
            };
            var testServiceHelper = new TestServiceHelper( new NLogLoggerFactory() );

            var actual = testServiceHelper.CreateProductionCode( testSheet );

            Assert.Equal( expectedResult, actual );
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
                CreatedDateTime = new DateTime( 2017, 1, 7 )
            };
            var testServiceHelper = new TestServiceHelper( new NLogLoggerFactory() );

            var actual = testServiceHelper.CreateProductionCode( testSheet );

            Assert.Equal( expectedResult, actual );
        }

        #region UpdateNotes Test

        /// <summary>
        ///     Tests if Null from the viewModel puts an new empty List on TestValue;
        /// </summary>
        [Fact]
        public void UpdateNotesNullNotesTest()
        {
            var expectedTestValue = new TestValue
            {
                TestValueNote = new List<TestValueNote>()
            };
            var testServiceHelper = new TestServiceHelper( new NLogLoggerFactory() );

            testServiceHelper.UpdateNotes( null, expectedTestValue );

            Assert.Equal( 0, expectedTestValue.TestValueNote.Count );
        }

        /// <summary>
        ///     Tests when the viewmodel has one note, the testvalue will have also one
        /// </summary>
        [Fact]
        public void UpdateNotesOneNoteTest()
        {
            var notes = new List<TestNote>
            {
                new TestNote { ErrorCodeId = 1, Id = 0, Message = "Testmessage" }
            };

            var testValue = new TestValue
            {
                TestValueNote = new List<TestValueNote>()
            };
            var testServiceHelper = new TestServiceHelper( new NLogLoggerFactory() );

            testServiceHelper.UpdateNotes( notes, testValue );

            Assert.Equal( 1, testValue.TestValueNote.Count );
        }

        /// <summary>
        ///     Tests if an existing Note will be updated
        /// </summary>
        [Fact]
        public void UpdateNotesUpdateExistingNoteTest()
        {
            var notes = new List<TestNote>
            {
                new TestNote { ErrorCodeId = 1, Id = 1, Message = "New Message" }
            };

            var testValue = new TestValue
            {
                TestValueNote = new List<TestValueNote>
                {
                    new TestValueNote
                    {
                        ErrorRefId = 2,
                        TestValueNoteId = 1,
                        Message = "Old Message"
                    }
                }
            };
            var testServiceHelper = new TestServiceHelper( new NLogLoggerFactory() );

            testServiceHelper.UpdateNotes( notes, testValue );

            Assert.Equal( 1, testValue.TestValueNote.Count );
            Assert.Equal( "New Message",
                          testValue.TestValueNote.First()
                                   .Message );
            Assert.Equal( 1,
                          testValue.TestValueNote.First()
                                   .ErrorRefId );
        }

        /// <summary>
        ///     Tests if an existing Note will be deleted
        /// </summary>
        [Fact]
        public void UpdateNotesDeleteNoteTest()
        {
            var notes = new List<TestNote>
            {
                new TestNote { ErrorCodeId = 0, Id = 1, Message = "TestMessage" }
            };

            var testValue = new TestValue
            {
                TestValueNote = new List<TestValueNote>
                {
                    new TestValueNote
                    {
                        ErrorRefId = 2,
                        TestValueNoteId = 1,
                        Message = "TestMessage"
                    }
                }
            };

            var testBll = MockHelperBll.GetTestBllForDeletingNotes();

            var testServiceHelper = new TestServiceHelper( new NLogLoggerFactory() )
            {
                TestBll = testBll
            };

            testServiceHelper.UpdateNotes( notes, testValue );

            Assert.Equal( 0, testValue.TestValueNote.Count );
        }

        #endregion
    }
}