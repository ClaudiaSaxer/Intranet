#region Usings

using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Intranet.Model;
using Xunit;

#endregion

namespace Intranet.ViewModel.Test
{
    /// <summary>
    ///     Class representing Tests for HomeViewModel
    /// </summary>
    public class HomeViewModelTest
    {
        /// <summary>
        ///     Test Init with nothing
        /// </summary>
        [Fact]
        public void InitTestWithNothing()
        {
            var actual = new HomeViewModel
            {
                Modules = new List<Module>()
            };

            actual.Should()
                  .NotBeNull( "is initialized" );
            actual.Modules.Should()
                  .NotBeNull( "is initialized" );
        }

        /// <summary>
        ///     Test Init with nothing
        /// </summary>
        [Fact]
        public void InitTestWithOneEmptyModule()
        {
            var actual = new HomeViewModel
            {
                Modules = new List<Module>
                {
                    new Module()
                }
            };

            actual.Should()
                  .NotBeNull( "is initialized" );
            actual.Modules.Should()
                  .NotBeNull( "is initialized" );
            actual.Modules.ToList()
                  .Count.Should()
                  .Be( 1 );
            actual.Modules.ToList()[0]
                  .Should()
                  .NotBeNull( "is initialized" );
        }
    }
}