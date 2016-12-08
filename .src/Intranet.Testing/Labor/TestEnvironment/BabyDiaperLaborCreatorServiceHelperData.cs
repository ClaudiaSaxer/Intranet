using System.Collections.Generic;
using Intranet.Labor.Model;

namespace Intranet.Labor.TestEnvironment
{
    /// <summary>
    ///     Test Data Helper Class for Labor Creator Service Test
    /// </summary>
    public class BabyDiaperLaborCreatorServiceHelperData
    {
        /// <summary>
        ///     Testdata
        /// </summary>
        /// <returns>TestValues</returns>
        public static List<TestValue> TestValuesEmptyPenetration2()
            => new List<TestValue>
            {
                new TestValue
                {
                    TestValueType = TestValueType.Single,
                    ArticleTestType = ArticleType.BabyDiaper,
                    BabyDiaperTestValue = new BabyDiaperTestValue { TestType = TestTypeBabyDiaper.Rewet }
                },
                new TestValue
                {
                    TestValueType = TestValueType.Single,
                    ArticleTestType = ArticleType.BabyDiaper,
                    BabyDiaperTestValue = new BabyDiaperTestValue { TestType = TestTypeBabyDiaper.Retention }
                }
            };

        /// <summary>
        ///     Testdata
        /// </summary>
        /// <returns>TestValues</returns>
        public static List<TestValue> TestValuesEmptyRetention2()
            => new List<TestValue>
            {
                new TestValue
                {
                    TestValueType = TestValueType.Single,
                    ArticleTestType = ArticleType.BabyDiaper,
                    BabyDiaperTestValue = new BabyDiaperTestValue { TestType = TestTypeBabyDiaper.Rewet }
                },
                new TestValue
                {
                    TestValueType = TestValueType.Single,
                    ArticleTestType = ArticleType.BabyDiaper,
                    BabyDiaperTestValue = new BabyDiaperTestValue { TestType = TestTypeBabyDiaper.RewetAndPenetrationTime }
                }
            };

        /// <summary>
        ///     Testdata
        /// </summary>
        /// <returns>TestValues</returns>
        public static List<TestValue> TestValuesEmptyRewet2()
            => new List<TestValue>
            {
                new TestValue
                {
                    TestValueType = TestValueType.Single,
                    ArticleTestType = ArticleType.BabyDiaper,
                    BabyDiaperTestValue =
                        new BabyDiaperTestValue
                        {
                            TestType = TestTypeBabyDiaper.RewetAndPenetrationTime
                        }
                },
                new TestValue
                {
                    TestValueType = TestValueType.Single,
                    ArticleTestType = ArticleType.BabyDiaper,
                    BabyDiaperTestValue =
                        new BabyDiaperTestValue
                        {
                            TestType = TestTypeBabyDiaper.RewetAndPenetrationTime
                        }
                }
            };

        /// <summary>
        ///     Testdata
        /// </summary>
        /// <returns>TestValues</returns>
        public static List<TestValue> TestValuesManyPenetrationTimeAverage()
            => new List<TestValue>
            {
                new TestValue
                {
                    TestValueType = TestValueType.Average,
                    ArticleTestType = ArticleType.BabyDiaper,
                    BabyDiaperTestValue =
                        new BabyDiaperTestValue
                        {
                            TestType = TestTypeBabyDiaper.RewetAndPenetrationTime
                        }
                },
                new TestValue
                {
                    TestValueType = TestValueType.Average,
                    ArticleTestType = ArticleType.BabyDiaper,
                    BabyDiaperTestValue =
                        new BabyDiaperTestValue
                        {
                            TestType = TestTypeBabyDiaper.RewetAndPenetrationTime
                        }
                }
            };

