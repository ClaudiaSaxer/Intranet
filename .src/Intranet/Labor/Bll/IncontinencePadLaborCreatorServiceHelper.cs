using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using Extend;
using Intranet.Common;
using Intranet.Labor.Definition;
using Intranet.Labor.Definition.Bll;
using Intranet.Labor.Model.labor;
using Intranet.Labor.ViewModel;

namespace Intranet.Labor.Bll
{
    /// <summary>
    ///     Class representing the labor creator service
    /// </summary>
    public class IncontinencePadLaborCreatorServiceHelper : ServiceBase, IIncontinencePadLaborCreatorServiceHelper
    {
        #region Ctor

        /// <summary>
        ///     Initialize a new instance of the <see cref="ServiceBase" /> class.
        /// </summary>
        /// <param name="loggerFactory">A <see cref="ILoggerFactory" />.</param>
        public IncontinencePadLaborCreatorServiceHelper( ILoggerFactory loggerFactory )
            : base( loggerFactory.CreateLogger( typeof(IncontinencePadLaborCreatorServiceHelper) ) )
        {
        }
        /// <summary>
        /// Labor Creator Service Helper for Common 
        /// </summary>
        public ILaborCreatorServiceHelper LaborCreatorServiceHelper { get; set; }

        #endregion

      
        /// <summary>
        ///     Gets the IncontinencePadTestValue out of a list of testvalues for the correct
        ///     <see cref="TestTypeIncontinencePad" /> and
        ///     <see cref="TestValueType" />
        ///     One must exists and only one.
        /// </summary>
        /// <param name="testValues">the test values containing the wanted item</param>
        /// <param name="testTypeIncontinencePad">the type of the incontinence pad. <see cref="TestTypeIncontinencePad" /></param>
        /// <param name="testValueType">the value type of the incontinence pad. <see cref="TestValueType" /></param>
        /// <returns></returns>
        public IncontinencePadTestValue GetIncontinencePadTestValueForType( IEnumerable<TestValue> testValues,
                                                                            TestTypeIncontinencePad testTypeIncontinencePad,
                                                                            TestValueType testValueType )
        {
            var values = testValues.ToList()
                                   .Where(
                                       x => ( x.TestValueType == testValueType ) && ( x.IncontinencePadTestValue.TestType == testTypeIncontinencePad ) )
                                   .ToList();
            var item = ValidateTestValueOnlyExactlyOneHasToExist( values, testTypeIncontinencePad.ToString(), testValueType.ToString() );
            return item.IncontinencePadTestValue;
        }

        /// <summary>
        ///     Sets the values for the Acquisition Time View Model out of the IncontinencePadTestValue Model
        /// </summary>
        /// <param name="acquisitionTime">the Incontinence Pad Test value with the acquisition time data</param>
        /// <returns>The acquisition time View Model with the data collected from the model</returns>
        public IncontinencePadAcquisitionTime ToAcquisitionTime( IncontinencePadTestValue acquisitionTime )
            => new IncontinencePadAcquisitionTime
            {
                AcquisitionTimeAdditionFirst = acquisitionTime.AcquisitionTimeFirst,
                AcquisitionTimeAdditionSecond = acquisitionTime.AcquisitionTimeSecond,
                AcquisitionTimeAdditionThird = acquisitionTime.AcquisitionTimeThird,
                Weight = acquisitionTime.AcquisitionWeight,
                AcquisitionTimeAdditionFirstRW = acquisitionTime.AcquisitionTimeFirstRw,
                AcquisitionTimeAdditionSecondRW = acquisitionTime.AcquisitionTimeSecondRw,
                AcquisitionTimeAdditionThirdRW = acquisitionTime.AcquisitionTimeThirdRw
            };

