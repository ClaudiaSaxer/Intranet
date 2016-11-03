using System;
using System.Collections.Generic;
using System.IO;
using FluentAssertions;
using Intranet.Common;
using Intranet.Labor.Model.labor;
using Intranet.Labor.Test;
using Intranet.Labor.ViewModel;
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
                        LaborCreatorServiceHelperData.TestValuesEmptyPenetration2(),
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
                                                                    LaborCreatorServiceHelperData.TestValuesEmptyRetention2(),
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
                                                         LaborCreatorServiceHelperData.TestValuesEmptyRewet2(),
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
                                                         LaborCreatorServiceHelperData.TestValuesPenetrationTimeManySingle(),
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
                Assert.Throws<InvalidDataException>( () => serviceHelper.GetBabyDiaperTestValueForType( LaborCreatorServiceHelperData.TestValuesManyPenetrationTimeAverage(),
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
                Assert.Throws<InvalidDataException>(
                    () => serviceHelper.GetBabyDiaperTestValueForType( LaborCreatorServiceHelperData.TestValuesManyPenetrationTimeStandardDeviation(),
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
                Assert.Throws<InvalidDataException>( () => serviceHelper.GetBabyDiaperTestValueForType( LaborCreatorServiceHelperData.TestValuesManyRententionSingle(),
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
                Assert.Throws<InvalidDataException>( () => serviceHelper.GetBabyDiaperTestValueForType( LaborCreatorServiceHelperData.TestValuesManyRetentionAverage(),
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
                Assert.Throws<InvalidDataException>( () => serviceHelper.GetBabyDiaperTestValueForType( LaborCreatorServiceHelperData.TestValuesManyRetentionStandardDeviation(),
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
                Assert.Throws<InvalidDataException>( () => serviceHelper.GetBabyDiaperTestValueForType( LaborCreatorServiceHelperData.TestValuesManyRewetSingle(),
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
                Assert.Throws<InvalidDataException>( () => serviceHelper.GetBabyDiaperTestValueForType( LaborCreatorServiceHelperData.TestValuesManyRewetAverage(),
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
                Assert.Throws<InvalidDataException>( () => serviceHelper.GetBabyDiaperTestValueForType( LaborCreatorServiceHelperData.TestValuesManyRewetStandardDeviation(),
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
                actual = serviceHelper.GetBabyDiaperTestValueForType( LaborCreatorServiceHelperData.TestValuesOkPenetrationTimeSingle( expected ),
                                                                      TestTypeBabyDiaper.RewetAndPenetrationTime,
                                                                      TestValueType.Single );

            actual.Should()
                  .Be( expected );
        }

        /// <summary>
        ///     Testing GetBabyDiaperTestValueForType Ok PenetrationTime 2
        /// </summary>
        [Fact]
        public void GetBabyDiaperTestValueForTypeOkPenetrationTimeTest2()
        {
            var serviceHelper = new LaborCreatorServiceHelper( new NLogLoggerFactory() );
            var expected = new BabyDiaperTestValue { TestType = TestTypeBabyDiaper.RewetAndPenetrationTime };
            var
                actual = serviceHelper.GetBabyDiaperTestValueForType( LaborCreatorServiceHelperData.TestValuesOkPenetrationTimeAverage( expected ),
                                                                      TestTypeBabyDiaper.RewetAndPenetrationTime,
                                                                      TestValueType.Average );

            actual.Should()
                  .Be( expected );
        }

        /// <summary>
        ///     Testing GetBabyDiaperTestValueForType Ok PenetrationTime 3
        /// </summary>
        [Fact]
        public void GetBabyDiaperTestValueForTypeOkPenetrationTimeTest3()
        {
            var serviceHelper = new LaborCreatorServiceHelper( new NLogLoggerFactory() );
            var expected = new BabyDiaperTestValue { TestType = TestTypeBabyDiaper.RewetAndPenetrationTime };
            var
                actual = serviceHelper.GetBabyDiaperTestValueForType( LaborCreatorServiceHelperData.TestValuesOKPenetrationTimeStandardDeviation( expected ),
                                                                      TestTypeBabyDiaper.RewetAndPenetrationTime,
                                                                      TestValueType.StandardDeviation );

            actual.Should()
                  .Be( expected );
        }

        /// <summary>
        ///     Testing GetBabyDiaperTestValueForType Ok Retention 1
        /// </summary>
        [Fact]
        public void GetBabyDiaperTestValueForTypeOkRetentionTest1()
        {
            var serviceHelper = new LaborCreatorServiceHelper( new NLogLoggerFactory() );
            var expected = new BabyDiaperTestValue { TestType = TestTypeBabyDiaper.Retention };
            var
                actual = serviceHelper.GetBabyDiaperTestValueForType( LaborCreatorServiceHelperData.TestValuesOkRetentionTimeSingle( expected ),
                                                                      TestTypeBabyDiaper.Retention,
                                                                      TestValueType.Single );

            actual.Should()
                  .Be( expected );
        }

        /// <summary>
        ///     Testing GetBabyDiaperTestValueForType Ok Retention 2
        /// </summary>
        [Fact]
        public void GetBabyDiaperTestValueForTypeOkRetentionTest2()
        {
            var serviceHelper = new LaborCreatorServiceHelper( new NLogLoggerFactory() );
            var expected = new BabyDiaperTestValue { TestType = TestTypeBabyDiaper.Retention };
            var
                actual = serviceHelper.GetBabyDiaperTestValueForType( LaborCreatorServiceHelperData.TestValuesOkRetentionAverage( expected ),
                                                                      TestTypeBabyDiaper.Retention,
                                                                      TestValueType.Average );

            actual.Should()
                  .Be( expected );
        }

        /// <summary>
        ///     Testing GetBabyDiaperTestValueForType Ok Retention 1
        /// </summary>
        [Fact]
        public void GetBabyDiaperTestValueForTypeOkRetentionTest3()
        {
            var serviceHelper = new LaborCreatorServiceHelper( new NLogLoggerFactory() );
            var expected = new BabyDiaperTestValue { TestType = TestTypeBabyDiaper.Retention };
            var
                actual = serviceHelper.GetBabyDiaperTestValueForType( LaborCreatorServiceHelperData.TestValuesOkRetentionStandardDeviation( expected ),
                                                                      TestTypeBabyDiaper.Retention,
                                                                      TestValueType.StandardDeviation );

            actual.Should()
                  .Be( expected );
        }

        /// <summary>
        ///     Testing GetBabyDiaperTestValueForType Ok Rewet 1
        /// </summary>
        [Fact]
        public void GetBabyDiaperTestValueForTypeOkRewetTest1()
        {
            var serviceHelper = new LaborCreatorServiceHelper( new NLogLoggerFactory() );
            var expected = new BabyDiaperTestValue { TestType = TestTypeBabyDiaper.Rewet };
            var
                actual = serviceHelper.GetBabyDiaperTestValueForType( LaborCreatorServiceHelperData.TestValuesOkRewetSingle( expected ),
                                                                      TestTypeBabyDiaper.Rewet,
                                                                      TestValueType.Single );

            actual.Should()
                  .Be( expected );
        }

        /// <summary>
        ///     Testing GetBabyDiaperTestValueForType Ok Rewet 2
        /// </summary>
        [Fact]
        public void GetBabyDiaperTestValueForTypeOkRewetTest2()
        {
            var serviceHelper = new LaborCreatorServiceHelper( new NLogLoggerFactory() );
            var expected = new BabyDiaperTestValue { TestType = TestTypeBabyDiaper.Rewet };
            var
                actual = serviceHelper.GetBabyDiaperTestValueForType( LaborCreatorServiceHelperData.TestValuesOkRewetAverage( expected ),
                                                                      TestTypeBabyDiaper.Rewet,
                                                                      TestValueType.Average );

            actual.Should()
                  .Be( expected );
        }

        /// <summary>
        ///     Testing GetBabyDiaperTestValueForType Ok Rewet 1
        /// </summary>
        [Fact]
        public void GetBabyDiaperTestValueForTypeOkRewetTest3()
        {
            var serviceHelper = new LaborCreatorServiceHelper( new NLogLoggerFactory() );
            var expected = new BabyDiaperTestValue { TestType = TestTypeBabyDiaper.Rewet };
            var
                actual = serviceHelper.GetBabyDiaperTestValueForType( LaborCreatorServiceHelperData.TestValuesOkRewetStandardDeviation( expected ),
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
            var serviceHelper = new LaborCreatorServiceHelper( new NLogLoggerFactory() );

            Exception ex =
                Assert.Throws<InvalidDataException>( () => serviceHelper.ToPenetrationTimeAverage( LaborCreatorServiceHelperData.TestValuesManyPenetrationTimeAverage() ) );

            Assert.Equal( "Only one Average for RewetAndPenetrationTime per Testsheet allowed", ex.Message );
        }

        /// <summary>
        ///     Testing ToPenetrationTimeAverage none
        /// </summary>
        [Fact]
        public void ToPenetrationTimeAverageTestExceptionNone()
        {
            var serviceHelper = new LaborCreatorServiceHelper( new NLogLoggerFactory() );

            Exception ex =
                Assert.Throws<InvalidDataException>( () => serviceHelper.ToPenetrationTimeAverage( LaborCreatorServiceHelperData.TestValuesEmptyPenetration2() ) );

            Assert.Equal( "No Average for RewetAndPenetrationTime per Testsheet existing", ex.Message );
        }

        /// <summary>
        ///     Testing ToPenetrationTimeAverage ok
        /// </summary>
        [Fact]
        public void ToPenetrationTimeAverageTestOk()
        {
            var serviceHelper = new LaborCreatorServiceHelper( new NLogLoggerFactory() );
            var expected = new BabyDiaperTestValue { TestType = TestTypeBabyDiaper.RewetAndPenetrationTime };

            var actual = serviceHelper.ToPenetrationTimeAverage( LaborCreatorServiceHelperData.TestValuesOkPenetrationTimeAverage( expected ) );
        }

        /// <summary>
        ///     Testing ToPenetrationTimeAverage many
        /// </summary>
        [Fact]
        public void ToPenetrationTimeStandardDeviationExceptionMany()
        {
            var serviceHelper = new LaborCreatorServiceHelper( new NLogLoggerFactory() );

            Exception ex =
                Assert.Throws<InvalidDataException>(
                    () => serviceHelper.ToPenetrationTimeStandardDeviation( LaborCreatorServiceHelperData.TestValuesManyPenetrationTimeStandardDeviation() ) );

            Assert.Equal( "Only one StandardDeviation for RewetAndPenetrationTime per Testsheet allowed", ex.Message );
        }

        /// <summary>
        ///     Testing ToPenetrationTimeAverage none
        /// </summary>
        [Fact]
        public void ToPenetrationTimeStandardDeviationTestExceptionNone()
        {
            var serviceHelper = new LaborCreatorServiceHelper( new NLogLoggerFactory() );

            Exception ex =
                Assert.Throws<InvalidDataException>( () => serviceHelper.ToPenetrationTimeStandardDeviation( LaborCreatorServiceHelperData.TestValuesEmptyPenetration2() ) );

            Assert.Equal( "No StandardDeviation for RewetAndPenetrationTime per Testsheet existing", ex.Message );
        }

        /// <summary>
        ///     Testing ToPenetrationTimeStandardDeviation ok
        /// </summary>
        [Fact]
        public void ToPenetrationTimeStandardDeviationTestOk()
        {
            var serviceHelper = new LaborCreatorServiceHelper( new NLogLoggerFactory() );
            var expected = new BabyDiaperTestValue { TestType = TestTypeBabyDiaper.RewetAndPenetrationTime };

            var actual = serviceHelper.ToPenetrationTimeStandardDeviation( LaborCreatorServiceHelperData.TestValuesOKPenetrationTimeStandardDeviation( expected ) );
        }

        /// <summary>
        ///     Testing To Penetration Time ok
        /// </summary>
        [Fact]
        public void ToPenetrationTimeTest()
        {
            var serviceHelper = new LaborCreatorServiceHelper( new NLogLoggerFactory() );

            var expected = new PenetrationTime
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

            actual.ShouldBeEquivalentTo(expected);

        }

        /// <summary>
        ///     Testing ToRetentionAverage many
        /// </summary>
        [Fact]
        public void ToRetentionAverageTestExceptionMany()
        {
            var serviceHelper = new LaborCreatorServiceHelper( new NLogLoggerFactory() );

            Exception ex =
                Assert.Throws<InvalidDataException>( () => serviceHelper.ToRetentionAverage( LaborCreatorServiceHelperData.TestValuesManyRetentionAverage() ) );

            Assert.Equal( "Only one Average for Retention per Testsheet allowed", ex.Message );
        }

        /// <summary>
        ///     Testing ToRetentionAverage none
        /// </summary>
        [Fact]
        public void ToRetentionAverageTestExceptionNone()
        {
            var serviceHelper = new LaborCreatorServiceHelper( new NLogLoggerFactory() );

            Exception ex =
                Assert.Throws<InvalidDataException>( () => serviceHelper.ToRetentionAverage( LaborCreatorServiceHelperData.TestValuesEmptyRetention2() ) );

            Assert.Equal( "No Average for Retention per Testsheet existing", ex.Message );
        }

        /// <summary>
        ///     Testing ToRetentionAverage ok
        /// </summary>
        [Fact]
        public void ToRetentionAverageTestOk()
        {
            var serviceHelper = new LaborCreatorServiceHelper( new NLogLoggerFactory() );
            var input = new BabyDiaperTestValue { TestType = TestTypeBabyDiaper.Retention, RetentionRw = RwType.Ok };

            var actual = serviceHelper.ToRetentionAverage( LaborCreatorServiceHelperData.TestValuesOkRetentionAverage( input ) );
            actual.RetentionRw.Should()
                  .Be( RwType.Ok );
        }

        /// <summary>
        ///     Testing ToRetentionStandardDeviation many
        /// </summary>
        [Fact]
        public void ToRetentionStandardDeviationTestExceptionMany()
        {
            var serviceHelper = new LaborCreatorServiceHelper( new NLogLoggerFactory() );

            Exception ex =
                Assert.Throws<InvalidDataException>( () => serviceHelper.ToRetentionStandardDeviation( LaborCreatorServiceHelperData.TestValuesManyRetentionStandardDeviation() ) );

            Assert.Equal( "Only one StandardDeviation for Retention per Testsheet allowed", ex.Message );
        }

        /// <summary>
        ///     Testing ToRetentionStandardDeviation none
        /// </summary>
        [Fact]
        public void ToRetentionStandardDeviationTestExceptionNone()
        {
            var serviceHelper = new LaborCreatorServiceHelper( new NLogLoggerFactory() );

            Exception ex =
                Assert.Throws<InvalidDataException>( () => serviceHelper.ToRetentionStandardDeviation( LaborCreatorServiceHelperData.TestValuesEmptyRetention2() ) );

            Assert.Equal( "No StandardDeviation for Retention per Testsheet existing", ex.Message );
        }

        /// <summary>
        ///     Testing ToRetentionStandardDeviation ok
        /// </summary>
        [Fact]
        public void ToRetentionStandardDeviationTestOk()
        {
            var serviceHelper = new LaborCreatorServiceHelper( new NLogLoggerFactory() );
            var input = new BabyDiaperTestValue { TestType = TestTypeBabyDiaper.Retention, RetentionRw = RwType.Ok };

            var actual = serviceHelper.ToRetentionStandardDeviation( LaborCreatorServiceHelperData.TestValuesOkRetentionStandardDeviation( input ) );
            actual.RetentionRw.Should()
                  .Be( RwType.Ok );
        }

        /// <summary>
        ///     Testing ToRewetAverage many
        /// </summary>
        [Fact]
        public void ToRewetAverageTestExceptionMany()
        {
            var serviceHelper = new LaborCreatorServiceHelper( new NLogLoggerFactory() );

            Exception ex =
                Assert.Throws<InvalidDataException>( () => serviceHelper.ToRewetAverage( LaborCreatorServiceHelperData.TestValuesManyRewetAverage() ) );

            Assert.Equal( "Only one Average for Rewet per Testsheet allowed", ex.Message );
        }

        /// <summary>
        ///     Testing ToRewetAverage none
        /// </summary>
        [Fact]
        public void ToRewetAverageTestExceptionNone()
        {
            var serviceHelper = new LaborCreatorServiceHelper( new NLogLoggerFactory() );

            Exception ex =
                Assert.Throws<InvalidDataException>( () => serviceHelper.ToRewetAverage( LaborCreatorServiceHelperData.TestValuesEmptyRewet2() ) );

            Assert.Equal( "No Average for Rewet per Testsheet existing", ex.Message );
        }

        /// <summary>
        ///     Testing ToRewetAverage ok
        /// </summary>
        [Fact]
        public void ToRewetAverageTestOk()
        {
            var serviceHelper = new LaborCreatorServiceHelper( new NLogLoggerFactory() );
            var expected = new BabyDiaperTestValue { TestType = TestTypeBabyDiaper.Rewet, Rewet210Rw = RwType.Better, Rewet140Rw = RwType.Ok };

            var actual = serviceHelper.ToRewetAverage( LaborCreatorServiceHelperData.TestValuesOkRewetAverage( expected ) );
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
            var serviceHelper = new LaborCreatorServiceHelper( new NLogLoggerFactory() );

            Exception ex =
                Assert.Throws<InvalidDataException>( () => serviceHelper.ToRewetStandardDeviation( LaborCreatorServiceHelperData.TestValuesManyRewetStandardDeviation() ) );

            Assert.Equal( "Only one StandardDeviation for Rewet per Testsheet allowed", ex.Message );
        }

        /// <summary>
        ///     Testing ToRewetStandardDeviation none
        /// </summary>
        [Fact]
        public void ToRewetStandardDeviationTestExceptionNone()
        {
            var serviceHelper = new LaborCreatorServiceHelper( new NLogLoggerFactory() );

            Exception ex =
                Assert.Throws<InvalidDataException>( () => serviceHelper.ToRewetStandardDeviation( LaborCreatorServiceHelperData.TestValuesEmptyRewet2() ) );

            Assert.Equal( "No StandardDeviation for Rewet per Testsheet existing", ex.Message );
        }

        /// <summary>
        ///     Testing ToReweStandardDeviation ok
        /// </summary>
        [Fact]
        public void ToRewetStandardDeviationTestOk()
        {
            var serviceHelper = new LaborCreatorServiceHelper( new NLogLoggerFactory() );
            var expected = new BabyDiaperTestValue { TestType = TestTypeBabyDiaper.Rewet, Rewet210Rw = RwType.Better, Rewet140Rw = RwType.Ok };

            var actual = serviceHelper.ToRewetStandardDeviation( LaborCreatorServiceHelperData.TestValuesOkRewetStandardDeviation( expected ) );
            actual.Rewet210Rw.Should()
                  .Be( RwType.Better );
            actual.Rewet140Rw.Should()
                  .Be( RwType.Ok );
        }

        /// <summary>
        ///     Testing to Rewet ok
        /// </summary>
        [Fact]
        public void toRewetTestOk()
        {
            var serviceHelper = new LaborCreatorServiceHelper( new NLogLoggerFactory() );
            var expected = new Rewet
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
    }
}