        /// <summary>
        ///     Testdata
        /// </summary>
        /// <returns>TestValues</returns>
        public static List<TestValue> TestValuesManyPenetrationTimeStandardDeviation()
            => new List<TestValue>
            {
                new TestValue
                {
                    TestValueType = TestValueType.StandardDeviation,
                    ArticleTestType = ArticleType.BabyDiaper,
                    BabyDiaperTestValue =
                        new BabyDiaperTestValue
                        {
                            TestType = TestTypeBabyDiaper.RewetAndPenetrationTime
                        }
                },
                new TestValue
                {
                    TestValueType = TestValueType.StandardDeviation,
                    ArticleTestType = ArticleType.BabyDiaper,
                    BabyDiaperTestValue =
                        new BabyDiaperTestValue
                        {
                            TestType = TestTypeBabyDiaper.RewetAndPenetrationTime
                        }
                }
            };

        /// <summary>
        ///     Testdata
        /// </summary>
        /// <returns>TestValues</returns>
        public static List<TestValue> TestValuesManyRententionSingle()
            => new List<TestValue>
            {
                new TestValue
                {
                    TestValueType = TestValueType.Single,
                    ArticleTestType = ArticleType.BabyDiaper,
                    BabyDiaperTestValue =
                        new BabyDiaperTestValue
                        {
                            TestType = TestTypeBabyDiaper.Retention
                        }
                },
                new TestValue
                {
                    TestValueType = TestValueType.Single,
                    ArticleTestType = ArticleType.BabyDiaper,
                    BabyDiaperTestValue =
                        new BabyDiaperTestValue
                        {
                            TestType = TestTypeBabyDiaper.Retention
                        }
                }
            };

        /// <summary>
        ///     Testdata
        /// </summary>
        /// <returns>TestValues</returns>
        public static List<TestValue> TestValuesManyRetentionAverage()
            => new List<TestValue>
            {
                new TestValue
                {
                    TestValueType = TestValueType.Average,
                    ArticleTestType = ArticleType.BabyDiaper,
                    BabyDiaperTestValue =
                        new BabyDiaperTestValue
                        {
                            TestType = TestTypeBabyDiaper.Retention
                        }
                },
                new TestValue
                {
                    TestValueType = TestValueType.Average,
                    ArticleTestType = ArticleType.BabyDiaper,
                    BabyDiaperTestValue =
                        new BabyDiaperTestValue
                        {
                            TestType = TestTypeBabyDiaper.Retention
                        }
                }
            };

        /// <summary>
        ///     Testdata
        /// </summary>
        /// <returns>TestValues</returns>
        public static List<TestValue> TestValuesManyRetentionStandardDeviation()
            => new List<TestValue>
            {
                new TestValue
                {
                    TestValueType = TestValueType.StandardDeviation,
                    ArticleTestType = ArticleType.BabyDiaper,
                    BabyDiaperTestValue =
                        new BabyDiaperTestValue
                        {
                            TestType = TestTypeBabyDiaper.Retention
                        }
                },
                new TestValue
                {
                    TestValueType = TestValueType.StandardDeviation,
                    ArticleTestType = ArticleType.BabyDiaper,
                    BabyDiaperTestValue =
                        new BabyDiaperTestValue
                        {
                            TestType = TestTypeBabyDiaper.Retention
                        }
                }
            };

        /// <summary>
        ///     Testdata
        /// </summary>
        /// <returns>TestValues</returns>
        public static List<TestValue> TestValuesManyRewetAverage()
            => new List<TestValue>
            {
                new TestValue
                {
                    TestValueType = TestValueType.Average,
                    ArticleTestType = ArticleType.BabyDiaper,
                    BabyDiaperTestValue =
                        new BabyDiaperTestValue { TestType = TestTypeBabyDiaper.Rewet }
                },
                new TestValue
                {
                    TestValueType = TestValueType.Average,
                    ArticleTestType = ArticleType.BabyDiaper,
                    BabyDiaperTestValue =
                        new BabyDiaperTestValue { TestType = TestTypeBabyDiaper.Rewet }
                }
            };

        /// <summary>
        ///     Testdata
        /// </summary>
        /// <returns>TestValues</returns>
        public static List<TestValue> TestValuesManyRewetSingle()
            => new List<TestValue>
            {
                new TestValue
                {
                    TestValueType = TestValueType.Single,
                    ArticleTestType = ArticleType.BabyDiaper,
                    BabyDiaperTestValue =
                        new BabyDiaperTestValue { TestType = TestTypeBabyDiaper.Rewet }
                },
                new TestValue
                {
                    TestValueType = TestValueType.Single,
                    ArticleTestType = ArticleType.BabyDiaper,
                    BabyDiaperTestValue =
                        new BabyDiaperTestValue { TestType = TestTypeBabyDiaper.Rewet }
                }
            };

