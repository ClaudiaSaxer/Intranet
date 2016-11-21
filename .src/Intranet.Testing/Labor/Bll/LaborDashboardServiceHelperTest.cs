#region Usings

using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Intranet.Common;
using Intranet.Labor.Model;
using Intranet.Labor.Model.labor;
using Intranet.Web.Areas.Labor.Controllers;
using Xunit;

#endregion

namespace Intranet.Labor.Bll.Test
{
    /// <summary>
    ///     Test Class for Labor Dashboard Service Helper
    /// </summary>
    public class LaborDashboardServiceHelperTest
    {
        /// <summary>
        ///     Calc Rw type test
        /// </summary>
        [Fact]
        public void CalcRwTypeTest()
        {
            var target = new LaborDashboardHelper( new NLogLoggerFactory() );
            target.CalcRwType( RwType.Ok, RwType.Better )
                  .Should()
                  .Be( RwType.Better );
            target.CalcRwType( RwType.Ok, RwType.Worse )
                  .Should()
                  .Be( RwType.Worse );
            target.CalcRwType( RwType.SomethingWorse, RwType.Worse )
                  .Should()
                  .Be( RwType.Worse );
            target.CalcRwType( RwType.Ok, RwType.SomethingWorse )
                  .Should()
                  .Be( RwType.SomethingWorse );
        }

        /// <summary>
        ///     ToDashboardInfosBabyDiapers PenetrationTime test
        /// </summary>
        [Fact]
        public void ToDashboardInfosBabyDiapersPenetrationTimeTest()
        {
            var testvalue = new TestValue
            {
                ArticleTestType = ArticleType.BabyDiaper,
                BabyDiaperTestValue = new BabyDiaperTestValue

                {
                    TestType = TestTypeBabyDiaper.RewetAndPenetrationTime,
                    PenetrationTimeAdditionFourth = 1.88991,
                    PenetrationRwType = RwType.SomethingWorse,
                    Rewet140Rw = RwType.Better,
                    Rewet140Value = 12.12345,
                    Rewet210Value = 14.444,
                    Rewet210Rw = RwType.Ok
                }
            };

            var target = new LaborDashboardHelper( new NLogLoggerFactory() );
            var info = target.ToDashboardInfosBabyDiapers( testvalue );
            info.Count.Should()
                .Be( 3 );
            info.ToList()[2].RwType.Should()
                .Be( RwType.SomethingWorse );
            info.ToList()[2].InfoValue.Should()
                .Be( "1.89" );
            info.ToList()[2].InfoKey.Should()
                .Be( "Penetrationszeit - Zugabe 4" );
            info.ToList()[0].RwType.Should()
                .Be( RwType.Better );
            info.ToList()[0].InfoValue.Should()
                .Be( "12.12" );
            info.ToList()[0].InfoKey.Should()
                .Be( "Rewet - 140 ml" );
            info.ToList()[1].RwType.Should()
                .Be( RwType.Ok );
            info.ToList()[1].InfoValue.Should()
                .Be( "14.44" );
            info.ToList()[1].InfoKey.Should()
                .Be( "Rewet - 210 ml" );
        }

        /// <summary>
        ///     ToDashboardInfosBabyDiapers Retention test
        /// </summary>
        [Fact]
        public void ToDashboardInfosBabyDiapersRetentionTest()
        {
            var testvalue = new TestValue
            {
                ArticleTestType = ArticleType.BabyDiaper,
                BabyDiaperTestValue = new BabyDiaperTestValue

                {
                    TestType = TestTypeBabyDiaper.Retention,
                    RetentionRw = RwType.Worse,
                    RetentionAfterZentrifugeValue = 12.0
                }
            };

            var target = new LaborDashboardHelper( new NLogLoggerFactory() );
            var info = target.ToDashboardInfosBabyDiapers( testvalue );
            info.Count.Should()
                .Be( 1 );
            info.ToList()[0].RwType.Should()
                .Be( RwType.Worse );
            info.ToList()[0].InfoValue.Should()
                .Be( "12" );
            info.ToList()[0].InfoKey.Should()
                .Be( "Retention - Nach Zentrifuge (g)" );
        }

