using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using FluentAssertions;
using Intranet.Common;
using Intranet.Labor.Model;
using Intranet.Labor.Model.labor;
using Intranet.Labor.Test;
using Intranet.Labor.ViewModel;
using Xunit;

namespace Intranet.Labor.Bll.Test
{
    /// <summary>
    ///     Class representing the Test for the class <see cref="BabyDiaperLaborCreatorServiceHelper" />
    /// </summary>
    public class BabyDiaperLaborCreatorServiceHelperTest
    {
        /// <summary>
        ///     Test Average 1
        /// </summary>
        [Fact]
        public void ComputeWeightAverageAllTest1()
        {
            var serviceHelper = new BabyDiaperLaborCreatorServiceHelper( new NLogLoggerFactory() );
            var actual = serviceHelper.ComputeWeightAverageAll( new List<TestValue>
                                                                {
                                                                    new TestValue
                                                                    {
                                                                        ArticleTestType = ArticleType.BabyDiaper,
                                                                        BabyDiaperTestValue = new BabyDiaperTestValue { WeightDiaperDry = 3.0 }
                                                                    },
                                                                    new TestValue
                                                                    {
                                                                        ArticleTestType = ArticleType.BabyDiaper,
                                                                        BabyDiaperTestValue = new BabyDiaperTestValue { WeightDiaperDry = 2.0 }
                                                                    },
                                                                    new TestValue
                                                                    {
                                                                        ArticleTestType = ArticleType.BabyDiaper,
                                                                        BabyDiaperTestValue = new BabyDiaperTestValue { WeightDiaperDry = 1.0 }
                                                                    },
                                                                    new TestValue
                                                                    {
                                                                        ArticleTestType = ArticleType.BabyDiaper,
                                                                        BabyDiaperTestValue = new BabyDiaperTestValue { WeightDiaperDry = 8.0 }
                                                                    }
                                                                } );
            const Double expected = 3.5;
            actual.Should()
                  .Be( expected );
        }

        /// <summary>
        ///     Test Average with average and standard deviation
        /// </summary>
        [Fact]
        public void ComputeWeightAverageAllTest2()
        {
            var serviceHelper = new BabyDiaperLaborCreatorServiceHelper( new NLogLoggerFactory() );
            var actual = serviceHelper.ComputeWeightAverageAll(
                new List<TestValue>
                {
                    new TestValue
                    {
                        ArticleTestType = ArticleType.BabyDiaper,
                        BabyDiaperTestValue = new BabyDiaperTestValue { WeightDiaperDry = 3.0 },
                        TestValueType = TestValueType.Single
                    },
                    new TestValue
                    {
                        ArticleTestType = ArticleType.BabyDiaper,
                        BabyDiaperTestValue = new BabyDiaperTestValue { WeightDiaperDry = 2.0 },
                        TestValueType = TestValueType.Single
                    },
                    new TestValue
                    {
                        ArticleTestType = ArticleType.BabyDiaper,
                        BabyDiaperTestValue = new BabyDiaperTestValue { WeightDiaperDry = 1.0 },
                        TestValueType = TestValueType.Single
                    },
                    new TestValue
                    {
                        ArticleTestType = ArticleType.BabyDiaper,
                        BabyDiaperTestValue = new BabyDiaperTestValue { WeightDiaperDry = 8.0 },
                        TestValueType = TestValueType.Single
                    },
                    new TestValue
                    {
                        ArticleTestType = ArticleType.BabyDiaper,
                        TestValueType = TestValueType.Average,
                        BabyDiaperTestValue = new BabyDiaperTestValue { WeightDiaperDry = 8.0 }
                    },
                    new TestValue
                    {
                        ArticleTestType = ArticleType.BabyDiaper,
                        TestValueType = TestValueType.StandardDeviation,
                        BabyDiaperTestValue = new BabyDiaperTestValue { WeightDiaperDry = 8.0 }
                    }
                } );
            const Double expected = 3.5;
            actual.Should()
                  .Be( expected );
        }

        /// <summary>
        ///     Test Standard Deviation
        /// </summary>
        [Fact]
        public void ComputeWeightStandardDeviationAllTest1()
        {
            var serviceHelper = new BabyDiaperLaborCreatorServiceHelper( new NLogLoggerFactory() );
            var actual = serviceHelper.ComputeWeightStandardDeviationAll(
                new List<TestValue>
                {
                    new TestValue
                    {
                        ArticleTestType = ArticleType.BabyDiaper,
                        BabyDiaperTestValue = new BabyDiaperTestValue { WeightDiaperDry = 3.0 },
                        TestValueType = TestValueType.Single
                    },
                    new TestValue
                    {
                        ArticleTestType = ArticleType.BabyDiaper,
                        BabyDiaperTestValue = new BabyDiaperTestValue { WeightDiaperDry = 2.0 },
                        TestValueType = TestValueType.Single
                    },
                    new TestValue
                    {
                        ArticleTestType = ArticleType.BabyDiaper,
                        BabyDiaperTestValue = new BabyDiaperTestValue { WeightDiaperDry = 1.0 },
                        TestValueType = TestValueType.Single
                    },
                    new TestValue
                    {
                        ArticleTestType = ArticleType.BabyDiaper,
                        BabyDiaperTestValue = new BabyDiaperTestValue { WeightDiaperDry = 8.0 },
                        TestValueType = TestValueType.Single
                    }
                } );
            const Double expected = 3.1091;
            var actualRounded = Math.Round( actual, 4 );
            actualRounded.Should()
                         .Be( expected );
        }

