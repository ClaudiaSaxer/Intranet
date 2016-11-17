using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Intranet.Common;
using Intranet.Labor.Model;
using Intranet.Labor.Model.labor;
using Intranet.Labor.TestEnvironment;
using Intranet.Web.Areas.Labor.Controllers;
using Xunit;

namespace Intranet.Labor.Bll.Test
{
    /// <summary>
    ///     Test Class for LaborCreatorBll
    /// </summary>
    public class LaborCreatorBllTest
    {
        /// <summary>
        ///     Normal Passing Test for GetCurrentShift
        /// </summary>
        [Fact]
        public void GetCurrentShiftNormalTest()
        {
            var now = DateTime.Now;

            var shift = new ShiftSchedule
            {
                Name = "The One",
                ShiftType = ShiftType.Morning,
                EndTime = new TimeSpan( now.Hour + 2, now.Minute, now.Second ),
                StartTime = new TimeSpan( now.Hour - 2, now.Minute, now.Second ),
                StartDay = now.DayOfWeek,
                EndDay = now.DayOfWeek
            };

            var shiftSheduleListQuery = new List<ShiftSchedule>
            {
                shift
            };

            var shiftSheduleRepository = MockHelperBll.GetAllShiftSchedules( shiftSheduleListQuery.AsQueryable() );

            var target = new LaborCreatorBll( new NLogLoggerFactory() )
            {
                ProductionOrderRepository = null,
                ShiftScheduleRepository = shiftSheduleRepository,
                TestSheetRepository = null
            };

            var actual = target.ShiftHelper.GetCurrentShift()
                               .Should()
                               .Be( ShiftType.Morning );
        }

        /// <summary>
        ///     Not Found Test for GetCurrentShift1
        /// </summary>
        [Fact]
        public void GetCurrentShiftTestNotFound1()
        {
            var now = DateTime.Now;

            var shift = new ShiftSchedule
            {
                Name = "The One",
                ShiftType = ShiftType.Late,
                EndTime = new TimeSpan( now.Hour - 1, now.Minute, now.Second ),
                StartTime = new TimeSpan( now.Hour + 2, now.Minute, now.Second ),
                StartDay = now.DayOfWeek,
                EndDay = now.AddDays( 1 )
                            .DayOfWeek
            };

            var shiftSheduleListQuery = new List<ShiftSchedule>
            {
                shift
            };

            var shiftSheduleRepository = MockHelperBll.GetAllShiftSchedules( shiftSheduleListQuery.AsQueryable() );

            var target = new LaborCreatorBll( new NLogLoggerFactory() )
            {
                ProductionOrderRepository = null,
                ShiftScheduleRepository = shiftSheduleRepository,
                TestSheetRepository = null
            };

            var actual = target.ShiftHelper.GetCurrentShift()
                               .Should()
                               .BeNull( "because not existing" );
        }

        /// <summary>
        ///     Not Found Test for GetCurrentShift2
        /// </summary>
        [Fact]
        public void GetCurrentShiftTestNotFound2()
        {
            var now = DateTime.Now;

            var shift = new ShiftSchedule
            {
                Name = "The One",
                ShiftType = ShiftType.Late,
                EndTime = new TimeSpan( now.Hour, now.Minute - 30, now.Second ),
                StartTime = new TimeSpan( now.Hour - 2, now.Minute, now.Second ),
                StartDay = now.DayOfWeek,
                EndDay = now.AddDays( 1 )
                            .DayOfWeek
            };

            var shiftSheduleListQuery = new List<ShiftSchedule>
            {
                shift
            };

            var shiftSheduleRepository = MockHelperBll.GetAllShiftSchedules( shiftSheduleListQuery.AsQueryable() );

            var target = new LaborCreatorBll( new NLogLoggerFactory() )
            {
                ProductionOrderRepository = null,
                ShiftScheduleRepository = shiftSheduleRepository,
                TestSheetRepository = null
            };

            var actual = target.ShiftHelper.GetCurrentShift()
                               .Should()
                               .BeNull( "because not existing" );
        }

