#region Usings

using System;
using System.Collections.Generic;
using System.Linq;
using Castle.Core.Internal;
using Intranet.Common;
using Intranet.Labor.Definition;
using Intranet.Labor.Definition.Bll;
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
        ///     A mock that adds test for test sheets Repository
        /// </summary>
        /// <param name="testSheet">Test sheet to be added</param>
        /// <returns>a generic repository for test sheets</returns>
        public static IGenericRepository<TestSheet> AddTestSheets( TestSheet testSheet )
        {
            var mock = new Mock<IGenericRepository<TestSheet>>
            {
                Name = "MockHelper.TestSheetRepository",
                DefaultValue = DefaultValue.Mock
            };

            return mock.Object;
        }

        /// <summary>
        ///     A mock for production order Repository
        /// </summary>
        /// <param name="productionOrders">Queryable production order returned by GetAll</param>
        /// <returns>a generic repository for production orders</returns>
        public static IGenericRepository<ProductionOrder> GetAllProductionOrders( IQueryable<ProductionOrder> productionOrders )
        {
            var mock = new Mock<IGenericRepository<ProductionOrder>>
            {
                Name = "MockHelper.GetAllProductionOrders",
                DefaultValue = DefaultValue.Mock
            };
            mock.Setup( x => x.GetAll() )
                .Returns( productionOrders );

            return mock.Object;
        }

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
        ///     A mock for Shift Shedule Repository
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
        ///     A mock for BabyDiaperRetentionBll for delete methods
        /// </summary>
        /// <param name="testValue">testValue data which would be in the db</param>
        /// <returns>a ITestBll moq</returns>
        public static ITestBll GetBabyDiaperBllForDelete( TestValue testValue )
        {
            var mock = new Mock<ITestBll>
            {
                Name = "MockHelper.GetBabyDiaperBllForDelete",
                DefaultValue = DefaultValue.Mock
            };

            mock.Setup( x => x.DeleteTestValue( It.IsAny<Int32>() ) )
                .Returns( testValue );

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

            mock.Setup( x => x.GetTestSheetForId( It.IsAny<Int32>() ) )
                .Returns( testSheet );

            return mock.Object;
        }

        /// <summary>
        ///     A mock for LaborCreatorBll
        /// </summary>
        /// <param name="testSheet">the testsheet</param>
        /// <returns>a moq for laborcreatorbll</returns>
        public static IIncontinencePadLaborCreatorBll GetIncontinencePadLaborCreatorBll( TestSheet testSheet )
        {
            var mock = new Mock<IIncontinencePadLaborCreatorBll>
            {
                Name = "MockHelper.IIncontinencePadLaborCreatorBll",
                DefaultValue = DefaultValue.Mock
            };

            mock.Setup( x => x.GetTestSheetForId( It.IsAny<Int32>() ) )
                .Returns( testSheet );

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

        /// <summary>
        ///     A mock for the shifthelper
        /// </summary>
        /// <param name="shiftType">the current shift type</param>
        /// <param name="shiftScheduleCurrent">the current shift schedule</param>
        /// <param name="lastXShiftSchedules">the last x shift schedules</param>
        /// <returns>a mock for the interface ishifthelper</returns>
        public static IShiftHelper GetShiftHelper( ShiftType? shiftType = null, ShiftSchedule shiftScheduleCurrent = null, List<ShiftSchedule> lastXShiftSchedules = null )
        {
            var mock = new Mock<IShiftHelper>
            {
                Name = "MockHelper.GetShiftHelper",
                DefaultValue = DefaultValue.Mock
            };

            mock.Setup( x => x.GetCurrentShift() )
                .Returns( shiftType );

            mock.Setup( x => x.GetCurrentShiftShedule() )
                .Returns( shiftScheduleCurrent );

            mock.Setup( x => x.GetLastXShiftSchedule( It.IsAny<Int32>() ) )
                .Returns( lastXShiftSchedules );

            mock.Setup(
                x =>
                    x.DateExistsInShifts(
                        It.Is<DateTime>(
                            time =>
                                lastXShiftSchedules.Exists(
                                    schedule =>
                                        ( schedule.StartTime.Hours <= time.Hour ) && ( schedule.EndTime.Hours >= time.Hour )
                                        && ( ( schedule.StartDay == time.DayOfWeek ) || ( schedule.EndDay == time.DayOfWeek ) ) ) ),
                        It.IsAny<List<ShiftSchedule>>() ) ).Returns( true );
            return mock.Object;
        }

        /// <summary>
        ///     A mock for BabyDiaperRetentionBll
        /// </summary>
        /// <param name="testSheet">testSheet data which would be in the db</param>
        /// <returns>a IBabyDiaperRetentionBll moq</returns>
        public static ITestBll GetTestBll( TestSheet testSheet )
        {
            var mock = new Mock<ITestBll>
            {
                Name = "MockHelper.GetTestBll",
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
        ///     A mock for BabyDiaperRetentionBll for Save methods
        /// </summary>
        /// <param name="testSheet">testSheet data which would be in the db</param>
        /// <param name="productionOrder">testSheet data which would be in the db</param>
        /// <param name="testValue">testValue data which would be in the db</param>
        /// <returns>a ITestBll moq</returns>
        public static ITestBll GetTestBllForSavingAndUpdating( TestSheet testSheet, ProductionOrder productionOrder, TestValue testValue )
        {
            var mock = new Mock<ITestBll>
            {
                Name = "MockHelper.GetTestBllForSavingAndUpdating",
                DefaultValue = DefaultValue.Mock
            };

            mock.Setup( x => x.SaveNewTestValue( It.IsAny<TestValue>() ) )
                .Returns( ( TestValue tValue ) => tValue );
            mock.Setup( x => x.UpdateTestValue( It.IsAny<TestValue>() ) )
                .Returns( ( TestValue tValue ) => tValue );
            mock.Setup( x => x.GetTestSheetInfo( It.IsAny<Int32>() ) )
                .Returns( testSheet );
            mock.Setup( x => x.GetProductionOrder( It.IsAny<String>() ) )
                .Returns( productionOrder );
            mock.Setup( x => x.GetTestValue( It.IsAny<Int32>() ) )
                .Returns( testValue );
            mock.Setup( x => x.UpdateTestSheet() )
                .Returns( 0 );

            return mock.Object;
        }

        /// <summary>
        ///     A mock for test sheets Repository
        /// </summary>
        /// <param name="testSheets">Queryable test sheets returned by GetAll, Or first testsheet returned for Add</param>
        /// <returns>a generic repository for test sheets</returns>
        public static IGenericRepository<TestSheet> TestSheetRepository( IQueryable<TestSheet> testSheets )
        {
            var mock = new Mock<IGenericRepository<TestSheet>>
            {
                Name = "MockHelper.TestSheetRepository",
                DefaultValue = DefaultValue.Mock
            };
            mock.Setup( x => x.GetAll() )
                .Returns( testSheets );
            mock.Setup( x => x.Add( testSheets.First() ) )
                .Returns( testSheets.First() );
            mock.Setup( x => x.SaveChanges() )
                .Returns( 1 );
            return mock.Object;
        }
    }
}