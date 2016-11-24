using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using Intranet.Labor.Model.labor;
using Intranet.Labor.ViewModel;

namespace Intranet.Labor.Definition
{
    /// <summary>
    ///     Class representing a interface for IncontinencePadLaborCreatorServiceHelper
    /// </summary>
    public interface IIncontinencePadLaborCreatorServiceHelper
    {
        

        /// <summary>
        ///     Gets the IncontinencePadTestValue out of a list of testvalues for the correct <see cref="TestTypeIncontinencePad" /> and
        ///     <see cref="TestValueType" />
        /// </summary>
        /// <param name="testValues">the test values containing the wanted item</param>
        /// <param name="testTypeIncontinencePad">the type of the invontinence pad. <see cref="TestTypeIncontinencePad" /></param>
        /// <param name="testValueType">the value type of the incontinence pad. <see cref="TestValueType" /></param>
        /// <returns></returns>
        IncontinencePadTestValue GetIncontinencePadTestValueForType( IEnumerable<TestValue> testValues, TestTypeIncontinencePad testTypeIncontinencePad, TestValueType testValueType );

        /// <summary>
        ///     Sets the values for the acquisition Time View Model out of the IncontinencePadTestValue Model
        /// </summary>
        /// <param name="penetrationTime">the Incontinence Pad Test value with the acquisition time data</param>
        /// <returns>The Acquisiiton Time View Model with the data collected from the model</returns>
        IncontinencePadAcquisitionTime ToAcquisitionTime( IncontinencePadTestValue penetrationTime );

        /// <summary>
        ///     Creates a acquisition time Average with the data from the test values
        /// </summary>
        /// <param name="testValues">testvalues containing a item representing data for the average</param>
        /// <returns>a class representing the acquisition time with the average</returns>
        IncontinencePadAcquisitionTime ToAcquisitionTimeAverage( IEnumerable<TestValue> testValues );

        /// <summary>
        ///     Creates a acquisition time Standard Deviation with the data from the test values
        /// </summary>
        /// <param name="testValues">testvalues containing a item representing data for the standard deviation</param>
        /// <returns>a class representing the acquisition time with the standard deviation</returns>
        IncontinencePadAcquisitionTime ToAcquisitionTimeStandardDeviation( IEnumerable<TestValue> testValues );

        /// <summary>
        ///     Creates the PenetrationTime TestValue from diffrent input data
        /// </summary>
        /// <param name="acquisitionTime">the incontinence pad test value containing the penetration time data</param>
        /// <param name="testPerson">the person who did the test</param>
        /// <param name="prodCode">the diaper production code</param>
        /// <param name="testValueId">the id of the testvalue</param>
        /// <returns>a Penetration Time test value</returns>
        IncontinencePadAcquisitionTimeTestValue ToAcquisitionTimeTestValue( IncontinencePadTestValue acquisitionTime, String testPerson, String prodCode,Int32 testValueId );

        /// <summary>
        ///     Creates the acquisition time test value collection for all singe tests
        /// </summary>
        /// <param name="testValues">the testvalues containing the acquisition time tests</param>
        /// <returns>a collection for all acquisition time tests</returns>
        ICollection<IncontinencePadAcquisitionTimeTestValue> ToAcquisitionTimeTestValuesCollection( IEnumerable<TestValue> testValues );

        /// <summary>
        ///     Sets the values for the retention View Model out of the TestValue Model
        /// </summary>
        /// <param name="retention">the Test value with the retention data</param>
        /// <returns>The View Model with the data collected from the model</returns>
        IncontinencePadRetention ToRetention( IncontinencePadTestValue retention );

        /// <summary>
        ///     Creates a retention Average with the data from the test values
        /// </summary>
        /// <param name="testValues">testvalues containing a item representing data for the average</param>
        /// <returns>a class representing the retention with the average</returns>
        IncontinencePadRetention ToRetentionAverage( IEnumerable<TestValue> testValues );

        /// <summary>
        ///     Creates a rRetention Standard Deviation with the data from the test values
        /// </summary>
        /// <param name="testValues">testvalues containing a item representing data for the standard deviation</param>
        /// <returns>a class representing the retention with the standard deviation</returns>
        IncontinencePadRetention ToRetentionStandardDeviation( IEnumerable<TestValue> testValues );

        /// <summary>
        ///     Creates the RetentionTestValues from diffrent input data
        /// </summary>
        /// <param name="retention">the test value containing the retention data</param>
        /// <param name="testPerson">the person who did the test</param>
        /// <param name="prodCode">the diaper production code</param>
        /// <param name="testValueId">the id of the test value</param>
        /// <returns>a test value for the retention test</returns>
        IncontinencePadRetentionTestValue ToRetentionTestValue( IncontinencePadTestValue retention, String testPerson, String prodCode, Int32 testValueId );

        /// <summary>
        ///     Creates the retention  test value collection for all singe tests
        /// </summary>
        /// <param name="testValues">the testvalues containing the retention tests</param>
        /// <returns>a collection for all retention tests</returns>
        ICollection<IncontinencePadRetentionTestValue> ToRetentionTestValuesCollection( IEnumerable<TestValue> testValues );

