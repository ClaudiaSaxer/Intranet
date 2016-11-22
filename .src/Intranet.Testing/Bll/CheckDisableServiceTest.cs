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
    ///     Test Class for CheckDisableService
    /// </summary>
    public class CheckDisableServiceTest
    {
        /// <summary>
        ///     Tests if IsVisbile returns true if module is visible
        /// </summary>
        [Fact]
        public void IsVisibleTrueTest()
        {
            var checkDisableBll =
                MockHelperBll.GetCheckDisableBll(
                    new Module { Name = "Labor", Visible = true }
                );

            var target = new CheckDisableService()
            {
                CheckDisableBll = checkDisableBll
            };

            var actual = target.IsVisible("Labor");

            Assert.True(actual);
        }

        /// <summary>
        ///     Tests if IsVisbile returns false if module is not visible
        /// </summary>
        [Fact]
        public void IsVisibleFalseTest()
        {
            var checkDisableBll =
                MockHelperBll.GetCheckDisableBll(
                    new Module { Name = "Labor", Visible = false }
                );

            var target = new CheckDisableService()
            {
                CheckDisableBll = checkDisableBll
            };

            var actual = target.IsVisible("Labor");

            Assert.False(actual);
        }

        /// <summary>
        ///     Tests if IsVisbile returns false if module does not exist
        /// </summary>
        [Fact]
        public void IsVisibleFalseNoModuleTest()
        {
            var checkDisableBll =
                MockHelperBll.GetCheckDisableBll(
                    null
                );

            var target = new CheckDisableService()
            {
                CheckDisableBll = checkDisableBll
            };

            var actual = target.IsVisible("Labor");

            Assert.False(actual);
        }
    }
}
