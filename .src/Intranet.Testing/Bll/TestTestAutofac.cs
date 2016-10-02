using Xunit;

namespace Intranet.Bll.Test
{
    public class TestTestAutofac
    {
        #region Fields

        private readonly TestAutofac _autofac;

        #endregion

        #region Ctor

        public TestTestAutofac()
        {
            _autofac = new TestAutofac();
        }

        #endregion

        [Fact]
        public void HelloWorldTest()
        {
            var hello = _autofac.GetHelloWorld( "world" );
            Assert.Equal( "Hello world", hello );
        }
    }
}