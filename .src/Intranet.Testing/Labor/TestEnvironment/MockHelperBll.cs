using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Intranet.Labor.Definition.Bll;
using Intranet.Model;
using Moq;

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
        public static ILaborHomeBll GetLaborHomeBll(IEnumerable<Module> modules)
        {
            var mock = new Mock<ILaborHomeBll>
            {
                Name = "MockHelper.GetLaborHomeBll",
                DefaultValue = DefaultValue.Mock
            };

            mock.Setup(x => x.AllLaborModulesForRoles(It.IsAny<IEnumerable<String>>()))
                .Returns(modules);

            return mock.Object;
        }
    }
}
