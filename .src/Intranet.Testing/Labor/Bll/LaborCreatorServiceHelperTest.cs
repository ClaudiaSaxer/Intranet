using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Intranet.Common;
using Intranet.Web.Areas.Labor.Controllers;
using Xunit;

namespace Intranet.Labor.Bll.Test
{
    /// <summary>
    /// Class representing the Test for the class <see cref="LaborCreatorService"/>
    /// </summary>
   public class LaborCreatorServiceHelperTest
    {
        [Fact]
        public void GenerateProdCodeTest()
        {
           var service = new LaborCreatorService( new NLogLoggerFactory() );
           
        }
    }
}
