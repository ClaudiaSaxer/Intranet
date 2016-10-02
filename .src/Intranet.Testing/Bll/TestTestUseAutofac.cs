using Xunit;

namespace Intranet.Bll.Test
{
    public class TestTestUseAutofac
    {
        //#region Fields

        //private readonly TestUseAutofac _useautofac;

        //#endregion

        //#region Ctor

        //public TestTestUseAutofac()
        //{
        //    _useautofac = new TestUseAutofac();
        //}

        //#endregion

        [Fact]
        public void HelloWorldAndNumberTest()
        {
            var target = new TestUseAutofac();
            var hello = target.GetHelloWorldAndNumer( "world", 42 );
            Assert.Equal( "Hello world 42", hello );
        }
    }
}