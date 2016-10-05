using System;
using FluentAssertions;
using Intranet.Definition;
using Moq;
using Xunit;

namespace Intranet.Bll.Test
{
    public class TestTestUseAutofac
    {
        [Fact]
        public void HelloWorldAndNumberTest()
        {
            var invoked = false;
            var testAutofac = MockHelper.GetTestAutofac( x => "Hello world", x => invoked = true );
            var target = new TestUseAutofac
            {
                TestAutofac = testAutofac
            };

            var actual = target.GetHelloWorldAndNumer( "world", 42 );

            invoked.Should()
                   .BeTrue( "My class should call this method" );
            actual
                .Should()
                .Be( "Hello world 42" );
        }
    }

    public static class MockHelper
    {
        public static ITestAutofac GetTestAutofac(
            Func<String, String> getHelloWorldFunc = null,
            Action<String> getHelloWorldCallback = null )
        {
            var mock = new Mock<ITestAutofac>
            {
                Name = "MockHelper.GetTestAutofac",
                DefaultValue = DefaultValue.Mock
            };

            mock
                .Setup( x => x.GetHelloWorld( It.IsAny<String>() ) )
                .Returns( ( String s ) => getHelloWorldFunc?.Invoke( s ) )
                .Callback( ( String s ) => getHelloWorldCallback?.Invoke( s ) );

            return mock.Object;
        }
    }
}