#region Usings

using System.Collections.Generic;
using Intranet.Labor.Definition;
using Intranet.Labor.Model.labor;
using Intranet.Labor.ViewModel;
using Moq;

#endregion

namespace Intranet.Labor.TestEnvironment
{
    /// <summary>
    ///     A mock helper class for LaborCreatorServiceHelper
    /// </summary>
    public static class MockHelperBabyDiaperLaborCreatorServiceHelper
    {
        /// <summary>
        ///     A mock for LaborCreatorServiceHelper
        /// </summary>
        public static IBabyDiaperLaborCreatorServiceHelper GetLaborCreatorServiceHelper()
        {
            var mock = new Mock<IBabyDiaperLaborCreatorServiceHelper>
            {
                Name = "MockHelper.GetBabyDiaperLaborCreatorServiceHelper",
                DefaultValue = DefaultValue.Mock
            };

            mock.Setup( x => x.ToRewetTestValuesCollection( It.IsAny<List<TestValue>>() ) )
                .Returns( new List<BabyDiaperRewetTestValue>() );

            mock.Setup( x => x.ToRewetTestValuesCollection( It.IsAny<List<TestValue>>() ) )
                .Returns( new List<BabyDiaperRewetTestValue>() );

            mock.Setup( x => x.ToRewetAverage( It.IsAny<List<TestValue>>() ) )
                .Returns( new BabyDiaperRewet() );

            mock.Setup( x => x.ToRewetStandardDeviation( It.IsAny<List<TestValue>>() ) )
                .Returns( new BabyDiaperRewet() );

            mock.Setup( x => x.ToRetentionTestValuesCollection( It.IsAny<List<TestValue>>() ) )
                .Returns( new List<BabyDiaperRetentionTestValue>() );

            mock.Setup( x => x.ToRetentionAverage( It.IsAny<List<TestValue>>() ) )
                .Returns( new BabyDiaperRetention() );

            mock.Setup( x => x.ToRetentionStandardDeviation( It.IsAny<List<TestValue>>() ) )
                .Returns( new BabyDiaperRetention() );

            mock.Setup(x => x.ToPenetrationTimeTestValuesCollection(It.IsAny<List<TestValue>>()))
         .Returns(new List<BabyDiaperPenetrationTimeTestValue>());

            mock.Setup(x => x.ToPenetrationTimeAverage(It.IsAny<List<TestValue>>()))
                .Returns(new BabyDiaperPenetrationTime());

            mock.Setup(x => x.ToPenetrationTimeStandardDeviation(It.IsAny<List<TestValue>>()))
                .Returns(new BabyDiaperPenetrationTime());

            return mock.Object;
        }
    }
}