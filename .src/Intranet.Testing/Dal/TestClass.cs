using System;
using Xunit;

namespace Intranet.Dal.Test
{
    /// <summary>
    /// to be removed
    /// test class
    /// </summary>
    public class TestClass
    {
        /// <summary>
        /// A passing test
        /// </summary>
        [Fact]
        public void PassingTest() => Assert.Equal( 4, Add( 2, 2 ) );

        private Int32 Add( Int32 x, Int32 y ) => x + y;
    }
}