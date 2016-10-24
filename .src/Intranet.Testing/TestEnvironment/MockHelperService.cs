using Intranet.Definition;
using Intranet.Definition.Bll;
using Intranet.ViewModel;
using Moq;

namespace Intranet.TestEnvironment
{
    /// <summary>
    ///     A mock helper class for Services
    /// </summary>
    public static class MockHelperService
    {
        /// <summary>
        ///     The Mock for the HomeService
        /// </summary>
        public static IHomeService GetHomeService(HomeViewModel homeViewModel)
        {
            var mock = new Mock<IHomeService>
            {
                Name = "MockHelperService.GetHomeService",
                DefaultValue = DefaultValue.Mock
            };

            mock.Setup( homeService => homeService.GetHomeViewModel() )
                .Returns(homeViewModel);

            return mock.Object;
        }
    }
}
