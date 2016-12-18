#region Usings

using System.Collections.Generic;
using System.Linq;
using Extend;
using FluentAssertions;
using Xunit;

#endregion

namespace Intranet.ViewModel.Test
{
    /// <summary>
    ///     Class representing Tests for SettingsViewModel
    /// </summary>
    public class SettingsViewModelTest
    {
        /// <summary>
        ///     Initialize Test
        /// </summary>
        [Fact]
        public void InitTest()
        {
            var actual = InstanceCreator
                .CreateInstanceOptions<SettingsViewModel>()
                .Complete()
                .CreateInstance();

            actual.Should()
                  .NotBeNull();
        }

        /// <summary>
        ///     Test Properties 1
        /// </summary>
        [Fact]
        public void PropertiesTestEmpty()
        {
            var actual = new SettingsViewModel
            {
                ModuleSettings = new List<ModuleSetting>()
            };
            actual.Should()
                  .NotBeNull( "is initialized" );
            actual.ModuleSettings.Should()
                  .NotBeNull( "is initialized" );
        }

        /// <summary>
        ///     Test Properties 1
        /// </summary>
        [Fact]
        public void PropertiesTestNotEmpty()
        {
            var actual = new SettingsViewModel
            {
                ModuleSettings = new List<ModuleSetting>
                {
                    new ModuleSetting
                    {
                        Visible = true,
                        Id = 666,
                        Name = "monsters are real"
                    }
                }
            };
            actual.Should()
                  .NotBeNull( "is initialized" );
            actual.ModuleSettings.Should()
                  .NotBeNull( "is initialized" );

            actual.Should()
                  .NotBeNull( "is initialized" );

            actual.ModuleSettings.Should()
                  .NotBeNull( "is initialized" );

            actual.ModuleSettings.ToList()[0]
                  .Id.Should()
                  .Be( 666 );
            actual.ModuleSettings.ToList()[0]
                  .Name.Should()
                  .Be( "monsters are real" );
            actual.ModuleSettings.ToList()[0]
                  .Visible.Should()
                  .BeTrue();
        }
    }
}