using Extend;
using FluentAssertions;
using Xunit;

namespace Intranet.Model.Test
{
    /// <summary>
    ///     Class representing Module Type Test
    /// </summary>
    public class ModuleTypeTest
    {
        /// <summary>
        ///     Test Main Type
        /// </summary>
        [Fact]
        public void MainTypeTest()
            => ModuleType.Main.ToInt32()
                         .Should()
                         .Be( 0 );

        /// <summary>
        ///     Test Setting Type
        /// </summary>
        [Fact]
        public void SettingTypeTest()
            => ModuleType.Setting.ToInt32()
                         .Should()
                         .Be( 2 );

        /// <summary>
        ///     Test Subt Type
        /// </summary>
        [Fact]
        public void SubTypeTest()
            => ModuleType.Sub.ToInt32()
                         .Should()
                         .Be( 1 );
    }
}