        /// <summary>
        ///     Test Standard Deviation with average and standard deviation test values
        /// </summary>
        [Fact]
        public void ComputeWeightStandardDeviationAllTest2()
        {
            var serviceHelper = new BabyDiaperLaborCreatorServiceHelper( new NLogLoggerFactory() );
            var actual = serviceHelper.ComputeWeightStandardDeviationAll(
                new List<TestValue>
                {
                    new TestValue
                    {
                        ArticleTestType = ArticleType.BabyDiaper,
                        BabyDiaperTestValue = new BabyDiaperTestValue { WeightDiaperDry = 3.0 },
                        TestValueType = TestValueType.Single
                    },
                    new TestValue
                    {
                        ArticleTestType = ArticleType.BabyDiaper,
                        BabyDiaperTestValue = new BabyDiaperTestValue { WeightDiaperDry = 2.0 },
                        TestValueType = TestValueType.Single
                    },
                    new TestValue
                    {
                        ArticleTestType = ArticleType.BabyDiaper,
                        BabyDiaperTestValue = new BabyDiaperTestValue { WeightDiaperDry = 1.0 },
                        TestValueType = TestValueType.Single
                    },
                    new TestValue
                    {
                        ArticleTestType = ArticleType.BabyDiaper,
                        BabyDiaperTestValue = new BabyDiaperTestValue { WeightDiaperDry = 8.0 },
                        TestValueType = TestValueType.Single
                    },
                    new TestValue
                    {
                        ArticleTestType = ArticleType.BabyDiaper,
                        BabyDiaperTestValue = new BabyDiaperTestValue { WeightDiaperDry = 8.0 },
                        TestValueType = TestValueType.StandardDeviation
                    },
                    new TestValue
                    {
                        ArticleTestType = ArticleType.BabyDiaper,
                        BabyDiaperTestValue = new BabyDiaperTestValue { WeightDiaperDry = 8.0 },
                        TestValueType = TestValueType.Average
                    }
                } );
            const Double expected = 3.1091;
            var actualRounded = Math.Round( actual, 4 );
            actualRounded.Should()
                         .Be( expected );
        }

        /// <summary>
        ///     Testing GenerateProdCode 1
        /// </summary>
        [Fact]
        public void GenerateProdCodeTest1()
        {
            var serviceHelper = new BabyDiaperLaborCreatorServiceHelper( new NLogLoggerFactory() );
            const String expected = "IT/11/16/158/23:58";
            var actual = serviceHelper.GenerateProdCode( "M11", 2016, 158, new TimeSpan( 23, 58, 0 ) );

            actual.Should()
                  .Be( expected );
        }

        /// <summary>
        ///     Testing GenerateProdCode 2
        /// </summary>
        [Fact]
        public void GenerateProdCodeTest2()
        {
            var serviceHelper = new BabyDiaperLaborCreatorServiceHelper( new NLogLoggerFactory() );
            const String expected = "IT/11/16/158/23:58";
            var actual = serviceHelper.GenerateProdCode("M11", 16, 158, new TimeSpan( 23, 58, 0 ) );

            actual.Should()
                  .Be( expected );
        }

        /// <summary>
        ///     Testing GenerateProdCode 3
        /// </summary>
        [Fact]
        public void GenerateProdCodeTest3()
        {
            var serviceHelper = new BabyDiaperLaborCreatorServiceHelper( new NLogLoggerFactory() );
            const String expected = "IT/11/06/158/23:00";
            var actual = serviceHelper.GenerateProdCode("M11", 2006, 158, new TimeSpan( 23, 00, 0 ) );

            actual.Should()
                  .Be( expected );
        }

        /// <summary>
        ///     Testing GenerateProdCode 4
        /// </summary>
        [Fact]
        public void GenerateProdCodeTest4()
        {
            var serviceHelper = new BabyDiaperLaborCreatorServiceHelper( new NLogLoggerFactory() );
            const String expected = "IT/11/16/158/03:01";
            var actual = serviceHelper.GenerateProdCode( "M11", 2016, 158, new TimeSpan( 3, 1, 0 ) );

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
            var serviceHelper = new BabyDiaperLaborCreatorServiceHelper( new NLogLoggerFactory() );

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
            var serviceHelper = new BabyDiaperLaborCreatorServiceHelper( new NLogLoggerFactory() );

            Exception ex =
                Assert.Throws<InvalidDataException>(
                    () => serviceHelper.GetBabyDiaperTestValueForType(
                        BabyDiaperLaborCreatorServiceHelperData.TestValuesEmptyPenetration2(),
                        TestTypeBabyDiaper.RewetAndPenetrationTime,
                        TestValueType.Single ) );

            Assert.Equal
            ( "No Single for RewetAndPenetrationTime per Testsheet existing",
              ex.Message
            );
        }

        /// <summary>
        ///     Testing GetBabyDiaperTestValueForType Empty BabyDiaperRetention
        /// </summary>
        [Fact]
        public void GetBabyDiaperTestValueForTypeEmptyRetentionTest1()
        {
            var serviceHelper = new BabyDiaperLaborCreatorServiceHelper( new NLogLoggerFactory() );

            Exception ex =
                Assert.Throws<InvalidDataException>( () => serviceHelper.GetBabyDiaperTestValueForType( new List<TestValue>(), TestTypeBabyDiaper.Retention, TestValueType.Single ) );

            Assert.Equal( "No Single for Retention per Testsheet existing", ex.Message );
        }

        /// <summary>
        ///     Testing GetBabyDiaperTestValueForType Empty BabyDiaperRetention
        /// </summary>
        [Fact]
        public void GetBabyDiaperTestValueForTypeEmptyRetentionTest2()
        {
            var serviceHelper = new BabyDiaperLaborCreatorServiceHelper( new NLogLoggerFactory() );

            Exception ex = Assert.Throws<InvalidDataException>( () => serviceHelper.GetBabyDiaperTestValueForType(
                                                                    BabyDiaperLaborCreatorServiceHelperData.TestValuesEmptyRetention2(),
                                                                    TestTypeBabyDiaper.Retention,
                                                                    TestValueType.Single ) );

            Assert.Equal( "No Single for Retention per Testsheet existing", ex.Message );
        }

        /// <summary>
        ///     Testing GetBabyDiaperTestValueForType Empty BabyDiaperRewet 1
        /// </summary>
        [Fact]
        public void GetBabyDiaperTestValueForTypeEmptyRewetTest1()
        {
            var serviceHelper = new BabyDiaperLaborCreatorServiceHelper( new NLogLoggerFactory() );

            Exception ex =
                Assert.Throws<InvalidDataException>( () => serviceHelper.GetBabyDiaperTestValueForType(
                                                         new List<TestValue>(),
                                                         TestTypeBabyDiaper.Rewet,
                                                         TestValueType.Single ) );

            Assert.Equal( "No Single for Rewet per Testsheet existing", ex.Message );
        }

