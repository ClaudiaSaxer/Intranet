using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using Extend;
using Intranet.Common;
using Intranet.Labor.Definition;
using Intranet.Labor.Model;
using Intranet.Labor.Model.labor;
using Intranet.Labor.ViewModel;

namespace Intranet.Labor.Bll
{
    /// <summary>
    ///     Class representing the labor creator service
    /// </summary>
    public class BabyDiaperLaborCreatorServiceHelper : ServiceBase, IBabyDiaperLaborCreatorServiceHelper
    {
        #region Ctor

        /// <summary>
        ///     Initialize a new instance of the <see cref="ServiceBase" /> class.
        /// </summary>
        /// <param name="loggerFactory">A <see cref="ILoggerFactory" />.</param>
        public BabyDiaperLaborCreatorServiceHelper( ILoggerFactory loggerFactory )
            : base( loggerFactory.CreateLogger( typeof(BabyDiaperLaborCreatorServiceHelper) ) )
        {
        }

        #endregion

        /// <summary>
        ///     gets the average weight for all tests
        /// </summary>
        public Double ComputeWeightAverageAll( IEnumerable<TestValue> testValues )
        {
            var weights = AllWeightsOfArticleTypeForSingle( testValues, ArticleType.BabyDiaper );
            return weights.Count == 0 ? 0 : weights.Average();
        }

        /// <summary>
        ///     gets the weight for the standard deviation for all tests
        /// </summary>
        public Double ComputeWeightStandardDeviationAll( IEnumerable<TestValue> testValues )
        {
            var weights = AllWeightsOfArticleTypeForSingle( testValues, ArticleType.BabyDiaper );
            var average = weights.Count == 0 ? 0 : weights.Average();

            var sumOfSquaresOfDifferences = weights.Sum( val => ( val - average ) * ( val - average ) );
            var standardDeviation = Math.Sqrt( sumOfSquaresOfDifferences / ( weights.Count - 1 ) );

            return standardDeviation;
        }

        /// <summary>
        ///     Generates the Production Code for the Diaper
        /// </summary>
        /// <param name="machine">The machine Nr</param>
        /// <param name="year">the year of the production of the diaper</param>
        /// <param name="dayOfyear">the day of the year of the production of the diaper</param>
        /// <param name="time">the time od the production of the diaper</param>
        /// <returns>A Production code for a single diaper</returns>
        public String GenerateProdCode( String machine, Int32 year, Int32 dayOfyear, TimeSpan time )
            => "IT/" + machine.Substring( 1 ) + "/" + year.ToString( "0000" )
                                           .SubstringRight( 2 ) + "/" + dayOfyear + "/" + time.Hours.ToString( "00" ) + ":" + time.Minutes.ToString( "00" );

        /// <summary>
        ///     Gets the BabyDiaperTestValue out of a list of testvalues for the correct <see cref="TestTypeBabyDiaper" /> and
        ///     <see cref="TestValueType" />
        ///     One must exists and only one.
        /// </summary>
        /// <param name="testValues">the test values containing the wanted item</param>
        /// <param name="testTypeBabyDiaper">the type of the baby diaper. <see cref="TestTypeBabyDiaper" /></param>
        /// <param name="testValueType">the value type of the baby diaper. <see cref="TestValueType" /></param>
        /// <returns></returns>
        public BabyDiaperTestValue GetBabyDiaperTestValueForType( IEnumerable<TestValue> testValues, TestTypeBabyDiaper testTypeBabyDiaper, TestValueType testValueType )
        {
            var values = testValues.ToList()
                                   .Where(
                                       x => ( x.TestValueType == testValueType ) && ( x.BabyDiaperTestValue.TestType == testTypeBabyDiaper ) )
                                   .ToList();
            var item = ValidateTestValueOnlyExactlyOneHasToExist( values, testTypeBabyDiaper.ToString(), testValueType.ToString() );
            return item.BabyDiaperTestValue;
        }

