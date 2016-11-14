using System.Collections.Generic;
using Intranet.Labor.Model;
using Intranet.Labor.Model.labor;

namespace Intranet.Labor.Test
{
    /// <summary>
    /// Test Data Helper Class for Labor Creator Service Test
    /// </summary>
    public class IncontinencePadLaborCreatorServiceHelperData
    {

        /// <summary>
        /// Testdata
        /// </summary>
        /// <returns>TestValues</returns>
        public static List<TestValue> TwoTestValuePerType()
            => new List<TestValue>
            {
                new TestValue
                {
                    TestValueType = TestValueType.Single,
                    ArticleTestType = ArticleType.IncontinencePad,
                    IncontinencePadTestValue = new IncontinencePadTestValue { TestType = TestTypeIncontinencePad.Retention,  RetentionRw = RwType.Better},TestSheet = new TestSheet {MachineNr = "M12"}
                },
                new TestValue
                {
                    TestValueType = TestValueType.Single,
                    ArticleTestType = ArticleType.IncontinencePad,
                    IncontinencePadTestValue = new IncontinencePadTestValue { TestType = TestTypeIncontinencePad.Retention,RetentionRw = RwType.Better},TestSheet = new TestSheet{MachineNr = "M12"}
                },
                new TestValue
                {
                    TestValueType = TestValueType.Single,
                    ArticleTestType = ArticleType.IncontinencePad,
                    IncontinencePadTestValue = new IncontinencePadTestValue { TestType = TestTypeIncontinencePad.RewetFree,RewetFreeRw = RwType.Better},TestSheet = new TestSheet{MachineNr = "M12"}
                },
                new TestValue
                {
                    TestValueType = TestValueType.Single,
                    ArticleTestType = ArticleType.IncontinencePad,
                  IncontinencePadTestValue = new IncontinencePadTestValue { TestType = TestTypeIncontinencePad.RewetFree,RewetFreeRw = RwType.Better},TestSheet = new TestSheet{MachineNr = "M12"}
                },
                new TestValue
                {
                    TestValueType = TestValueType.Single,
                    ArticleTestType = ArticleType.IncontinencePad,
                   IncontinencePadTestValue = new IncontinencePadTestValue { TestType = TestTypeIncontinencePad.AcquisitionTimeAndRewet,RewetAfterAcquisitionTimeRw = RwType.Better,AcquisitionTimeFirstRw = RwType.Better,AcquisitionTimeSecondRw = RwType.Better,AcquisitionTimeThirdRw = RwType.Better,RewetFreeRw = RwType.Better},TestSheet = new TestSheet{MachineNr = "M12"}
                },
                new TestValue
                {
                    TestValueType = TestValueType.Single,
                    ArticleTestType = ArticleType.IncontinencePad,
                   IncontinencePadTestValue = new IncontinencePadTestValue { TestType = TestTypeIncontinencePad.AcquisitionTimeAndRewet ,RewetAfterAcquisitionTimeRw = RwType.Better,AcquisitionTimeFirstRw = RwType.Better,AcquisitionTimeSecondRw = RwType.Better,AcquisitionTimeThirdRw = RwType.Better,RewetFreeRw = RwType.Better},TestSheet = new TestSheet{MachineNr = "M12"}
                },
           
                new TestValue
                {
                    TestValueType = TestValueType.StandardDeviation,
                    ArticleTestType = ArticleType.IncontinencePad,
                    IncontinencePadTestValue = new IncontinencePadTestValue { TestType = TestTypeIncontinencePad.Retention,RetentionRw = RwType.Better},TestSheet = new TestSheet{MachineNr = "M12"}
                },
                new TestValue
                {
                    TestValueType = TestValueType.Average,
                    ArticleTestType = ArticleType.IncontinencePad,
                    IncontinencePadTestValue = new IncontinencePadTestValue { TestType = TestTypeIncontinencePad.RewetFree,RewetFreeRw = RwType.Better},TestSheet = new TestSheet{MachineNr = "M12"}
                },
                new TestValue
                {
                    TestValueType = TestValueType.StandardDeviation,
                    ArticleTestType = ArticleType.IncontinencePad,
                    IncontinencePadTestValue = new IncontinencePadTestValue { TestType = TestTypeIncontinencePad.RewetFree,RewetFreeRw = RwType.Better },TestSheet = new TestSheet{MachineNr = "M12"}
                },
                new TestValue
                {
                    TestValueType = TestValueType.Average,
                    ArticleTestType = ArticleType.IncontinencePad,
                   IncontinencePadTestValue = new IncontinencePadTestValue { TestType = TestTypeIncontinencePad.AcquisitionTimeAndRewet,RewetAfterAcquisitionTimeRw = RwType.Better},TestSheet = new TestSheet{MachineNr = "M12"}
                },
                new TestValue
                {
                    TestValueType = TestValueType.StandardDeviation,
                    ArticleTestType = ArticleType.IncontinencePad,
                   IncontinencePadTestValue = new IncontinencePadTestValue { TestType = TestTypeIncontinencePad.AcquisitionTimeAndRewet , AcquisitionTimeFirstRw= RwType.Better,AcquisitionTimeSecondRw = RwType.Better, AcquisitionTimeThirdRw = RwType.Better},TestSheet = new TestSheet{MachineNr = "M12"}
                }
            };
        /// <summary>
        /// Testdata
        /// </summary>
        /// <returns>TestValues</returns>
        public static List<TestValue> TestValuesEmptyRewet2() 
            => new List<TestValue>
        {
            new TestValue
            {
                TestValueType = TestValueType.Single,
                ArticleTestType = ArticleType.IncontinencePad,
                IncontinencePadTestValue =
                    new IncontinencePadTestValue
                    {
                        TestType = TestTypeIncontinencePad.RewetFree
                    }
            },
            new TestValue
            {
                TestValueType = TestValueType.Single,
                ArticleTestType = ArticleType.IncontinencePad,
               IncontinencePadTestValue =
                    new IncontinencePadTestValue
                    {
                        TestType = TestTypeIncontinencePad.RewetFree
                    }
            }
        };

