#region Usings

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using Intranet.Common;
using Intranet.Labor.Definition;
using Intranet.Labor.Model.labor;
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
        /// <returns>a IloaborHomebll moq</returns>
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
        /// <returns>a generic reposisory for roles</returns>
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

        /// <summary>
        ///     A mock for LaborCreatorBll
        /// </summary>
        /// <param name="testSheet">the testsheet</param>
        /// <returns>a moq for laborcreatorbll</returns>
        public static ILaborCreatorBll GetLaborCreatorBll(TestSheet testSheet)
        {
            var mock = new Mock<ILaborCreatorBll>
            {
                Name = "MockHelper.ILaborCreatorBll",
                DefaultValue = DefaultValue.Mock
            };

            mock.Setup(x => x.getTestSheetForId(It.IsAny<Int32>()))
                .Returns(testSheet);

            return mock.Object;
        }

        /// <summary>
        ///     A mock for BabyDiaperRetentionBll
        /// </summary>
        /// <param name="testSheet">testSheet data which would be in the db</param>
        /// <returns>a IBabyDiaperRetentionBll moq</returns>
        public static IBabyDiaperRetentionBll GetBabyDiaperRetentionBll(TestSheet testSheet)
        {
            var mock = new Mock<IBabyDiaperRetentionBll>
            {
                Name = "MockHelper.GetBabyDiaperRetentionBll",
                DefaultValue = DefaultValue.Mock
            };

            mock.Setup(x => x.GetTestSheetInfo(testSheet.TestSheetId))
                .Returns(testSheet);
            mock.Setup(x => x.GetTestSheetInfo(It.IsNotIn(testSheet.TestSheetId)))
                .Returns((TestSheet) null);

            return mock.Object;
        }

    }
}