        /// <summary>
        ///     Sets the values for the Penetration Time View Model out of the BabyDiaperTestValue Model
        /// </summary>
        /// <param name="penetrationTime">the Baby Diaper Test value with the penetration time data</param>
        /// <returns>The Penetration Time View Model with the data collected from the model</returns>
        public BabyDiaperPenetrationTime ToPenetrationTime( BabyDiaperTestValue penetrationTime )
            => new BabyDiaperPenetrationTime
            {
                PenetrationTimeAdditionFourth = penetrationTime.PenetrationTimeAdditionFourth,
                PenetrationTimeAdditionSecond = penetrationTime.PenetrationTimeAdditionSecond,
                PenetrationTimeAdditionFirst = penetrationTime.PenetrationTimeAdditionFirst,
                PenetrationTimeAdditionThird = penetrationTime.PenetrationTimeAdditionThird
            };

        /// <summary>
        ///     Creates a BabyDiaperPenetrationTime Average with the data from the test values
        /// </summary>
        /// <param name="testValues">testvalues containing a item representing data for the average</param>
        /// <returns>a class representing the penetration time with the average</returns>
        public BabyDiaperPenetrationTime ToPenetrationTimeAverage( IEnumerable<TestValue> testValues )
            => ToPenetrationTime( GetBabyDiaperTestValueForType( testValues, TestTypeBabyDiaper.RewetAndPenetrationTime, TestValueType.Average ) );

        /// <summary>
        ///     Creates a BabyDiaperPenetrationTime Standard Deviation with the data from the test values
        /// </summary>
        /// <param name="testValues">testvalues containing a item representing data for the standard deviation</param>
        /// <returns>a class representing the penetration time with the standard deviation</returns>
        public BabyDiaperPenetrationTime ToPenetrationTimeStandardDeviation( IEnumerable<TestValue> testValues )
            => ToPenetrationTime( GetBabyDiaperTestValueForType( testValues, TestTypeBabyDiaper.RewetAndPenetrationTime, TestValueType.StandardDeviation ) );

        /// <summary>
        ///     Creates the BabyDiaperPenetrationTime TestValue from diffrent input data
        /// </summary>
        /// <param name="penetrationTime">the baby diaper test value containing the penetration time data</param>
        /// <param name="testPerson">the person who did the test</param>
        /// <param name="prodCode">the diaper production code</param>
        /// <param name="testValueId">the id of the test value</param>
        /// <returns></returns>
        public BabyDiaperPenetrationTimeTestValue ToPenetrationTimeTestValue( BabyDiaperTestValue penetrationTime, String testPerson, String prodCode, Int32 testValueId )
        {
            var vm = new BabyDiaperPenetrationTimeTestValue
            {
                TestInfo = toTestInfo( testPerson, prodCode, penetrationTime.WeightDiaperDry, testValueId ),
                BabyDiaperPenetrationTime = ToPenetrationTime( penetrationTime )
            };
            return vm;
        }

        /// <summary>
        ///     Creates the penetration time test value collection for all singe tests
        /// </summary>
        /// <param name="testValues">the testvalues containing the penetration time tests</param>
        /// <returns>a collection for all penetration time tests</returns>
        public ICollection<BabyDiaperPenetrationTimeTestValue> ToPenetrationTimeTestValuesCollection( IEnumerable<TestValue> testValues )
            => ToTestValuesCollectionByTestType( testValues,
                                                 TestValueType.Single,
                                                 new List<TestTypeBabyDiaper> { TestTypeBabyDiaper.RewetAndPenetrationTime },
                                                 ToPenetrationTimeTestValue );

        /// <summary>
        ///     Sets the values for the BabyDiaperRetention View Model out of the BabyDiaperTestValue Model
        /// </summary>
        /// <param name="retention">the Baby Diaper Test value with the retention data</param>
        /// <returns>The BabyDiaperRetention View Model with the data collected from the model</returns>
        public BabyDiaperRetention ToRetention( BabyDiaperTestValue retention )
        {
            ValidateRequiredItem( retention.RetentionRw, "retention rw" );
            return new BabyDiaperRetention
            {
                SapNr = retention.SapNr,
                RetentionAfterZentrifugeValue = retention.RetentionAfterZentrifugeValue,
                SapType = retention.SapType,
                RetentionRw = retention.RetentionRw.GetValueOrDefault(),
                RetentionWetWeight = retention.RetentionWetWeight,
                RetentionAfterZentrifugePercent = retention.RetentionAfterZentrifugePercent,
                SapGHoewiValue = retention.SapGHoewiValue
            };
        }

