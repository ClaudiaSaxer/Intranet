using System.Collections.Generic;
using FluentAssertions;
using Intranet.Common;
using Intranet.Labor.Model;
using Intranet.Labor.TestEnvironment;
using Xunit;

namespace Intranet.Labor.Bll.Test
{
    /// <summary>
    ///     Test Class for LaborCreatorService
    /// </summary>
    public class LaborCreatorServiceTest
    {
        /// <summary>
        ///     Normal Passing Test for GetLaborCreatorViewModel
        /// </summary>
        [Fact]
        public void GetLaborCreatorViewModelTest()
        {
            var serviceHelperBllMoq = MockHelperBll.GetLaborCreatorsBll( new TestSheet(), new List<TestSheet> { new TestSheet(), new TestSheet() } );

            var target = new LaborCreatorService( new NLogLoggerFactory() )
            {
                LaborCreatorBll = serviceHelperBllMoq
            };

            var actual = target.GetLaborCreatorViewModel();
            actual.ChosenPo.Should()
                  .BeNullOrEmpty( "because it will be filled by user" );
            actual.Message.Should()
                  .BeNullOrEmpty( "because it is for error messages" );
            actual.ProductionOrders.Count.Should()
                  .Be( 2 );
        }

        /// <summary>
        ///     Normal Passing Test for GetLaborCreatorViewModel
        /// </summary>
        [Fact]
        public void GetLaborCreatorViewModelTestNotExisting()
        {
            var serviceHelperBllMoq = MockHelperBll.GetLaborCreatorsBll( null, null );

            var target = new LaborCreatorService( new NLogLoggerFactory() )
            {
                LaborCreatorBll = serviceHelperBllMoq
            };

            var actual = target.GetLaborCreatorViewModel();
            actual.ChosenPo.Should()
                  .BeNullOrEmpty( "because it will be filled by user" );
            actual.Message.Should()
                  .BeNullOrEmpty( "because it is for error messages" );
            actual.ProductionOrders.Should()
                  .BeNullOrEmpty( "because there are no existing" );
        }

        /// <summary>
        ///     Normal Passing Test for GetTestSheetId
        /// </summary>
        [Fact]
        public void GetTestSheetIdTest()
        {
            var serviceHelperBllMoq = MockHelperBll.GetLaborCreatorsBll( new TestSheet { FaNr = "666" }, new List<TestSheet>() );

            var target = new LaborCreatorService( new NLogLoggerFactory() )
            {
                LaborCreatorBll = serviceHelperBllMoq
            };

            var actual = target.GetTestSheetId( "666" );
            actual.FaNr.Should()
                  .Be( "666" );
        }

        /// <summary>
        ///     Test for GetTestSheetId
        /// </summary>
        [Fact]
        public void GetTestSheetIdTestNotExisting()
        {
            var serviceHelperBllMoq = MockHelperBll.GetLaborCreatorsBll( null, new List<TestSheet>() );

            var target = new LaborCreatorService( new NLogLoggerFactory() )
            {
                LaborCreatorBll = serviceHelperBllMoq
            };

            var actual = target.GetTestSheetId( "666" );
            actual.Should()
                  .BeNull( "because it is not found" );
        }
    }
}