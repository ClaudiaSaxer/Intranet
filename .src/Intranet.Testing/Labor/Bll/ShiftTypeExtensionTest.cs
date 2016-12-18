#region Usings

using FluentAssertions;
using Intranet.Labor.Model;
using Xunit;

#endregion

namespace Intranet.Labor.Bll.Test
{
    /// <summary>
    ///     Class representing Test for ShiftTypeExtension
    /// </summary>
    public class ShiftTypeExtensionTest
    {
        /// <summary>
        ///     Test ToFriendlyString
        /// </summary>
        [Fact]
        public void ToFriendlyStringTest()
        {
            ShiftType.Night.ToFriendlyString()
                     .Should()
                     .Be( "Nacht" );

            ShiftType.Late.ToFriendlyString()
                     .Should()
                     .Be( "Spät" );

            ShiftType.Morning.ToFriendlyString()
                     .Should()
                     .Be( "Morgen" );
        }
    }
}