#region Usings

using System.Collections.Generic;
using System.Linq;
using Intranet.Labor.Model;
using Intranet.Labor.TestEnvironment;
using Xunit;

#endregion

namespace Intranet.Labor.Bll.Test
{
    /// <summary>
    ///     Test Class for HistoryBll
    /// </summary>
    public class HistoryBllTest
    {
        /// <summary>
        ///     Test when it doesnt have Testsheets with the FaNr you are looking for
        /// </summary>
        [Fact]
        public void GetTestSheetsNoSheetTest()
        {
            var testSheets = new List<TestSheet>();
            var testSheetRepository =
                MockHelperBll.GetTestSheetRepositoryForHistory(
                    testSheets.AsQueryable()
                );

            var target = new HistoryBll
            {
                TestSheetRepository = testSheetRepository
            };

            var actual = target.GetTestSheets( "FA123456" );

            Assert.Equal( 0,
                          actual.ToList()
                                .Count );
        }

        /// <summary>
        ///     Test when it has one Testsheet (of total 2) with the FaNr you are looking for
        /// </summary>
        [Fact]
        public void GetTestSheetsOneOfTwoSheetTest()
        {
            var testSheets = new List<TestSheet>
            {
                new TestSheet { FaNr = "FA654321" },
                new TestSheet { FaNr = "FA123456" }
            };
            var testSheetRepository =
                MockHelperBll.GetTestSheetRepositoryForHistory(
                    testSheets.AsQueryable()
                );

            var target = new HistoryBll
            {
                TestSheetRepository = testSheetRepository
            };

            var actual = target.GetTestSheets( "FA123456" );

            Assert.Equal( 1,
                          actual.ToList()
                                .Count );
        }

        /// <summary>
        ///     Test when it has one Testsheet with the FaNr you are looking for
        /// </summary>
        [Fact]
        public void GetTestSheetsOneSheetTest()
        {
            var testSheets = new List<TestSheet>
            {
                new TestSheet { FaNr = "FA123456" }
            };
            var testSheetRepository =
                MockHelperBll.GetTestSheetRepositoryForHistory(
                    testSheets.AsQueryable()
                );

            var target = new HistoryBll
            {
                TestSheetRepository = testSheetRepository
            };

            var actual = target.GetTestSheets( "FA123456" );

            Assert.Equal( 1,
                          actual.ToList()
                                .Count );
        }

        /// <summary>
        ///     Test when it has two Testsheets with the FaNr you are looking for
        /// </summary>
        [Fact]
        public void GetTestSheetsTwoSheetTest()
        {
            var testSheets = new List<TestSheet>
            {
                new TestSheet { FaNr = "FA123456" },
                new TestSheet { FaNr = "FA123456" }
            };
            var testSheetRepository =
                MockHelperBll.GetTestSheetRepositoryForHistory(
                    testSheets.AsQueryable()
                );

            var target = new HistoryBll
            {
                TestSheetRepository = testSheetRepository
            };

            var actual = target.GetTestSheets( "FA123456" );

            Assert.Equal( 2,
                          actual.ToList()
                                .Count );
        }
    }
}