#region Usings

using System.Collections.Generic;
using FluentAssertions;
using Xunit;

#endregion

namespace Intranet.Model.Test
{
    /// <summary>
    ///     Class representing Role Test
    /// </summary>
    public class RoleTest
    {
        /// <summary>
        ///     Sets and gets all Properties
        /// </summary>
        [Fact]
        public void SetPropertiesTest()
        {
            var Role = new Role
            {
                Modules = new List<Module>(),
                Name = "Frankenstein",
                RoleId = 666
            };
            Role.Name.Should()
                .Be( "Frankenstein" );
            Role.RoleId.Should()
                .Be( 666 );
            Role.Modules.Should()
                .NotBeNull( "is initialized" );
        }
    }
}