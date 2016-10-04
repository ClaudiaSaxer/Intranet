using Xunit;

namespace Intranet.Bll.Test
{
    public class TestTestAutofac
    {
   
        [Fact]
        public void HelloWorldTest()
        { 


            var target = new TestAutofac();

            var hello = target.GetHelloWorld( "world" );
            Assert.Equal( "Hello world", hello );
        }
    }
}