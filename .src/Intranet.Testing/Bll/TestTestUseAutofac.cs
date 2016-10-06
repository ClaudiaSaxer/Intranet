using System;
using FluentAssertions;
using Intranet.Definition;
using Moq;
using Xunit;

namespace Intranet.Bll.Test
{
    /// <summary>
    /// to be removed
    /// tests autofac
    /// </summary>
    public class TestTestUseAutofac
    {
        /// <summary>
        /// A test 
        /// to be removed
        /// </summary>
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

    /// <summary>
    /// A mock helper class
    /// </summary>
    public static class MockHelper
    {
        /// <summary>
        /// A mock
        /// </summary>
        /// <param name="getHelloWorldFunc"></param>
        /// <param name="getHelloWorldCallback"></param>
        /// <returns></returns>
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