        /// <summary>
        ///     Testing GetBabyDiaperTestValueForType Empty BabyDiaperRewet 2
        /// </summary>
        [Fact]
        public void GetBabyDiaperTestValueForTypeEmptyRewetTest2()
        {
            var serviceHelper = new BabyDiaperLaborCreatorServiceHelper( new NLogLoggerFactory() );

            Exception ex =
                Assert.Throws<InvalidDataException>( () => serviceHelper.GetBabyDiaperTestValueForType(
                                                         BabyDiaperLaborCreatorServiceHelperData.TestValuesEmptyRewet2(),
                                                         TestTypeBabyDiaper.Rewet,
                                                         TestValueType.Single ) );

            Assert.Equal( "No Single for Rewet per Testsheet existing", ex.Message );
        }

        /// <summary>
        ///     Testing GetBabyDiaperTestValueForType Many BabyDiaperPenetrationTime 1
        /// </summary>
        [Fact]
        public void GetBabyDiaperTestValueForTypeManyPenetrationTimeTest1()
        {
            var serviceHelper = new BabyDiaperLaborCreatorServiceHelper( new NLogLoggerFactory() );

            Exception ex =
                Assert.Throws<InvalidDataException>( () => serviceHelper.GetBabyDiaperTestValueForType(
                                                         BabyDiaperLaborCreatorServiceHelperData.TestValuesPenetrationTimeManySingle(),
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
            var serviceHelper = new BabyDiaperLaborCreatorServiceHelper( new NLogLoggerFactory() );

            Exception ex =
                Assert.Throws<InvalidDataException>(
                    () => serviceHelper.GetBabyDiaperTestValueForType( BabyDiaperLaborCreatorServiceHelperData.TestValuesManyPenetrationTimeAverage(),
                                                                       TestTypeBabyDiaper.RewetAndPenetrationTime,
                                                                       TestValueType.Average ) );

            Assert.Equal( "Only one Average for RewetAndPenetrationTime per Testsheet allowed", ex.Message );
        }

        /// <summary>
        ///     Testing GetBabyDiaperTestValueForType Many BabyDiaperPenetrationTime 3
        /// </summary>
        [Fact]
        public void GetBabyDiaperTestValueForTypeManyPenetrationTimeTest3()
        {
            var serviceHelper = new BabyDiaperLaborCreatorServiceHelper( new NLogLoggerFactory() );

            Exception ex =
                Assert.Throws<InvalidDataException>(
                    () => serviceHelper.GetBabyDiaperTestValueForType( BabyDiaperLaborCreatorServiceHelperData.TestValuesManyPenetrationTimeStandardDeviation(),
                                                                       TestTypeBabyDiaper.RewetAndPenetrationTime,
                                                                       TestValueType.StandardDeviation ) );

            Assert.Equal( "Only one StandardDeviation for RewetAndPenetrationTime per Testsheet allowed", ex.Message );
        }

        /// <summary>
        ///     Testing GetBabyDiaperTestValueForType Many BabyDiaperRetention 1
        /// </summary>
        [Fact]
        public void GetBabyDiaperTestValueForTypeManyRetentionTest1()
        {
            var serviceHelper = new BabyDiaperLaborCreatorServiceHelper( new NLogLoggerFactory() );

            Exception ex =
                Assert.Throws<InvalidDataException>( () => serviceHelper.GetBabyDiaperTestValueForType( BabyDiaperLaborCreatorServiceHelperData.TestValuesManyRententionSingle(),
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
            var serviceHelper = new BabyDiaperLaborCreatorServiceHelper( new NLogLoggerFactory() );

            Exception ex =
                Assert.Throws<InvalidDataException>( () => serviceHelper.GetBabyDiaperTestValueForType( BabyDiaperLaborCreatorServiceHelperData.TestValuesManyRetentionAverage(),
                                                                                                        TestTypeBabyDiaper.Retention,
                                                                                                        TestValueType.Average ) );

            Assert.Equal( "Only one Average for Retention per Testsheet allowed", ex.Message );
        }

        /// <summary>
        ///     Testing GetBabyDiaperTestValueForType Many BabyDiaperRewet 3
        /// </summary>
        [Fact]
        public void GetBabyDiaperTestValueForTypeManyRetentionTest3()
        {
            var serviceHelper = new BabyDiaperLaborCreatorServiceHelper( new NLogLoggerFactory() );

            Exception ex =
                Assert.Throws<InvalidDataException>(
                    () => serviceHelper.GetBabyDiaperTestValueForType( BabyDiaperLaborCreatorServiceHelperData.TestValuesManyRetentionStandardDeviation(),
                                                                       TestTypeBabyDiaper.Retention,
                                                                       TestValueType.StandardDeviation ) );

            Assert.Equal( "Only one StandardDeviation for Retention per Testsheet allowed", ex.Message );
        }

        /// <summary>
        ///     Testing GetBabyDiaperTestValueForType Many BabyDiaperRewet 1
        /// </summary>
        [Fact]
        public void GetBabyDiaperTestValueForTypeManyRewetTest1()
        {
            var serviceHelper = new BabyDiaperLaborCreatorServiceHelper( new NLogLoggerFactory() );

            Exception ex =
                Assert.Throws<InvalidDataException>( () => serviceHelper.GetBabyDiaperTestValueForType( BabyDiaperLaborCreatorServiceHelperData.TestValuesManyRewetSingle(),
                                                                                                        TestTypeBabyDiaper.Rewet,
                                                                                                        TestValueType.Single ) );

            Assert.Equal( "Only one Single for Rewet per Testsheet allowed", ex.Message );
        }

        /// <summary>
        ///     Testing GetBabyDiaperTestValueForType Many BabyDiaperRewet 2
        /// </summary>
        [Fact]
        public void GetBabyDiaperTestValueForTypeManyRewetTest2()
        {
            var serviceHelper = new BabyDiaperLaborCreatorServiceHelper( new NLogLoggerFactory() );

            Exception ex =
                Assert.Throws<InvalidDataException>( () => serviceHelper.GetBabyDiaperTestValueForType( BabyDiaperLaborCreatorServiceHelperData.TestValuesManyRewetAverage(),
                                                                                                        TestTypeBabyDiaper.Rewet,
                                                                                                        TestValueType.Average ) );

            Assert.Equal( "Only one Average for Rewet per Testsheet allowed", ex.Message );
        }

        /// <summary>
        ///     Testing GetBabyDiaperTestValueForType Many BabyDiaperRewet 3
        /// </summary>
        [Fact]
        public void GetBabyDiaperTestValueForTypeManyRewetTest3()
        {
            var serviceHelper = new BabyDiaperLaborCreatorServiceHelper( new NLogLoggerFactory() );

            Exception ex =
                Assert.Throws<InvalidDataException>(
                    () => serviceHelper.GetBabyDiaperTestValueForType( BabyDiaperLaborCreatorServiceHelperData.TestValuesManyRewetStandardDeviation(),
                                                                       TestTypeBabyDiaper.Rewet,
                                                                       TestValueType.StandardDeviation ) );

            Assert.Equal( "Only one StandardDeviation for Rewet per Testsheet allowed", ex.Message );
        }

        /// <summary>
        ///     Testing GetBabyDiaperTestValueForType Ok BabyDiaperPenetrationTime 1
        /// </summary>
        [Fact]
        public void GetBabyDiaperTestValueForTypeOkPenetrationTimeTest1()
        {
            var serviceHelper = new BabyDiaperLaborCreatorServiceHelper( new NLogLoggerFactory() );
            var expected = new BabyDiaperTestValue { TestType = TestTypeBabyDiaper.RewetAndPenetrationTime };
            var
                actual = serviceHelper.GetBabyDiaperTestValueForType( BabyDiaperLaborCreatorServiceHelperData.TestValuesOkPenetrationTimeSingle( expected ),
                                                                      TestTypeBabyDiaper.RewetAndPenetrationTime,
                                                                      TestValueType.Single );

            actual.Should()
                  .Be( expected );
        }

        /// <summary>
        ///     Testing GetBabyDiaperTestValueForType Ok BabyDiaperPenetrationTime 2
        /// </summary>
        [Fact]
        public void GetBabyDiaperTestValueForTypeOkPenetrationTimeTest2()
        {
            var serviceHelper = new BabyDiaperLaborCreatorServiceHelper( new NLogLoggerFactory() );
            var expected = new BabyDiaperTestValue { TestType = TestTypeBabyDiaper.RewetAndPenetrationTime };
            var
                actual = serviceHelper.GetBabyDiaperTestValueForType( BabyDiaperLaborCreatorServiceHelperData.TestValuesOkPenetrationTimeAverage( expected ),
                                                                      TestTypeBabyDiaper.RewetAndPenetrationTime,
                                                                      TestValueType.Average );

            actual.Should()
                  .Be( expected );
        }

        /// <summary>
        ///     Testing GetBabyDiaperTestValueForType Ok BabyDiaperPenetrationTime 3
        /// </summary>
        [Fact]
        public void GetBabyDiaperTestValueForTypeOkPenetrationTimeTest3()
        {
            var serviceHelper = new BabyDiaperLaborCreatorServiceHelper( new NLogLoggerFactory() );
            var expected = new BabyDiaperTestValue { TestType = TestTypeBabyDiaper.RewetAndPenetrationTime };
            var
                actual = serviceHelper.GetBabyDiaperTestValueForType( BabyDiaperLaborCreatorServiceHelperData.TestValuesOKPenetrationTimeStandardDeviation( expected ),
                                                                      TestTypeBabyDiaper.RewetAndPenetrationTime,
                                                                      TestValueType.StandardDeviation );

            actual.Should()
                  .Be( expected );
        }

        /// <summary>
        ///     Testing GetBabyDiaperTestValueForType Ok BabyDiaperRetention 1
        /// </summary>
        [Fact]
        public void GetBabyDiaperTestValueForTypeOkRetentionTest1()
        {
            var serviceHelper = new BabyDiaperLaborCreatorServiceHelper( new NLogLoggerFactory() );
            var expected = new BabyDiaperTestValue { TestType = TestTypeBabyDiaper.Retention };
            var
                actual = serviceHelper.GetBabyDiaperTestValueForType( BabyDiaperLaborCreatorServiceHelperData.TestValuesOkRetentionTimeSingle( expected ),
                                                                      TestTypeBabyDiaper.Retention,
                                                                      TestValueType.Single );

            actual.Should()
                  .Be( expected );
        }

        /// <summary>
        ///     Testing GetBabyDiaperTestValueForType Ok BabyDiaperRetention 2
        /// </summary>
        [Fact]
        public void GetBabyDiaperTestValueForTypeOkRetentionTest2()
        {
            var serviceHelper = new BabyDiaperLaborCreatorServiceHelper( new NLogLoggerFactory() );
            var expected = new BabyDiaperTestValue { TestType = TestTypeBabyDiaper.Retention };
            var
                actual = serviceHelper.GetBabyDiaperTestValueForType( BabyDiaperLaborCreatorServiceHelperData.TestValuesOkRetentionAverage( expected ),
                                                                      TestTypeBabyDiaper.Retention,
                                                                      TestValueType.Average );

            actual.Should()
                  .Be( expected );
        }

        /// <summary>
        ///     Testing GetBabyDiaperTestValueForType Ok BabyDiaperRetention 1
        /// </summary>
        [Fact]
        public void GetBabyDiaperTestValueForTypeOkRetentionTest3()
        {
            var serviceHelper = new BabyDiaperLaborCreatorServiceHelper( new NLogLoggerFactory() );
            var expected = new BabyDiaperTestValue { TestType = TestTypeBabyDiaper.Retention };
            var
                actual = serviceHelper.GetBabyDiaperTestValueForType( BabyDiaperLaborCreatorServiceHelperData.TestValuesOkRetentionStandardDeviation( expected ),
                                                                      TestTypeBabyDiaper.Retention,
                                                                      TestValueType.StandardDeviation );

            actual.Should()
                  .Be( expected );
        }

        /// <summary>
        ///     Testing GetBabyDiaperTestValueForType Ok BabyDiaperRewet 1
        /// </summary>
        [Fact]
        public void GetBabyDiaperTestValueForTypeOkRewetTest1()
        {
            var serviceHelper = new BabyDiaperLaborCreatorServiceHelper( new NLogLoggerFactory() );
            var expected = new BabyDiaperTestValue { TestType = TestTypeBabyDiaper.Rewet };
            var
                actual = serviceHelper.GetBabyDiaperTestValueForType( BabyDiaperLaborCreatorServiceHelperData.TestValuesOkRewetSingle( expected ),
                                                                      TestTypeBabyDiaper.Rewet,
                                                                      TestValueType.Single );

            actual.Should()
                  .Be( expected );
        }

        /// <summary>
        ///     Testing GetBabyDiaperTestValueForType Ok BabyDiaperRewet 2
        /// </summary>
        [Fact]
        public void GetBabyDiaperTestValueForTypeOkRewetTest2()
        {
            var serviceHelper = new BabyDiaperLaborCreatorServiceHelper( new NLogLoggerFactory() );
            var expected = new BabyDiaperTestValue { TestType = TestTypeBabyDiaper.Rewet };
            var
                actual = serviceHelper.GetBabyDiaperTestValueForType( BabyDiaperLaborCreatorServiceHelperData.TestValuesOkRewetAverage( expected ),
                                                                      TestTypeBabyDiaper.Rewet,
                                                                      TestValueType.Average );

            actual.Should()
                  .Be( expected );
        }

        /// <summary>
        ///     Testing GetBabyDiaperTestValueForType Ok BabyDiaperRewet 1
        /// </summary>
        [Fact]
        public void GetBabyDiaperTestValueForTypeOkRewetTest3()
        {
            var serviceHelper = new BabyDiaperLaborCreatorServiceHelper( new NLogLoggerFactory() );
            var expected = new BabyDiaperTestValue { TestType = TestTypeBabyDiaper.Rewet };
            var
                actual = serviceHelper.GetBabyDiaperTestValueForType( BabyDiaperLaborCreatorServiceHelperData.TestValuesOkRewetStandardDeviation( expected ),
                                                                      TestTypeBabyDiaper.Rewet,
                                                                      TestValueType.StandardDeviation );

            actual.Should()
                  .Be( expected );
        }

        /// <summary>
        ///     Testing ToPenetrationTimeAverage many
        /// </summary>
        [Fact]
        public void ToPenetrationTimeAverageTestExceptionMany()
        {
            var serviceHelper = new BabyDiaperLaborCreatorServiceHelper( new NLogLoggerFactory() );

            Exception ex =
                Assert.Throws<InvalidDataException>( () => serviceHelper.ToPenetrationTimeAverage( BabyDiaperLaborCreatorServiceHelperData.TestValuesManyPenetrationTimeAverage() ) );

            Assert.Equal( "Only one Average for RewetAndPenetrationTime per Testsheet allowed", ex.Message );
        }

        /// <summary>
        ///     Testing ToPenetrationTimeAverage none
        /// </summary>
        [Fact]
        public void ToPenetrationTimeAverageTestExceptionNone()
        {
            var serviceHelper = new BabyDiaperLaborCreatorServiceHelper( new NLogLoggerFactory() );

            Exception ex =
                Assert.Throws<InvalidDataException>( () => serviceHelper.ToPenetrationTimeAverage( BabyDiaperLaborCreatorServiceHelperData.TestValuesEmptyPenetration2() ) );

            Assert.Equal( "No Average for RewetAndPenetrationTime per Testsheet existing", ex.Message );
        }

        /// <summary>
        ///     Testing ToPenetrationTimeAverage ok
        /// </summary>
        [Fact]
        public void ToPenetrationTimeAverageTestOk()
        {
            var serviceHelper = new BabyDiaperLaborCreatorServiceHelper( new NLogLoggerFactory() );
            var expected = new BabyDiaperTestValue { TestType = TestTypeBabyDiaper.RewetAndPenetrationTime };

            var actual = serviceHelper.ToPenetrationTimeAverage( BabyDiaperLaborCreatorServiceHelperData.TestValuesOkPenetrationTimeAverage( expected ) );
        }

        /// <summary>
        ///     Testing ToPenetrationTimeAverage many
        /// </summary>
        [Fact]
        public void ToPenetrationTimeStandardDeviationExceptionMany()
        {
            var serviceHelper = new BabyDiaperLaborCreatorServiceHelper( new NLogLoggerFactory() );

            Exception ex =
                Assert.Throws<InvalidDataException>(
                    () => serviceHelper.ToPenetrationTimeStandardDeviation( BabyDiaperLaborCreatorServiceHelperData.TestValuesManyPenetrationTimeStandardDeviation() ) );

            Assert.Equal( "Only one StandardDeviation for RewetAndPenetrationTime per Testsheet allowed", ex.Message );
        }

        /// <summary>
        ///     Testing ToPenetrationTimeAverage none
        /// </summary>
        [Fact]
        public void ToPenetrationTimeStandardDeviationTestExceptionNone()
        {
            var serviceHelper = new BabyDiaperLaborCreatorServiceHelper( new NLogLoggerFactory() );

            Exception ex =
                Assert.Throws<InvalidDataException>( () => serviceHelper.ToPenetrationTimeStandardDeviation( BabyDiaperLaborCreatorServiceHelperData.TestValuesEmptyPenetration2() ) );

            Assert.Equal( "No StandardDeviation for RewetAndPenetrationTime per Testsheet existing", ex.Message );
        }

        /// <summary>
        ///     Testing ToPenetrationTimeStandardDeviation ok
        /// </summary>
        [Fact]
        public void ToPenetrationTimeStandardDeviationTestOk()
        {
            var serviceHelper = new BabyDiaperLaborCreatorServiceHelper( new NLogLoggerFactory() );
            var expected = new BabyDiaperTestValue { TestType = TestTypeBabyDiaper.RewetAndPenetrationTime };

            var actual = serviceHelper.ToPenetrationTimeStandardDeviation( BabyDiaperLaborCreatorServiceHelperData.TestValuesOKPenetrationTimeStandardDeviation( expected ) );
        }

        /// <summary>
        ///     Testing To Penetration Time ok
        /// </summary>
        [Fact]
        public void ToPenetrationTimeTest()
        {
            var serviceHelper = new BabyDiaperLaborCreatorServiceHelper( new NLogLoggerFactory() );

            var expected = new BabyDiaperPenetrationTime
            {
                PenetrationTimeAdditionFirst = 1.0,
                PenetrationTimeAdditionSecond = 2.0,
                PenetrationTimeAdditionThird = 3.0,
                PenetrationTimeAdditionFourth = 4.0
            };
            var actual = serviceHelper.ToPenetrationTime(
                new BabyDiaperTestValue
                {
                    PenetrationTimeAdditionFirst = 1.0,
                    PenetrationTimeAdditionSecond = 2.0,
                    PenetrationTimeAdditionThird = 3.0,
                    PenetrationTimeAdditionFourth = 4.0
                } );

            actual.ShouldBeEquivalentTo( expected );
        }

        /// <summary>
        ///     Testing ToPenetrationTimeTestValueCollection ok
        /// </summary>
        [Fact]
        public void ToPenetrationTimeTestValueCollectionTest()
        {
            var serviceHelper = new BabyDiaperLaborCreatorServiceHelper( new NLogLoggerFactory() );

            var actual = serviceHelper.ToPenetrationTimeTestValuesCollection( BabyDiaperLaborCreatorServiceHelperData.TwoTestValuePerType() );

            actual.Count.Should()
                  .Be( 2 );
        }

        /// <summary>
        ///     Testing ToRetentionAverage many
        /// </summary>
        [Fact]
        public void ToRetentionAverageTestExceptionMany()
        {
            var serviceHelper = new BabyDiaperLaborCreatorServiceHelper( new NLogLoggerFactory() );

            Exception ex =
                Assert.Throws<InvalidDataException>( () => serviceHelper.ToRetentionAverage( BabyDiaperLaborCreatorServiceHelperData.TestValuesManyRetentionAverage() ) );

            Assert.Equal( "Only one Average for Retention per Testsheet allowed", ex.Message );
        }

        /// <summary>
        ///     Testing ToRetentionAverage none
        /// </summary>
        [Fact]
        public void ToRetentionAverageTestExceptionNone()
        {
            var serviceHelper = new BabyDiaperLaborCreatorServiceHelper( new NLogLoggerFactory() );

            Exception ex =
                Assert.Throws<InvalidDataException>( () => serviceHelper.ToRetentionAverage( BabyDiaperLaborCreatorServiceHelperData.TestValuesEmptyRetention2() ) );

            Assert.Equal( "No Average for Retention per Testsheet existing", ex.Message );
        }

        /// <summary>
        ///     Testing ToRetentionAverage ok
        /// </summary>
        [Fact]
        public void ToRetentionAverageTestOk()
        {
            var serviceHelper = new BabyDiaperLaborCreatorServiceHelper( new NLogLoggerFactory() );
            var input = new BabyDiaperTestValue { TestType = TestTypeBabyDiaper.Retention, RetentionRw = RwType.Ok };

            var actual = serviceHelper.ToRetentionAverage( BabyDiaperLaborCreatorServiceHelperData.TestValuesOkRetentionAverage( input ) );
            actual.RetentionRw.Should()
                  .Be( RwType.Ok );
        }

        /// <summary>
        ///     Testing ToRetentionStandardDeviation many
        /// </summary>
        [Fact]
        public void ToRetentionStandardDeviationTestExceptionMany()
        {
            var serviceHelper = new BabyDiaperLaborCreatorServiceHelper( new NLogLoggerFactory() );

            Exception ex =
                Assert.Throws<InvalidDataException>(
                    () => serviceHelper.ToRetentionStandardDeviation( BabyDiaperLaborCreatorServiceHelperData.TestValuesManyRetentionStandardDeviation() ) );

            Assert.Equal( "Only one StandardDeviation for Retention per Testsheet allowed", ex.Message );
        }

        /// <summary>
        ///     Testing ToRetentionStandardDeviation none
        /// </summary>
        [Fact]
        public void ToRetentionStandardDeviationTestExceptionNone()
        {
            var serviceHelper = new BabyDiaperLaborCreatorServiceHelper( new NLogLoggerFactory() );

            Exception ex =
                Assert.Throws<InvalidDataException>( () => serviceHelper.ToRetentionStandardDeviation( BabyDiaperLaborCreatorServiceHelperData.TestValuesEmptyRetention2() ) );

            Assert.Equal( "No StandardDeviation for Retention per Testsheet existing", ex.Message );
        }

        /// <summary>
        ///     Testing ToRetentionStandardDeviation ok
        /// </summary>
        [Fact]
        public void ToRetentionStandardDeviationTestOk()
        {
            var serviceHelper = new BabyDiaperLaborCreatorServiceHelper( new NLogLoggerFactory() );
            var input = new BabyDiaperTestValue { TestType = TestTypeBabyDiaper.Retention, RetentionRw = RwType.Ok };

            var actual = serviceHelper.ToRetentionStandardDeviation( BabyDiaperLaborCreatorServiceHelperData.TestValuesOkRetentionStandardDeviation( input ) );
            actual.RetentionRw.Should()
                  .Be( RwType.Ok );
        }

        /// <summary>
        ///     Testing ToRetentionTestValueCollection ok
        /// </summary>
        [Fact]
        public void ToRetentionTestValueCollectionTest()
        {
            var serviceHelper = new BabyDiaperLaborCreatorServiceHelper( new NLogLoggerFactory() );

            var actual = serviceHelper.ToRetentionTestValuesCollection( BabyDiaperLaborCreatorServiceHelperData.TwoTestValuePerType() );

            actual.Count.Should()
                  .Be( 2 );
        }

        /// <summary>
        ///     Testing ToRewetAverage many
        /// </summary>
        [Fact]
        public void ToRewetAverageTestExceptionMany()
        {
            var serviceHelper = new BabyDiaperLaborCreatorServiceHelper( new NLogLoggerFactory() );

            Exception ex =
                Assert.Throws<InvalidDataException>( () => serviceHelper.ToRewetAverage( BabyDiaperLaborCreatorServiceHelperData.TestValuesManyRewetAverage() ) );

            Assert.Equal( "Only one Average for Rewet per Testsheet allowed", ex.Message );
        }

        /// <summary>
        ///     Testing ToRewetAverage none
        /// </summary>
        [Fact]
        public void ToRewetAverageTestExceptionNone()
        {
            var serviceHelper = new BabyDiaperLaborCreatorServiceHelper( new NLogLoggerFactory() );

            Exception ex =
                Assert.Throws<InvalidDataException>( () => serviceHelper.ToRewetAverage( BabyDiaperLaborCreatorServiceHelperData.TestValuesEmptyRewet2() ) );

            Assert.Equal( "No Average for Rewet per Testsheet existing", ex.Message );
        }

        /// <summary>
        ///     Testing ToRewetAverage ok
        /// </summary>
        [Fact]
        public void ToRewetAverageTestOk()
        {
            var serviceHelper = new BabyDiaperLaborCreatorServiceHelper( new NLogLoggerFactory() );
            var expected = new BabyDiaperTestValue { TestType = TestTypeBabyDiaper.Rewet, Rewet210Rw = RwType.Better, Rewet140Rw = RwType.Ok };

            var actual = serviceHelper.ToRewetAverage( BabyDiaperLaborCreatorServiceHelperData.TestValuesOkRewetAverage( expected ) );
            actual.Rewet210Rw.Should()
                  .Be( RwType.Better );
            actual.Rewet140Rw.Should()
                  .Be( RwType.Ok );
        }

        /// <summary>
        ///     Testing ToRewetStandardDeviation many
        /// </summary>
        [Fact]
        public void ToRewetStandardDeviationTestExceptionMany()
        {
            var serviceHelper = new BabyDiaperLaborCreatorServiceHelper( new NLogLoggerFactory() );

            Exception ex =
                Assert.Throws<InvalidDataException>( () => serviceHelper.ToRewetStandardDeviation( BabyDiaperLaborCreatorServiceHelperData.TestValuesManyRewetStandardDeviation() ) );

            Assert.Equal( "Only one StandardDeviation for Rewet per Testsheet allowed", ex.Message );
        }

        /// <summary>
        ///     Testing ToRewetStandardDeviation none
        /// </summary>
        [Fact]
        public void ToRewetStandardDeviationTestExceptionNone()
        {
            var serviceHelper = new BabyDiaperLaborCreatorServiceHelper( new NLogLoggerFactory() );

            Exception ex =
                Assert.Throws<InvalidDataException>( () => serviceHelper.ToRewetStandardDeviation( BabyDiaperLaborCreatorServiceHelperData.TestValuesEmptyRewet2() ) );

            Assert.Equal( "No StandardDeviation for Rewet per Testsheet existing", ex.Message );
        }

        /// <summary>
        ///     Testing ToReweStandardDeviation ok
        /// </summary>
        [Fact]
        public void ToRewetStandardDeviationTestOk()
        {
            var serviceHelper = new BabyDiaperLaborCreatorServiceHelper( new NLogLoggerFactory() );
            var expected = new BabyDiaperTestValue { TestType = TestTypeBabyDiaper.Rewet, Rewet210Rw = RwType.Better, Rewet140Rw = RwType.Ok };

            var actual = serviceHelper.ToRewetStandardDeviation( BabyDiaperLaborCreatorServiceHelperData.TestValuesOkRewetStandardDeviation( expected ) );
            actual.Rewet210Rw.Should()
                  .Be( RwType.Better );
            actual.Rewet140Rw.Should()
                  .Be( RwType.Ok );
        }

        /// <summary>
        ///     Testing to BabyDiaperRewet ok
        /// </summary>
        [Fact]
        public void ToRewetTestOk()
        {
            var serviceHelper = new BabyDiaperLaborCreatorServiceHelper( new NLogLoggerFactory() );
            var expected = new BabyDiaperRewet
            {
                Rewet140Rw = RwType.Ok,
                Rewet210Rw = RwType.Ok,
                Rewet140Value = 1.0,
                Rewet210Value = 2.0,
                DistributionOfTheStrikeTrough = 3.0,
                StrikeTroughValue = 4.0
            };
            var actual = serviceHelper.ToRewet(
                new BabyDiaperTestValue
                {
                    Rewet210Rw = RwType.Ok,
                    Rewet140Rw = RwType.Ok,
                    Rewet140Value = 1.0,
                    Rewet210Value = 2.0,
                    DistributionOfTheStrikeTrough = 3.0,
                    StrikeTroughValue = 4.0
                } );

            actual.DistributionOfTheStrikeTrough.Should()
                  .Be( expected.DistributionOfTheStrikeTrough );
            actual.Rewet140Rw.Should()
                  .Be( expected.Rewet140Rw );
            actual.Rewet140Value.Should()
                  .Be( expected.Rewet140Value );
            actual.Rewet210Value.Should()
                  .Be( expected.Rewet210Value );
            actual.Rewet210Rw.Should()
                  .Be( expected.Rewet210Rw );
            actual.StrikeTroughValue.Should()
                  .Be( expected.StrikeTroughValue );
            actual.ShouldBeEquivalentTo( expected );
        }

        /// <summary>
        ///     Testing ToRewetTestValueCollection ok
        /// </summary>
        [Fact]
        public void ToRewetTestValueCollectionTest()
        {
            var serviceHelper = new BabyDiaperLaborCreatorServiceHelper( new NLogLoggerFactory() );

            var actual = serviceHelper.ToRewetTestValuesCollection( BabyDiaperLaborCreatorServiceHelperData.TwoTestValuePerType() );

            actual.Count.Should()
                  .Be( 4 );
        }

        /// <summary>
        ///     Testing ToRewetTestValue ok
        /// </summary>
        [Fact]
        public void ToRewetTestValueTest()
        {
            var serviceHelper = new BabyDiaperLaborCreatorServiceHelper( new NLogLoggerFactory() );
            var expected = new BabyDiaperRewetTestValue
            {
                BabyDiaperRewet = new BabyDiaperRewet { Rewet140Rw = RwType.Better, Rewet210Rw = RwType.Better },
                TestInfo = new TestInfo
                {
                    TestPerson = "test person",
                    ProductionCode = "theprodcode",
                    WeightyDiaperDry = 666,
                    TestValueId = 1
                }
            };

            var actual = serviceHelper.ToRewetTestValue( new BabyDiaperTestValue { WeightDiaperDry = 666, Rewet140Rw = RwType.Better, Rewet210Rw = RwType.Better },
                                                         "test person",
                                                         "theprodcode",
                                                         1 );

            actual.ShouldBeEquivalentTo( expected );
        }

        /// <summary>
        ///     Testing toTestInfo
        /// </summary>
        [Fact]
        public void ToTestInfoTest()
        {
            var serviceHelper = new BabyDiaperLaborCreatorServiceHelper( new NLogLoggerFactory() );

            var expected = new TestInfo
            {
                TestPerson = "gandalf",
                ProductionCode = "you shall not pass",
                WeightyDiaperDry = 666,
                TestValueId = 1
            };

            var actual = serviceHelper.toTestInfo( "gandalf", "you shall not pass", 666, 1 );

            actual.ShouldBeEquivalentTo( expected );
        }

        /// <summary>
        ///     Testing ToTestValuesCollectionByTestType  1
        /// </summary>
        [Fact]
        public void ToTestValuesCollectionByTestTypeTest1()
        {
            var serviceHelper = new BabyDiaperLaborCreatorServiceHelper( new NLogLoggerFactory() );

            var actual = serviceHelper.ToTestValuesCollectionByTestType( BabyDiaperLaborCreatorServiceHelperData.TwoTestValuePerType(),
                                                                         TestValueType.Single,
                                                                         new List<TestTypeBabyDiaper> { TestTypeBabyDiaper.Retention },
                                                                         serviceHelper.ToRetentionTestValue )
                                      .ToList();
            actual.Count.Should()
                  .Be( 2 );
        }

        /// <summary>
        ///     Testing ToTestValuesCollectionByTestType  1
        /// </summary>
        [Fact]
        public void ToTestValuesCollectionByTestTypeTest2()
        {
            var serviceHelper = new BabyDiaperLaborCreatorServiceHelper( new NLogLoggerFactory() );

            var actual = serviceHelper.ToTestValuesCollectionByTestType( BabyDiaperLaborCreatorServiceHelperData.TwoTestValuePerType(),
                                                                         TestValueType.Single,
                                                                         new List<TestTypeBabyDiaper> { TestTypeBabyDiaper.Rewet },
                                                                         serviceHelper.ToRewetTestValue )
                                      .ToList();
            actual.Count.Should()
                  .Be( 2 );
        }

        /// <summary>
        ///     Testing ToTestValuesCollectionByTestType  1
        /// </summary>
        [Fact]
        public void ToTestValuesCollectionByTestTypeTest3()
        {
            var serviceHelper = new BabyDiaperLaborCreatorServiceHelper( new NLogLoggerFactory() );

            var actual = serviceHelper.ToTestValuesCollectionByTestType( BabyDiaperLaborCreatorServiceHelperData.TwoTestValuePerType(),
                                                                         TestValueType.Single,
                                                                         new List<TestTypeBabyDiaper> { TestTypeBabyDiaper.RewetAndPenetrationTime },
                                                                         serviceHelper.ToPenetrationTimeTestValue )
                                      .ToList();
            actual.Count.Should()
                  .Be( 2 );
        }

        /// <summary>
        ///     Testing ToTestValuesCollectionByTestType  4
        /// </summary>
        [Fact]
        public void ToTestValuesCollectionByTestTypeTest4()
        {
            var serviceHelper = new BabyDiaperLaborCreatorServiceHelper( new NLogLoggerFactory() );

            var actual = serviceHelper.ToTestValuesCollectionByTestType( BabyDiaperLaborCreatorServiceHelperData.TwoTestValuePerType(),
                                                                         TestValueType.Average,
                                                                         new List<TestTypeBabyDiaper> { TestTypeBabyDiaper.Retention },
                                                                         serviceHelper.ToRetentionTestValue )
                                      .ToList();
            actual.Count.Should()
                  .Be( 1 );
        }

        /// <summary>
        ///     Testing ToTestValuesCollectionByTestType  5
        /// </summary>
        [Fact]
        public void ToTestValuesCollectionByTestTypeTest5()
        {
            var serviceHelper = new BabyDiaperLaborCreatorServiceHelper( new NLogLoggerFactory() );

            var actual = serviceHelper.ToTestValuesCollectionByTestType( BabyDiaperLaborCreatorServiceHelperData.TwoTestValuePerType(),
                                                                         TestValueType.StandardDeviation,
                                                                         new List<TestTypeBabyDiaper> { TestTypeBabyDiaper.Retention },
                                                                         serviceHelper.ToRetentionTestValue )
                                      .ToList();
            actual.Count.Should()
                  .Be( 1 );
        }

        /// <summary>
        ///     Testing ValidateRequiredItem 1
        /// </summary>
        [Fact]
        public void ValidateRequiredItemTest1()
        {
            var serviceHelper = new BabyDiaperLaborCreatorServiceHelper( new NLogLoggerFactory() );

            serviceHelper.ValidateRequiredItem( "something", "something" );
        }

        /// <summary>
        ///     Testing ValidateRequiredItem 2
        /// </summary>
        [Fact]
        public void ValidateRequiredItemTest2()
        {
            var serviceHelper = new BabyDiaperLaborCreatorServiceHelper( new NLogLoggerFactory() );

            String something = null;

            Exception ex =
                Assert.Throws<InvalidDataException>(
                    () => serviceHelper.ValidateRequiredItem( something, "something" ) );

            Assert.Equal( "Item something of type " + typeof(String) + " is required and can not be null", ex.Message );
        }

        /// <summary>
        ///     Testing ValidateTestValueOnlyExactlyOneHasToExist1
        /// </summary>
        [Fact]
        public void ValidateTestValueOnlyExactlyOneHasToExistTest1()
        {
            var serviceHelper = new BabyDiaperLaborCreatorServiceHelper( new NLogLoggerFactory() );
            const String valueType = "type";
            const String testType = "test";

            Exception ex =
                Assert.Throws<InvalidDataException>( () => serviceHelper.ValidateTestValueOnlyExactlyOneHasToExist( new List<TestValue>(), testType, valueType ) );

            Assert.Equal( "No " + valueType + " for " + testType + " per Testsheet existing", ex.Message );
        }

        /// <summary>
        ///     Testing ValidateTestValueOnlyExactlyOneHasToExist2
        /// </summary>
        [Fact]
        public void ValidateTestValueOnlyExactlyOneHasToExistTest2()
        {
            var serviceHelper = new BabyDiaperLaborCreatorServiceHelper( new NLogLoggerFactory() );
            const String valueType = "type";
            const String testType = "test";

            Exception ex =
                Assert.Throws<InvalidDataException>(
                    () => serviceHelper.ValidateTestValueOnlyExactlyOneHasToExist( new List<TestValue> { new TestValue(), new TestValue() }, testType, valueType ) );

            Assert.Equal( "Only one " + valueType + " for " + testType + " per Testsheet allowed", ex.Message );
        }

        /// <summary>
        ///     Testing ValidateTestValueOnlyExactlyOneHasToExist3
        /// </summary>
        [Fact]
        public void ValidateTestValueOnlyExactlyOneHasToExistTest3()
        {
            var serviceHelper = new BabyDiaperLaborCreatorServiceHelper( new NLogLoggerFactory() );
            const String valueType = "type";
            const String testType = "test";

            serviceHelper.ValidateTestValueOnlyExactlyOneHasToExist( new List<TestValue> { new TestValue() }, testType, valueType );
        }
    }
}