        /// <summary>
        ///     Testdata
        /// </summary>
        /// <returns>TestValues</returns>
        public static List<TestValue> TestValuesManyRewetStandardDeviation()
            => new List<TestValue>
            {
                new TestValue
                {
                    TestValueType = TestValueType.StandardDeviation,
                    ArticleTestType = ArticleType.BabyDiaper,
                    BabyDiaperTestValue =
                        new BabyDiaperTestValue { TestType = TestTypeBabyDiaper.Rewet }
                },
                new TestValue
                {
                    TestValueType = TestValueType.StandardDeviation,
                    ArticleTestType = ArticleType.BabyDiaper,
                    BabyDiaperTestValue =
                        new BabyDiaperTestValue { TestType = TestTypeBabyDiaper.Rewet }
                }
            };

        /// <summary>
        ///     Testdata
        /// </summary>
        /// <returns>TestValues</returns>
        public static List<TestValue> TestValuesOkPenetrationTimeAverage( BabyDiaperTestValue expected )
            => new List<TestValue>
            {
                new TestValue
                {
                    TestValueType = TestValueType.Average,
                    ArticleTestType = ArticleType.BabyDiaper,
                    BabyDiaperTestValue = new BabyDiaperTestValue { TestType = TestTypeBabyDiaper.Rewet }
                },
                new TestValue
                {
                    TestValueType = TestValueType.Average,
                    ArticleTestType = ArticleType.BabyDiaper,
                    BabyDiaperTestValue = expected
                },
                new TestValue
                {
                    TestValueType = TestValueType.Average,
                    ArticleTestType = ArticleType.BabyDiaper,
                    BabyDiaperTestValue = new BabyDiaperTestValue { TestType = TestTypeBabyDiaper.Retention }
                }
            };

        /// <summary>
        ///     Testdata
        /// </summary>
        /// <returns>TestValues</returns>
        public static List<TestValue> TestValuesOkPenetrationTimeSingle( BabyDiaperTestValue expected )
            => new List<TestValue>
            {
                new TestValue
                {
                    TestValueType = TestValueType.Single,
                    ArticleTestType = ArticleType.BabyDiaper,
                    BabyDiaperTestValue = new BabyDiaperTestValue { TestType = TestTypeBabyDiaper.Rewet }
                },
                new TestValue
                {
                    TestValueType = TestValueType.Single,
                    ArticleTestType = ArticleType.BabyDiaper,
                    BabyDiaperTestValue = expected
                },
                new TestValue
                {
                    TestValueType = TestValueType.Single,
                    ArticleTestType = ArticleType.BabyDiaper,
                    BabyDiaperTestValue = new BabyDiaperTestValue { TestType = TestTypeBabyDiaper.Retention }
                }
            };

        /// <summary>
        ///     Testdata
        /// </summary>
        /// <returns>TestValues</returns>
        public static List<TestValue> TestValuesOKPenetrationTimeStandardDeviation( BabyDiaperTestValue expected )
            => new List<TestValue>
            {
                new TestValue
                {
                    TestValueType = TestValueType.StandardDeviation,
                    ArticleTestType = ArticleType.BabyDiaper,
                    BabyDiaperTestValue = new BabyDiaperTestValue { TestType = TestTypeBabyDiaper.Rewet }
                },
                new TestValue
                {
                    TestValueType = TestValueType.StandardDeviation,
                    ArticleTestType = ArticleType.BabyDiaper,
                    BabyDiaperTestValue = expected
                },
                new TestValue
                {
                    TestValueType = TestValueType.StandardDeviation,
                    ArticleTestType = ArticleType.BabyDiaper,
                    BabyDiaperTestValue = new BabyDiaperTestValue { TestType = TestTypeBabyDiaper.Retention }
                }
            };

