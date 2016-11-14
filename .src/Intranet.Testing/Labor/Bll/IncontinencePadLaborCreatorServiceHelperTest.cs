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
    ///     Class representing the Test for the class <see cref="IncontinencePadLaborCreatorServiceHelper" />
    /// </summary>
    public class IncontinencePadLaborCreatorServiceHelperTest
    {
      


        /// <summary>
        ///     Testing GenerateProdCode 1
        /// </summary>
        [Fact]
        public void GenerateProdCodeTest1()
        {
            var serviceHelper = new IncontinencePadLaborCreatorServiceHelper( new NLogLoggerFactory() );
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
            var serviceHelper = new IncontinencePadLaborCreatorServiceHelper( new NLogLoggerFactory() );
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
            var serviceHelper = new IncontinencePadLaborCreatorServiceHelper( new NLogLoggerFactory() );
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
            var serviceHelper = new IncontinencePadLaborCreatorServiceHelper( new NLogLoggerFactory() );
            const String expected = "IT/11/16/158/03:01";
            var actual = serviceHelper.GenerateProdCode( "M11", 2016, 158, new TimeSpan( 3, 1, 0 ) );

            actual.Should()
                  .Be( expected );
        }

        /// <summary>
        ///     Testing GetIncontinencePadTestValueForType Empty Acquisition Time
        /// </summary>
        [Fact]
        public
            void GetIncontinencePadTestValueForTypeEmptyAquisitionTimeTimeTest1()
        {
            var serviceHelper = new IncontinencePadLaborCreatorServiceHelper( new NLogLoggerFactory() );

            Exception ex =
                Assert.Throws<InvalidDataException>(
                    () => serviceHelper.GetIncontinencePadTestValueForType(
                        new List<TestValue>(),
                        TestTypeIncontinencePad.AcquisitionTimeAndRewet,
                        TestValueType.Single ) );

            Assert.Equal
            ("No Single for AcquisitionTimeAndRewet per Testsheet existing",
              ex.Message
            );
        }

        /// <summary>
        ///     Testing GetIncontinencePadTestValueForType Empty Acquisition Time 2
        /// </summary>
        [Fact]
        public
            void GetIncontinencePadTestValueForTypeEmptyAcquisitionTimeTest2()
        {
            var serviceHelper = new IncontinencePadLaborCreatorServiceHelper( new NLogLoggerFactory() );

            Exception ex =
                Assert.Throws<InvalidDataException>(
                    () => serviceHelper.GetIncontinencePadTestValueForType(
                        IncontinencePadLaborCreatorServiceHelperData.TestValuesEmptyAcquisition2(),
                        TestTypeIncontinencePad.AcquisitionTimeAndRewet,
                        TestValueType.Single ) );

            Assert.Equal
            ("No Single for AcquisitionTimeAndRewet per Testsheet existing",
              ex.Message
            );
        }

        /// <summary>
        ///     Testing GetIncontinencePadTestValueForType Empty IncontinencePadRetention
        /// </summary>
        [Fact]
        public void GetIncontinencePadTestValueForTypeEmptyRetentionTest1()
        {
            var serviceHelper = new IncontinencePadLaborCreatorServiceHelper( new NLogLoggerFactory() );

            Exception ex =
                Assert.Throws<InvalidDataException>( () => serviceHelper.GetIncontinencePadTestValueForType( new List<TestValue>(), TestTypeIncontinencePad.Retention, TestValueType.Single ) );

            Assert.Equal( "No Single for Retention per Testsheet existing", ex.Message );
        }

        /// <summary>
        ///     Testing GetIncontinencePadTestValueForType Empty IncontinencePadRetention
        /// </summary>
        [Fact]
        public void GetIncontinencePadTestValueForTypeEmptyRetentionTest2()
        {
            var serviceHelper = new IncontinencePadLaborCreatorServiceHelper( new NLogLoggerFactory() );

            Exception ex = Assert.Throws<InvalidDataException>( () => serviceHelper.GetIncontinencePadTestValueForType(
                                                                    IncontinencePadLaborCreatorServiceHelperData.TestValuesEmptyRetention2(),
                                                                    TestTypeIncontinencePad.Retention,
                                                                    TestValueType.Single ) );

            Assert.Equal( "No Single for Retention per Testsheet existing", ex.Message );
        }

        /// <summary>
        ///     Testing GetIncontinencePadTestValueForType Empty IncontinencePadRewet 1
        /// </summary>
        [Fact]
        public void GetIncontinencePadTestValueForTypeEmptyRewetTest1()
        {
            var serviceHelper = new IncontinencePadLaborCreatorServiceHelper( new NLogLoggerFactory() );

            Exception ex =
                Assert.Throws<InvalidDataException>( () => serviceHelper.GetIncontinencePadTestValueForType(
                                                         new List<TestValue>(),
                                                         TestTypeIncontinencePad.RewetFree,
                                                         TestValueType.Single ) );

            Assert.Equal( "No Single for RewetFree per Testsheet existing", ex.Message );
        }

        /// <summary>
        ///     Testing GetIncontinencePadTestValueForType Empty IncontinencePadRewet 2
        /// </summary>
        [Fact]
        public void GetIncontinencePadTestValueForTypeEmptyRewetTest2()
        {
            var serviceHelper = new IncontinencePadLaborCreatorServiceHelper( new NLogLoggerFactory() );

            Exception ex =
                Assert.Throws<InvalidDataException>( () => serviceHelper.GetIncontinencePadTestValueForType(
                                                         IncontinencePadLaborCreatorServiceHelperData.TestValuesEmptyRewet2(),
                                                         TestTypeIncontinencePad.RewetFree,
                                                         TestValueType.Single ) );

            Assert.Equal( "No Single for RewetFree per Testsheet existing", ex.Message );
        }

        /// <summary>
        ///     Testing GetIncontinencePadTestValueForType ManyAcquisition Time 1
        /// </summary>
        [Fact]
        public void GetIncontinencePadTestValueForTypeManyAcquisitionTimeTest1()
        {
            var serviceHelper = new IncontinencePadLaborCreatorServiceHelper( new NLogLoggerFactory() );

            Exception ex =
                Assert.Throws<InvalidDataException>( () => serviceHelper.GetIncontinencePadTestValueForType(
                                                         IncontinencePadLaborCreatorServiceHelperData.TestValuesAcquisitionTimeManySingle(),
                                                         TestTypeIncontinencePad.AcquisitionTimeAndRewet,
                                                         TestValueType.Single ) );

            Assert.Equal("Only one Single for AcquisitionTimeAndRewet per Testsheet allowed", ex.Message );
        }

        /// <summary>
        ///     Testing GetIncontinencePadTestValueForType Many Acquisition time 2
        /// </summary>
        [Fact]
        public void GetIncontinencePadTestValueForTypeManyAcquisitionTimeTest2()
        {
            var serviceHelper = new IncontinencePadLaborCreatorServiceHelper( new NLogLoggerFactory() );

            Exception ex =
                Assert.Throws<InvalidDataException>(
                    () => serviceHelper.GetIncontinencePadTestValueForType( IncontinencePadLaborCreatorServiceHelperData.TestValuesManyAcquisitionTimeAverage(),
                                                                       TestTypeIncontinencePad.AcquisitionTimeAndRewet,
                                                                       TestValueType.Average ) );

            Assert.Equal("Only one Average for AcquisitionTimeAndRewet per Testsheet allowed", ex.Message );
        }

        /// <summary>
        ///     Testing GetIncontinencePadTestValueForType Many Acquisition time 3
        /// </summary>
        [Fact]
        public void GetIncontinencePadTestValueForTypeManyAcquisitionTimeTest3()
        {
            var serviceHelper = new IncontinencePadLaborCreatorServiceHelper( new NLogLoggerFactory() );

            Exception ex =
                Assert.Throws<InvalidDataException>(
                    () => serviceHelper.GetIncontinencePadTestValueForType( IncontinencePadLaborCreatorServiceHelperData.TestValuesManyAcquisitionTimeStandardDeviation(),
                                                                       TestTypeIncontinencePad.AcquisitionTimeAndRewet,
                                                                       TestValueType.StandardDeviation ) );

            Assert.Equal("Only one StandardDeviation for AcquisitionTimeAndRewet per Testsheet allowed", ex.Message );
        }

        /// <summary>
        ///     Testing GetIncontinencePadTestValueForType Many IncontinencePadRetention 1
        /// </summary>
        [Fact]
        public void GetIncontinencePadTestValueForTypeManyRetentionTest1()
        {
            var serviceHelper = new IncontinencePadLaborCreatorServiceHelper( new NLogLoggerFactory() );

            Exception ex =
                Assert.Throws<InvalidDataException>( () => serviceHelper.GetIncontinencePadTestValueForType( IncontinencePadLaborCreatorServiceHelperData.TestValuesManyRententionSingle(),
                                                                                                        TestTypeIncontinencePad.Retention,
                                                                                                        TestValueType.Single ) );

            Assert.Equal( "Only one Single for Retention per Testsheet allowed", ex.Message );
        }

        /// <summary>
        ///     Testing GetIncontinencePadTestValueForType Many retention 2
        /// </summary>
        [Fact]
        public void GetIncontinencePadTestValueForTypeManyRetentionTest2()
        {
            var serviceHelper = new IncontinencePadLaborCreatorServiceHelper( new NLogLoggerFactory() );

            Exception ex =
                Assert.Throws<InvalidDataException>( () => serviceHelper.GetIncontinencePadTestValueForType( IncontinencePadLaborCreatorServiceHelperData.TestValuesManyRetentionAverage(),
                                                                                                        TestTypeIncontinencePad.Retention,
                                                                                                        TestValueType.Average ) );

            Assert.Equal( "Only one Average for Retention per Testsheet allowed", ex.Message );
        }

        /// <summary>
        ///     Testing GetIncontinencePadTestValueForType Many IncontinencePadRewet 3
        /// </summary>
        [Fact]
        public void GetIncontinencePadTestValueForTypeManyRetentionTest3()
        {
            var serviceHelper = new IncontinencePadLaborCreatorServiceHelper( new NLogLoggerFactory() );

            Exception ex =
                Assert.Throws<InvalidDataException>(
                    () => serviceHelper.GetIncontinencePadTestValueForType( IncontinencePadLaborCreatorServiceHelperData.TestValuesManyRetentionStandardDeviation(),
                                                                       TestTypeIncontinencePad.Retention,
                                                                       TestValueType.StandardDeviation ) );

            Assert.Equal( "Only one StandardDeviation for Retention per Testsheet allowed", ex.Message );
        }

        /// <summary>
        ///     Testing GetIncontinencePadTestValueForType Many IncontinencePadRewet 1
        /// </summary>
        [Fact]
        public void GetIncontinencePadTestValueForTypeManyRewetTest1()
        {
            var serviceHelper = new IncontinencePadLaborCreatorServiceHelper( new NLogLoggerFactory() );

            Exception ex =
                Assert.Throws<InvalidDataException>( () => serviceHelper.GetIncontinencePadTestValueForType( IncontinencePadLaborCreatorServiceHelperData.TestValuesManyRewetSingle(),
                                                                                                        TestTypeIncontinencePad.RewetFree,
                                                                                                        TestValueType.Single ) );

            Assert.Equal( "Only one Single for RewetFree per Testsheet allowed", ex.Message );
        }

        /// <summary>
        ///     Testing GetIncontinencePadTestValueForType Many IncontinencePadRewet 2
        /// </summary>
        [Fact]
        public void GetIncontinencePadTestValueForTypeManyRewetTest2()
        {
            var serviceHelper = new IncontinencePadLaborCreatorServiceHelper( new NLogLoggerFactory() );

            Exception ex =
                Assert.Throws<InvalidDataException>( () => serviceHelper.GetIncontinencePadTestValueForType( IncontinencePadLaborCreatorServiceHelperData.TestValuesManyRewetAverage(),
                                                                                                        TestTypeIncontinencePad.RewetFree,
                                                                                                        TestValueType.Average ) );

            Assert.Equal( "Only one Average for RewetFree per Testsheet allowed", ex.Message );
        }

        /// <summary>
        ///     Testing GetIncontinencePadTestValueForType Many IncontinencePadRewet 3
        /// </summary>
        [Fact]
        public void GetIncontinencePadTestValueForTypeManyRewetTest3()
        {
            var serviceHelper = new IncontinencePadLaborCreatorServiceHelper( new NLogLoggerFactory() );

            Exception ex =
                Assert.Throws<InvalidDataException>(
                    () => serviceHelper.GetIncontinencePadTestValueForType( IncontinencePadLaborCreatorServiceHelperData.TestValuesManyRewetStandardDeviation(),
                                                                       TestTypeIncontinencePad.RewetFree,
                                                                       TestValueType.StandardDeviation ) );

            Assert.Equal( "Only one StandardDeviation for RewetFree per Testsheet allowed", ex.Message );
        }

        /// <summary>
        ///     Testing GetIncontinencePadTestValueForType Ok IncontinencePadAcquisitionTime 1
        /// </summary>
        [Fact]
        public void GetIncontinencePadTestValueForTypeOkAcquisitionTimeTest1()
        {
            var serviceHelper = new IncontinencePadLaborCreatorServiceHelper( new NLogLoggerFactory() );
            var expected = new IncontinencePadTestValue { TestType = TestTypeIncontinencePad.AcquisitionTimeAndRewet };
            var
                actual = serviceHelper.GetIncontinencePadTestValueForType( IncontinencePadLaborCreatorServiceHelperData.TestValuesOkAcquisitionTimeSingle( expected ),
                                                                      TestTypeIncontinencePad.AcquisitionTimeAndRewet,
                                                                      TestValueType.Single );

            actual.Should()
                  .Be( expected );
        }

        /// <summary>
        ///     Testing GetIncontinencePadTestValueForType Ok IncontinencePadAcquisitionTime 2
        /// </summary>
        [Fact]
        public void GetIncontinencePadTestValueForTypeOkAcquisitionTimeTest2()
        {
            var serviceHelper = new IncontinencePadLaborCreatorServiceHelper( new NLogLoggerFactory() );
            var expected = new IncontinencePadTestValue { TestType = TestTypeIncontinencePad.AcquisitionTimeAndRewet };
            var
                actual = serviceHelper.GetIncontinencePadTestValueForType( IncontinencePadLaborCreatorServiceHelperData.TestValuesOkAcquisitionTimeAverage( expected ),
                                                                      TestTypeIncontinencePad.AcquisitionTimeAndRewet,
                                                                      TestValueType.Average );

            actual.Should()
                  .Be( expected );
        }

        /// <summary>
        ///     Testing GetIncontinencePadTestValueForType Ok IncontinencePadAcquisitionTime 3
        /// </summary>
        [Fact]
        public void GetIncontinencePadTestValueForTypeOkAcquisitionTimeTest3()
        {
            var serviceHelper = new IncontinencePadLaborCreatorServiceHelper( new NLogLoggerFactory() );
            var expected = new IncontinencePadTestValue { TestType = TestTypeIncontinencePad.AcquisitionTimeAndRewet };
            var
                actual = serviceHelper.GetIncontinencePadTestValueForType( IncontinencePadLaborCreatorServiceHelperData.TestValuesOKAcquisitionTimeStandardDeviation( expected ),
                                                                      TestTypeIncontinencePad.AcquisitionTimeAndRewet,
                                                                      TestValueType.StandardDeviation );

            actual.Should()
                  .Be( expected );
        }

        /// <summary>
        ///     Testing GetIncontinencePadTestValueForType Ok IncontinencePadRetention 1
        /// </summary>
        [Fact]
        public void GetIncontinencePadTestValueForTypeOkRetentionTest1()
        {
            var serviceHelper = new IncontinencePadLaborCreatorServiceHelper( new NLogLoggerFactory() );
            var expected = new IncontinencePadTestValue { TestType = TestTypeIncontinencePad.Retention };
            var
                actual = serviceHelper.GetIncontinencePadTestValueForType( IncontinencePadLaborCreatorServiceHelperData.TestValuesOkRetentionTimeSingle( expected ),
                                                                      TestTypeIncontinencePad.Retention,
                                                                      TestValueType.Single );

            actual.Should()
                  .Be( expected );
        }

        /// <summary>
        ///     Testing GetIncontinencePadTestValueForType Ok IncontinencePadRetention 2
        /// </summary>
        [Fact]
        public void GetIncontinencePadTestValueForTypeOkRetentionTest2()
        {
            var serviceHelper = new IncontinencePadLaborCreatorServiceHelper( new NLogLoggerFactory() );
            var expected = new IncontinencePadTestValue { TestType = TestTypeIncontinencePad.Retention };
            var
                actual = serviceHelper.GetIncontinencePadTestValueForType( IncontinencePadLaborCreatorServiceHelperData.TestValuesOkRetentionAverage( expected ),
                                                                      TestTypeIncontinencePad.Retention,
                                                                      TestValueType.Average );

            actual.Should()
                  .Be( expected );
        }

        /// <summary>
        ///     Testing GetIncontinencePadTestValueForType Ok IncontinencePadRetention 1
        /// </summary>
        [Fact]
        public void GetIncontinencePadTestValueForTypeOkRetentionTest3()
        {
            var serviceHelper = new IncontinencePadLaborCreatorServiceHelper( new NLogLoggerFactory() );
            var expected = new IncontinencePadTestValue { TestType = TestTypeIncontinencePad.Retention };
            var
                actual = serviceHelper.GetIncontinencePadTestValueForType( IncontinencePadLaborCreatorServiceHelperData.TestValuesOkRetentionStandardDeviation( expected ),
                                                                      TestTypeIncontinencePad.Retention,
                                                                      TestValueType.StandardDeviation );

            actual.Should()
                  .Be( expected );
        }

        /// <summary>
        ///     Testing GetIncontinencePadTestValueForType Ok IncontinencePadRewet 1
        /// </summary>
        [Fact]
        public void GetIncontinencePadTestValueForTypeOkRewetTest1()
        {
            var serviceHelper = new IncontinencePadLaborCreatorServiceHelper( new NLogLoggerFactory() );
            var expected = new IncontinencePadTestValue { TestType = TestTypeIncontinencePad.RewetFree };
            var
                actual = serviceHelper.GetIncontinencePadTestValueForType( IncontinencePadLaborCreatorServiceHelperData.TestValuesOkRewetSingle( expected ),
                                                                      TestTypeIncontinencePad.RewetFree,
                                                                      TestValueType.Single );

            actual.Should()
                  .Be( expected );
        }

        /// <summary>
        ///     Testing GetIncontinencePadTestValueForType Ok IncontinencePadRewet 2
        /// </summary>
        [Fact]
        public void GetIncontinencePadTestValueForTypeOkRewetTest2()
        {
            var serviceHelper = new IncontinencePadLaborCreatorServiceHelper( new NLogLoggerFactory() );
            var expected = new IncontinencePadTestValue { TestType = TestTypeIncontinencePad.RewetFree };
            var
                actual = serviceHelper.GetIncontinencePadTestValueForType( IncontinencePadLaborCreatorServiceHelperData.TestValuesOkRewetAverage( expected ),
                                                                      TestTypeIncontinencePad.RewetFree,
                                                                      TestValueType.Average );

            actual.Should()
                  .Be( expected );
        }

        /// <summary>
        ///     Testing GetIncontinencePadTestValueForType Ok IncontinencePadRewet 1
        /// </summary>
        [Fact]
        public void GetIncontinencePadTestValueForTypeOkRewetTest3()
        {
            var serviceHelper = new IncontinencePadLaborCreatorServiceHelper( new NLogLoggerFactory() );
            var expected = new IncontinencePadTestValue { TestType = TestTypeIncontinencePad.RewetFree };
            var
                actual = serviceHelper.GetIncontinencePadTestValueForType( IncontinencePadLaborCreatorServiceHelperData.TestValuesOkRewetStandardDeviation( expected ),
                                                                      TestTypeIncontinencePad.RewetFree,
                                                                      TestValueType.StandardDeviation );

            actual.Should()
                  .Be( expected );
        }

        /// <summary>
        ///     Testing ToAcquisitionTimeAverage many
        /// </summary>
        [Fact]
        public void ToAcquisitionTimeAverageTestExceptionMany()
        {
            var serviceHelper = new IncontinencePadLaborCreatorServiceHelper( new NLogLoggerFactory() );

            Exception ex =
                Assert.Throws<InvalidDataException>( () => serviceHelper.ToAcquisitionTimeAverage( IncontinencePadLaborCreatorServiceHelperData.TestValuesManyAcquisitionTimeAverage() ) );

            Assert.Equal("Only one Average for AcquisitionTimeAndRewet per Testsheet allowed", ex.Message );
        }

        /// <summary>
        ///     Testing ToAcquisitionTimeAverage none
        /// </summary>
        [Fact]
        public void ToAcquisitionTimeAverageTestExceptionNone()
        {
            var serviceHelper = new IncontinencePadLaborCreatorServiceHelper( new NLogLoggerFactory() );

            Exception ex =
                Assert.Throws<InvalidDataException>( () => serviceHelper.ToAcquisitionTimeAverage( IncontinencePadLaborCreatorServiceHelperData.TestValuesEmptyAcquisition2() ) );

            Assert.Equal("No Average for AcquisitionTimeAndRewet per Testsheet existing", ex.Message );
        }

        /// <summary>
        ///     Testing ToAcquisitionTimeAverage ok
        /// </summary>
        [Fact]
        public void ToAcquisitionTimeAverageTestOk()
        {
            var serviceHelper = new IncontinencePadLaborCreatorServiceHelper( new NLogLoggerFactory() );
            var expected = new IncontinencePadTestValue { TestType = TestTypeIncontinencePad.AcquisitionTimeAndRewet };

            var actual = serviceHelper.ToAcquisitionTimeAverage( IncontinencePadLaborCreatorServiceHelperData.TestValuesOkAcquisitionTimeAverage( expected ) );
        }

        /// <summary>
        ///     Testing ToAcquisitionTimeAverage many
        /// </summary>
        [Fact]
        public void ToAcquisitionTimeStandardDeviationExceptionMany()
        {
            var serviceHelper = new IncontinencePadLaborCreatorServiceHelper( new NLogLoggerFactory() );

            Exception ex =
                Assert.Throws<InvalidDataException>(
                    () => serviceHelper.ToAcquisitionTimeStandardDeviation( IncontinencePadLaborCreatorServiceHelperData.TestValuesManyAcquisitionTimeStandardDeviation() ) );

            Assert.Equal("Only one StandardDeviation for AcquisitionTimeAndRewet per Testsheet allowed", ex.Message );
        }

        /// <summary>
        ///     Testing ToAcquisitionTimeAverage none
        /// </summary>
        [Fact]
        public void ToAcquisitionTimeStandardDeviationTestExceptionNone()
        {
            var serviceHelper = new IncontinencePadLaborCreatorServiceHelper( new NLogLoggerFactory() );

            Exception ex =
                Assert.Throws<InvalidDataException>( () => serviceHelper.ToAcquisitionTimeStandardDeviation( IncontinencePadLaborCreatorServiceHelperData.TestValuesEmptyAcquisition2() ) );

            Assert.Equal("No StandardDeviation for AcquisitionTimeAndRewet per Testsheet existing", ex.Message );
        }

        /// <summary>
        ///     Testing ToAcquisitionTimeStandardDeviation ok
        /// </summary>
        [Fact]
        public void ToAcquisitionTimeStandardDeviationTestOk()
        {
            var serviceHelper = new IncontinencePadLaborCreatorServiceHelper( new NLogLoggerFactory() );
            var expected = new IncontinencePadTestValue { TestType = TestTypeIncontinencePad.AcquisitionTimeAndRewet };

            var actual = serviceHelper.ToAcquisitionTimeStandardDeviation( IncontinencePadLaborCreatorServiceHelperData.TestValuesOKAcquisitionTimeStandardDeviation( expected ) );
        }

        /// <summary>
        ///     Testing To Acquisition Time ok
        /// </summary>
        [Fact]
        public void ToAcquisitionTimeTest()
        {
            var serviceHelper = new IncontinencePadLaborCreatorServiceHelper( new NLogLoggerFactory() );

            var expected = new IncontinencePadAcquisitionTime
            {
                AcquisitionTimeAdditionFirst = 1.0,
                AcquisitionTimeAdditionSecond = 2.0,
                AcquisitionTimeAdditionThird = 3.0,
                AcquisitionTimeAdditionSecondRW = RwType.Better,
                AcquisitionTimeAdditionFirstRW = RwType.Better,
                AcquisitionTimeAdditionThirdRW = RwType.Better
            };
            var actual = serviceHelper.ToAcquisitionTime(
                new IncontinencePadTestValue
                {
                    
                    AcquisitionTimeFirst = 1.0,
                    AcquisitionTimeSecond = 2.0,
                    AcquisitionTimeThird = 3.0,
AcquisitionTimeFirstRw = RwType.Better,
AcquisitionTimeThirdRw = RwType.Better,AcquisitionTimeSecondRw = RwType.Better
                } );

            actual.ShouldBeEquivalentTo( expected );
        }

        /// <summary>
        ///     Testing ToAcquisitionTimeTestValueCollection ok
        /// </summary>
        [Fact]
        public void ToAcquisitionTimeTestValueCollectionTest()
        {
            var serviceHelper = new IncontinencePadLaborCreatorServiceHelper( new NLogLoggerFactory() );

            var actual = serviceHelper.ToAcquisitionTimeTestValuesCollection( IncontinencePadLaborCreatorServiceHelperData.TwoTestValuePerType() );

            actual.Count.Should()
                  .Be( 2 );
        }

        /// <summary>
        ///     Testing ToRetentionAverage many
        /// </summary>
        [Fact]
        public void ToRetentionAverageTestExceptionMany()
        {
            var serviceHelper = new IncontinencePadLaborCreatorServiceHelper( new NLogLoggerFactory() );

            Exception ex =
                Assert.Throws<InvalidDataException>( () => serviceHelper.ToRetentionAverage( IncontinencePadLaborCreatorServiceHelperData.TestValuesManyRetentionAverage() ) );

            Assert.Equal( "Only one Average for Retention per Testsheet allowed", ex.Message );
        }

        /// <summary>
        ///     Testing ToRetentionAverage none
        /// </summary>
        [Fact]
        public void ToRetentionAverageTestExceptionNone()
        {
            var serviceHelper = new IncontinencePadLaborCreatorServiceHelper( new NLogLoggerFactory() );

            Exception ex =
                Assert.Throws<InvalidDataException>( () => serviceHelper.ToRetentionAverage( IncontinencePadLaborCreatorServiceHelperData.TestValuesEmptyRetention2() ) );

            Assert.Equal( "No Average for Retention per Testsheet existing", ex.Message );
        }

        /// <summary>
        ///     Testing ToRetentionAverage ok
        /// </summary>
        [Fact]
        public void ToRetentionAverageTestOk()
        {
            var serviceHelper = new IncontinencePadLaborCreatorServiceHelper( new NLogLoggerFactory() );
            var input = new IncontinencePadTestValue { TestType = TestTypeIncontinencePad.Retention, RetentionRw = RwType.Ok };

            var actual = serviceHelper.ToRetentionAverage( IncontinencePadLaborCreatorServiceHelperData.TestValuesOkRetentionAverage( input ) );
            actual.RetentionRw.Should()
                  .Be( RwType.Ok );
        }

        /// <summary>
        ///     Testing ToRetentionStandardDeviation many
        /// </summary>
        [Fact]
        public void ToRetentionStandardDeviationTestExceptionMany()
        {
            var serviceHelper = new IncontinencePadLaborCreatorServiceHelper( new NLogLoggerFactory() );

            Exception ex =
                Assert.Throws<InvalidDataException>(
                    () => serviceHelper.ToRetentionStandardDeviation( IncontinencePadLaborCreatorServiceHelperData.TestValuesManyRetentionStandardDeviation() ) );

            Assert.Equal( "Only one StandardDeviation for Retention per Testsheet allowed", ex.Message );
        }

        /// <summary>
        ///     Testing ToRetentionStandardDeviation none
        /// </summary>
        [Fact]
        public void ToRetentionStandardDeviationTestExceptionNone()
        {
            var serviceHelper = new IncontinencePadLaborCreatorServiceHelper( new NLogLoggerFactory() );

            Exception ex =
                Assert.Throws<InvalidDataException>( () => serviceHelper.ToRetentionStandardDeviation( IncontinencePadLaborCreatorServiceHelperData.TestValuesEmptyRetention2() ) );

            Assert.Equal( "No StandardDeviation for Retention per Testsheet existing", ex.Message );
        }

        /// <summary>
        ///     Testing ToRetentionStandardDeviation ok
        /// </summary>
        [Fact]
        public void ToRetentionStandardDeviationTestOk()
        {
            var serviceHelper = new IncontinencePadLaborCreatorServiceHelper( new NLogLoggerFactory() );
            var input = new IncontinencePadTestValue { TestType = TestTypeIncontinencePad.Retention, RetentionRw = RwType.Ok };

            var actual = serviceHelper.ToRetentionStandardDeviation( IncontinencePadLaborCreatorServiceHelperData.TestValuesOkRetentionStandardDeviation( input ) );
            actual.RetentionRw.Should()
                  .Be( RwType.Ok );
        }

        /// <summary>
        ///     Testing ToRetentionTestValueCollection ok
        /// </summary>
        [Fact]
        public void ToRetentionTestValueCollectionTest()
        {
            var serviceHelper = new IncontinencePadLaborCreatorServiceHelper( new NLogLoggerFactory() );

            var actual = serviceHelper.ToRetentionTestValuesCollection( IncontinencePadLaborCreatorServiceHelperData.TwoTestValuePerType() );

            actual.Count.Should()
                  .Be( 2 );
        }


        /// <summary>
        ///     Testing ToRewetAverage many
        /// </summary>
        [Fact]
        public void ToRewetAverageTestExceptionMany()
        {
            var serviceHelper = new IncontinencePadLaborCreatorServiceHelper( new NLogLoggerFactory() );

            Exception ex =
                Assert.Throws<InvalidDataException>( () => serviceHelper.ToRewetAverage( IncontinencePadLaborCreatorServiceHelperData.TestValuesManyRewetAverage() ) );

            Assert.Equal( "Only one Average for RewetFree per Testsheet allowed", ex.Message );
        }

        /// <summary>
        ///     Testing ToRewetAverage none
        /// </summary>
        [Fact]
        public void ToRewetAverageTestExceptionNone()
        {
            var serviceHelper = new IncontinencePadLaborCreatorServiceHelper( new NLogLoggerFactory() );

            Exception ex =
                Assert.Throws<InvalidDataException>( () => serviceHelper.ToRewetAverage( IncontinencePadLaborCreatorServiceHelperData.TestValuesEmptyRewet2() ) );

            Assert.Equal( "No Average for RewetFree per Testsheet existing", ex.Message );
        }

        /// <summary>
        ///     Testing ToRewetAverage ok
        /// </summary>
        [Fact]
        public void ToRewetAverageTestOk()
        {
            var serviceHelper = new IncontinencePadLaborCreatorServiceHelper( new NLogLoggerFactory() );
            var expected = new IncontinencePadTestValue { TestType = TestTypeIncontinencePad.RewetFree, RewetFreeRw = RwType.Better, };

            var actual = serviceHelper.ToRewetAverage( IncontinencePadLaborCreatorServiceHelperData.TestValuesOkRewetAverage( expected ) );
            actual.RewetRW.Should()
                  .Be( RwType.Better );
        }

        /// <summary>
        ///     Testing ToRewetStandardDeviation many
        /// </summary>
        [Fact]
        public void ToRewetStandardDeviationTestExceptionMany()
        {
            var serviceHelper = new IncontinencePadLaborCreatorServiceHelper( new NLogLoggerFactory() );

            Exception ex =
                Assert.Throws<InvalidDataException>( () => serviceHelper.ToRewetStandardDeviation( IncontinencePadLaborCreatorServiceHelperData.TestValuesManyRewetStandardDeviation() ) );

            Assert.Equal( "Only one StandardDeviation for RewetFree per Testsheet allowed", ex.Message );
        }

        /// <summary>
        ///     Testing ToRewetStandardDeviation none
        /// </summary>
        [Fact]
        public void ToRewetStandardDeviationTestExceptionNone()
        {
            var serviceHelper = new IncontinencePadLaborCreatorServiceHelper( new NLogLoggerFactory() );

            Exception ex =
                Assert.Throws<InvalidDataException>( () => serviceHelper.ToRewetStandardDeviation( IncontinencePadLaborCreatorServiceHelperData.TestValuesEmptyRewet2() ) );

            Assert.Equal( "No StandardDeviation for RewetFree per Testsheet existing", ex.Message );
        }

        /// <summary>
        ///     Testing ToReweStandardDeviation ok
        /// </summary>
        [Fact]
        public void ToRewetStandardDeviationTestOk()
        {
            var serviceHelper = new IncontinencePadLaborCreatorServiceHelper( new NLogLoggerFactory() );
            var expected = new IncontinencePadTestValue { TestType = TestTypeIncontinencePad.RewetFree, RewetFreeRw = RwType.Better };

            var actual = serviceHelper.ToRewetStandardDeviation( IncontinencePadLaborCreatorServiceHelperData.TestValuesOkRewetStandardDeviation( expected ) );
            actual.RewetRW.Should()
                  .Be( RwType.Better );
        }

        /// <summary>
        ///     Testing to IncontinencePadRewet ok
        /// </summary>
        [Fact]
        public void ToRewetTestOk()
        {
            var serviceHelper = new IncontinencePadLaborCreatorServiceHelper( new NLogLoggerFactory() );
            var expected = new IncontinencePadRewet
            {
                RewetRW = RwType.Ok,
                WeightDry = 1.0,WeightWet = 2.0,WeightDiff = 1.0
            };
            var actual = serviceHelper.ToRewet(
                new IncontinencePadTestValue
                {
                    RewetFreeRw = RwType.Ok,
                    RewetFreeDryValue = 1.0,
                    RewetFreeWetValue = 2.0,
                    RewetFreeDifference = 1.0
                } );

           
            actual.RewetRW.Should()
                  .Be( expected.RewetRW );
            actual.WeightDiff.Should()
                  .Be( expected.WeightDiff );
            actual.WeightDry.Should()
                  .Be( expected.WeightDry );
            actual.WeightWet.Should()
                  .Be( expected.WeightWet );
          
            actual.ShouldBeEquivalentTo( expected );
        }

        /// <summary>
        ///     Testing ToRewetTestValueCollection ok
        /// </summary>
        [Fact]
        public void ToRewetTestValueCollectionTest()
        {
            var serviceHelper = new IncontinencePadLaborCreatorServiceHelper( new NLogLoggerFactory() );

            var actual = serviceHelper.ToRewetTestValuesCollection( IncontinencePadLaborCreatorServiceHelperData.TwoTestValuePerType() );

            actual.Count.Should()
                  .Be( 2 );
        }

        /// <summary>
        ///     Testing ToRewetTestValue ok
        /// </summary>
        [Fact]
        public void ToRewetTestValueTest()
        {
            var serviceHelper = new IncontinencePadLaborCreatorServiceHelper( new NLogLoggerFactory() );
            var expected = new IncontinencePadRewetTestValue
            {
                IncontinencePadRewet = new IncontinencePadRewet { RewetRW = RwType.Better },
                IncontinencePadTestInfo = new IncontinencePadTestInfo
                {
                    TestPerson = "test person",
                    ProductionCode = "theprodcode",
                    TestValueId = 1
                }
            };

            var actual = serviceHelper.ToRewetTestValue( new IncontinencePadTestValue {  RewetFreeRw = RwType.Better },
                                                         "test person",
                                                         "theprodcode",
                                                         1 );

            actual.ShouldBeEquivalentTo( expected );
        }

        /// <summary>
        ///     Testing ToTestInfo
        /// </summary>
        [Fact]
        public void ToTestInfoTest()
        {
            var serviceHelper = new IncontinencePadLaborCreatorServiceHelper( new NLogLoggerFactory() );

            var expected = new IncontinencePadTestInfo
            {
                TestPerson = "gandalf",
                ProductionCode = "you shall not pass",
                TestValueId = 1
            };

            var actual = serviceHelper.ToTestInfo( "gandalf", "you shall not pass", 1 );

            actual.ShouldBeEquivalentTo( expected );
        }

        /// <summary>
        ///     Testing ToTestValuesCollectionByTestType  1
        /// </summary>
        [Fact]
        public void ToTestValuesCollectionByTestTypeTest1()
        {
            var serviceHelper = new IncontinencePadLaborCreatorServiceHelper( new NLogLoggerFactory() );

            var actual = serviceHelper.ToTestValuesCollectionByTestType( IncontinencePadLaborCreatorServiceHelperData.TwoTestValuePerType(),
                                                                         TestValueType.Single,
                                                                         new List<TestTypeIncontinencePad> { TestTypeIncontinencePad.Retention },
                                                                         serviceHelper.ToRetentionTestValue )
                                      .ToList();
            actual.Count.Should()
                  .Be( 2 );
        }

        /// <summary>
        ///     Testing ToTestValuesCollectionByTestType  2
        /// </summary>
        [Fact]
        public void ToTestValuesCollectionByTestTypeTest2()
        {
            var serviceHelper = new IncontinencePadLaborCreatorServiceHelper( new NLogLoggerFactory() );

            var actual = serviceHelper.ToTestValuesCollectionByTestType( IncontinencePadLaborCreatorServiceHelperData.TwoTestValuePerType(),
                                                                         TestValueType.Single,
                                                                         new List<TestTypeIncontinencePad> { TestTypeIncontinencePad.RewetFree },
                                                                         serviceHelper.ToRewetTestValue )
                                      .ToList();
            actual.Count.Should()
                  .Be( 2 );
        }

        /// <summary>
        ///     Testing ToTestValuesCollectionByTestType  3
        /// </summary>
        [Fact]
        public void ToTestValuesCollectionByTestTypeTest3()
        {
            var serviceHelper = new IncontinencePadLaborCreatorServiceHelper( new NLogLoggerFactory() );

            var actual = serviceHelper.ToTestValuesCollectionByTestType( IncontinencePadLaborCreatorServiceHelperData.TwoTestValuePerType(),
                                                                         TestValueType.Single,
                                                                         new List<TestTypeIncontinencePad> { TestTypeIncontinencePad.AcquisitionTimeAndRewet },
                                                                         serviceHelper.ToAcquisitionTimeTestValue )
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
            var serviceHelper = new IncontinencePadLaborCreatorServiceHelper( new NLogLoggerFactory() );

            var actual = serviceHelper.ToTestValuesCollectionByTestType( IncontinencePadLaborCreatorServiceHelperData.TwoTestValuePerType(),
                                                                         TestValueType.Average,
                                                                         new List<TestTypeIncontinencePad> { TestTypeIncontinencePad.Retention },
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
            var serviceHelper = new IncontinencePadLaborCreatorServiceHelper( new NLogLoggerFactory() );

            var actual = serviceHelper.ToTestValuesCollectionByTestType( IncontinencePadLaborCreatorServiceHelperData.TwoTestValuePerType(),
                                                                         TestValueType.StandardDeviation,
                                                                         new List<TestTypeIncontinencePad> { TestTypeIncontinencePad.Retention },
                                                                         serviceHelper.ToRetentionTestValue )
                                      .ToList();
            actual.Count.Should()
                  .Be( 1 );
        }

        /// <summary>
        ///     Testing ToTestValuesCollectionByTestType  6
        /// </summary>
        [Fact]
        public void ToTestValuesCollectionByTestTypeTest6()
        {
            var serviceHelper = new IncontinencePadLaborCreatorServiceHelper(new NLogLoggerFactory());

            var actual = serviceHelper.ToTestValuesCollectionByTestType(IncontinencePadLaborCreatorServiceHelperData.TwoTestValuePerType(),
                                                                         TestValueType.Average,
                                                                         new List<TestTypeIncontinencePad> { TestTypeIncontinencePad.RewetFree },
                                                                         serviceHelper.ToRewetTestValue)
                                      .ToList();
            actual.Count.Should()
                  .Be(1);
        }

        /// <summary>
        ///     Testing ToTestValuesCollectionByTestType  7
        /// </summary>
        [Fact]
        public void ToTestValuesCollectionByTestTypeTest7()
        {
            var serviceHelper = new IncontinencePadLaborCreatorServiceHelper(new NLogLoggerFactory());

            var actual = serviceHelper.ToTestValuesCollectionByTestType(IncontinencePadLaborCreatorServiceHelperData.TwoTestValuePerType(),
                                                                         TestValueType.StandardDeviation,
                                                                         new List<TestTypeIncontinencePad> { TestTypeIncontinencePad.RewetFree },
                                                                         serviceHelper.ToRewetTestValue)
                                      .ToList();
            actual.Count.Should()
                  .Be(1);
        }

        /// <summary>
        ///     Testing ValidateRequiredItem 1
        /// </summary>
        [Fact]
        public void ValidateRequiredItemTest1()
        {
            var serviceHelper = new IncontinencePadLaborCreatorServiceHelper( new NLogLoggerFactory() );

            serviceHelper.ValidateRequiredItem( "something", "something" );
        }

        /// <summary>
        ///     Testing ValidateRequiredItem 2
        /// </summary>
        [Fact]
        public void ValidateRequiredItemTest2()
        {
            var serviceHelper = new IncontinencePadLaborCreatorServiceHelper( new NLogLoggerFactory() );

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
            var serviceHelper = new IncontinencePadLaborCreatorServiceHelper( new NLogLoggerFactory() );
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
            var serviceHelper = new IncontinencePadLaborCreatorServiceHelper( new NLogLoggerFactory() );
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
            var serviceHelper = new IncontinencePadLaborCreatorServiceHelper( new NLogLoggerFactory() );
            const String valueType = "type";
            const String testType = "test";

            serviceHelper.ValidateTestValueOnlyExactlyOneHasToExist( new List<TestValue> { new TestValue() }, testType, valueType );
        }
    }
}