        /// <summary>
        /// Testdata
        /// </summary>
        /// <returns>TestValues</returns>
        public static List<TestValue> TestValuesEmptyRetention2()
            => new List<TestValue>
        {
            new TestValue
            {
                TestValueType = TestValueType.Single,
                ArticleTestType = ArticleType.IncontinencePad,
                IncontinencePadTestValue = new IncontinencePadTestValue { TestType = TestTypeIncontinencePad.RewetFree }
            },
            new TestValue
            {
                TestValueType = TestValueType.Single,
                ArticleTestType = ArticleType.IncontinencePad,
                IncontinencePadTestValue = new IncontinencePadTestValue { TestType = TestTypeIncontinencePad.AcquisitionTimeAndRewet }
            }
        };

        /// <summary>
        /// Testdata
        /// </summary>
        /// <returns>TestValues</returns>
        public static List<TestValue> TestValuesEmptyAcquisition2() 
            => new List<TestValue>
        {
            new TestValue
            {
                TestValueType = TestValueType.Single,
                ArticleTestType = ArticleType.IncontinencePad,
                IncontinencePadTestValue = new IncontinencePadTestValue { TestType = TestTypeIncontinencePad.RewetFree }
            },
            new TestValue
            {
                TestValueType = TestValueType.Single,
                ArticleTestType = ArticleType.IncontinencePad,
                IncontinencePadTestValue = new IncontinencePadTestValue { TestType = TestTypeIncontinencePad.Retention }
            }
        };

