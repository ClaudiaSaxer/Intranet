using System;
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
        ///     Testing GenerateProdCode
        /// </summary>
        [Fact]
        public void GenerateProdCodeTest()
        {
            var serviceHelper = new LaborCreatorServiceHelper( new NLogLoggerFactory() );
            var expected = "IT/11/16/158/23:51";

            serviceHelper.GenerateProdCode( "11",
                                            2016,
                                            158,
                                            new TimeSpan(
                                                23,
                                                58,
                                                0 ) );
        }
    }
}