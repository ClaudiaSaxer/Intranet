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
        /// <param name="getRolesFunc">func for roles</param>
        /// <param name="getRolesCallback">callback for roles</param>
        /// <returns></returns>
        public static IRoles GetRoles(
            Func<IEnumerable<String>, IEnumerable<String>> getRolesFunc = null,
            Action<IEnumerable<String>> getRolesCallback = null )
        {
            var mock = new Mock<IRoles>
            {
                Name = "MockHelper.GetRoles",
                DefaultValue = DefaultValue.Mock
            };

            mock.Setup( x => x.GetRolesForUser() )
                .Returns( ( IEnumerable<String> roles ) => getRolesFunc?.Invoke( roles ) )
                .Callback( ( IEnumerable<String> roles ) => getRolesCallback?.Invoke( roles ) );

            return mock.Object;
        }
    }
}