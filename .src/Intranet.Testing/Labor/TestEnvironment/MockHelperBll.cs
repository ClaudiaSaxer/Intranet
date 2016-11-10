#region Usings

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using Castle.Core.Internal;
using Intranet.Common;
using Intranet.Labor.Definition;
using Intranet.Labor.Model;
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
        public static ILaborHome GetLaborHomeBll( IEnumerable<Module> modules )
        {
            var mock = new Mock<ILaborHome>
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
        public static IBabyDiaperLaborCreatorBll GetLaborCreatorBll(TestSheet testSheet)
        {
            var mock = new Mock<IBabyDiaperLaborCreatorBll>
            {
                Name = "MockHelper.IBabyDiaperLaborCreatorBll",
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
        /// <returns>a IBabyDiaperBll moq</returns>
        public static IBabyDiaperBll GetBabyDiaperBll(TestSheet testSheet)
        {
            var mock = new Mock<IBabyDiaperBll>
            {
                Name = "MockHelper.GetBabyDiaperBll",
                DefaultValue = DefaultValue.Mock
            };

            var noteCodes = new List<Error>
            {
                new Error {ErrorId = 1, ErrorCode = "404", Value = "Error Not Found"},
                new Error {ErrorId = 2, ErrorCode = "500", Value = "Error not authorized"}
            };

            mock.Setup(x => x.GetAllNoteCodes()).Returns(noteCodes);

            mock.Setup(x => x.GetTestSheetInfo(testSheet.TestSheetId))
                .Returns(testSheet);
            mock.Setup(x => x.GetTestSheetInfo(It.IsNotIn(testSheet.TestSheetId)))
                .Returns((TestSheet) null);

            if (testSheet.TestValues.IsNullOrEmpty()) testSheet.TestValues = new List<TestValue>();
            var testValuesId = testSheet.TestValues.Select( tv => tv.TestValueId )
                                        .ToArray();
            mock.Setup(x => x.GetTestValue(It.IsIn(testValuesId)))
                .Returns((Int32 testValueId) => testSheet.TestValues.FirstOrDefault(tv => tv.TestValueId == testValueId));
            mock.Setup(x => x.GetTestValue(It.IsNotIn(testValuesId)))
                .Returns((TestValue) null);

            return mock.Object;
        }

        /// <summary>
        ///     A mock for BabyDiaperRetentionBll for delete methods
        /// </summary>
        /// <param name="testValue">testValue data which would be in the db</param>
        /// <returns>a IBabyDiaperBll moq</returns>
        public static IBabyDiaperBll GetBabyDiaperBllForDelete(TestValue testValue)
        {
            var mock = new Mock<IBabyDiaperBll>
            {
                Name = "MockHelper.GetBabyDiaperBllForDelete",
                DefaultValue = DefaultValue.Mock
            };

            mock.Setup( x => x.DeleteTestValue(It.IsAny<Int32>()))
                .Returns( testValue );

            return mock.Object;
        }

        /// <summary>
        ///     A mock for BabyDiaperRetentionBll for Save methods
        /// </summary>
        /// <param name="testSheet">testSheet data which would be in the db</param>
        /// <param name="productionOrder">testSheet data which would be in the db</param>
        /// <param name="testValue">testValue data which would be in the db</param>
        /// <returns>a IBabyDiaperBll moq</returns>
        public static IBabyDiaperBll GetBabyDiaperBllForSavingAndUpdating(TestSheet testSheet, ProductionOrder productionOrder, TestValue testValue)
        {
            var mock = new Mock<IBabyDiaperBll>
            {
                Name = "MockHelper.GetBabyDiaperBllForSavingAndUpdating",
                DefaultValue = DefaultValue.Mock
            };

            mock.Setup(x => x.SaveNewTestValue(It.IsAny<TestValue>()))
                .Returns((TestValue tValue) => tValue);
            mock.Setup(x => x.UpdateTestValue(It.IsAny<TestValue>()))
                .Returns((TestValue tValue) => tValue);
            mock.Setup( x => x.GetTestSheetInfo( It.IsAny<Int32>() ) )
                .Returns( testSheet );
            mock.Setup(x => x.GetProductionOrder(It.IsAny<String>()))
                .Returns(productionOrder);
            mock.Setup(x => x.GetTestValue(It.IsAny<Int32>()))
                .Returns(testValue);
            mock.Setup( x => x.UpdateTestSheet() )
                .Returns( 0 );

            return mock.Object;
        }

    }
}