        /// <summary>
        ///     Not Found Test for GetCurrentShift3
        /// </summary>
        [Fact]
        public void GetCurrentShiftTestNotFound3()
        {
            var now = DateTime.Now;

            var shift = new ShiftSchedule
            {
                Name = "The One",
                ShiftType = ShiftType.Late,
                EndTime = new TimeSpan( now.Hour, now.Minute, now.Second ),
                StartTime = new TimeSpan( now.Hour, now.Minute + 30, now.Second ),
                StartDay = now.DayOfWeek,
                EndDay = now.AddDays( 1 )
                            .DayOfWeek
            };

            var shiftSheduleListQuery = new List<ShiftSchedule>
            {
                shift
            };

            var shiftSheduleRepository = MockHelperBll.GetAllShiftSchedules( shiftSheduleListQuery.AsQueryable() );

            var target = new LaborCreatorBll( new NLogLoggerFactory() )
            {
                ProductionOrderRepository = null,
                ShiftScheduleRepository = shiftSheduleRepository,
                TestSheetRepository = null
            };

            var actual = target.ShiftHelper.GetCurrentShift()
                               .Should()
                               .BeNull( "because not existing" );
        }

        /// <summary>
        ///     Test for GetCurrentShift To Many Found
        /// </summary>
        [Fact]
        public void GetCurrentShiftTestToMany()
        {
            var now = DateTime.Now;

            var shift = new ShiftSchedule
            {
                Name = "The One",
                ShiftType = ShiftType.Late,
                EndTime = new TimeSpan( now.Hour + 2, now.Minute, now.Second ),
                StartTime = new TimeSpan( now.Hour - 2, now.Minute, now.Second ),
                StartDay = now.DayOfWeek,
                EndDay = now.DayOfWeek
            };

            var shiftSheduleListQuery = new List<ShiftSchedule>
            {
                shift,
                shift
            };

            var shiftSheduleRepository = MockHelperBll.GetAllShiftSchedules( shiftSheduleListQuery.AsQueryable() );

            var target = new LaborCreatorBll( new NLogLoggerFactory() )
            {
                ProductionOrderRepository = null,
                ShiftScheduleRepository = shiftSheduleRepository,
                TestSheetRepository = null
            };

            target.ShiftHelper.GetCurrentShift()
                  .Should()
                  .BeNull( "because more than one existing" );
        }

        /// <summary>
        ///     Normal Passing Test for  GetTestSheetForFaNr
        /// </summary>
        [Fact]
        public void GetTestSheetForFaNrTestNormal()
        {
            var now = DateTime.Now;

            var testsheet = new TestSheet
            {
                FaNr = "666",
                ShiftType = ShiftType.Late,
                DayInYear = now.DayOfYear
            };

            var shift = new ShiftSchedule
            {
                Name = "The One",
                ShiftType = ShiftType.Late,
                EndTime = new TimeSpan( now.Hour + 2, now.Minute, now.Second ),
                StartTime = new TimeSpan( now.Hour - 2, now.Minute, now.Second ),
                StartDay = now.DayOfWeek,
                EndDay = now.AddDays( 1 )
                            .DayOfWeek
            };

            var shiftSheduleListQuery = new List<ShiftSchedule>
            {
                shift
            };

            var shiftSheduleRepository = MockHelperBll.GetAllShiftSchedules( shiftSheduleListQuery.AsQueryable() );
            var testSheetList = new List<TestSheet>
            {
                testsheet
            };

            var testSheetRepository = MockHelperBll.TestSheetRepository( testSheetList.AsQueryable() );

            var target = new LaborCreatorBll( new NLogLoggerFactory() )
            {
                ProductionOrderRepository = null,
                ShiftScheduleRepository = shiftSheduleRepository,
                TestSheetRepository = testSheetRepository
            };

            var actual = target.GetTestSheetForFaNr( "666" )
                               .Should()
                               .Be( testsheet );
        }