        /// <summary>
        ///     Sets the values for the rewet View Model out of the IncontinencePadTestValue Model
        /// </summary>
        /// <param name="rewet">the Incontinence Pad Test value with the rewet data</param>
        /// <returns>The rewet View Model with the data collected from the model</returns>
        IncontinencePadRewet ToRewet( IncontinencePadTestValue rewet );

        /// <summary>
        ///     Creates a rewet Average with the data from the test values
        /// </summary>
        /// <param name="testValues">testvalues containing a item representing data for the average</param>
        /// <returns>a class representing the rewet with the average</returns>
        IncontinencePadRewet ToRewetAverage( IEnumerable<TestValue> testValues );

        /// <summary>
        ///     Creates a rewet Standard Deviation with the data from the test values
        /// </summary>
        /// <param name="testValues">testvalues containing a item representing data for the standard deviation</param>
        /// <returns>a class representing the rewet with the standard deviation</returns>
        IncontinencePadRewet ToRewetStandardDeviation( IEnumerable<TestValue> testValues );

        /// <summary>
        ///     Creates a rewet after acquisition  Average with the data from the test values
        /// </summary>
        /// <param name="testValues">testvalues containing a item representing data for the average</param>
        /// <returns>a class representing the rewet after acquisition with the average</returns>
        IncontinencePadRewet ToRewetAfterAcquisitionTimeAverage(IEnumerable<TestValue> testValues);

        /// <summary>
        ///     Creates a rewet after acquisition Standard Deviation with the data from the test values
        /// </summary>
        /// <param name="testValues">testvalues containing a item representing data for the standard deviation</param>
        /// <returns>a class representing the rewet after acquisition with the standard deviation</returns>
        IncontinencePadRewet ToRewetAfterAcquisitionTimeStandardDeviation(IEnumerable<TestValue> testValues);


        /// <summary>
        ///     Creates the TestValue from diffrent input data
        /// </summary>
        /// <param name="rewet">the incontinence pad test value containing the rewet data</param>
        /// <param name="testPerson">the person who did the test</param>
        /// <param name="prodCode">the diaper production code</param>
        /// <param name="testValueId">the id of the test value</param>
        /// <returns>the test value for the rewet test</returns>
        IncontinencePadRewetTestValue ToRewetTestValue( IncontinencePadTestValue rewet, String testPerson, String prodCode, Int32 testValueId);

        /// <summary>
        ///     Creates the rewet test value collection for all singe tests
        /// </summary>
        /// <param name="testValues">the testvalues containing the rewet tests</param>
        /// <returns>a collection for all rewet tests</returns>
        ICollection<IncontinencePadRewetTestValue> ToRewetTestValuesCollection( IEnumerable<TestValue> testValues );

        /// <summary>
        ///     Creates to  IncontinencePadTestInfo from diffrent input data
        /// </summary>
        /// <param name="testPerson">the person who did the test</param>
        /// <param name="prodCode">the prodcution code from the diaper</param>
        /// <param name="testValueId">the id of the testvalue</param>
        /// <returns>a Incontinence Pad TestInfo</returns>
        IncontinencePadTestInfo ToTestInfo( String testPerson, String prodCode, Int32 testValueId);

        /// <summary>
        ///     Creates a collection of a TestValue Type and selects only the needed items from a Collection with help of the input
        ///     parameter
        /// </summary>
        /// <typeparam name="T">
        ///     the collection type. For example: <see cref=" IncontinencePadRewetTestValue" />,
        ///     <see cref=" IncontinencePadAcquisitionTimeTestValue" />,<see cref=" IncontinencePadRetentionTestValue" />
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
        Collection<T> ToTestValuesCollectionByTestType<T>( IEnumerable<TestValue> testValue,
                                                           TestValueType testValueType,
                                                           ICollection<TestTypeIncontinencePad> testTypeIncontinencePad,
                                                           Func<IncontinencePadTestValue, String, String,Int32, T> toTestTypeTestValueAction );

        /// <summary>
        ///     Validates the test value where only one item is allowed to exists for the given input parameter. Throws a
        ///     <see cref="InvalidDataException" /> if more than one or none is found.
        ///     Used for example as average or standard deviation.
        /// </summary>
        /// <param name="testValue">the testvalue collection to validate.</param>
        /// <param name="testType">the testtype that is validate</param>
        /// <param name="valueType">the valuetype that is validated</param>
        /// <returns></returns>
        TestValue ValidateTestValueOnlyExactlyOneHasToExist( ICollection<TestValue> testValue, String testType, String valueType );

        /// <summary>
        /// Validates a required item
        /// </summary>
        /// <param name="item">the idtem to be validated</param>
        /// <param name="name">the name of the item</param>
        /// <typeparam name="T">the type of the item</typeparam>
        /// <exception cref="InvalidDataException">a Invalid Data Exception because the item must be set</exception>
        void ValidateRequiredItem<T>( T item, String name );

        /// <summary>
        ///     Computes if the user can Edit the Incontinence Pad
        /// </summary>
        /// <returns></returns>
        Boolean CanUserEdit();


    }
}