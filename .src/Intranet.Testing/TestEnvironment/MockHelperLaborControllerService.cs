#region Usings

using System;
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

        /// <summary>
        ///     The Mock for the IBabyDiaperRetentionService
        /// </summary>
        public static IBabyDiaperRetentionService GetBabyDiaperRetentionService(BabyDiaperRetentionEditViewModel viewModel)
        {
            var mock = new Mock<IBabyDiaperRetentionService>
            {
                Name = "MockHelperService.GetBabyDiaperRetentionService",
                DefaultValue = DefaultValue.Mock
            };

            mock.Setup(s => s.GetNewBabyDiapersRetentionEditViewModel(0))
                .Returns((BabyDiaperRetentionEditViewModel)null);
            mock.Setup(s => s.GetNewBabyDiapersRetentionEditViewModel(It.Is<Int32>(x => x > 0)))
                .Returns(viewModel);

            mock.Setup(s => s.GetBabyDiapersRetentionEditViewModel(0))
                .Returns((BabyDiaperRetentionEditViewModel)null);
            mock.Setup(s => s.GetBabyDiapersRetentionEditViewModel(It.Is<Int32>(x => x > 0)))
                .Returns(viewModel);

            return mock.Object;
        }
    }
}