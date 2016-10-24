using System;
using System.Collections.Generic;
using Intranet.Common;
using Moq;

namespace Intranet.TestEnvironment
{
    /// <summary>
    ///     A mock helper class for bll
    /// </summary>
    public static class MockHelperRoles
    {
        /// <summary>
        ///     Mock for Service Base
        /// </summary>
        /// <param name="getServiceBaseFunc">func for servicebase</param>
        /// <param name="getServiceBaseCallback">callback for servicebase</param>
        /// <returns></returns>
        public static IRoles GetRoles(
            Func<IEnumerable<String>, IEnumerable<String>> getServiceBaseFunc = null,
            Action<IEnumerable<String>> getServiceBaseCallback = null )
        {
            var mock = new Mock<IRoles>
            {
                Name = "MockHelper.GetServiceBase",
                DefaultValue = DefaultValue.Mock
            };

            mock.Setup( x => x.GetRolesForUser() )
                .Returns( ( IEnumerable<String> roles ) => getServiceBaseFunc?.Invoke( roles ) )
                .Callback( ( IEnumerable<String> roles ) => getServiceBaseCallback?.Invoke( roles ) );

            return mock.Object;
        }
    }
}