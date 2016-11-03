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
                    () => serviceHelper.GetBabyDiaperTestValueForType( new List<TestValue>(), TestTypeBabyDiaper.RewetAndPenetrationTime, TestValueType.Single ) );

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
            var serviceHelper = new LaborCreatorServiceHelper(new NLogLoggerFactory());

            Exception ex =
                Assert.Throws<InvalidDataException>(
                    () => serviceHelper.GetBabyDiaperTestValueForType(new List<TestValue>
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
                                                                    }, TestTypeBabyDiaper.RewetAndPenetrationTime, TestValueType.Single));

            Assert.Equal
            ("No Single for RewetAndPenetrationTime per Testsheet existing",
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
                Assert.Throws<InvalidDataException>( () => serviceHelper.GetBabyDiaperTestValueForType( new List<TestValue>(), TestTypeBabyDiaper.Rewet, TestValueType.Single ) );

            Assert.Equal( "No Single for Rewet per Testsheet existing", ex.Message );
        }
        /// <summary>
        ///     Testing GetBabyDiaperTestValueForType Empty Rewet 2
        /// </summary>
        [Fact]
        public void GetBabyDiaperTestValueForTypeEmptyRewetTest2()
        {
            var serviceHelper = new LaborCreatorServiceHelper(new NLogLoggerFactory());

            Exception ex =
                Assert.Throws<InvalidDataException>(() => serviceHelper.GetBabyDiaperTestValueForType(new List<TestValue>
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
                                                                            BabyDiaperTestValue = new BabyDiaperTestValue { TestType = TestTypeBabyDiaper.RewetAndPenetrationTime }
                                                                        }
                                                                    }, TestTypeBabyDiaper.Rewet, TestValueType.Single));

            Assert.Equal("No Single for Rewet per Testsheet existing", ex.Message);
        }
    }
}