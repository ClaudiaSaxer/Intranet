using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;

namespace Intranet.Model.Test
{
    /// <summary>
    /// Class representing Module Test
    /// </summary>
    public class ModuleTest
    {
        /// <summary>
        ///     Sets and gets all Properties
        /// </summary>
        [Fact]
        public void SetPropertiesTest()
        {

            var module = new Module()
            {
                Visible = true,Name = "Heidi",Roles = new List<Role>(),Type = ModuleType.Main,ModuleId = 666,ControllerName = "thecontroller",ActionName = "theaction",Submodules = new List<Module>(),AreaName = "thearea",Description = "blabla blub"
            };
            module.Should()
                  .NotBeNull( "is initialized" );
            module.Name.Should()
                  .Be( "Heidi" );
            module.Roles.Should()
                  .NotBeNull( "is initialized" );
            module.ActionName.Should()
                  .Be( "theaction" );
            module.AreaName.Should()
                  .Be( "thearea" );
            module.ControllerName.Should()
                  .Be( "thecontroller" );
            module.Description.Should()
                  .Be( "blabla blub" );
            module.Submodules.Should()
                  .NotBeNull( "is initialized" );
            module.Visible.Should()
                  .BeTrue( "is set true" );
            module.Type.Should()
                  .Be( ModuleType.Main );
            module.ModuleId.Should()
                  .Be( 666 );

        }
    }
}
