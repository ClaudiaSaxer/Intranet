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
    public static class MockHelperLaborCreatorServiceHelper
    {
        /// <summary>
        ///     A mock for LaborCreatorServiceHelper
        /// </summary>
        public static ILaborCreatorServiceHelper GetLaborCreatorServiceHelper()
        {
            var mock = new Mock<ILaborCreatorServiceHelper>
            {
                Name = "MockHelper.GetLaborCreatorServiceHelper",
                DefaultValue = DefaultValue.Mock
            };

            mock.Setup( x => x.ToRewetTestValuesCollection( It.IsAny<List<TestValue>>() ) )
                .Returns( new List<RewetTestValue>() );

            mock.Setup( x => x.ToRewetTestValuesCollection( It.IsAny<List<TestValue>>() ) )
                .Returns( new List<RewetTestValue>() );

            mock.Setup( x => x.ToRewetAverage( It.IsAny<List<TestValue>>() ) )
                .Returns( new Rewet() );

            mock.Setup( x => x.ToRewetStandardDeviation( It.IsAny<List<TestValue>>() ) )
                .Returns( new Rewet() );

            mock.Setup( x => x.ToRetentionTestValuesCollection( It.IsAny<List<TestValue>>() ) )
                .Returns( new List<RetentionTestValue>() );

            mock.Setup( x => x.ToRetentionAverage( It.IsAny<List<TestValue>>() ) )
                .Returns( new Retention() );

            mock.Setup( x => x.ToRetentionStandardDeviation( It.IsAny<List<TestValue>>() ) )
                .Returns( new Retention() );

            mock.Setup(x => x.ToPenetrationTimeTestValuesCollection(It.IsAny<List<TestValue>>()))
         .Returns(new List<PenetrationTimeTestValue>());

            mock.Setup(x => x.ToPenetrationTimeAverage(It.IsAny<List<TestValue>>()))
                .Returns(new PenetrationTime());

            mock.Setup(x => x.ToPenetrationTimeStandardDeviation(It.IsAny<List<TestValue>>()))
                .Returns(new PenetrationTime());

            return mock.Object;
        }
    }
}