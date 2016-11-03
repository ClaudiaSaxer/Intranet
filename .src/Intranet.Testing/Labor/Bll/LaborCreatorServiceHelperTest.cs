using System;
using FluentAssertions;
using Intranet.Common;
using Intranet.Web.Areas.Labor.Controllers;
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
            const String expected = "IT/11/16/158/23:51";

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
            var serviceHelper = new LaborCreatorServiceHelper(new NLogLoggerFactory());
            const String expected = "IT/11/16/158/23:51";

            var actual = serviceHelper.GenerateProdCode("11", 16, 158, new TimeSpan(23, 58, 0));

            actual.Should()
                  .Be(expected);
        }
       
    }
}