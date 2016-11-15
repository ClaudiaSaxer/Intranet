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
    public static class MockHelperTestServiceHelper
    {
        /// <summary>
        ///     A mock for BabyDiaperServiceHelper
        /// </summary>
        /// <param name="productionCode">productionCode returned by CreateProductionCode</param>
        /// <returns>a ITestServiceHelper moq</returns>
        public static ITestServiceHelper GetTestServiceHelper(String productionCode)
        {
            var mock = new Mock<ITestServiceHelper>
            {
                Name = "MockHelper.GetTestServiceHelper",
                DefaultValue = DefaultValue.Mock
            };

            mock.Setup( x => x.CreateProductionCode( It.IsAny<TestSheet>() ) )
                .Returns(productionCode);
            return mock.Object;
        }

        /// <summary>
        ///     A mock for BabyDiaperServiceHelper
        /// </summary>
        /// <param name="testValue">testValue returned by CreateNewTestValue</param>
        /// <returns>a ITestServiceHelper moq</returns>
        public static ITestServiceHelper GetTestServiceHelperCreateNewTestValue(TestValue testValue)
        {
            var mock = new Mock<ITestServiceHelper>
            {
                Name = "MockHelper.GetTestServiceHelperCreateNewTestValue",
                DefaultValue = DefaultValue.Mock
            };

            mock.Setup(x => x.CreateNewTestValue(It.IsAny<Int32>(),It.IsAny<String>(),It.IsAny<Int32>(),It.IsAny<IList<TestNote>>()))
                .Returns(testValue);
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

        /// <summary>
        ///     A mock for InkoRewetServiceHelper
        /// </summary>
        /// <param name="testValue">testValue returned by update or save new</param>
        /// <returns>a IBabyDiaperRewetServiceHelper moq</returns>
        public static IInkoRewetServiceHelper GetInkoRewetServiceHelper(TestValue testValue)
        {
            var mock = new Mock<IInkoRewetServiceHelper>
            {
                Name = "MockHelper.GetInkoRewetServiceHelper",
                DefaultValue = DefaultValue.Mock
            };

            mock.Setup(x => x.UpdateRewetTest(It.IsAny<InkoRewetEditViewModel>()))
                .Returns(testValue);
            mock.Setup(x => x.SaveNewRewetTest(It.IsAny<InkoRewetEditViewModel>()))
                .Returns(testValue);

            mock.Setup(x => x.UpdateRewetAverageAndStv(It.IsAny<Int32>()))
                .Returns(new TestSheet());

            return mock.Object;
        }

        /// <summary>
        ///     A mock for InkoRewetServiceHelper
        /// </summary>
        /// <param name="testValue">testValue returned by update or save new</param>
        /// <returns>a IBabyDiaperRewetServiceHelper moq</returns>
        public static IInkoRetentionServiceHelper GetInkoRetentionServiceHelper(TestValue testValue)
        {
            var mock = new Mock<IInkoRetentionServiceHelper>
            {
                Name = "MockHelper.GetInkoRetentionServiceHelper",
                DefaultValue = DefaultValue.Mock
            };

            mock.Setup(x => x.UpdateRetentionTest(It.IsAny<InkoRetentionEditViewModel>()))
                .Returns(testValue);
            mock.Setup(x => x.SaveNewRetentionTest(It.IsAny<InkoRetentionEditViewModel>()))
                .Returns(testValue);

            mock.Setup(x => x.UpdateRetentionAverageAndStv(It.IsAny<Int32>()))
                .Returns(new TestSheet());

            return mock.Object;
        }
    }
}