        /// <summary>
        ///     Creates a Acauisition Average with the data from the test values
        /// </summary>
        /// <param name="testValues">testvalues containing a item representing data for the average</param>
        /// <returns>a class representing the acquisition time with the average</returns>
        public IncontinencePadAcquisitionTime ToAcquisitionTimeAverage( IEnumerable<TestValue> testValues )
            => ToAcquisitionTime( GetIncontinencePadTestValueForType( testValues, TestTypeIncontinencePad.AcquisitionTimeAndRewet, TestValueType.Average ) );

        /// <summary>
        ///     Creates a Acquisition Time Standard Deviation with the data from the test values
        /// </summary>
        /// <param name="testValues">testvalues containing a item representing data for the standard deviation</param>
        /// <returns>a class representing the acquisition time with the standard deviation</returns>
        public IncontinencePadAcquisitionTime ToAcquisitionTimeStandardDeviation( IEnumerable<TestValue> testValues )
            => ToAcquisitionTime( GetIncontinencePadTestValueForType( testValues, TestTypeIncontinencePad.AcquisitionTimeAndRewet, TestValueType.StandardDeviation ) );

        /// <summary>
        ///     Creates the Acquisition TestValue from diffrent input data
        /// </summary>
        /// <param name="acquisitionTime">the incontinence pad test value containing the acquisition time data</param>
        /// <param name="testPerson">the person who did the test</param>
        /// <param name="prodCode">the diaper production code</param>
        /// <param name="testValueId">the id of the test value</param>
        /// <returns></returns>
        public IncontinencePadAcquisitionTimeTestValue ToAcquisitionTimeTestValue( IncontinencePadTestValue acquisitionTime, String testPerson, String prodCode, Int32 testValueId )
        {
            var vm = new IncontinencePadAcquisitionTimeTestValue
            {
                IncontinencePadTestInfo = ToTestInfo( testPerson, prodCode, testValueId ),
                AcquisitionTime = ToAcquisitionTime( acquisitionTime ),
                RewetAfterAcquisitionTime = ToRewetAfterAcquisitionTime( acquisitionTime )
            };
            return vm;
        }

        /// <summary>
        ///     Creates the acquisition time test value collection for all singe tests
        /// </summary>
        /// <param name="testValues">the testvalues containing the acquisiton time tests</param>
        /// <returns>a collection for all acquisition time tests</returns>
        public ICollection<IncontinencePadAcquisitionTimeTestValue> ToAcquisitionTimeTestValuesCollection( IEnumerable<TestValue> testValues )
            => ToTestValuesCollectionByTestType( testValues,
                                                 TestValueType.Single,
                                                 new List<TestTypeIncontinencePad> { TestTypeIncontinencePad.AcquisitionTimeAndRewet },
                                                 ToAcquisitionTimeTestValue );

        /// <summary>
        ///     Sets the values for the Retention View Model out of the incontinence pad TestValue Model
        /// </summary>
        /// <param name="retention">the Incontinence Pad Test value with the retention data</param>
        /// <returns>The Retention View Model with the data collected from the model</returns>
        public IncontinencePadRetention ToRetention( IncontinencePadTestValue retention )
        {
            ValidateRequiredItem( retention.RetentionRw, "retention rw" );
            return new IncontinencePadRetention
            {
                RetentionAfterZentrifugeValue = retention.RetentionAfterZentrifuge,
                RetentionRw = retention.RetentionRw,
                RetentionWetWeight = retention.RetentionWetValue,
                RetentionDryWeight = retention.RetentionWeight,
                AbsorptionDiff = retention.RetentionAbsorbtion,
                RetentionDiff = retention.RetentionEndValue
            };
        }

        /// <summary>
        ///     Creates a retention Average with the data from the test values
        /// </summary>
        /// <param name="testValues">testvalues containing a item representing data for the average</param>
        /// <returns>a class representing the retention with the average</returns>
        public IncontinencePadRetention ToRetentionAverage( IEnumerable<TestValue> testValues )
            => ToRetention( GetIncontinencePadTestValueForType( testValues, TestTypeIncontinencePad.Retention, TestValueType.Average ) );