        /// <summary>
        ///     Testdata
        /// </summary>
        /// <returns>TestValues</returns>
        public static List<TestValue> TestValuesOkRetentionAverage( BabyDiaperTestValue expected )
            => new List<TestValue>
            {
                new TestValue
                {
                    TestValueType = TestValueType.Average,
                    ArticleTestType = ArticleType.BabyDiaper,
                    BabyDiaperTestValue = new BabyDiaperTestValue { TestType = TestTypeBabyDiaper.RewetAndPenetrationTime }
                },
                new TestValue
                {
                    TestValueType = TestValueType.Average,
                    ArticleTestType = ArticleType.BabyDiaper,
                    BabyDiaperTestValue = expected
                },
                new TestValue
                {
                    TestValueType = TestValueType.Average,
                    ArticleTestType = ArticleType.BabyDiaper,
                    BabyDiaperTestValue = new BabyDiaperTestValue { TestType = TestTypeBabyDiaper.Rewet, Rewet210Rw = RwType.Ok, Rewet140Rw = RwType.Ok }
                }
            };

        /// <summary>
        ///     Testdata
        /// </summary>
        /// <returns>TestValues</returns>
        public static List<TestValue> TestValuesOkRetentionStandardDeviation( BabyDiaperTestValue expected )
            => new List<TestValue>
            {
                new TestValue
                {
                    TestValueType = TestValueType.StandardDeviation,
                    ArticleTestType = ArticleType.BabyDiaper,
                    BabyDiaperTestValue = new BabyDiaperTestValue { TestType = TestTypeBabyDiaper.RewetAndPenetrationTime }
                },
                new TestValue
                {
                    TestValueType = TestValueType.StandardDeviation,
                    ArticleTestType = ArticleType.BabyDiaper,
                    BabyDiaperTestValue = expected
                },
                new TestValue
                {
                    TestValueType = TestValueType.StandardDeviation,
                    ArticleTestType = ArticleType.BabyDiaper,
                    BabyDiaperTestValue = new BabyDiaperTestValue { TestType = TestTypeBabyDiaper.Rewet }
                }
            };

        /// <summary>
        ///     Testdata
        /// </summary>
        /// <returns>TestValues</returns>
        public static List<TestValue> TestValuesOkRetentionTimeSingle( BabyDiaperTestValue expected )
            => new List<TestValue>
            {
                new TestValue
                {
                    TestValueType = TestValueType.Single,
                    ArticleTestType = ArticleType.BabyDiaper,
                    BabyDiaperTestValue = new BabyDiaperTestValue { TestType = TestTypeBabyDiaper.RewetAndPenetrationTime }
                },
                new TestValue
                {
                    TestValueType = TestValueType.Single,
                    ArticleTestType = ArticleType.BabyDiaper,
                    BabyDiaperTestValue = expected
                },
                new TestValue
                {
                    TestValueType = TestValueType.Single,
                    ArticleTestType = ArticleType.BabyDiaper,
                    BabyDiaperTestValue = new BabyDiaperTestValue { TestType = TestTypeBabyDiaper.Rewet }
                }
            };

        /// <summary>
        ///     Testdata
        /// </summary>
        /// <returns>TestValues</returns>
        public static List<TestValue> TestValuesOkRewetAverage( BabyDiaperTestValue expected )
            => new List<TestValue>
            {
                new TestValue
                {
                    TestValueType = TestValueType.Average,
                    ArticleTestType = ArticleType.BabyDiaper,
                    BabyDiaperTestValue = new BabyDiaperTestValue { TestType = TestTypeBabyDiaper.RewetAndPenetrationTime }
                },
                new TestValue
                {
                    TestValueType = TestValueType.Average,
                    ArticleTestType = ArticleType.BabyDiaper,
                    BabyDiaperTestValue = expected
                },
                new TestValue
                {
                    TestValueType = TestValueType.Average,
                    ArticleTestType = ArticleType.BabyDiaper,
                    BabyDiaperTestValue = new BabyDiaperTestValue { TestType = TestTypeBabyDiaper.Retention }
                }
            };

