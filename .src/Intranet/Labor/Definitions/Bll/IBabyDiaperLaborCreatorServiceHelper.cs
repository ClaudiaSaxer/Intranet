using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using Intranet.Labor.Model.labor;
using Intranet.Labor.ViewModel;

namespace Intranet.Labor.Definition
{
    /// <summary>
    ///     Class representing a interface for BabyDiaperLaborCreatorServiceHelper
    /// </summary>
    public interface IBabyDiaperLaborCreatorServiceHelper
    {
        /// <summary>
        ///     Generates the Production Code for the Diaper
        /// </summary>
        /// <param name="machine">The machine Nr</param>
        /// <param name="year">the year of the production of the diaper</param>
        /// <param name="dayOfyear">the day of the year of the production of the diaper</param>
        /// <param name="time">the time od the production of the diaper</param>
        /// <returns>A Production code for a single diaper</returns>
        String GenerateProdCode( String machine, Int32 year, Int32 dayOfyear, TimeSpan time );

        /// <summary>
        ///     Gets the BabyDiaperTestValue out of a list of testvalues for the correct <see cref="TestTypeBabyDiaper" /> and
        ///     <see cref="TestValueType" />
        /// </summary>
        /// <param name="testValues">the test values containing the wanted item</param>
        /// <param name="testTypeBabyDiaper">the type of the baby diaper. <see cref="TestTypeBabyDiaper" /></param>
        /// <param name="testValueType">the value type of the baby diaper. <see cref="TestValueType" /></param>
        /// <returns></returns>
        BabyDiaperTestValue GetBabyDiaperTestValueForType( IEnumerable<TestValue> testValues, TestTypeBabyDiaper testTypeBabyDiaper, TestValueType testValueType );

        /// <summary>
        ///     Sets the values for the Penetration Time View Model out of the BabyDiaperTestValue Model
        /// </summary>
        /// <param name="penetrationTime">the Baby Diaper Test value with the penetration time data</param>
        /// <returns>The Penetration Time View Model with the data collected from the model</returns>
        PenetrationTime ToPenetrationTime( BabyDiaperTestValue penetrationTime );

        /// <summary>
        ///     Creates a PenetrationTime Average with the data from the test values
        /// </summary>
        /// <param name="testValues">testvalues containing a item representing data for the average</param>
        /// <returns>a class representing the penetration time with the average</returns>
        PenetrationTime ToPenetrationTimeAverage( IEnumerable<TestValue> testValues );

        /// <summary>
        ///     Creates a PenetrationTime Standard Deviation with the data from the test values
        /// </summary>
        /// <param name="testValues">testvalues containing a item representing data for the standard deviation</param>
        /// <returns>a class representing the penetration time with the standard deviation</returns>
        PenetrationTime ToPenetrationTimeStandardDeviation( IEnumerable<TestValue> testValues );

        /// <summary>
        ///     Creates the PenetrationTime TestValue from diffrent input data
        /// </summary>
        /// <param name="penetrationTime">the baby diaper test value containing the penetration time data</param>
        /// <param name="testPerson">the person who did the test</param>
        /// <param name="prodCode">the diaper production code</param>
        /// <param name="testValueId">the id of the testvalue</param>
        /// <returns>a Penetration Time test value</returns>
        PenetrationTimeTestValue ToPenetrationTimeTestValue( BabyDiaperTestValue penetrationTime, String testPerson, String prodCode,Int32 testValueId );

        /// <summary>
        ///     Creates the penetration time test value collection for all singe tests
        /// </summary>
        /// <param name="testValues">the testvalues containing the penetration time tests</param>
        /// <returns>a collection for all penetration time tests</returns>
        ICollection<PenetrationTimeTestValue> ToPenetrationTimeTestValuesCollection( IEnumerable<TestValue> testValues );

        /// <summary>
        ///     Sets the values for the Retention View Model out of the BabyDiaperTestValue Model
        /// </summary>
        /// <param name="retention">the Baby Diaper Test value with the retention data</param>
        /// <returns>The Retention View Model with the data collected from the model</returns>
        Retention ToRetention( BabyDiaperTestValue retention );

        /// <summary>
        ///     Creates a retention Average with the data from the test values
        /// </summary>
        /// <param name="testValues">testvalues containing a item representing data for the average</param>
        /// <returns>a class representing the retention with the average</returns>
        Retention ToRetentionAverage( IEnumerable<TestValue> testValues );

        /// <summary>
        ///     Creates a Retention Standard Deviation with the data from the test values
        /// </summary>
        /// <param name="testValues">testvalues containing a item representing data for the standard deviation</param>
        /// <returns>a class representing the retention with the standard deviation</returns>
        Retention ToRetentionStandardDeviation( IEnumerable<TestValue> testValues );

