using System;
using System.Collections.Generic;
using System.IO;
using FluentAssertions;
using Intranet.Common;
using Intranet.Labor.Model;
using Intranet.Labor.Model.labor;
using Xunit;

namespace Intranet.Labor.Bll.Test
{
    /// <summary>
    ///     Class representing the Test for the class <see cref="LaborCreatorServiceHelper" />
    /// </summary>
    public class LaborCreatorServiceHelperTest
    {
        /// <summary>
        ///     Testing GenerateProdCode 1
        /// </summary>
        [Fact]
        public void GenerateProdCodeTest1()
        {
            var serviceHelper = new LaborCreatorServiceHelper( new NLogLoggerFactory() );
            const String expected = "IT/11/16/158/23:58";
            var actual = serviceHelper.GenerateProdCode( "11", 2016, 158, new TimeSpan( 23, 58, 0 ) );

            actual.Should()
                  .Be( expected );
        }

        /// <summary>
        ///     Testing GenerateProdCode 2
        /// </summary>
        [Fact]
        public void GenerateProdCodeTest2()
        {
            var serviceHelper = new LaborCreatorServiceHelper( new NLogLoggerFactory() );
            const String expected = "IT/11/16/158/23:58";
            var actual = serviceHelper.GenerateProdCode( "11", 16, 158, new TimeSpan( 23, 58, 0 ) );

            actual.Should()
                  .Be( expected );
        }

        /// <summary>
        ///     Testing GenerateProdCode 3
        /// </summary>
        [Fact]
        public void GenerateProdCodeTest3()
        {
            var serviceHelper = new LaborCreatorServiceHelper( new NLogLoggerFactory() );
            const String expected = "IT/11/06/158/23:00";
            var actual = serviceHelper.GenerateProdCode( "11", 2006, 158, new TimeSpan( 23, 00, 0 ) );

            actual.Should()
                  .Be( expected );
        }

        /// <summary>
        ///     Testing GenerateProdCode 4
        /// </summary>
        [Fact]
        public void GenerateProdCodeTest4()
        {
            var serviceHelper = new LaborCreatorServiceHelper( new NLogLoggerFactory() );
            const String expected = "IT/11/16/158/03:01";
            var actual = serviceHelper.GenerateProdCode( "11", 2016, 158, new TimeSpan( 3, 1, 0 ) );

            actual.Should()
                  .Be( expected );
        }

        /// <summary>
        ///     Testing GetBabyDiaperTestValueForType Empty Penetration Time
        /// </summary>
        [Fact]
        public
            void GetBabyDiaperTestValueForTypeEmptyPenetrationTimeTest1()
        {
            var serviceHelper = new LaborCreatorServiceHelper( new NLogLoggerFactory() );

            Exception ex =
                Assert.Throws<InvalidDataException>(
                    () => serviceHelper.GetBabyDiaperTestValueForType(
                        new List<TestValue>(),
                        TestTypeBabyDiaper.RewetAndPenetrationTime,
                        TestValueType.Single ) );

            Assert.Equal
            ( "No Single for RewetAndPenetrationTime per Testsheet existing",
              ex.Message
            );
        }

        /// <summary>
        ///     Testing GetBabyDiaperTestValueForType Empty Penetration Time 2
        /// </summary>
        [Fact]
        public
            void GetBabyDiaperTestValueForTypeEmptyPenetrationTimeTest2()
        {
            var serviceHelper = new LaborCreatorServiceHelper( new NLogLoggerFactory() );

            Exception ex =
                Assert.Throws<InvalidDataException>(
                    () => serviceHelper.GetBabyDiaperTestValueForType(
                        new List<TestValue>
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
                        },
                        TestTypeBabyDiaper.RewetAndPenetrationTime,
                        TestValueType.Single ) );

            Assert.Equal
            ( "No Single for RewetAndPenetrationTime per Testsheet existing",
              ex.Message
            );
        }

