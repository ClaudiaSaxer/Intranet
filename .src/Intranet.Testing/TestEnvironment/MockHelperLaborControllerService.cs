#region Usings

using Intranet.Labor.Definition;
using Intranet.Labor.ViewModel;
using Moq;

#endregion

namespace Intranet.TestEnvironment
{
    /// <summary>
    ///     A mock helper class for Labor Controller Services
    /// </summary>
    public class MockHelperLaborControllerService
    {
        /// <summary>
        ///     The Mock for the HomeService
        /// </summary>
        public static ILaborHomeService GetLaborHomeService( LaborHomeViewModel viewModel )
        {
            var mock = new Mock<ILaborHomeService>
            {
                Name = "MockHelperService.GetLaborHomeService",
                DefaultValue = DefaultValue.Mock
            };

            mock.Setup( laborHomeService => laborHomeService.GetLaborHomeViewModel() )
                .Returns( viewModel );

            return mock.Object;
        }
    }
}