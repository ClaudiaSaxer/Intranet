#region Usings

using System;
using System.Collections.Generic;
using System.Linq;
using Castle.Core.Internal;
using Intranet.Common;
using Intranet.Labor.Definition;
using Intranet.Labor.Model;
using Intranet.Labor.Model.labor;
using Intranet.Model;
using Intranet.Web.Areas.Labor.Controllers;
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
        ///     A mock for GenericRepository
        /// </summary>
        /// <param name="roles">Queryable Roles returned by GetAll</param>
        /// <returns>a generic repository for roles</returns>
        public static IGenericRepository<Role> GetAllRoles( IQueryable<Role> roles )
        {
            var mock = new Mock<IGenericRepository<Role>>
            {
                Name = "MockHelper.GetAllRoles",
                DefaultValue = DefaultValue.Mock
            };

            mock.Setup( x => x.GetAll() )
                .Returns( roles );

            return mock.Object;
        }
        /// <summary>
        /// A mock for production order Repository
        /// </summary>
        /// <param name="productionOrders">Queryable production order returned by GetAll</param>
        /// <returns>a generic repository for production orders</returns>
        public static IGenericRepository<ProductionOrder> GetAllProductionOrders(IQueryable<ProductionOrder> productionOrders)
        {
            var mock = new Mock<IGenericRepository<ProductionOrder>>
            {
                Name = "MockHelper.GetAllProductionOrders",
                DefaultValue = DefaultValue.Mock
            };
            mock.Setup(x => x.GetAll())
                .Returns(productionOrders);

            return mock.Object;
        }
        /// <summary>
        /// A mock for Shift Shedule Repository
        /// </summary>
        /// <param name="shiftSchedules">Queryable Shift Schedules returned by GetAll</param>
        /// <returns>a generic repository for shift schedules</returns>
        public static IGenericRepository<ShiftSchedule> GetAllShiftSchedules( IQueryable<ShiftSchedule> shiftSchedules )
        {
            var mock = new Mock<IGenericRepository<ShiftSchedule>>
            {
                Name = "MockHelper.GetAllShiftSchedules",
                DefaultValue = DefaultValue.Mock
            };
            mock.Setup( x => x.GetAll() )
                .Returns( shiftSchedules );

            return mock.Object;
        }
        /// <summary>
        /// A mock for test sheets Repository
        /// </summary>
        /// <param name="testSheets">Queryable test sheets returned by GetAll</param>
        /// <returns>a generic repository for test sheets</returns>
        public static IGenericRepository<TestSheet> GetAllTestSheets( IQueryable<TestSheet> testSheets )
        {
            var mock = new Mock<IGenericRepository<TestSheet>>
            {
                Name = "MockHelper.GetAllTestSheets",
                DefaultValue = DefaultValue.Mock
            };
            mock.Setup( x => x.GetAll() )
                .Returns( testSheets );

            return mock.Object;
        }

        /// <summary>
        ///     A mock for LaborCreatorBll
        /// </summary>
        /// <param name="testSheet">the testsheet</param>
        /// <returns>a moq for laborcreatorbll</returns>
        public static IBabyDiaperLaborCreatorBll GetBabyDiaperLaborCreatorBll( TestSheet testSheet )
        {
            var mock = new Mock<IBabyDiaperLaborCreatorBll>
            {
                Name = "MockHelper.IBabyDiaperLaborCreatorBll",
                DefaultValue = DefaultValue.Mock
            };

            mock.Setup( x => x.getTestSheetForId( It.IsAny<Int32>() ) )
                .Returns( testSheet );

            return mock.Object;
        }

        /// <summary>
        ///     A mock for BabyDiaperRetentionBll
        /// </summary>
        /// <param name="testSheet">testSheet data which would be in the db</param>
        /// <returns>a IBabyDiaperRetentionBll moq</returns>
        public static IBabyDiaperRetentionBll GetBabyDiaperRetentionBll( TestSheet testSheet )
        {
            var mock = new Mock<IBabyDiaperRetentionBll>
            {
                Name = "MockHelper.GetBabyDiaperRetentionBll",
                DefaultValue = DefaultValue.Mock
            };

            var noteCodes = new List<Error>
            {
                new Error { ErrorId = 1, ErrorCode = "404", Value = "Error Not Found" },
                new Error { ErrorId = 2, ErrorCode = "500", Value = "Error not authorized" }
            };

            mock.Setup( x => x.GetAllNoteCodes() )
                .Returns( noteCodes );

            mock.Setup( x => x.GetTestSheetInfo( testSheet.TestSheetId ) )
                .Returns( testSheet );
            mock.Setup( x => x.GetTestSheetInfo( It.IsNotIn( testSheet.TestSheetId ) ) )
                .Returns( (TestSheet) null );

            if ( testSheet.TestValues.IsNullOrEmpty() )
                testSheet.TestValues = new List<TestValue>();
            var testValuesId = testSheet.TestValues.Select( tv => tv.TestValueId )
                                        .ToArray();
            mock.Setup( x => x.GetTestValue( It.IsIn( testValuesId ) ) )
                .Returns( ( Int32 testValueId ) => testSheet.TestValues.FirstOrDefault( tv => tv.TestValueId == testValueId ) );
            mock.Setup( x => x.GetTestValue( It.IsNotIn( testValuesId ) ) )
                .Returns( (TestValue) null );

            return mock.Object;
        }

        /// <summary>
        ///     A mock for LaborCreatorBll
        /// </summary>
        /// <param name="testSheet">the testsheet</param>
        /// <param name="runningTestSheets">the running testsheets</param>
        /// <returns>a moq for laborcreatorbll</returns>
        public static ILaborCreatorBll GetLaborCreatorsBll( TestSheet testSheet, ICollection<TestSheet> runningTestSheets )
        {
            var mock = new Mock<ILaborCreatorBll>
            {
                Name = "MockHelper.ILaborCreatorBll",
                DefaultValue = DefaultValue.Mock
            };

            mock.Setup( x => x.GetTestSheetForFaNr( It.IsAny<String>() ) )
                .Returns( testSheet );

            mock.Setup( x => x.InitTestSheetForFaNr( It.IsAny<String>() ) )
                .Returns( testSheet );

            mock.Setup( x => x.RunningTestSheets() )
                .Returns( runningTestSheets );

            return mock.Object;
        }

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
    }
}