        /// <summary>
        ///     Testing GetBabyDiaperTestValueForType Empty Retention
        /// </summary>
        [Fact]
        public void GetBabyDiaperTestValueForTypeEmptyRetentionTest1()
        {
            var serviceHelper = new LaborCreatorServiceHelper( new NLogLoggerFactory() );

            Exception ex =
                Assert.Throws<InvalidDataException>( () => serviceHelper.GetBabyDiaperTestValueForType( new List<TestValue>(), TestTypeBabyDiaper.Retention, TestValueType.Single ) );

            Assert.Equal( "No Single for Retention per Testsheet existing", ex.Message );
        }

        /// <summary>
        ///     Testing GetBabyDiaperTestValueForType Empty Retention
        /// </summary>
        [Fact]
        public void GetBabyDiaperTestValueForTypeEmptyRetentionTest2()
        {
            var serviceHelper = new LaborCreatorServiceHelper( new NLogLoggerFactory() );

            Exception ex = Assert.Throws<InvalidDataException>( () => serviceHelper.GetBabyDiaperTestValueForType(
                                                                    new List<TestValue>
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
                                                                    },
                                                                    TestTypeBabyDiaper.Retention,
                                                                    TestValueType.Single ) );

            Assert.Equal( "No Single for Retention per Testsheet existing", ex.Message );
        }

        /// <summary>
        ///     Testing GetBabyDiaperTestValueForType Empty Rewet 1
        /// </summary>
        [Fact]
        public void GetBabyDiaperTestValueForTypeEmptyRewetTest1()
        {
            var serviceHelper = new LaborCreatorServiceHelper( new NLogLoggerFactory() );

            Exception ex =
                Assert.Throws<InvalidDataException>( () => serviceHelper.GetBabyDiaperTestValueForType(
                                                         new List<TestValue>(),
                                                         TestTypeBabyDiaper.Rewet,
                                                         TestValueType.Single ) );

            Assert.Equal( "No Single for Rewet per Testsheet existing", ex.Message );
        }

        /// <summary>
        ///     Testing GetBabyDiaperTestValueForType Empty Rewet 2
        /// </summary>
        [Fact]
        public void GetBabyDiaperTestValueForTypeEmptyRewetTest2()
        {
            var serviceHelper = new LaborCreatorServiceHelper( new NLogLoggerFactory() );

            Exception ex =
                Assert.Throws<InvalidDataException>( () => serviceHelper.GetBabyDiaperTestValueForType(
                                                         new List<TestValue>
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
                                                         },
                                                         TestTypeBabyDiaper.Rewet,
                                                         TestValueType.Single ) );

            Assert.Equal( "No Single for Rewet per Testsheet existing", ex.Message );
        }

        /// <summary>
        ///     Testing GetBabyDiaperTestValueForType Many PenetrationTime 1
        /// </summary>
        [Fact]
        public void GetBabyDiaperTestValueForTypeManyPenetrationTimeTest1()
        {
            var serviceHelper = new LaborCreatorServiceHelper( new NLogLoggerFactory() );

            Exception ex =
                Assert.Throws<InvalidDataException>( () => serviceHelper.GetBabyDiaperTestValueForType(
                                                         new List<TestValue>
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
                                                         },
                                                         TestTypeBabyDiaper.RewetAndPenetrationTime,
                                                         TestValueType.Single ) );

            Assert.Equal( "Only one Single for RewetAndPenetrationTime per Testsheet allowed", ex.Message );
        }

        /// <summary>
        ///     Testing GetBabyDiaperTestValueForType Many penetrationtime 2
        /// </summary>
        [Fact]
        public void GetBabyDiaperTestValueForTypeManyPenetrationTimeTest2()
        {
            var serviceHelper = new LaborCreatorServiceHelper( new NLogLoggerFactory() );

            Exception ex =
                Assert.Throws<InvalidDataException>( () => serviceHelper.GetBabyDiaperTestValueForType(
                                                         new List<TestValue>
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
                                                         },
                                                         TestTypeBabyDiaper.RewetAndPenetrationTime,
                                                         TestValueType.Average ) );

            Assert.Equal( "Only one Average for RewetAndPenetrationTime per Testsheet allowed", ex.Message );
        }

        /// <summary>
        ///     Testing GetBabyDiaperTestValueForType Many PenetrationTime 3
        /// </summary>
        [Fact]
        public void GetBabyDiaperTestValueForTypeManyPenetrationTimeTest3()
        {
            var serviceHelper = new LaborCreatorServiceHelper( new NLogLoggerFactory() );

            Exception ex =
                Assert.Throws<InvalidDataException>( () => serviceHelper.GetBabyDiaperTestValueForType(
                                                         new List<TestValue>
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
                                                         },
                                                         TestTypeBabyDiaper.RewetAndPenetrationTime,
                                                         TestValueType.StandardDeviation ) );

            Assert.Equal( "Only one StandardDeviation for RewetAndPenetrationTime per Testsheet allowed", ex.Message );
        }

        /// <summary>
        ///     Testing GetBabyDiaperTestValueForType Many Retention 1
        /// </summary>
        [Fact]
        public void GetBabyDiaperTestValueForTypeManyRetentionTest1()
        {
            var serviceHelper = new LaborCreatorServiceHelper( new NLogLoggerFactory() );

            Exception ex =
                Assert.Throws<InvalidDataException>( () => serviceHelper.GetBabyDiaperTestValueForType(
                                                         new List<TestValue>
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
                                                         },
                                                         TestTypeBabyDiaper.Retention,
                                                         TestValueType.Single ) );

            Assert.Equal( "Only one Single for Retention per Testsheet allowed", ex.Message );
        }

        /// <summary>
        ///     Testing GetBabyDiaperTestValueForType Many retention 2
        /// </summary>
        [Fact]
        public void GetBabyDiaperTestValueForTypeManyRetentionTest2()
        {
            var serviceHelper = new LaborCreatorServiceHelper( new NLogLoggerFactory() );

            Exception ex =
                Assert.Throws<InvalidDataException>( () => serviceHelper.GetBabyDiaperTestValueForType(
                                                         new List<TestValue>
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
                                                         },
                                                         TestTypeBabyDiaper.Retention,
                                                         TestValueType.Average ) );

            Assert.Equal( "Only one Average for Retention per Testsheet allowed", ex.Message );
        }

        /// <summary>
        ///     Testing GetBabyDiaperTestValueForType Many Rewet 3
        /// </summary>
        [Fact]
        public void GetBabyDiaperTestValueForTypeManyRetentionTest3()
        {
            var serviceHelper = new LaborCreatorServiceHelper( new NLogLoggerFactory() );

            Exception ex =
                Assert.Throws<InvalidDataException>( () => serviceHelper.GetBabyDiaperTestValueForType(
                                                         new List<TestValue>
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
                                                         },
                                                         TestTypeBabyDiaper.Retention,
                                                         TestValueType.StandardDeviation ) );

            Assert.Equal( "Only one StandardDeviation for Retention per Testsheet allowed", ex.Message );
        }

        /// <summary>
        ///     Testing GetBabyDiaperTestValueForType Many Rewet 1
        /// </summary>
        [Fact]
        public void GetBabyDiaperTestValueForTypeManyRewetTest1()
        {
            var serviceHelper = new LaborCreatorServiceHelper( new NLogLoggerFactory() );

            Exception ex =
                Assert.Throws<InvalidDataException>( () => serviceHelper.GetBabyDiaperTestValueForType(
                                                         new List<TestValue>
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
                                                         },
                                                         TestTypeBabyDiaper.Rewet,
                                                         TestValueType.Single ) );

            Assert.Equal( "Only one Single for Rewet per Testsheet allowed", ex.Message );
        }

        /// <summary>
        ///     Testing GetBabyDiaperTestValueForType Many Rewet 2
        /// </summary>
        [Fact]
        public void GetBabyDiaperTestValueForTypeManyRewetTest2()
        {
            var serviceHelper = new LaborCreatorServiceHelper( new NLogLoggerFactory() );

            Exception ex =
                Assert.Throws<InvalidDataException>( () => serviceHelper.GetBabyDiaperTestValueForType(
                                                         new List<TestValue>
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
                                                         },
                                                         TestTypeBabyDiaper.Rewet,
                                                         TestValueType.Average ) );

            Assert.Equal( "Only one Average for Rewet per Testsheet allowed", ex.Message );
        }

        /// <summary>
        ///     Testing GetBabyDiaperTestValueForType Many Rewet 3
        /// </summary>
        [Fact]
        public void GetBabyDiaperTestValueForTypeManyRewetTest3()
        {
            var serviceHelper = new LaborCreatorServiceHelper( new NLogLoggerFactory() );

            Exception ex =
                Assert.Throws<InvalidDataException>( () => serviceHelper.GetBabyDiaperTestValueForType(
                                                         new List<TestValue>
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
                                                         },
                                                         TestTypeBabyDiaper.Rewet,
                                                         TestValueType.StandardDeviation ) );

            Assert.Equal( "Only one StandardDeviation for Rewet per Testsheet allowed", ex.Message );
        }

        /// <summary>
        ///     Testing GetBabyDiaperTestValueForType Ok PenetrationTime 1
        /// </summary>
        [Fact]
        public void GetBabyDiaperTestValueForTypeOkPenetrationTimeTest1()
        {
            var serviceHelper = new LaborCreatorServiceHelper( new NLogLoggerFactory() );
            var expected = new BabyDiaperTestValue { TestType = TestTypeBabyDiaper.RewetAndPenetrationTime };
            var
                actual = serviceHelper.GetBabyDiaperTestValueForType(
                    new List<TestValue>
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
                    },
                    TestTypeBabyDiaper.RewetAndPenetrationTime,
                    TestValueType.Single );

            actual.Should()
                  .Be( expected );
        }
        /// <summary>
        ///     Testing GetBabyDiaperTestValueForType Ok Rewet 1
        /// </summary>
        [Fact]
        public void GetBabyDiaperTestValueForTypeOkRewetTest1()
        {
            var serviceHelper = new LaborCreatorServiceHelper(new NLogLoggerFactory());
            var expected = new BabyDiaperTestValue { TestType = TestTypeBabyDiaper.Rewet };
            var
                actual = serviceHelper.GetBabyDiaperTestValueForType(
                    new List<TestValue>
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
                    },
                    TestTypeBabyDiaper.Rewet,
                    TestValueType.Single);

            actual.Should()
                  .Be(expected);
        }
        /// <summary>
        ///     Testing GetBabyDiaperTestValueForType Ok Retention 1
        /// </summary>
        [Fact]
        public void GetBabyDiaperTestValueForTypeOkRetentionTest1()
        {
            var serviceHelper = new LaborCreatorServiceHelper(new NLogLoggerFactory());
            var expected = new BabyDiaperTestValue { TestType = TestTypeBabyDiaper.Retention };
            var
                actual = serviceHelper.GetBabyDiaperTestValueForType(
                    new List<TestValue>
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
                    },
                    TestTypeBabyDiaper.Retention,
                    TestValueType.Single);

            actual.Should()
                  .Be(expected);
        }


        /// <summary>
        ///     Testing GetBabyDiaperTestValueForType Ok PenetrationTime 2
        /// </summary>
        [Fact]
        public void GetBabyDiaperTestValueForTypeOkPenetrationTimeTest2()
        {
            var serviceHelper = new LaborCreatorServiceHelper(new NLogLoggerFactory());
            var expected = new BabyDiaperTestValue { TestType = TestTypeBabyDiaper.RewetAndPenetrationTime };
            var
                actual = serviceHelper.GetBabyDiaperTestValueForType(
                    new List<TestValue>
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
                    },
                    TestTypeBabyDiaper.RewetAndPenetrationTime,
                    TestValueType.Average);

            actual.Should()
                  .Be(expected);
        }
        /// <summary>
        ///     Testing GetBabyDiaperTestValueForType Ok Rewet 2
        /// </summary>
        [Fact]
        public void GetBabyDiaperTestValueForTypeOkRewetTest2()
        {
            var serviceHelper = new LaborCreatorServiceHelper(new NLogLoggerFactory());
            var expected = new BabyDiaperTestValue { TestType = TestTypeBabyDiaper.Rewet };
            var
                actual = serviceHelper.GetBabyDiaperTestValueForType(
                    new List<TestValue>
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
                    },
                    TestTypeBabyDiaper.Rewet,
                    TestValueType.Average);

            actual.Should()
                  .Be(expected);
        }
        /// <summary>
        ///     Testing GetBabyDiaperTestValueForType Ok Retention 2
        /// </summary>
        [Fact]
        public void GetBabyDiaperTestValueForTypeOkRetentionTest2()
        {
            var serviceHelper = new LaborCreatorServiceHelper(new NLogLoggerFactory());
            var expected = new BabyDiaperTestValue { TestType = TestTypeBabyDiaper.Retention };
            var
                actual = serviceHelper.GetBabyDiaperTestValueForType(
                    new List<TestValue>
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
                            BabyDiaperTestValue = new BabyDiaperTestValue { TestType = TestTypeBabyDiaper.Rewet }
                        }
                    },
                    TestTypeBabyDiaper.Retention,
                    TestValueType.Average);

            actual.Should()
                  .Be(expected);
        }


        /// <summary>
        ///     Testing GetBabyDiaperTestValueForType Ok PenetrationTime 3
        /// </summary>
        [Fact]
        public void GetBabyDiaperTestValueForTypeOkPenetrationTimeTest3()
        {
            var serviceHelper = new LaborCreatorServiceHelper(new NLogLoggerFactory());
            var expected = new BabyDiaperTestValue { TestType = TestTypeBabyDiaper.RewetAndPenetrationTime };
            var
                actual = serviceHelper.GetBabyDiaperTestValueForType(
                    new List<TestValue>
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
                    },
                    TestTypeBabyDiaper.RewetAndPenetrationTime,
                    TestValueType.StandardDeviation);

            actual.Should()
                  .Be(expected);
        }
        /// <summary>
        ///     Testing GetBabyDiaperTestValueForType Ok Rewet 1
        /// </summary>
        [Fact]
        public void GetBabyDiaperTestValueForTypeOkRewetTest3()
        {
            var serviceHelper = new LaborCreatorServiceHelper(new NLogLoggerFactory());
            var expected = new BabyDiaperTestValue { TestType = TestTypeBabyDiaper.Rewet };
            var
                actual = serviceHelper.GetBabyDiaperTestValueForType(
                    new List<TestValue>
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
                    },
                    TestTypeBabyDiaper.Rewet,
                    TestValueType.StandardDeviation);

            actual.Should()
                  .Be(expected);
        }
        /// <summary>
        ///     Testing GetBabyDiaperTestValueForType Ok Retention 1
        /// </summary>
        [Fact]
        public void GetBabyDiaperTestValueForTypeOkRetentionTest3()
        {
            var serviceHelper = new LaborCreatorServiceHelper(new NLogLoggerFactory());
            var expected = new BabyDiaperTestValue { TestType = TestTypeBabyDiaper.Retention };
            var
                actual = serviceHelper.GetBabyDiaperTestValueForType(
                    new List<TestValue>
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
                    },
                    TestTypeBabyDiaper.Retention,
                    TestValueType.StandardDeviation);

            actual.Should()
                  .Be(expected);
        }


    }
}