        /// <summary>
        ///     Test for  GetTestSheetForFaNr Not Found1
        /// </summary>
        [Fact]
        public void GetTestSheetForFaNrTestNotFound1()
        {
            var now = DateTime.Now;

            var testsheet = new TestSheet
            {
                FaNr = "1234",
                ShiftType = ShiftType.Late,
                DayInYear = now.DayOfYear
            };

            var shift = new ShiftSchedule
            {
                Name = "The One",
                ShiftType = ShiftType.Late,
                EndTime = new TimeSpan( now.Hour + 2, now.Minute, now.Second ),
                StartTime = new TimeSpan( now.Hour - 2, now.Minute, now.Second ),
                StartDay = now.DayOfWeek,
                EndDay = now.AddDays( 1 )
                            .DayOfWeek
            };

            var shiftSheduleListQuery = new List<ShiftSchedule>
            {
                shift
            };

            var shiftSheduleRepository = MockHelperBll.GetAllShiftSchedules( shiftSheduleListQuery.AsQueryable() );
            var testSheetList = new List<TestSheet>
            {
                testsheet
            };

            var testSheetRepository = MockHelperBll.TestSheetRepository( testSheetList.AsQueryable() );

            var target = new LaborCreatorBll( new NLogLoggerFactory() )
            {
                ProductionOrderRepository = null,
                ShiftScheduleRepository = shiftSheduleRepository,
                TestSheetRepository = testSheetRepository
            };

            var actual = target.GetTestSheetForFaNr( "666" )
                               .Should()
                               .BeNull( "because no existing" );
        }

        /// <summary>
        ///     Test for  GetTestSheetForFaNr Not Found2
        /// </summary>
        [Fact]
        public void GetTestSheetForFaNrTestNotFound2()
        {
            var now = DateTime.Now;

            var testsheet = new TestSheet
            {
                FaNr = "666",
                ShiftType = ShiftType.Morning,
                DayInYear = now.DayOfYear
            };

            var shift = new ShiftSchedule
            {
                Name = "The One",
                ShiftType = ShiftType.Late,
                EndTime = new TimeSpan( now.Hour + 2, now.Minute, now.Second ),
                StartTime = new TimeSpan( now.Hour - 2, now.Minute, now.Second ),
                StartDay = now.DayOfWeek,
                EndDay = now.AddDays( 1 )
                            .DayOfWeek
            };

            var shiftSheduleListQuery = new List<ShiftSchedule>
            {
                shift
            };

            var shiftSheduleRepository = MockHelperBll.GetAllShiftSchedules( shiftSheduleListQuery.AsQueryable() );
            var testSheetList = new List<TestSheet>
            {
                testsheet
            };

            var testSheetRepository = MockHelperBll.TestSheetRepository( testSheetList.AsQueryable() );

            var target = new LaborCreatorBll( new NLogLoggerFactory() )
            {
                ProductionOrderRepository = null,
                ShiftScheduleRepository = shiftSheduleRepository,
                TestSheetRepository = testSheetRepository
            };

            var actual = target.GetTestSheetForFaNr( "666" )
                               .Should()
                               .BeNull( "because no existing" );
        }

