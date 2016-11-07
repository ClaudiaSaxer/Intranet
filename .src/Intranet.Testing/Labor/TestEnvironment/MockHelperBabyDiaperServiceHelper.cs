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
        /// <param name="testValue">testValue returned by update or save new</param>
        /// <returns>a IBabyDiaperServiceHelper moq</returns>
        public static IBabyDiaperServiceHelper GetBabyDiaperServiceHelper(TestValue testValue)
        {
            var mock = new Mock<IBabyDiaperServiceHelper>
            {
                Name = "MockHelper.GetBabyDiaperServiceHelper",
                DefaultValue = DefaultValue.Mock
            };

            mock.Setup(x => x.UpdateRetentionTest(It.IsAny<BabyDiaperRetentionEditViewModel>()))
                .Returns(testValue);
            mock.Setup(x => x.SaveNewRetentionTest(It.IsAny<BabyDiaperRetentionEditViewModel>()))
                .Returns(testValue);

            mock.Setup( x => x.UpdateAverageAndStv( It.IsAny<Int32>() ) )
                .Returns( new TestSheet() );

            return mock.Object;
        }
    }
}