        /// <summary>
        ///     Creates a retention Average with the data from the test values
        /// </summary>
        /// <param name="testValues">testvalues containing a item representing data for the average</param>
        /// <returns>a class representing the retention with the average</returns>
        public BabyDiaperRetention ToRetentionAverage( IEnumerable<TestValue> testValues )
            => ToRetention( GetBabyDiaperTestValueForType( testValues, TestTypeBabyDiaper.Retention, TestValueType.Average ) );

        /// <summary>
        ///     Creates a BabyDiaperRetention Standard Deviation with the data from the test values
        /// </summary>
        /// <param name="testValues">testvalues containing a item representing data for the standard deviation</param>
        /// <returns>a class representing the retention with the standard deviation</returns>
        public BabyDiaperRetention ToRetentionStandardDeviation( IEnumerable<TestValue> testValues )
            => ToRetention( GetBabyDiaperTestValueForType( testValues, TestTypeBabyDiaper.Retention, TestValueType.StandardDeviation ) );

        /// <summary>
        ///     Creates the RetentionTestValues from diffrent input data
        /// </summary>
        /// <param name="retention">the baby diaper test value containing the retention data</param>
        /// <param name="testPerson">the person who did the test</param>
        /// <param name="prodCode">the diaper production code</param>
        /// <param name="testValueId">the id of the testvalue</param>
        /// <returns></returns>
        public BabyDiaperRetentionTestValue ToRetentionTestValue( BabyDiaperTestValue retention, String testPerson, String prodCode, Int32 testValueId )
        {
            var vm = new BabyDiaperRetentionTestValue
            {
                TestInfo = toTestInfo( testPerson, prodCode, retention.WeightDiaperDry, testValueId ),
                BabyDiaperRetention = ToRetention( retention )
            };
            return vm;
        }

        /// <summary>
        ///     Creates the retention  test value collection for all singe tests
        /// </summary>
        /// <param name="testValues">the testvalues containing the retention tests</param>
        /// <returns>a collection for all retention tests</returns>
        public ICollection<BabyDiaperRetentionTestValue> ToRetentionTestValuesCollection( IEnumerable<TestValue> testValues )
            => ToTestValuesCollectionByTestType( testValues,
                                                 TestValueType.Single,
                                                 new List<TestTypeBabyDiaper> { TestTypeBabyDiaper.Retention },
                                                 ToRetentionTestValue );

        /// <summary>
        ///     Sets the values for the BabyDiaperRewet View Model out of the BabyDiaperTestValue Model
        /// </summary>
        /// <param name="rewet">the Baby Diaper Test value with the rewet data</param>
        /// <returns>The rewet View Model with the data collected from the model</returns>
        public BabyDiaperRewet ToRewet( BabyDiaperTestValue rewet )
        {
            ValidateRequiredItem( rewet.Rewet210Rw, "rewet 210 rw" );
            ValidateRequiredItem( rewet.Rewet140Rw, "rewet 140 rw" );

            return
                new BabyDiaperRewet
                {
                    Rewet210Rw = rewet.Rewet210Rw.GetValueOrDefault(),
                    StrikeTroughValue = rewet.StrikeTroughValue,
                    DistributionOfTheStrikeTrough = rewet.DistributionOfTheStrikeTrough,
                    Rewet210Value = rewet.Rewet210Value,
                    Rewet140Rw = rewet.Rewet140Rw.GetValueOrDefault(),
                    Rewet140Value = rewet.Rewet140Value
                };
        }

        /// <summary>
        ///     Creates a retention Average with the data from the test values
        /// </summary>
        /// <param name="testValues">testvalues containing a item representing data for the average</param>
        /// <returns>a class representing the retention with the average</returns>
        public BabyDiaperRewet ToRewetAverage( IEnumerable<TestValue> testValues )
            => ToRewet( GetBabyDiaperTestValueForType( testValues, TestTypeBabyDiaper.Rewet, TestValueType.Average ) );

        /// <summary>
        ///     Creates a BabyDiaperRewet Standard Deviation with the data from the test values
        /// </summary>
        /// <param name="testValues">testvalues containing a item representing data for the standard deviation</param>
        /// <returns>a class representing the rewet with the standard deviation</returns>
        public BabyDiaperRewet ToRewetStandardDeviation( IEnumerable<TestValue> testValues )
            => ToRewet( GetBabyDiaperTestValueForType( testValues, TestTypeBabyDiaper.Rewet, TestValueType.StandardDeviation ) );