        /// <summary>
        ///     Testdata
        /// </summary>
        /// <returns>TestValues</returns>
        public static List<TestValue> TestValuesOkRewetSingle( BabyDiaperTestValue expected )
            => new List<TestValue>
            {
                new TestValue
                {
                    TestValueType = TestValueType.Single,
                    ArticleTestType = ArticleType.BabyDiaper,
                    BabyDiaperTestValue = new BabyDiaperTestValue { TestType = TestTypeBabyDiaper.RewetAndPenetrationTime }
                },
                new TestValue
                {
                    TestValueType = TestValueType.Single,
                    ArticleTestType = ArticleType.BabyDiaper,
                    BabyDiaperTestValue = expected
                },
                new TestValue
                {
                    TestValueType = TestValueType.Single,
                    ArticleTestType = ArticleType.BabyDiaper,
                    BabyDiaperTestValue = new BabyDiaperTestValue { TestType = TestTypeBabyDiaper.Retention }
                }
            };

        /// <summary>
        ///     Testdata
        /// </summary>
        /// <returns>TestValues</returns>
        public static List<TestValue> TestValuesOkRewetStandardDeviation( BabyDiaperTestValue expected )
            => new List<TestValue>
            {
                new TestValue
                {
                    TestValueType = TestValueType.StandardDeviation,
                    ArticleTestType = ArticleType.BabyDiaper,
                    BabyDiaperTestValue = new BabyDiaperTestValue { TestType = TestTypeBabyDiaper.RewetAndPenetrationTime }
                },
                new TestValue
                {
                    TestValueType = TestValueType.StandardDeviation,
                    ArticleTestType = ArticleType.BabyDiaper,
                    BabyDiaperTestValue = expected
                },
                new TestValue
                {
                    TestValueType = TestValueType.StandardDeviation,
                    ArticleTestType = ArticleType.BabyDiaper,
                    BabyDiaperTestValue = new BabyDiaperTestValue { TestType = TestTypeBabyDiaper.Retention }
                }
            };

        /// <summary>
        ///     Testdata
        /// </summary>
        /// <returns>TestValues</returns>
        public static List<TestValue> TestValuesPenetrationTimeManySingle() => new List<TestValue>
        {
            new TestValue
            {
                TestValueType = TestValueType.Single,
                ArticleTestType = ArticleType.BabyDiaper,
                BabyDiaperTestValue =
                    new BabyDiaperTestValue
                    {
                        TestType = TestTypeBabyDiaper.RewetAndPenetrationTime
                    }
            },
            new TestValue
            {
                TestValueType = TestValueType.Single,
                ArticleTestType = ArticleType.BabyDiaper,
                BabyDiaperTestValue =
                    new BabyDiaperTestValue
                    {
                        TestType = TestTypeBabyDiaper.RewetAndPenetrationTime
                    }
            }
        };

