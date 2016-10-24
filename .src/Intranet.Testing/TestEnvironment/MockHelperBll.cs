using System;
using System.Collections.Generic;
using Intranet.Common;
using Intranet.Model;
using Moq;

namespace Intranet.TestEnvironment
{
    /// <summary>
    ///     A mock helper class for bll
    /// </summary>
    public static class MockHelperBll
    {
        /// <summary>
        ///     A mock for HomeBll
        /// </summary>
        /// <param name="getHomeBllFunc">Func for HomeBll</param>
        /// <param name="getHomeBllCallback">Callback for HomeBll</param>
        /// <returns></returns>
        public static IHomeBll GetHomeBll( Func<IEnumerable<Module>, IEnumerable<Module>> getHomeBllFunc = null,Action<IEnumerable<Module>> getHomeBllCallback = null )
        {
            var mock = new Mock<IHomeBll>
            {
                Name = "MockHelper.GetHomeBll",
                DefaultValue = DefaultValue.Mock
            };

            mock.Setup( x => x.AllVisibleModulesForRoles( It.IsAny<IEnumerable<String>>() ) )
                .Returns( ( IEnumerable<Module> modules ) => getHomeBllFunc?.Invoke(modules) )
                .Callback( ( IEnumerable<Module> modules) => getHomeBllCallback?.Invoke(modules) );

            return mock.Object;
        }
    }
}