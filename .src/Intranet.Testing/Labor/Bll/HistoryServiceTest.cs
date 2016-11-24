#region Usings

using System.Collections.Generic;
using System.Linq;
using Intranet.Common;
using Intranet.Labor.Model;
using Intranet.Labor.Model.labor;
using Intranet.Labor.TestEnvironment;
using Xunit;

#endregion

namespace Intranet.Labor.Bll.Test
{
    /// <summary>
    ///     Class representing Tests for HistoryService
    /// </summary>
    public class HistoryServiceTest
    {
        /// <summary>
        ///     Test GetHistoryViewModel Method if the DB fails
        /// </summary>
        [Fact]
        public void GetHistoryViewModelDbFailTest()
        {
            var historyBll =
                MockHelperBll.GetHistoryBll(
                    null
                );

            var target = new HistoryService( new NLogLoggerFactory() )
            {
                HistoryBll = historyBll
            };

            var actual = target.GetHistoryViewModel( "FA123456" );
            Assert.Equal( "Es ist ein Problem aufgetreten. Bitte wenden Sie sich an ihren Administrator", actual.Message );
        }

        /// <summary>
        ///     Test GetHistoryViewModel Method there is no TestSheet with the given FaNr
        /// </summary>
        [Fact]
        public void GetHistoryViewModelNoTestSheetTest()
        {
            var historyBll =
                MockHelperBll.GetHistoryBll(
                    new List<TestSheet>()
                );

            var target = new HistoryService( new NLogLoggerFactory() )
            {
                HistoryBll = historyBll
            };

            var actual = target.GetHistoryViewModel( "FA123456" );
            Assert.Equal( "Kein Eintrag mit Fertigunsnummer FA123456 gefunden.", actual.Message );
        }

        /// <summary>
        ///     Test GetHistoryViewModel Method with one TestSheet
        /// </summary>
        [Fact]
        public void GetHistoryViewModelBaseTest()
        {
            var testSheetsInDb = new List<TestSheet>
            {
                new TestSheet
                {
                    FaNr = "FA123456",
                    ShiftType = ShiftType.Night,
                    ArticleType = ArticleType.BabyDiaper,
                    TestValues = new List<TestValue>
                    {
                        new TestValue
                        {
                            TestValueType = TestValueType.Average,
                            ArticleTestType = ArticleType.BabyDiaper,
                            BabyDiaperTestValue = new BabyDiaperTestValue
                            {
                                TestType = TestTypeBabyDiaper.Retention,
                                RetentionRw = RwType.Ok
                            }
                        }
                    }
                }
            };

            var historyBll =
                MockHelperBll.GetHistoryBll(
                    testSheetsInDb
                );

            var target = new HistoryService(new NLogLoggerFactory())
            {
                HistoryBll = historyBll
            };

            var actual = target.GetHistoryViewModel("FA123456");
            Assert.Equal(1,actual.Sheets.Count);
        }

        /// <summary>
        ///     Test GetHistoryViewModel Method if The TestSheet is Ok
        /// </summary>
        [Fact]
        public void GetHistoryViewModelOneTestSheetOkTest()
        {
            var testSheetsInDb = new List<TestSheet>
            {
                new TestSheet
                {
                    FaNr = "FA123456",
                    ShiftType = ShiftType.Night,
                    ArticleType = ArticleType.BabyDiaper,
                    TestValues = new List<TestValue>
                    {
                        new TestValue
                        {
                            TestValueType = TestValueType.Average,
                            ArticleTestType = ArticleType.BabyDiaper,
                            BabyDiaperTestValue = new BabyDiaperTestValue
                            {
                                TestType = TestTypeBabyDiaper.Retention,
                                RetentionRw = RwType.Ok
                            }
                        },
                        new TestValue
                        {
                            TestValueType = TestValueType.Average,
                            ArticleTestType = ArticleType.BabyDiaper,
                            BabyDiaperTestValue = new BabyDiaperTestValue
                            {
                                TestType = TestTypeBabyDiaper.Rewet,
                                Rewet140Rw = RwType.Ok,
                                Rewet210Rw = RwType.Ok
                            }
                        },
                        new TestValue
                        {
                            TestValueType = TestValueType.Average,
                            ArticleTestType = ArticleType.BabyDiaper,
                            BabyDiaperTestValue = new BabyDiaperTestValue
                            {
                                TestType = TestTypeBabyDiaper.RewetAndPenetrationTime,
                                Rewet140Rw = RwType.Ok,
                                Rewet210Rw = RwType.Ok,
                                PenetrationRwType = RwType.Ok
                            }
                        }
                    }
                }
            };

            var historyBll =
                MockHelperBll.GetHistoryBll(
                    testSheetsInDb
                );

            var target = new HistoryService(new NLogLoggerFactory())
            {
                HistoryBll = historyBll
            };

            var actual = target.GetHistoryViewModel("FA123456");
            Assert.Equal(RwType.Ok, actual.Sheets.First().RwType);
        }

