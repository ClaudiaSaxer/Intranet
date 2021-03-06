﻿#region Usings

using System;
using System.Collections.Generic;
using Intranet.Common;
using Moq;

#endregion

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
        /// <param name="roles"></param>
        /// <returns></returns>
        public static IRoles GetRoles( IEnumerable<String> roles )
        {
            var mock = new Mock<IRoles>
            {
                Name = "MockHelper.GetRoles",
                DefaultValue = DefaultValue.Mock
            };

            mock.Setup( x => x.GetRolesForUser() )
                .Returns( roles );

            return mock.Object;
        }
    }
}