        /// <summary>
        ///     ToDashboardInfosBabyDiapers Rewettest
        /// </summary>
        [Fact]
        public void ToDashboardInfosBabyDiapersRewetTest()
        {
            var testvalue = new TestValue
            {
                ArticleTestType = ArticleType.BabyDiaper,
                BabyDiaperTestValue = new BabyDiaperTestValue

                {
                    TestType = TestTypeBabyDiaper.Rewet,
                    Rewet140Rw = RwType.Better,
                    Rewet140Value = 12.12345,
                    Rewet210Value = 14.444,
                    Rewet210Rw = RwType.Ok
                }
            };

            var target = new LaborDashboardHelper( new NLogLoggerFactory() );
            var info = target.ToDashboardInfosBabyDiapers( testvalue );
            info.Count.Should()
                .Be( 2 );
            info.ToList()[0].RwType.Should()
                .Be( RwType.Better );
            info.ToList()[0].InfoValue.Should()
                .Be( "12.12" );
            info.ToList()[0].InfoKey.Should()
                .Be( "Rewet - 140 ml" );
            info.ToList()[1].RwType.Should()
                .Be( RwType.Ok );
            info.ToList()[1].InfoValue.Should()
                .Be( "14.44" );
            info.ToList()[1].InfoKey.Should()
                .Be( "Rewet - 210 ml" );
        }

        /// <summary>
        ///     ToDashboardInfos test
        /// </summary>
        [Fact]
        public void ToDashboardInfosEmptyTest()
        {
            var target = new LaborDashboardHelper( new NLogLoggerFactory() );
            var infos = target.ToDashboardInfos( new List<TestValue>() );
            infos.Count.Should()
                 .Be( 0 );
        }

        /// <summary>
        ///     ToDashboardInfosIncontinencePad test AcquisitionTime
        /// </summary>
        [Fact]
        public void ToDashboardInfosIncontinencePadAcquisistionTimeTest()
        {
            var testvalue = new TestValue
            {
                ArticleTestType = ArticleType.IncontinencePad,
                IncontinencePadTestValue = new IncontinencePadTestValue

                {
                    TestType = TestTypeIncontinencePad.AcquisitionTimeAndRewet,
                    AcquisitionTimeFirst = 13.123,
                    AcquisitionTimeFirstRw = RwType.Better,
                    AcquisitionTimeSecond = 14.117,
                    AcquisitionTimeSecondRw = RwType.Better,
                    AcquisitionTimeThird = 15.118,
                    AcquisitionTimeThirdRw = RwType.Better,
                    RewetAfterAcquisitionTimeWeightDifference = 16.119,
                    RewetAfterAcquisitionTimeRw = RwType.Better
                }
            };
            var target = new LaborDashboardHelper( new NLogLoggerFactory() );
            var info = target.ToDashboardInfosIncontinencePad( testvalue );
            info.Count.Should()
                .Be( 4 );
        
            info.ToList()[0].RwType.Should()
                .Be( RwType.Better );
            info.ToList()[0].InfoKey.Should()
                .Be( "Aquisitionszeit - Zugabe 1" );
            info.ToList()[0].InfoValue.Should()
                .Be( "13.12" );
            info.ToList()[1].RwType.Should()
                .Be( RwType.Better );
            info.ToList()[1].InfoKey.Should()
                .Be( "Aquisitionszeit - Zugabe 2" );
            info.ToList()[1].InfoValue.Should()
                .Be( "14.12" );
            info.ToList()[2].RwType.Should()
                .Be( RwType.Better );
            info.ToList()[2].InfoKey.Should()
                .Be( "Aquisitionszeit - Zugabe 3" );
            info.ToList()[2].InfoValue.Should()
                .Be( "15.12" );
            info.ToList()[3].RwType.Should()
                .Be( RwType.Better );
            info.ToList()[3].InfoValue.Should()
                .Be( "16.12" );
            info.ToList()[3].InfoKey.Should()
                .Be( "Rewet nach Aquisition" );
        }

