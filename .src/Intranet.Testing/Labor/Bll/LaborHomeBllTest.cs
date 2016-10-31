using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Intranet.Common;
using Intranet.Labor.Bll;
using Intranet.Model;
using Intranet.TestEnvironment;
using Xunit;
using MockHelperBll = Intranet.Labor.TestEnvironment.MockHelperBll;

namespace Intranet.Labor.Bll.Test
{
    /// <summary>
    ///     Test Class for LaborHomeBll
    /// </summary>
    public class LaborHomeBllTest
    {
        /// <summary>
        ///     Normal Passing Test with no matching module and role
        /// </summary>
        [Fact]
        public void AllLaborModulesForRolesNoMatchTest()
        {
            var rolesListQuery = new List<Role>();
            var genericRepo =
                MockHelperBll.GetAllRoles(
                    rolesListQuery.AsQueryable()
                );


            var target = new LaborHomeBll()
            {
                RoleRepository = genericRepo
            };

            var rolenamesList = new List<String>
            {
                "TestRole"
            };

            var actual = target.AllLaborModulesForRoles(rolenamesList);

            Assert.Equal(0, actual.ToList().Count);
        }


        /// <summary>
        ///     Normal Passing Test with one matching module and role
        /// </summary>
        [Fact]
        public void AllLaborModulesForRolesOneMatchTest()
        {
            var roleItem = new Role();
            roleItem.Name = "TestRole";
            roleItem.Modules = new List<Module>
            {
                new Module {Type = ModuleType.Sub, AreaName = "Labor", Roles = new List<Role> {roleItem} }
            };
            var rolesListQuery = new List<Role>
            {
                roleItem
            };
            var genericRepo =
                MockHelperBll.GetAllRoles(
                    rolesListQuery.AsQueryable()
                );


            var target = new LaborHomeBll()
            {
                RoleRepository = genericRepo
            };

            var rolenamesList = new List<String>
            {
                "TestRole"
            };

            var actual = target.AllLaborModulesForRoles( rolenamesList );

            Assert.Equal(1, actual.ToList().Count);
        }

        /// <summary>
        ///     Normal Passing Test with Matching role but no Matching Submodule
        /// </summary>
        [Fact]
        public void AllLaborModulesForRolesMatchingRoleButNotSubmoduleTest()
        {
            var roleItem = new Role();
            roleItem.Name = "TestRole";
            roleItem.Modules = new List<Module>
            {
                new Module {Type = ModuleType.Sub, AreaName = "NotLabor", Roles = new List<Role> {roleItem} }
            };
            var rolesListQuery = new List<Role>
            {
                roleItem
            };
            var genericRepo =
                MockHelperBll.GetAllRoles(
                    rolesListQuery.AsQueryable()
                );


            var target = new LaborHomeBll()
            {
                RoleRepository = genericRepo
            };

            var rolenamesList = new List<String>
            {
                "TestRole"
            };

            var actual = target.AllLaborModulesForRoles(rolenamesList);

            Assert.Equal(0, actual.ToList().Count);
        }

        /// <summary>
        ///     Normal Passing Test with Matching Submodule but no Matching Role
        /// </summary>
        [Fact]
        public void AllLaborModulesForRolesMatchingSubmoduleNotRoleTest()
        {
            var roleItem = new Role();
            roleItem.Name = "WrongRole";
            roleItem.Modules = new List<Module>
            {
                new Module {Type = ModuleType.Sub, AreaName = "Labor", Roles = new List<Role> {roleItem} }
            };
            var rolesListQuery = new List<Role>
            {
                roleItem
            };
            var genericRepo =
                MockHelperBll.GetAllRoles(
                    rolesListQuery.AsQueryable()
                );


            var target = new LaborHomeBll()
            {
                RoleRepository = genericRepo
            };

            var rolenamesList = new List<String>
            {
                "TestRole"
            };

            var actual = target.AllLaborModulesForRoles(rolenamesList);

            Assert.Equal(0, actual.ToList().Count);
        }
    }
}
