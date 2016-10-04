using Xunit;

namespace Intranet.Bll.Test
{
    public class TestTestUseAutofac
    {
       

        [Fact]
        public void HelloWorldAndNumberTest()
        {
            var target = new TestUseAutofac();
            var hello = target.GetHelloWorldAndNumer( "world", 42 );
            Assert.Equal( "Hello world 42", hello );
        }
    }
}