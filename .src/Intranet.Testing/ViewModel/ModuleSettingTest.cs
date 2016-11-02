using System;
using System.Collections.Generic;
using Extend;
using FluentAssertions;
using Xunit;

namespace Intranet.ViewModel.Test
{
    /// <summary>
    ///     Class representing Tests for ModuleSetting
    /// </summary>
    public class ModuleSettingTest
    {
        /// <summary>
        ///     Initialize Test
        /// </summary>
        [Fact]
        public void InitTest()
        {
            var actual = InstanceCreator
                .CreateInstanceOptions<ModuleSetting>()
                .WithFactory( x => new List<String>( RandomValueEx.GetRandomStrings( 10 ) ) )
                .For( x => x.IsTypeOf<ICollection<String>>() )
                .Complete()
                .CreateInstance();

            actual.Should()
                  .NotBeNull();
        }

        /// <summary>
        ///     Test Properties 1
        /// </summary>
        [Fact]
        public void PropertiesTest1()
        {
            var actual = new ModuleSetting
            {
                Id = 666,
                Name = "the awesome one",
                Visible = true
            };
            actual.Id.Should()
                  .Be( 666 );
            actual.Name.Should()
                  .Be( "the awesome one" );
            actual.Visible.Should()
                  .BeTrue( "because is set true" );
        }

        /// <summary>
        ///     Test Properties 2
        /// </summary>
        [Fact]
        public void PropertiesTest2()
        {
            var actual = new ModuleSetting
            {
                Id = -12,
                Name = "the awesome other",
                Visible = false
            };
            actual.Id.Should()
                  .Be( -12 );
            actual.Name.Should()
                  .Be( "the awesome other" );
            actual.Visible.Should()
                  .BeFalse( "because is set false" );
        }

        /// <summary>
        ///     Test Properties 3
        /// </summary>
        [Fact]
        public void PropertiesTest3()
        {
            var actual = new ModuleSetting
            {
                Id = 0,
                Name = "",
                Visible = false
            };
            actual.Id.Should()
                  .Be( 0 );
            actual.Name.Should()
                  .BeEmpty( "because set empty" );
            actual.Visible.Should()
                  .BeFalse( "because is set false" );
        }

        /// <summary>
        /// Test Properties 4
        /// </summary>
        [Fact]
        public void PropertiesTest4()
        {
            var actual = InstanceCreator
                .CreateInstanceOptions<ModuleSetting>()
                .WithFactory( x => new List<String>( RandomValueEx.GetRandomStrings( 10 ) ) )
                .For( x => x.IsTypeOf<ICollection<String>>() )
                .WithFactory( x => 666 )
                .For( x => x.IsTypeOf<Int32>() )
                .WithFactory( x => "the devil is alive" )
                .For( x => x.IsTypeOf<String>() )
                .WithFactory( x => true )
                .For( x => x.IsTypeOf<Boolean>() )
                .Complete()
                .CreateInstance();

            actual.Should()
                  .NotBeNull( "is initialized" );
            actual.Name.Should()
                  .Be( "the devil is alive" );
            actual.Id.Should()
                  .Be( 666 );
            actual.Visible.Should()
                  .Be( true );
        }
    }
}