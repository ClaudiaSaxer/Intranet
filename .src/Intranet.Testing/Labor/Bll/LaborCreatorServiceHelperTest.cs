using System;
using FluentAssertions;
using Intranet.Common;
using Xunit;

namespace Intranet.Labor.Bll.Test
{
   /// <summary>
    ///     Class representing the Test for the class <see cref="LaborCreatorServiceHelper" />
    /// </summary>
    public class LaborCreatorServiceHelperTest
    {
        /// <summary>
        ///     Testing GenerateProdCode1
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
        ///     Testing GenerateProdCode2
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
        ///     Testing GenerateProdCode3
        /// </summary>
        [Fact]
        public void GenerateProdCodeTest3()
        {
            var serviceHelper = new LaborCreatorServiceHelper(new NLogLoggerFactory());
            const String expected = "IT/11/06/158/23:00";
            var actual = serviceHelper.GenerateProdCode("11", 2006, 158, new TimeSpan(23, 00, 0));

            actual.Should()
                  .Be(expected);
        }

        /// <summary>
        ///     Testing GenerateProdCode4
        /// </summary>
        [Fact]
        public void GenerateProdCodeTest4()
        {
            var serviceHelper = new LaborCreatorServiceHelper(new NLogLoggerFactory());
            const String expected = "IT/11/16/158/03:01";
            var actual = serviceHelper.GenerateProdCode("11", 2016, 158, new TimeSpan(3, 1, 0));

            actual.Should()
                  .Be(expected);
        }
    }
}