using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Intranet.Common;
using Intranet.Model;
using Intranet.TestEnvironment;
using Xunit;

namespace Intranet.Bll.Test
{
    /// <summary>
    ///     Test Class for CheckDisableBll
    /// </summary>
    public class CheckDisableBllTest
    {
        /// <summary>
        ///     Tests if module exist with visibility = true
        /// </summary>
        [Fact]
        public void GetModuleTrueTest()
        {
            var moduleRepository =
                MockHelperBll.GetAllModules(
                    new List<Module>
                    {
                        new Module
                        {
                            Visible = true,
                            Name = "Labor"
                        }
                    }.AsQueryable()
                );

            var target = new CheckDisableBll()
            {
                ModuleRepository = moduleRepository
            };

            var actual = target.GetModule("Labor");

            Assert.Equal("Labor", actual.Name);
            Assert.Equal(true, actual.Visible);
        }

        /// <summary>
        ///     Tests if module exist with visibility = false
        /// </summary>
        [Fact]
        public void GetModuleFalseTest()
        {
            var moduleRepository =
                MockHelperBll.GetAllModules(
                    new List<Module>
                    {
                        new Module
                        {
                            Visible = false,
                            Name = "Labor"
                        }
                    }.AsQueryable()
                );

            var target = new CheckDisableBll()
            {
                ModuleRepository = moduleRepository
            };

            var actual = target.GetModule("Labor");

            Assert.Equal("Labor", actual.Name);
            Assert.Equal(false, actual.Visible);
        }

        /// <summary>
        ///     Tests if bll returns null if module doesnt exist
        /// </summary>
        [Fact]
        public void GetModuleNullTest()
        {
            var moduleRepository =
                MockHelperBll.GetAllModules(
                    new List<Module>
                    {
                    }.AsQueryable()
                );

            var target = new CheckDisableBll()
            {
                ModuleRepository = moduleRepository
            };

            var actual = target.GetModule("Labor");

            Assert.Equal(null, actual);
        }
    }
}
