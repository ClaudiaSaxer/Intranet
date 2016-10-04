using Autofac.Extras.Moq;
using Intranet.Definition;
using Xunit;

namespace Intranet.Bll.Test
{
    public class TestTestUseAutofac
    {
        [Fact]
        public void HelloWorldAndNumberTest()
        {
            using ( var mock = AutoMock.GetLoose() )
            {
                // Arrange - configure the mock
                mock.Mock<ITestAutofac>()
                    .Setup( x => x.GetHelloWorld( "world" ) )
                    .Returns( "Hello world" );
                var sut = mock.Create<ITestAutofac>();
                var target = new TestUseAutofac { TestAutofac = sut };
                // Act

                var hello = target.GetHelloWorldAndNumer( "world", 42 );

                // Assert - assert on the mock
                mock.Mock<ITestAutofac>()
                    .Verify( x => x.GetHelloWorld( "world" ) );
                Assert.Equal( "Hello world 42", hello );
            }
        }
    }
}