        /// <summary>
        ///     Creates the BabyDiaperRewet TestValue from diffrent input data
        /// </summary>
        /// <param name="rewet">the baby diaper test value containing the rewet data</param>
        /// <param name="testPerson">the person who did the test</param>
        /// <param name="prodCode">the diaper production code</param>
        /// <param name="testValueId">the id of the testvalue</param>
        /// <returns></returns>
        public BabyDiaperRewetTestValue ToRewetTestValue( BabyDiaperTestValue rewet, String testPerson, String prodCode, Int32 testValueId )
        {
            var vm = new BabyDiaperRewetTestValue
            {
                TestInfo = toTestInfo( testPerson, prodCode, rewet.WeightDiaperDry, testValueId ),
                BabyDiaperRewet = ToRewet( rewet )
            };
            return vm;
        }

        /// <summary>
        ///     Creates the rewet test value collection for all singe tests
        /// </summary>
        /// <param name="testValues">the testvalues containing the rewet tests</param>
        /// <returns>a collection for all rewet tests</returns>
        public ICollection<BabyDiaperRewetTestValue> ToRewetTestValuesCollection( IEnumerable<TestValue> testValues )
            => ToTestValuesCollectionByTestType( testValues,
                                                 TestValueType.Single,
                                                 new List<TestTypeBabyDiaper> { TestTypeBabyDiaper.Rewet, TestTypeBabyDiaper.RewetAndPenetrationTime },
                                                 ToRewetTestValue );

        /// <summary>
        ///     Creates to TestInfo from diffrent input data
        /// </summary>
        /// <param name="testPerson">the person who did the test</param>
        /// <param name="prodCode">the prodcution code from the diaper</param>
        /// <param name="weightDiaperDry">the weight of the dry diaper</param>
        /// <param name="testValueId">the id of the testvalue</param>
        /// <returns></returns>
        public TestInfo toTestInfo( String testPerson, String prodCode, Double weightDiaperDry, Int32 testValueId )
            => new TestInfo
            {
                TestPerson = testPerson,
                ProductionCode = prodCode,
                WeightyDiaperDry = weightDiaperDry,
                TestValueId = testValueId
            };

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

        /// <summary>
        ///     All weight for the article type
        /// </summary>
        /// <param name="testValues">the test values</param>
        /// <param name="articleType">the type of the article</param>
        /// <returns>a collection of all types</returns>
        public Collection<Double> AllWeightsOfArticleTypeForSingle( IEnumerable<TestValue> testValues, ArticleType articleType )
        {
            var weights = new Collection<Double>();
            
            testValues.ForEach( value =>
                                {
                                    if ( value.ArticleTestType == articleType && value.TestValueType == TestValueType.Single)
                                        weights.Add( value.BabyDiaperTestValue.WeightDiaperDry );
                                }
            );
            return weights;
        }

        /// <summary>
        ///     Creates a collection of a TestValue Type and selects only the needed items from a Collection with help of the input
        ///     parameter
        /// </summary>
        /// <typeparam name="T">
        ///     the collection type. For example: <see cref="BabyDiaperRewetTestValue" />,
        ///     <see cref="BabyDiaperPenetrationTimeTestValue" />,<see cref="BabyDiaperRetentionTestValue" />
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
        public Collection<T> ToTestValuesCollectionByTestType<T>( IEnumerable<TestValue> testValue,
                                                                  TestValueType testValueType,
                                                                  ICollection<TestTypeBabyDiaper> testTypeBabyDiaper,
                                                                  Func<BabyDiaperTestValue, String, String, Int32, T> toTestTypeTestValueAction )
        {
            var tests = new Collection<T>();
            var values = testValue.ToList()
                                  .Where(
                                      x =>
                                          ( x.TestValueType == testValueType )
                                          && x.BabyDiaperTestValue.TestType.IsIn( testTypeBabyDiaper ) )
                                  .ForEach(
                                      x =>
                                          tests.Add( toTestTypeTestValueAction( x.BabyDiaperTestValue,
                                                                                x.LastEditedPerson,
                                                                                GenerateProdCode( x.TestSheet.MachineNr,
                                                                                                  x.TestSheet.CreatedDateTime.Year,
                                                                                                  x.DayInYearOfArticleCreation,
                                                                                                  x.BabyDiaperTestValue.DiaperCreatedTime )
                                                                                ,
                                                                                x.TestValueId ) ) );
            return tests;
        }
    }
}