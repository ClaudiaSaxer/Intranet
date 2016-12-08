#region Usings

using System;
using Intranet.Labor.Definition;
using Moq;

#endregion

namespace Intranet.Labor.TestEnvironment
{
    /// <summary>
    ///     A mock helper class for labor creator service helper
    /// </summary>
    public static class MockHelperLaborCreatorServiceHelper
    {
        /// <summary>
        ///     A mock for LaborCreatorServiceHelper
        /// </summary>
        /// <param name="prodCode"></param>
        /// <returns>a moq for laborcreatorbll</returns>
        public static ILaborCreatorServiceHelper GetLaborCreatorServiceHelper( String prodCode )
        {
            var mock = new Mock<ILaborCreatorServiceHelper>
            {
                Name = "MockHelper.ILaborCreatorServiceHelper",
                DefaultValue = DefaultValue.Mock
            };

            mock.Setup( x => x.GenerateProdCode( It.IsAny<String>(), It.IsAny<Int32>(), It.IsAny<Int32>(), It.IsAny<TimeSpan>() ) )
                .Returns( prodCode );

            return mock.Object;
        }
    }
}