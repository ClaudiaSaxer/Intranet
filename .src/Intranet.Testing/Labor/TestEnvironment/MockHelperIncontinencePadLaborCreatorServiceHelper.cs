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
    public static class MockHelperIncontinencePadLaborCreatorServiceHelper
    {
        /// <summary>
        ///     A mock for LaborCreatorServiceHelper
        /// </summary>
        public static IIncontinencePadLaborCreatorServiceHelper GetLaborCreatorServiceHelper()
        {
            var mock = new Mock<IIncontinencePadLaborCreatorServiceHelper>
            {
                Name = "MockHelper.GetIncontinencePadLaborCreatorServiceHelper",
                DefaultValue = DefaultValue.Mock
            };


      
            mock.Setup( x => x.ToRewetTestValuesCollection( It.IsAny<List<TestValue>>() ) )
                .Returns( new List<IncontinencePadRewetTestValue>() );

            mock.Setup( x => x.ToRewetTestValuesCollection( It.IsAny<List<TestValue>>() ) )
                .Returns( new List<IncontinencePadRewetTestValue>() );

            mock.Setup( x => x.ToRewetAverage( It.IsAny<List<TestValue>>() ) )
                .Returns( new IncontinencePadRewet() );

            mock.Setup( x => x.ToRewetStandardDeviation( It.IsAny<List<TestValue>>() ) )
                .Returns( new IncontinencePadRewet() );

            mock.Setup( x => x.ToRetentionTestValuesCollection( It.IsAny<List<TestValue>>() ) )
                .Returns( new List<IncontinencePadRetentionTestValue>() );

            mock.Setup( x => x.ToRetentionAverage( It.IsAny<List<TestValue>>() ) )
                .Returns( new IncontinencePadRetention() );

            mock.Setup( x => x.ToRetentionStandardDeviation( It.IsAny<List<TestValue>>() ) )
                .Returns( new IncontinencePadRetention() );

            mock.Setup(x => x.ToAcquisitionTimeTestValuesCollection(It.IsAny<List<TestValue>>()))
         .Returns(new List<IncontinencePadAcquisitionTimeTestValue>());

            mock.Setup(x => x.ToAcquisitionTimeAverage(It.IsAny<List<TestValue>>()))
                .Returns(new IncontinencePadAcquisitionTime());

            mock.Setup(x => x.ToAcquisitionTimeStandardDeviation(It.IsAny<List<TestValue>>()))
                .Returns(new IncontinencePadAcquisitionTime());

            mock.Setup(x => x.ToRewetAfterAcquisitionTimeAverage(It.IsAny<List<TestValue>>()))
         .Returns(new IncontinencePadRewet());

            mock.Setup(x => x.ToRewetAfterAcquisitionTimeStandardDeviation(It.IsAny<List<TestValue>>()))
                .Returns(new IncontinencePadRewet());
            return mock.Object;
        }
    }
}