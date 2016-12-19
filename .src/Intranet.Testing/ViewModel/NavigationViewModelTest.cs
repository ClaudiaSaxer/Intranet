#region Usings

using System.Collections.Generic;
using FluentAssertions;
using Intranet.Model;
using Xunit;

#endregion

namespace Intranet.ViewModel.Test
{
    /// <summary>
    ///     Class representing Tests for NavigationViewModel
    /// </summary>
    public class NavigationViewModelTest
    {
        /// <summary>
        ///     Test Init with nothing
        /// </summary>
        [Fact]
        public void InitTestWithNothing()
        {
            var actual = new NavigationViewModel
            {
                MainModules = new List<Module>(),
                SettingModules = new List<Module>()
            };

            actual.Should()
                  .NotBeNull( "is initialized" );
            actual.MainModules.Should()
                  .NotBeNull( "is initialized" );
            actual.SettingModules.Should()
                  .NotBeNull( "is initialized" );
        }
    }
}