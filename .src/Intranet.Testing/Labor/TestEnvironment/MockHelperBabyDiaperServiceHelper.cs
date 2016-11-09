using System;
using System.Collections.Generic;
using Intranet.Labor.Definition;
using Intranet.Labor.Model.labor;
using Intranet.Labor.ViewModel;
using Moq;

namespace Intranet.Labor.TestEnvironment
{
    /// <summary>
    ///     A mock helper class for BabyDiaperServiceHelper
    /// </summary>
    public static class MockHelperBabyDiaperServiceHelper
    {
        /// <summary>
        ///     A mock for BabyDiaperServiceHelper
        /// </summary>
        /// <param name="productionCode">productionCode returned by CreateProductionCode</param>
        /// <returns>a IBabyDiaperServiceHelper moq</returns>
        public static IBabyDiaperServiceHelper GetBabyDiaperServiceHelper(String productionCode)
        {
            var mock = new Mock<IBabyDiaperServiceHelper>
            {
                Name = "MockHelper.GetBabyDiaperServiceHelper",
                DefaultValue = DefaultValue.Mock
            };

            mock.Setup( x => x.CreateProductionCode( It.IsAny<TestSheet>() ) )
                .Returns(productionCode);
            return mock.Object;
        }

        /// <summary>
        ///     A mock for BabyDiaperRetentionServiceHelper
        /// </summary>
        /// <param name="testValue">testValue returned by update or save new</param>
        /// <returns>a IBabyDiaperRetentionServiceHelper moq</returns>
        public static IBabyDiaperRetentionServiceHelper GetBabyDiaperRetentionServiceHelper(TestValue testValue)
        {
            var mock = new Mock<IBabyDiaperRetentionServiceHelper>
            {
                Name = "MockHelper.GetBabyDiaperRetentionServiceHelper",
                DefaultValue = DefaultValue.Mock
            };

            mock.Setup(x => x.UpdateRetentionTest(It.IsAny<BabyDiaperRetentionEditViewModel>()))
                .Returns(testValue);
            mock.Setup(x => x.SaveNewRetentionTest(It.IsAny<BabyDiaperRetentionEditViewModel>()))
                .Returns(testValue);

            mock.Setup(x => x.UpdateRetentionAverageAndStv(It.IsAny<Int32>()))
                .Returns(new TestSheet());

            return mock.Object;
        }

        /// <summary>
        ///     A mock for BabyDiaperRewetServiceHelper
        /// </summary>
        /// <param name="testValue">testValue returned by update or save new</param>
        /// <returns>a IBabyDiaperRewetServiceHelper moq</returns>
        public static IBabyDiaperRewetServiceHelper GetBabyDiaperRewetServiceHelper(TestValue testValue)
        {
            var mock = new Mock<IBabyDiaperRewetServiceHelper>
            {
                Name = "MockHelper.GetBabyDiaperRewetServiceHelper",
                DefaultValue = DefaultValue.Mock
            };

            mock.Setup(x => x.UpdateRewetTest(It.IsAny<BabyDiaperRewetEditViewModel>()))
                .Returns(testValue);
            mock.Setup(x => x.SaveNewRewetTest(It.IsAny<BabyDiaperRewetEditViewModel>()))
                .Returns(testValue);

            mock.Setup(x => x.UpdateRewetAverageAndStv(It.IsAny<Int32>()))
                .Returns(new TestSheet());

            return mock.Object;
        }
    }
}