        /// <summary>
        ///     Test GetHistoryViewModel Method if in the Testsheet is Something Worse
        /// </summary>
        [Fact]
        public void GetHistoryViewModelOneTestSheetSomethingWorseTest()
        {
            var testSheetsInDb = new List<TestSheet>
            {
                new TestSheet
                {
                    FaNr = "FA123456",
                    ShiftType = ShiftType.Night,
                    ArticleType = ArticleType.BabyDiaper,
                    TestValues = new List<TestValue>
                    {
                        new TestValue
                        {
                            TestValueType = TestValueType.Average,
                            ArticleTestType = ArticleType.BabyDiaper,
                            BabyDiaperTestValue = new BabyDiaperTestValue
                            {
                                TestType = TestTypeBabyDiaper.Retention,
                                RetentionRw = RwType.Ok
                            }
                        },
                        new TestValue
                        {
                            TestValueType = TestValueType.Average,
                            ArticleTestType = ArticleType.BabyDiaper,
                            BabyDiaperTestValue = new BabyDiaperTestValue
                            {
                                TestType = TestTypeBabyDiaper.Rewet,
                                Rewet140Rw = RwType.SomethingWorse,
                                Rewet210Rw = RwType.Ok
                            }
                        },
                        new TestValue
                        {
                            TestValueType = TestValueType.Average,
                            ArticleTestType = ArticleType.BabyDiaper,
                            BabyDiaperTestValue = new BabyDiaperTestValue
                            {
                                TestType = TestTypeBabyDiaper.RewetAndPenetrationTime,
                                Rewet140Rw = RwType.Ok,
                                Rewet210Rw = RwType.Ok,
                                PenetrationRwType = RwType.Ok
                            }
                        }
                    }
                }
            };

            var historyBll =
                MockHelperBll.GetHistoryBll(
                    testSheetsInDb
                );

            var target = new HistoryService(new NLogLoggerFactory())
            {
                HistoryBll = historyBll
            };

            var actual = target.GetHistoryViewModel("FA123456");
            Assert.Equal(RwType.SomethingWorse, actual.Sheets.First().RwType);
        }

        /// <summary>
        ///     Test GetHistoryViewModel Method if the Testsheet is worse
        /// </summary>
        [Fact]
        public void GetHistoryViewModelOneTestSheetWorseTest()
        {
            var testSheetsInDb = new List<TestSheet>
            {
                new TestSheet
                {
                    FaNr = "FA123456",
                    ShiftType = ShiftType.Night,
                    ArticleType = ArticleType.BabyDiaper,
                    TestValues = new List<TestValue>
                    {
                        new TestValue
                        {
                            TestValueType = TestValueType.Average,
                            ArticleTestType = ArticleType.BabyDiaper,
                            BabyDiaperTestValue = new BabyDiaperTestValue
                            {
                                TestType = TestTypeBabyDiaper.Retention,
                                RetentionRw = RwType.Worse
                            }
                        },
                        new TestValue
                        {
                            TestValueType = TestValueType.Average,
                            ArticleTestType = ArticleType.BabyDiaper,
                            BabyDiaperTestValue = new BabyDiaperTestValue
                            {
                                TestType = TestTypeBabyDiaper.Rewet,
                                Rewet140Rw = RwType.SomethingWorse,
                                Rewet210Rw = RwType.Ok
                            }
                        },
                        new TestValue
                        {
                            TestValueType = TestValueType.Average,
                            ArticleTestType = ArticleType.BabyDiaper,
                            BabyDiaperTestValue = new BabyDiaperTestValue
                            {
                                TestType = TestTypeBabyDiaper.RewetAndPenetrationTime,
                                Rewet140Rw = RwType.Ok,
                                Rewet210Rw = RwType.Ok,
                                PenetrationRwType = RwType.Ok
                            }
                        }
                    }
                }
            };

            var historyBll =
                MockHelperBll.GetHistoryBll(
                    testSheetsInDb
                );

            var target = new HistoryService(new NLogLoggerFactory())
            {
                HistoryBll = historyBll
            };

            var actual = target.GetHistoryViewModel("FA123456");
            Assert.Equal(RwType.Worse, actual.Sheets.First().RwType);
        }
    }
}