        /// <summary>
        ///     Creates a Retention Standard Deviation with the data from the test values
        /// </summary>
        /// <param name="testValues">testvalues containing a item representing data for the standard deviation</param>
        /// <returns>a class representing the retention with the standard deviation</returns>
        public IncontinencePadRetention ToRetentionStandardDeviation( IEnumerable<TestValue> testValues )
            => ToRetention( GetIncontinencePadTestValueForType( testValues, TestTypeIncontinencePad.Retention, TestValueType.StandardDeviation ) );

        /// <summary>
        ///     Creates the RetentionTestValues from diffrent input data
        /// </summary>
        /// <param name="retention">the incontinence pad test value containing the retention data</param>
        /// <param name="testPerson">the person who did the test</param>
        /// <param name="prodCode">the diaper production code</param>
        /// <param name="testValueId">the id of the testvalue</param>
        /// <returns></returns>
        public IncontinencePadRetentionTestValue ToRetentionTestValue( IncontinencePadTestValue retention, String testPerson, String prodCode, Int32 testValueId )
        {
            var vm = new IncontinencePadRetentionTestValue
            {
                IncontinencePadTestInfo = ToTestInfo( testPerson, prodCode, testValueId ),
                IncontinencePadRetention = ToRetention( retention )
                
            };
            return vm;
        }
       

        /// <summary>
        ///     Creates the retention  test value collection for all singe tests
        /// </summary>
        /// <param name="testValues">the testvalues containing the retention tests</param>
        /// <returns>a collection for all retention tests</returns>
        public ICollection<IncontinencePadRetentionTestValue> ToRetentionTestValuesCollection( IEnumerable<TestValue> testValues )
            => ToTestValuesCollectionByTestType( testValues,
                                                 TestValueType.Single,
                                                 new List<TestTypeIncontinencePad> { TestTypeIncontinencePad.Retention },
                                                 ToRetentionTestValue );



        /// <summary>
        ///     Sets the values for the rewet View Model out of the incontinence pad TestValue Model
        /// </summary>
        /// <param name="rewet">the incontinence pad Test value with the rewet data</param>
        /// <returns>The rewet View Model with the data collected from the model</returns>
        public IncontinencePadRewet ToRewet( IncontinencePadTestValue rewet )
        {
            ValidateRequiredItem( rewet.RewetFreeRw, "rewet free rw" );

            return
                new IncontinencePadRewet
                { 
                    WeightDry = rewet.RewetFreeDryValue, 
                    WeightWet = rewet.RewetFreeWetValue,
                    WeightDiff = rewet.RewetFreeDifference,
                    RewetRW = rewet.RewetFreeRw
                };
        }

        /// <summary>
        ///     Sets the values for the rewet View Model out of the incontinence pad TestValue Model
        /// </summary>
        /// <param name="rewet">the incontinence pad Test value with the rewet after acquisition data</param>
        /// <returns>The rewet after acquisition View Model with the data collected from the model</returns>
        public IncontinencePadRewet ToRewetAfterAcquisitionTime(IncontinencePadTestValue rewet)
        {
            ValidateRequiredItem(rewet.RewetAfterAcquisitionTimeRw, "rewet after acquisition rw");

            return
                new IncontinencePadRewet
                {
                    WeightDry = rewet.RewetAfterAcquisitionTimeDryWeight,
                    WeightWet = rewet.RewetAfterAcquisitionTimeWetWeight,
                    WeightDiff = rewet.RewetAfterAcquisitionTimeWeightDifference,
                    RewetRW = rewet.RewetAfterAcquisitionTimeRw
                };
        }
        /// <summary>
        ///     Creates a rewet after acquisition  Average with the data from the test values
        /// </summary>
        /// <param name="testValues">testvalues containing a item representing data for the average</param>
        /// <returns>a class representing the rewet after acquisition with the average</returns>
        public IncontinencePadRewet ToRewetAfterAcquisitionTimeAverage( IEnumerable<TestValue> testValues )
            => ToRewetAfterAcquisitionTime( GetIncontinencePadTestValueForType( testValues, TestTypeIncontinencePad.AcquisitionTimeAndRewet, TestValueType.Average ) );