        /// <summary>
        ///     Testdata
        /// </summary>
        /// <returns>TestValues</returns>
        public static List<TestValue> TwoTestValuePerType()
            => new List<TestValue>
            {
                new TestValue
                {
                    TestValueType = TestValueType.Single,
                    ArticleTestType = ArticleType.BabyDiaper,
                    BabyDiaperTestValue = new BabyDiaperTestValue { TestType = TestTypeBabyDiaper.Retention, RetentionRw = RwType.Better },
                    TestSheet = new TestSheet { MachineNr = "M12" }
                },
                new TestValue
                {
                    TestValueType = TestValueType.Single,
                    ArticleTestType = ArticleType.BabyDiaper,
                    BabyDiaperTestValue = new BabyDiaperTestValue { TestType = TestTypeBabyDiaper.Retention, RetentionRw = RwType.Better },
                    TestSheet = new TestSheet { MachineNr = "M12" }
                },
                new TestValue
                {
                    TestValueType = TestValueType.Single,
                    ArticleTestType = ArticleType.BabyDiaper,
                    BabyDiaperTestValue = new BabyDiaperTestValue { TestType = TestTypeBabyDiaper.Rewet, Rewet140Rw = RwType.Better, Rewet210Rw = RwType.Better },
                    TestSheet = new TestSheet { MachineNr = "M12" }
                },
                new TestValue
                {
                    TestValueType = TestValueType.Single,
                    ArticleTestType = ArticleType.BabyDiaper,
                    BabyDiaperTestValue = new BabyDiaperTestValue { TestType = TestTypeBabyDiaper.Rewet, Rewet140Rw = RwType.Better, Rewet210Rw = RwType.Better },
                    TestSheet = new TestSheet { MachineNr = "M12" }
                },
                new TestValue
                {
                    TestValueType = TestValueType.Single,
                    ArticleTestType = ArticleType.BabyDiaper,
                    BabyDiaperTestValue =
                        new BabyDiaperTestValue
                        {
                            TestType = TestTypeBabyDiaper.RewetAndPenetrationTime,
                            Rewet140Rw = RwType.Better,
                            Rewet210Rw = RwType.Better,
                            PenetrationRwType = RwType.Better
                        },
                    TestSheet = new TestSheet { MachineNr = "M12" }
                },
                new TestValue
                {
                    TestValueType = TestValueType.Single,
                    ArticleTestType = ArticleType.BabyDiaper,
                    BabyDiaperTestValue =
                        new BabyDiaperTestValue
                        {
                            TestType = TestTypeBabyDiaper.RewetAndPenetrationTime,
                            Rewet140Rw = RwType.Better,
                            Rewet210Rw = RwType.Better,
                            PenetrationRwType = RwType.Better
                        },
                    TestSheet = new TestSheet { MachineNr = "M12" }
                },
                new TestValue
                {
                    TestValueType = TestValueType.Average,
                    ArticleTestType = ArticleType.BabyDiaper,
                    BabyDiaperTestValue = new BabyDiaperTestValue { TestType = TestTypeBabyDiaper.Retention, RetentionRw = RwType.Better },
                    TestSheet = new TestSheet { MachineNr = "M12" }
                },
                new TestValue
                {
                    TestValueType = TestValueType.StandardDeviation,
                    ArticleTestType = ArticleType.BabyDiaper,
                    BabyDiaperTestValue = new BabyDiaperTestValue { TestType = TestTypeBabyDiaper.Retention, RetentionRw = RwType.Better },
                    TestSheet = new TestSheet { MachineNr = "M12" }
                },
                new TestValue
                {
                    TestValueType = TestValueType.Average,
                    ArticleTestType = ArticleType.BabyDiaper,
                    BabyDiaperTestValue = new BabyDiaperTestValue { TestType = TestTypeBabyDiaper.Rewet, Rewet140Rw = RwType.Better, Rewet210Rw = RwType.Better },
                    TestSheet = new TestSheet { MachineNr = "M12" }
                },
                new TestValue
                {
                    TestValueType = TestValueType.StandardDeviation,
                    ArticleTestType = ArticleType.BabyDiaper,
                    BabyDiaperTestValue = new BabyDiaperTestValue { TestType = TestTypeBabyDiaper.Rewet, Rewet140Rw = RwType.Better, Rewet210Rw = RwType.Better },
                    TestSheet = new TestSheet { MachineNr = "M12" }
                },
                new TestValue
                {
                    TestValueType = TestValueType.Average,
                    ArticleTestType = ArticleType.BabyDiaper,
                    BabyDiaperTestValue =
                        new BabyDiaperTestValue
                        {
                            TestType = TestTypeBabyDiaper.RewetAndPenetrationTime,
                            Rewet140Rw = RwType.Better,
                            Rewet210Rw = RwType.Better,
                            PenetrationRwType = RwType.Better
                        },
                    TestSheet = new TestSheet { MachineNr = "M12" }
                },
                new TestValue
                {
                    TestValueType = TestValueType.StandardDeviation,
                    ArticleTestType = ArticleType.BabyDiaper,
                    BabyDiaperTestValue =
                        new BabyDiaperTestValue
                        {
                            TestType = TestTypeBabyDiaper.RewetAndPenetrationTime,
                            Rewet140Rw = RwType.Better,
                            Rewet210Rw = RwType.Better,
                            PenetrationRwType = RwType.Better
                        },
                    TestSheet = new TestSheet { MachineNr = "M12" }
                }
            };
    }
}