        /// <summary>
        ///     Test for  GetTestSheetForFaNr Not Found3
        /// </summary>
        [Fact]
        public void GetTestSheetForFaNrTestNotFound3()
        {
            var now = DateTime.Now;

            var testsheet = new TestSheet
            {
                FaNr = "666",
                ShiftType = ShiftType.Late,
                DayInYear = now.DayOfYear + 1
            };

            var shift = new ShiftSchedule
            {
                Name = "The One",
                ShiftType = ShiftType.Late,
                EndTime = new TimeSpan( now.Hour + 2, now.Minute, now.Second ),
                StartTime = new TimeSpan( now.Hour - 2, now.Minute, now.Second ),
                StartDay = now.DayOfWeek,
                EndDay = now.AddDays( 1 )
                            .DayOfWeek
            };

            var shiftSheduleListQuery = new List<ShiftSchedule>
            {
                shift
            };

            var shiftSheduleRepository = MockHelperBll.GetAllShiftSchedules( shiftSheduleListQuery.AsQueryable() );
            var testSheetList = new List<TestSheet>
            {
                testsheet
            };

            var testSheetRepository = MockHelperBll.TestSheetRepository( testSheetList.AsQueryable() );

            var target = new LaborCreatorBll( new NLogLoggerFactory() )
            {
                ProductionOrderRepository = null,
                ShiftScheduleRepository = shiftSheduleRepository,
                TestSheetRepository = testSheetRepository
            };

            var actual = target.GetTestSheetForFaNr( "666" )
                               .Should()
                               .BeNull( "because no existing" );
        }

        /// <summary>
        ///     Test for InitTestSheetForFaNr Not Found Fa Nr
        /// </summary>
        [Fact]
        public void InitTestSheetForFaNrTestNotFoundFaNr()
        {
            var now = DateTime.Now;

            var shift = new ShiftSchedule
            {
                Name = "The One",
                ShiftType = ShiftType.Late,
                EndTime = new TimeSpan( now.Hour + 2, now.Minute, now.Second ),
                StartTime = new TimeSpan( now.Hour - 2, now.Minute, now.Second ),
                StartDay = now.DayOfWeek,
                EndDay = now.AddDays( 1 )
                            .DayOfWeek
            };

            var productionOrderListQuery = new List<ProductionOrder>
            {
                new ProductionOrder
                {
                    FaNr = "123"
                }
            };
            var shiftSheduleListQuery = new List<ShiftSchedule>
            {
                shift
            };

            var shiftSheduleRepository = MockHelperBll.GetAllShiftSchedules( shiftSheduleListQuery.AsQueryable() );

            var productionOrderRepository = MockHelperBll.GetAllProductionOrders( productionOrderListQuery.AsQueryable() );

            var target = new LaborCreatorBll( new NLogLoggerFactory() )
            {
                ProductionOrderRepository = productionOrderRepository,
                ShiftScheduleRepository = shiftSheduleRepository,
                TestSheetRepository = null
            };

            var actual = target.InitTestSheetForFaNr( "666" );
            actual.Should()
                  .BeNull( "because no production order with fanr existing" );
        }

        /// <summary>
        ///     Test for InitTestSheetForFaNr Not Found Shift
        /// </summary>
        [Fact]
        public void InitTestSheetForFaNrTestNotFoundShift()
        {
            var now = DateTime.Now;

            var shift = new ShiftSchedule
            {
                Name = "The One",
                ShiftType = ShiftType.Late,
                EndTime = new TimeSpan( now.Hour - 2, now.Minute, now.Second ),
                StartTime = new TimeSpan( now.Hour + 2, now.Minute, now.Second ),
                StartDay = now.DayOfWeek,
                EndDay = now.AddDays( 1 )
                            .DayOfWeek
            };

            var productionOrderListQuery = new List<ProductionOrder>
            {
                new ProductionOrder
                {
                    FaNr = "666"
                }
            };
            var shiftSheduleListQuery = new List<ShiftSchedule>
            {
                shift
            };

            var shiftSheduleRepository = MockHelperBll.GetAllShiftSchedules( shiftSheduleListQuery.AsQueryable() );

            var productionOrderRepository = MockHelperBll.GetAllProductionOrders( productionOrderListQuery.AsQueryable() );

            var target = new LaborCreatorBll( new NLogLoggerFactory() )
            {
                ProductionOrderRepository = productionOrderRepository,
                ShiftScheduleRepository = shiftSheduleRepository,
                TestSheetRepository = null
            };

            var actual = target.InitTestSheetForFaNr( "666" );
            actual.Should()
                  .BeNull( "because no shift existing" );
        }

