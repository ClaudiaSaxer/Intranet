using Xunit;

namespace Intranet.Bll.Test
{
    /// <summary>
    ///     Test the file Test Autofac
    ///     to be removed
    /// </summary>
    public class TestTestAutofac
    {
        /// <summary>
        ///     A Test for HelloWorld
        /// </summary>
        [Fact]
        public void HelloWorldTest()
        {
            var target = new TestAutofac();

            var hello = target.GetHelloWorld( "world" );
            Assert.Equal( "Hello world", hello );
        }
    }
}