        /// <summary>
        /// Testdata
        /// </summary>
        /// <returns>TestValues</returns>
        public static List<TestValue> TestValuesManyAcquisitionTimeAverage() 
            => new List<TestValue>
        {
            new TestValue
            {
                TestValueType = TestValueType.Average,
                ArticleTestType = ArticleType.IncontinencePad,
                IncontinencePadTestValue =
                    new IncontinencePadTestValue
                    {
                        TestType = TestTypeIncontinencePad.AcquisitionTimeAndRewet
                    }
            },
            new TestValue
            {
                TestValueType = TestValueType.Average,
                ArticleTestType = ArticleType.IncontinencePad,
                IncontinencePadTestValue =
                    new IncontinencePadTestValue
                    {
                        TestType = TestTypeIncontinencePad.AcquisitionTimeAndRewet
                    }
            }
        };
        /// <summary>
        /// Testdata
        /// </summary>
        /// <returns>TestValues</returns>
        public static List<TestValue> TestValuesManyAcquisitionTimeStandardDeviation() 
            => new List<TestValue>
        {
            new TestValue
            {
                TestValueType = TestValueType.StandardDeviation,
                ArticleTestType = ArticleType.IncontinencePad,
                IncontinencePadTestValue =
                    new IncontinencePadTestValue
                    {
                        TestType = TestTypeIncontinencePad.AcquisitionTimeAndRewet
                    }
            },
            new TestValue
            {
                TestValueType = TestValueType.StandardDeviation,
                ArticleTestType = ArticleType.IncontinencePad,
                IncontinencePadTestValue =
                    new IncontinencePadTestValue
                    {
                        TestType = TestTypeIncontinencePad.AcquisitionTimeAndRewet
                    }
            }
        };
        /// <summary>
        /// Testdata
        /// </summary>
        /// <returns>TestValues</returns>
        public static List<TestValue> TestValuesManyRententionSingle()
            => new List<TestValue>
        {
            new TestValue
            {
                TestValueType = TestValueType.Single,
                ArticleTestType = ArticleType.IncontinencePad,
                IncontinencePadTestValue =
                    new IncontinencePadTestValue
                    {
                        TestType = TestTypeIncontinencePad.Retention
                    }
            },
            new TestValue
            {
                TestValueType = TestValueType.Single,
                ArticleTestType = ArticleType.IncontinencePad,
                IncontinencePadTestValue =
                    new IncontinencePadTestValue
                    {
                        TestType = TestTypeIncontinencePad.Retention
                    }
            }
        };
        /// <summary>
        /// Testdata
        /// </summary>
        /// <returns>TestValues</returns>
        public static List<TestValue> TestValuesManyRetentionAverage()
            => new List<TestValue>
        {
            new TestValue
            {
                TestValueType = TestValueType.Average,
                ArticleTestType = ArticleType.IncontinencePad,
                IncontinencePadTestValue =
                    new IncontinencePadTestValue
                    {
                        TestType = TestTypeIncontinencePad.Retention
                    }
            },
            new TestValue
            {
                TestValueType = TestValueType.Average,
                ArticleTestType = ArticleType.IncontinencePad,
                IncontinencePadTestValue =
                    new IncontinencePadTestValue
                    {
                        TestType = TestTypeIncontinencePad.Retention
                    }
            }
        };
        /// <summary>
        /// Testdata
        /// </summary>
        /// <returns>TestValues</returns>
        public static List<TestValue> TestValuesManyRetentionStandardDeviation()
            => new List<TestValue>
        {
            new TestValue
            {
                TestValueType = TestValueType.StandardDeviation,
                ArticleTestType = ArticleType.IncontinencePad,
                IncontinencePadTestValue =
                    new IncontinencePadTestValue
                    {
                        TestType = TestTypeIncontinencePad.Retention
                    }
            },
            new TestValue
            {
                TestValueType = TestValueType.StandardDeviation,
                ArticleTestType = ArticleType.IncontinencePad,
                IncontinencePadTestValue =
                    new IncontinencePadTestValue
                    {
                        TestType = TestTypeIncontinencePad.Retention
                    }
            }
        };
        /// <summary>
        /// Testdata
        /// </summary>
        /// <returns>TestValues</returns>
        public static List<TestValue> TestValuesManyRewetAverage() 
            => new List<TestValue>
        {
            new TestValue
            {
                TestValueType = TestValueType.Average,
                ArticleTestType = ArticleType.IncontinencePad,
                IncontinencePadTestValue =
                    new IncontinencePadTestValue { TestType = TestTypeIncontinencePad.RewetFree }
            },
            new TestValue
            {
                TestValueType = TestValueType.Average,
                ArticleTestType = ArticleType.IncontinencePad,
                IncontinencePadTestValue =
                    new IncontinencePadTestValue { TestType = TestTypeIncontinencePad.RewetFree }
            }
        };
        /// <summary>
        /// Testdata
        /// </summary>
        /// <returns>TestValues</returns>
        public static List<TestValue> TestValuesManyRewetSingle() 
            => new List<TestValue>
        {
            new TestValue
            {
                TestValueType = TestValueType.Single,
                ArticleTestType = ArticleType.IncontinencePad,
                IncontinencePadTestValue =
                    new IncontinencePadTestValue { TestType = TestTypeIncontinencePad.RewetFree }
            },
            new TestValue
            {
                TestValueType = TestValueType.Single,
                ArticleTestType = ArticleType.IncontinencePad,
                IncontinencePadTestValue =
                    new IncontinencePadTestValue { TestType = TestTypeIncontinencePad.RewetFree }
            }
        };
        /// <summary>
        /// Testdata
        /// </summary>
        /// <returns>TestValues</returns>
        public static List<TestValue> TestValuesManyRewetStandardDeviation() 
            => new List<TestValue>
        {
            new TestValue
            {
                TestValueType = TestValueType.StandardDeviation,
                ArticleTestType = ArticleType.IncontinencePad,
                IncontinencePadTestValue =
                    new IncontinencePadTestValue { TestType = TestTypeIncontinencePad.RewetFree }
            },
            new TestValue
            {
                TestValueType = TestValueType.StandardDeviation,
                ArticleTestType = ArticleType.IncontinencePad,
                IncontinencePadTestValue =
                    new IncontinencePadTestValue { TestType = TestTypeIncontinencePad.RewetFree }
            }
        };
        /// <summary>
        /// Testdata
        /// </summary>
        /// <returns>TestValues</returns>
        public static List<TestValue> TestValuesOkAcquisitionTimeAverage( IncontinencePadTestValue expected )
            => new List<TestValue>
        {
            new TestValue
            {
                TestValueType = TestValueType.Average,
                ArticleTestType = ArticleType.IncontinencePad,
                IncontinencePadTestValue = new IncontinencePadTestValue { TestType = TestTypeIncontinencePad.RewetFree }
            },
            new TestValue
            {
                TestValueType = TestValueType.Average,
                ArticleTestType = ArticleType.IncontinencePad,
                IncontinencePadTestValue = expected
            },
            new TestValue
            {
                TestValueType = TestValueType.Average,
                ArticleTestType = ArticleType.IncontinencePad,
                IncontinencePadTestValue = new IncontinencePadTestValue { TestType = TestTypeIncontinencePad.Retention }
            }
        };
        /// <summary>
        /// Testdata
        /// </summary>
        /// <returns>TestValues</returns>
        public static List<TestValue> TestValuesOkAcquisitionTimeSingle( IncontinencePadTestValue expected )
            => new List<TestValue>
        {
            new TestValue
            {
                TestValueType = TestValueType.Single,
                ArticleTestType = ArticleType.IncontinencePad,
                IncontinencePadTestValue = new IncontinencePadTestValue { TestType = TestTypeIncontinencePad.RewetFree }
            },
            new TestValue
            {
                TestValueType = TestValueType.Single,
                ArticleTestType = ArticleType.IncontinencePad,
                IncontinencePadTestValue = expected
            },
            new TestValue
            {
                TestValueType = TestValueType.Single,
                ArticleTestType = ArticleType.IncontinencePad,
                IncontinencePadTestValue = new IncontinencePadTestValue { TestType = TestTypeIncontinencePad.Retention }
            }
        };
        /// <summary>
        /// Testdata
        /// </summary>
        /// <returns>TestValues</returns>
        public static List<TestValue> TestValuesOKAcquisitionTimeStandardDeviation( IncontinencePadTestValue expected ) 
            => new List<TestValue>
        {
            new TestValue
            {
                TestValueType = TestValueType.StandardDeviation,
                ArticleTestType = ArticleType.IncontinencePad,
                IncontinencePadTestValue = new IncontinencePadTestValue { TestType = TestTypeIncontinencePad.RewetFree }
            },
            new TestValue
            {
                TestValueType = TestValueType.StandardDeviation,
                ArticleTestType = ArticleType.IncontinencePad,
                IncontinencePadTestValue = expected
            },
            new TestValue
            {
                TestValueType = TestValueType.StandardDeviation,
                ArticleTestType = ArticleType.IncontinencePad,
                IncontinencePadTestValue = new IncontinencePadTestValue { TestType = TestTypeIncontinencePad.Retention }
            }
        };
        /// <summary>
        /// Testdata
        /// </summary>
        /// <returns>TestValues</returns>
        public static List<TestValue> TestValuesOkRetentionAverage( IncontinencePadTestValue expected )
            => new List<TestValue>
        {
            new TestValue
            {
                TestValueType = TestValueType.Average,
                ArticleTestType = ArticleType.IncontinencePad,
                IncontinencePadTestValue = new IncontinencePadTestValue { TestType = TestTypeIncontinencePad.AcquisitionTimeAndRewet }
            },
            new TestValue
            {
                TestValueType = TestValueType.Average,
                ArticleTestType = ArticleType.IncontinencePad,
                IncontinencePadTestValue = expected
            },
            new TestValue
            {
                TestValueType = TestValueType.Average,
                ArticleTestType = ArticleType.IncontinencePad,
                IncontinencePadTestValue = new IncontinencePadTestValue { TestType = TestTypeIncontinencePad.RewetFree, RewetFreeRw = RwType.Ok}
            }
        };
        /// <summary>
        /// Testdata
        /// </summary>
        /// <returns>TestValues</returns>
        public static List<TestValue> TestValuesOkRetentionStandardDeviation( IncontinencePadTestValue expected ) 
            => new List<TestValue>
        {
            new TestValue
            {
                TestValueType = TestValueType.StandardDeviation,
                ArticleTestType = ArticleType.IncontinencePad,
                IncontinencePadTestValue = new IncontinencePadTestValue { TestType = TestTypeIncontinencePad.AcquisitionTimeAndRewet }
            },
            new TestValue
            {
                TestValueType = TestValueType.StandardDeviation,
                ArticleTestType = ArticleType.IncontinencePad,
                IncontinencePadTestValue = expected
            },
            new TestValue
            {
                TestValueType = TestValueType.StandardDeviation,
                ArticleTestType = ArticleType.IncontinencePad,
                IncontinencePadTestValue = new IncontinencePadTestValue { TestType = TestTypeIncontinencePad.RewetFree }
            }
        };
        /// <summary>
        /// Testdata
        /// </summary>
        /// <returns>TestValues</returns>
        public static List<TestValue> TestValuesOkRetentionTimeSingle( IncontinencePadTestValue expected )
            => new List<TestValue>
        {
            new TestValue
            {
                TestValueType = TestValueType.Single,
                ArticleTestType = ArticleType.IncontinencePad,
                IncontinencePadTestValue = new IncontinencePadTestValue { TestType = TestTypeIncontinencePad.AcquisitionTimeAndRewet }
            },
            new TestValue
            {
                TestValueType = TestValueType.Single,
                ArticleTestType = ArticleType.IncontinencePad,
                IncontinencePadTestValue = expected
            },
            new TestValue
            {
                TestValueType = TestValueType.Single,
                ArticleTestType = ArticleType.IncontinencePad,
                IncontinencePadTestValue = new IncontinencePadTestValue { TestType = TestTypeIncontinencePad.RewetFree }
            }
        };
        /// <summary>
        /// Testdata
        /// </summary>
        /// <returns>TestValues</returns>
        public static List<TestValue> TestValuesOkRewetAverage( IncontinencePadTestValue expected ) 
            => new List<TestValue>
        {
            new TestValue
            {
                TestValueType = TestValueType.Average,
                ArticleTestType = ArticleType.IncontinencePad,
                IncontinencePadTestValue = new IncontinencePadTestValue { TestType = TestTypeIncontinencePad.AcquisitionTimeAndRewet }
            },
            new TestValue
            {
                TestValueType = TestValueType.Average,
                ArticleTestType = ArticleType.IncontinencePad,
                IncontinencePadTestValue = expected
            },
            new TestValue
            {
                TestValueType = TestValueType.Average,
                ArticleTestType = ArticleType.IncontinencePad,
                IncontinencePadTestValue = new IncontinencePadTestValue { TestType = TestTypeIncontinencePad.Retention }
            }
        };
        /// <summary>
        /// Testdata
        /// </summary>
        /// <returns>TestValues</returns>
        public static List<TestValue> TestValuesOkRewetSingle( IncontinencePadTestValue expected )
            => new List<TestValue>
        {
            new TestValue
            {
                TestValueType = TestValueType.Single,
                ArticleTestType = ArticleType.IncontinencePad,
                IncontinencePadTestValue = new IncontinencePadTestValue { TestType = TestTypeIncontinencePad.AcquisitionTimeAndRewet }
            },
            new TestValue
            {
                TestValueType = TestValueType.Single,
                ArticleTestType = ArticleType.IncontinencePad,
                IncontinencePadTestValue = expected
            },
            new TestValue
            {
                TestValueType = TestValueType.Single,
                ArticleTestType = ArticleType.IncontinencePad,
                IncontinencePadTestValue = new IncontinencePadTestValue { TestType = TestTypeIncontinencePad.Retention }
            }
        };
        /// <summary>
        /// Testdata
        /// </summary>
        /// <returns>TestValues</returns>
        public static List<TestValue> TestValuesOkRewetStandardDeviation( IncontinencePadTestValue expected ) 
            => new List<TestValue>
        {
            new TestValue
            {
                TestValueType = TestValueType.StandardDeviation,
                ArticleTestType = ArticleType.IncontinencePad,
                IncontinencePadTestValue = new IncontinencePadTestValue { TestType = TestTypeIncontinencePad.AcquisitionTimeAndRewet }
            },
            new TestValue
            {
                TestValueType = TestValueType.StandardDeviation,
                ArticleTestType = ArticleType.IncontinencePad,
                IncontinencePadTestValue = expected
            },
            new TestValue
            {
                TestValueType = TestValueType.StandardDeviation,
                ArticleTestType = ArticleType.IncontinencePad,
                IncontinencePadTestValue = new IncontinencePadTestValue { TestType = TestTypeIncontinencePad.Retention }
            }
        };
        /// <summary>
        /// Testdata
        /// </summary>
        /// <returns>TestValues</returns>
        public static List<TestValue> TestValuesAcquisitionTimeManySingle() => new List<TestValue>
        {
            new TestValue
            {
                TestValueType = TestValueType.Single,
                ArticleTestType = ArticleType.IncontinencePad,
                IncontinencePadTestValue =
                    new IncontinencePadTestValue
                    {
                        TestType = TestTypeIncontinencePad.AcquisitionTimeAndRewet
                    }
            },
            new TestValue
            {
                TestValueType = TestValueType.Single,
                ArticleTestType = ArticleType.IncontinencePad,
                IncontinencePadTestValue =
                    new IncontinencePadTestValue
                    {
                        TestType = TestTypeIncontinencePad.AcquisitionTimeAndRewet
                    }
            }
        };
    }
}