        /// <summary>
        ///     Normal Passing Test for InitTestSheetForFaNr
        /// </summary>
        [Fact]
        public void InitTestSheetForFaNrTestPassing()
        {
            var now = DateTime.Now;

            var testsheet = new TestSheet
            {
                FaNr = "666",
                ShiftType = ShiftType.Late,
                DayInYear = now.DayOfYear
            };

            var shift = new ShiftSchedule
            {
                Name = "The One",
                ShiftType = ShiftType.Late,
                EndTime = new TimeSpan( now.Hour + 2, now.Minute, now.Second ),
                StartTime = new TimeSpan( now.Hour - 2, now.Minute, now.Second ),
                StartDay = now.DayOfWeek,
                EndDay = now.AddDays( 1 )
                            .DayOfWeek
            };

            var productionorder = new ProductionOrder
            {
                FaNr = "666",
                Article = new Article(),
                Machine = new Machine
                {
                    MachineNr = "M12"
                },
                Component = new ProductionOrderComponent(),
                EndDateTime = now,
                StartDateTime = now,
                FaId = 1,
                ArticleRefId = 1,
                MachineRefId = 1
            };
            var productionOrderListQuery = new List<ProductionOrder>
            {
                productionorder
            };
            var shiftSheduleListQuery = new List<ShiftSchedule>
            {
                shift
            };
            var testSheetList = new List<TestSheet>
            {
                testsheet
            };

            var shiftSheduleRepository = MockHelperBll.GetAllShiftSchedules( shiftSheduleListQuery.AsQueryable() );

            var productionOrderRepository = MockHelperBll.GetAllProductionOrders( productionOrderListQuery.AsQueryable() );

            var testSheetRepository = MockHelperBll.TestSheetRepository( testSheetList.AsQueryable() );

            var target = new LaborCreatorBll( new NLogLoggerFactory() )
            {
                ProductionOrderRepository = productionOrderRepository,
                ShiftScheduleRepository = shiftSheduleRepository,
                TestSheetRepository = testSheetRepository
            };

            var actual = target.InitTestSheetForFaNr( "666" );
            actual.Should()
                  .NotBeNull( "should be initialized" );
            actual.FaNr.Should()
                  .Be( productionorder.FaNr );
            actual.ShiftType.Should()
                  .Be( shift.ShiftType );
            actual.ArticleType.Should()
                  .Be( productionorder.Article.ArticleType );
            actual.CreatedDateTime.Hour.Should()
                  .Be( now.Hour );
            actual.CreatedDateTime.Minute.Should()
              .BeLessOrEqualTo( now.Minute );
            actual.DayInYear.Should()
                  .Be( now.DayOfYear );
            actual.MachineNr.Should()
                  .Be( productionorder.Machine.MachineNr);
            actual.TestValues.Count.Should()
                  .Be( 6 );
            actual.TestValues.Should()
                  .Contain( value => value.TestValueType == TestValueType.Average );
            actual.TestValues.Should()
                  .Contain( value => value.TestValueType == TestValueType.StandardDeviation );
            actual.SAPNr.Should()
                  .Be( productionorder.Component.ComponentNr );
            actual.SizeName.Should()
                  .Be( productionorder.Article.SizeName );
            actual.ProductName.Should()
                  .Be( productionorder.Article.ProductName );
            actual.SAPType.Should()
                  .Be( productionorder.Component.ComponentType );
        }

