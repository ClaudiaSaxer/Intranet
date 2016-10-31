#region Usings

using System;
using System.Collections.Generic;
using System.Linq;
using Intranet.Common;
using Intranet.Labor.Definition;
using Intranet.Model;
using Moq;

#endregion

namespace Intranet.Labor.TestEnvironment
{
    /// <summary>
    ///     A mock helper class for bll
    /// </summary>
    public static class MockHelperBll
    {
        /// <summary>
        ///     A mock for LaborHomeBll
        /// </summary>
        /// <param name="modules">Modules returned by AllLaborModulesForRoles</param>
        /// <returns></returns>
        public static ILaborHomeBll GetLaborHomeBll( IEnumerable<Module> modules )
        {
            var mock = new Mock<ILaborHomeBll>
            {
                Name = "MockHelper.GetLaborHomeBll",
                DefaultValue = DefaultValue.Mock
            };

            mock.Setup( x => x.AllLaborModulesForRoles( It.IsAny<IEnumerable<String>>() ) )
                .Returns( modules );

            return mock.Object;
        }

        /// <summary>
        ///     A mock for GenericRepository
        /// </summary>
        /// <param name="roles">Queryable Roles returned by GetAll</param>
        /// <returns></returns>
        public static IGenericRepository<Role> GetAllRoles(IQueryable<Role> roles)
        {
            var mock = new Mock<IGenericRepository<Role>>
            {
                Name = "MockHelper.GetAllRoles",
                DefaultValue = DefaultValue.Mock
            };

            mock.Setup(x => x.GetAll())
                .Returns(roles);

            return mock.Object;
        }
    }
}