        /// <summary>
        ///     Creates a rewet after acquisition Standard Deviation with the data from the test values
        /// </summary>
        /// <param name="testValues">testvalues containing a item representing data for the standard deviation</param>
        /// <returns>a class representing the rewet after acquisition with the standard deviation</returns>
        public IncontinencePadRewet ToRewetAfterAcquisitionTimeStandardDeviation( IEnumerable<TestValue> testValues )
            => ToRewetAfterAcquisitionTime( GetIncontinencePadTestValueForType( testValues, TestTypeIncontinencePad.AcquisitionTimeAndRewet, TestValueType.StandardDeviation ) );

        /// <summary>
        ///     Creates a retention Average with the data from the test values
        /// </summary>
        /// <param name="testValues">testvalues containing a item representing data for the average</param>
        /// <returns>a class representing the retention with the average</returns>
        public IncontinencePadRewet ToRewetAverage( IEnumerable<TestValue> testValues )
            => ToRewet( GetIncontinencePadTestValueForType( testValues, TestTypeIncontinencePad.RewetFree, TestValueType.Average ) );

        /// <summary>
        ///     Creates a rewet Standard Deviation with the data from the test values
        /// </summary>
        /// <param name="testValues">testvalues containing a item representing data for the standard deviation</param>
        /// <returns>a class representing the rewet with the standard deviation</returns>
        public IncontinencePadRewet ToRewetStandardDeviation( IEnumerable<TestValue> testValues )
            => ToRewet( GetIncontinencePadTestValueForType( testValues, TestTypeIncontinencePad.RewetFree, TestValueType.StandardDeviation ) );

        /// <summary>
        ///     Creates the rewet TestValue from diffrent input data
        /// </summary>
        /// <param name="rewet">the incontinence pad  test value containing the rewet data</param>
        /// <param name="testPerson">the person who did the test</param>
        /// <param name="prodCode">the diaper production code</param>
        /// <param name="testValueId">the id of the testvalue</param>
        /// <returns></returns>
        public IncontinencePadRewetTestValue ToRewetTestValue( IncontinencePadTestValue rewet, String testPerson, String prodCode, Int32 testValueId )
        {
            var vm = new IncontinencePadRewetTestValue
            {
                IncontinencePadTestInfo = ToTestInfo( testPerson, prodCode, testValueId ),
                IncontinencePadRewet = ToRewet( rewet )
            };
            return vm;
        }

        /// <summary>
        ///     Creates the rewet test value collection for all singe tests
        /// </summary>
        /// <param name="testValues">the testvalues containing the rewet tests</param>
        /// <returns>a collection for all rewet tests</returns>
        public ICollection<IncontinencePadRewetTestValue> ToRewetTestValuesCollection( IEnumerable<TestValue> testValues )
            => ToTestValuesCollectionByTestType( testValues,
                                                 TestValueType.Single,
                                                 new List<TestTypeIncontinencePad> { TestTypeIncontinencePad.RewetFree },
                                                 ToRewetTestValue );

        /// <summary>
        ///     Creates to incontinence pad TestInfo from diffrent input data
        /// </summary>
        /// <param name="testPerson">the person who did the test</param>
        /// <param name="prodCode">the prodcution code from the diaper</param>
        /// <param name="testValueId">the id of the testvalue</param>
        /// <returns></returns>
        public IncontinencePadTestInfo ToTestInfo( String testPerson, String prodCode, Int32 testValueId )
            => new IncontinencePadTestInfo
            {
                TestPerson = testPerson,
                ProductionCode = prodCode,
                TestValueId = testValueId
            };