        /// <summary>
        ///     ToDashboardInfosIncontinencePad test retention
        /// </summary>
        [Fact]
        public void ToDashboardInfosIncontinencePadRetentionTest()
        {
            var testvalue = new TestValue
            {
                ArticleTestType = ArticleType.IncontinencePad,
                IncontinencePadTestValue = new IncontinencePadTestValue

                {
                    TestType = TestTypeIncontinencePad.Retention,
                    RetentionRw = RwType.Ok,
                    RetentionEndValue = 12
                }
            };
            var target = new LaborDashboardHelper( new NLogLoggerFactory() );
            var info = target.ToDashboardInfosIncontinencePad( testvalue );
            info.Count.Should()
                .Be( 1 );
            info.ToList()[0].RwType.Should()
                .Be( RwType.Ok );
            info.ToList()[0].InfoKey.Should()
                .Be( "Retention" );
            info.ToList()[0].InfoValue.Should()
                .Be( "12" );
        }

        /// <summary>
        ///     ToDashboardInfosIncontinencePad test rewet
        /// </summary>
        [Fact]
        public void ToDashboardInfosIncontinencePadRewetTest()
        {
            var testvalue = new TestValue
            {
                ArticleTestType = ArticleType.IncontinencePad,
                IncontinencePadTestValue = new IncontinencePadTestValue

                {
                    TestType = TestTypeIncontinencePad.RewetFree,
                    RewetFreeRw = RwType.Better,
                    RewetFreeDifference = 12.123
                }
            };
            var target = new LaborDashboardHelper( new NLogLoggerFactory() );
            var info = target.ToDashboardInfosIncontinencePad( testvalue );
            info.Count.Should()
                .Be( 1 );
            info.ToList()[0].RwType.Should()
                .Be( RwType.Better );
            info.ToList()[0].InfoKey.Should()
                .Be( "Rewet" );
            info.ToList()[0].InfoValue.Should()
                .Be( "12.12" );
        }

        /// <summary>
        ///     ToDashboardNote test
        /// </summary>
        [Fact]
        public void ToDashboardNoteEmptyTest()
        {
            var target = new LaborDashboardHelper( new NLogLoggerFactory() );
            var note = target.ToDashboardNote( new List<TestValue>() );
            note.Count.Should()
                .Be( 0 );
        }

        /// <summary>
        ///     ToProductionOrderItems test
        /// </summary>
        [Fact]
        public void ToProductionOrderItemsTest()
        {
            var target = new LaborDashboardHelper( new NLogLoggerFactory() );
            var po = target.ToProductionOrderItems( new List<TestSheet> {new TestSheet(),new TestSheet()} );
            po.Count.Should()
              .Be( 2 );
        }

        /// <summary>
        ///     ToProductionOrderItem test
        /// </summary>
        [Fact]
        public void ToProductionOrderItemEmptyTest()
        {
            var target = new LaborDashboardHelper( new NLogLoggerFactory() );
            var po = target.ToProductionOrderItem( new TestSheet() );
            po.RwType.Should()
              .Be( RwType.Ok );
            po.HasNotes.Should()
              .BeFalse( "because its empty" );
            po.Action.Should()
              .Be( "Edit" );
            po.Controller.Should()
              .Be("LaborCreatorBaby");
            po.DashboardInfos.Should()
              .BeNullOrEmpty( "because it is empty" );
            po.Notes.Should()
              .BeNullOrEmpty("because it is empty");
            po.ProductionOrderName.Should()
              .BeNullOrEmpty( "because it is empty" );
            po.SheetId.Should()
              .Be( 0 );
        }

        /// <summary>
        ///     ToRwTypeAll test
        /// </summary>
        [Fact]
        public void ToRwTypeAllTest()
        {
            var target = new LaborDashboardHelper( new NLogLoggerFactory() );
            target.ToRwTypeAll( null )
                  .Should()
                  .Be( RwType.Ok );
        }
    }
}