        /// <summary>
        ///     Normal Passing Test for RunningTestSheets
        /// </summary>
        [Fact]
        public void RunningTestSheetsTest1()
        {
            var now = DateTime.Now;

            var testsheet1 = new TestSheet
            {
                FaNr = "666",
                ShiftType = ShiftType.Late,
                DayInYear = now.DayOfYear
            };

            var testsheet2 = new TestSheet
            {
                FaNr = "7",
                ShiftType = ShiftType.Late,
                DayInYear = now.DayOfYear
            };

            var shift = new ShiftSchedule
            {
                Name = "The One",
                ShiftType = ShiftType.Late,
                EndTime = new TimeSpan( now.Hour + 2, now.Minute, now.Second ),
                StartTime = new TimeSpan( now.Hour - 2, now.Minute, now.Second ),
                StartDay = now.DayOfWeek,
                EndDay = now.AddDays( 1 )
                            .DayOfWeek
            };
            var testsheets = new List<TestSheet> { testsheet1, testsheet2 };

            var shiftSheduleListQuery = new List<ShiftSchedule>
            {
                shift
            };

            var shiftSheduleRepository = MockHelperBll.GetAllShiftSchedules( shiftSheduleListQuery.AsQueryable() );
            var testSheetList = new List<TestSheet>
            {
                testsheet1,
                testsheet2
            };

            var testSheetRepository = MockHelperBll.TestSheetRepository( testSheetList.AsQueryable() );

            var target = new LaborCreatorBll( new NLogLoggerFactory() )
            {
                ProductionOrderRepository = null,
                ShiftScheduleRepository = shiftSheduleRepository,
                TestSheetRepository = testSheetRepository
            };

            var actual = target.RunningTestSheets();
            actual.Count.Should()
                  .Be( 2 );
            actual.Should()
                  .BeEquivalentTo( testsheets );
        }

        /// <summary>
        ///     Normal Passing Test for RunningTestSheets
        /// </summary>
        [Fact]
        public void RunningTestSheetsTest2()
        {
            var now = DateTime.Now;

            var testsheet1 = new TestSheet
            {
                FaNr = "666",
                ShiftType = ShiftType.Late,
                DayInYear = now.DayOfYear
            };

            var testsheet2 = new TestSheet
            {
                FaNr = "7",
                ShiftType = ShiftType.Late,
                DayInYear = now.DayOfYear
            };
            var testsheet3 = new TestSheet
            {
                FaNr = "5",
                ShiftType = ShiftType.Late,
                DayInYear = now.DayOfYear + 1
            };
            var testsheet4 = new TestSheet
            {
                FaNr = "127",
                ShiftType = ShiftType.Morning,
                DayInYear = now.DayOfYear
            };
            var testsheet5 = new TestSheet
            {
                FaNr = "778",
                ShiftType = ShiftType.Late,
                DayInYear = now.DayOfYear - 1
            };
            var testsheet6 = new TestSheet
            {
                FaNr = "111",
                ShiftType = ShiftType.Night,
                DayInYear = now.DayOfYear - 1
            };
            var shift = new ShiftSchedule
            {
                Name = "The One",
                ShiftType = ShiftType.Late,
                EndTime = new TimeSpan( now.Hour + 2, now.Minute, now.Second ),
                StartTime = new TimeSpan( now.Hour - 2, now.Minute, now.Second ),
                StartDay = now.DayOfWeek,
                EndDay = now.AddDays( 1 )
                            .DayOfWeek
            };
            var testsheets = new List<TestSheet> { testsheet1, testsheet2 };

            var shiftSheduleListQuery = new List<ShiftSchedule>
            {
                shift
            };

            var shiftSheduleRepository = MockHelperBll.GetAllShiftSchedules( shiftSheduleListQuery.AsQueryable() );
            var testSheetList = new List<TestSheet>
            {
                testsheet1,
                testsheet2,
                testsheet3,
                testsheet4,
                testsheet5,
                testsheet6
            };

            var testSheetRepository = MockHelperBll.TestSheetRepository( testSheetList.AsQueryable() );

            var target = new LaborCreatorBll( new NLogLoggerFactory() )
            {
                ProductionOrderRepository = null,
                ShiftScheduleRepository = shiftSheduleRepository,
                TestSheetRepository = testSheetRepository
            };

            var actual = target.RunningTestSheets();
            actual.Count.Should()
                  .Be( 2 );
            actual.Should()
                  .BeEquivalentTo( testsheets );
        }
    }
}