        /// <summary>
        ///     Creates the RetentionTestValues from diffrent input data
        /// </summary>
        /// <param name="retention">the baby diaper test value containing the retention data</param>
        /// <param name="testPerson">the person who did the test</param>
        /// <param name="prodCode">the diaper production code</param>
        /// <param name="testValueId">the id of the test value</param>
        /// <returns>a test value for the retention test</returns>
        RetentionTestValue ToRetentionTestValue( BabyDiaperTestValue retention, String testPerson, String prodCode, Int32 testValueId );

        /// <summary>
        ///     Creates the retention  test value collection for all singe tests
        /// </summary>
        /// <param name="testValues">the testvalues containing the retention tests</param>
        /// <returns>a collection for all retention tests</returns>
        ICollection<RetentionTestValue> ToRetentionTestValuesCollection( IEnumerable<TestValue> testValues );

        /// <summary>
        ///     Sets the values for the Rewet View Model out of the BabyDiaperTestValue Model
        /// </summary>
        /// <param name="rewet">the Baby Diaper Test value with the rewet data</param>
        /// <returns>The rewet View Model with the data collected from the model</returns>
        Rewet ToRewet( BabyDiaperTestValue rewet );

        /// <summary>
        ///     Creates a retention Average with the data from the test values
        /// </summary>
        /// <param name="testValues">testvalues containing a item representing data for the average</param>
        /// <returns>a class representing the retention with the average</returns>
        Rewet ToRewetAverage( IEnumerable<TestValue> testValues );

        /// <summary>
        ///     Creates a Rewet Standard Deviation with the data from the test values
        /// </summary>
        /// <param name="testValues">testvalues containing a item representing data for the standard deviation</param>
        /// <returns>a class representing the rewet with the standard deviation</returns>
        Rewet ToRewetStandardDeviation( IEnumerable<TestValue> testValues );

        /// <summary>
        ///     Creates the Rewet TestValue from diffrent input data
        /// </summary>
        /// <param name="rewet">the baby diaper test value containing the rewet data</param>
        /// <param name="testPerson">the person who did the test</param>
        /// <param name="prodCode">the diaper production code</param>
        /// <param name="testValueId">the id of the test value</param>
        /// <returns>the test value for the rewet test</returns>
        RewetTestValue ToRewetTestValue( BabyDiaperTestValue rewet, String testPerson, String prodCode, Int32 testValueId);

        /// <summary>
        ///     Creates the rewet test value collection for all singe tests
        /// </summary>
        /// <param name="testValues">the testvalues containing the rewet tests</param>
        /// <returns>a collection for all rewet tests</returns>
        ICollection<RewetTestValue> ToRewetTestValuesCollection( IEnumerable<TestValue> testValues );

        /// <summary>
        ///     Creates to TestInfo from diffrent input data
        /// </summary>
        /// <param name="testPerson">the person who did the test</param>
        /// <param name="prodCode">the prodcution code from the diaper</param>
        /// <param name="weightDiaperDry">the weight of the dry diaper</param>
        /// <param name="testValueId">the id of the testvalue</param>
        /// <returns>a TestInfo</returns>
        TestInfo toTestInfo( String testPerson, String prodCode, Double weightDiaperDry, Int32 testValueId);

        /// <summary>
        ///     Creates a collection of a TestValue Type and selects only the needed items from a Collection with help of the input
        ///     parameter
        /// </summary>
        /// <typeparam name="T">
        ///     the collection type. For example: <see cref="RewetTestValue" />,
        ///     <see cref="PenetrationTimeTestValue" />,<see cref="RetentionTestValue" />
        /// </typeparam>
        /// <param name="testValue">the testvalues that contain the data</param>
        /// <param name="testValueType">the value type of the test. <see cref="TestValueType" /></param>
        /// <param name="testTypeBabyDiaper">the type of the baby diaper. <see cref="TestTypeBabyDiaper" /></param>
        /// <param name="toTestTypeTestValueAction">
        ///     the action the get the correct data for a collection item. For example:
        ///     <see cref="ToRewetTestValue" />, <see cref="ToRetentionTestValue" />, <see cref="ToPenetrationTimeTestValue" />
        /// </param>
        /// <returns>
        ///     A Collection with the Type of the output from the <paramref name="toTestTypeTestValueAction" /> given as a
        ///     input action
        /// </returns>
        Collection<T> ToTestValuesCollectionByTestType<T>( IEnumerable<TestValue> testValue,
                                                           TestValueType testValueType,
                                                           ICollection<TestTypeBabyDiaper> testTypeBabyDiaper,
                                                           Func<BabyDiaperTestValue, String, String,Int32, T> toTestTypeTestValueAction );

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
       /// gets the weight for the standard deviation for all tests
       /// </summary>
        Double ComputeWeightStandardDeviationAll(IEnumerable<TestValue> testValue);

        /// <summary>
        /// gets the average weight for all tests
        /// </summary>
        Double ComputeWeightAverageAll(IEnumerable<TestValue> testValue);
    }
}