        /// <summary>
        ///     Creates a collection of a TestValue Type and selects only the needed items from a Collection with help of the input
        ///     parameter
        /// </summary>
        /// <typeparam name="T">
        ///     the collection type. For example: <see cref="IncontinencePadRewetTestValue" />,
        ///     <see cref="IncontinencePadAcquisitionTimeTestValue" />,<see cref="IncontinencePadRetentionTestValue" />
        /// </typeparam>
        /// <param name="testValue">the testvalues that contain the data</param>
        /// <param name="testValueType">the value type of the test. <see cref="TestValueType" /></param>
        /// <param name="testTypeIncontinencePad">the type of the incontinence pad. <see cref="TestTypeIncontinencePad" /></param>
        /// <param name="toTestTypeTestValueAction">
        ///     the action the get the correct data for a collection item. For example:
        ///     <see cref="ToRewetTestValue" />, <see cref="ToRetentionTestValue" />, <see cref="ToAcquisitionTimeTestValue" />
        /// </param>
        /// <returns>
        ///     A Collection with the Type of the output from the <paramref name="toTestTypeTestValueAction" /> given as a
        ///     input action
        /// </returns>
        public Collection<T> ToTestValuesCollectionByTestType<T>( IEnumerable<TestValue> testValue,
                                                                  TestValueType testValueType,
                                                                  ICollection<TestTypeIncontinencePad> testTypeIncontinencePad,
                                                                  Func<IncontinencePadTestValue, String, String, Int32, T> toTestTypeTestValueAction )
        {
            var tests = new Collection<T>();
            var values = testValue.ToList()
                                  .Where(
                                      x =>
                                          ( x.TestValueType == testValueType )
                                          && x.IncontinencePadTestValue.TestType.IsIn( testTypeIncontinencePad ) )
                                  .ForEach(
                                      x =>
                                          tests.Add( toTestTypeTestValueAction(
                                                         x.IncontinencePadTestValue,
                                                         x.LastEditedPerson,
                                                         LaborCreatorServiceHelper.GenerateProdCode( x.TestSheet.MachineNr,
                                                                           x.TestSheet.CreatedDateTime.Year,
                                                                           x.DayInYearOfArticleCreation,
                                                                           x.IncontinencePadTestValue.IncontinencePadTime )
                                                         ,
                                                         x.TestValueId ) ) );
            return tests;
        }

        /// <summary>
        ///     Validates a required item
        /// </summary>
        /// <param name="item">the idtem to be validated</param>
        /// <param name="name">the name of the item</param>
        /// <typeparam name="T">the type of the item</typeparam>
        /// <exception cref="InvalidDataException">a Invalid Data Exception because the item must be set</exception>
        public void ValidateRequiredItem<T>( T item, String name )
        {
            if ( item.IsNull() )
                throw new InvalidDataException( "Item " + name + " of type " + typeof(T) + " is required and can not be null" );
        }

        /// <summary>
        ///     Validates the test value where only one item is allowed to exists for the given input parameter. Throws a
        ///     <see cref="InvalidDataException" /> if more than one or none is found.
        ///     Used for example as average or standard deviation.
        /// </summary>
        /// <param name="testValue">the testvalue collection to validate.</param>
        /// <param name="testType">the testtype that is validate</param>
        /// <param name="valueType">the valuetype that is validated</param>
        /// <returns></returns>
        public TestValue ValidateTestValueOnlyExactlyOneHasToExist( ICollection<TestValue> testValue, String testType, String valueType )
        {
            if ( testValue.Count > 1 )
            {
                Logger.Error( "Only one Average for " + testType + " per Testsheet allowed" );
                throw new InvalidDataException( "Only one " + valueType + " for " + testType + " per Testsheet allowed" );
            }

            var item = testValue.FirstOrDefault();
            if ( item == null )
            {
                Logger.Error( "No " + valueType + " for " + testType + " per Testsheet existing" );
                throw new InvalidDataException( "No " + valueType + " for " + testType + " per Testsheet existing" );
            }
            return item;
        }
    }
}