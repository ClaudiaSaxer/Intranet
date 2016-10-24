using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Extend;
using FluentAssertions;
using Intranet.Model;
using Xunit;

namespace Intranet.Bll.Test
{
    /// <summary>
    /// 
    /// </summary>
    public class TempTest
    {

    
        /// <summary>
        /// 
        /// </summary>
        [Fact]
        public void asdfTest()
        {

            var actual = InstanceCreator.CreateInstanceOptions<Module>().Complete().CreateInstance();
            actual.Should()
                 .NotBeNull();

        }
    }
}
