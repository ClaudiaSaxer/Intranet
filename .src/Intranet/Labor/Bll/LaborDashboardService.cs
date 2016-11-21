using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Intranet.Common;
using Intranet.Labor.Model;
using Intranet.Labor.Model.labor;
using Intranet.Labor.ViewModel;
using Intranet.Labor.ViewModel.LaborDashboard;

namespace Intranet.Web.Areas.Labor.Controllers
{
    /// <summary>
    ///     Class representing labor dashboard service
    /// </summary>
    public class LaborDashboardService : ServiceBase, ILaborDashboardService
    {
        #region Properties

        /// <summary>
        ///     Gets or sets the bll for the labor dashboard service
        /// </summary>
        /// <value>the bll</value>
        public ILaborDashboardBll LaborDashboardBll { get; set; }

        #endregion

        #region Ctor

        /// <summary>
        ///     Initialize a new instance of the <see cref="ServiceBase" /> class.
        /// </summary>
        /// <param name="loggerFactory">A <see cref="ILoggerFactory" />.</param>
        public LaborDashboardService( ILoggerFactory loggerFactory )
            : base( loggerFactory.CreateLogger( typeof(LaborDashboardService) ) )
        {
        }

        #endregion

        #region Implementation of ILaborDashboardService

        /// <summary>
        ///     Labor creator view model
        /// </summary>
        /// <returns>the LaborDashboardViewModel</returns>
        public LaborDashboardViewModel GetLaborDashboardViewModel()
        {
            var testsheets = LaborDashboardBll.GetTestSheetForActualAndLastThreeShifts();

            var items = new List<DashboardItem>();

            foreach ( var testSheet in testsheets )
            {
                var sheet = items.Find( item => item.MachineName.Equals( testSheet.MachineNr ) );
                if ( sheet == null )
                {
                    sheet = new DashboardItem
                    {
                        MachineName = testSheet.MachineNr,
                        ShiftItems =
                            new List<ShiftItem>
                            {
                                new ShiftItem { ProductionOrderItems = new List<ProductionOrderItem>() },
                                new ShiftItem { ProductionOrderItems = new List<ProductionOrderItem>() },
                                new ShiftItem { ProductionOrderItems = new List<ProductionOrderItem>() },
                                new ShiftItem { ProductionOrderItems = new List<ProductionOrderItem>() }
                            }
                    };
                    items.Add( sheet );
                }
                sheet.ShiftItems.ToList()[0].ProductionOrderItems.Add( new ProductionOrderItem
                                                                       {
                                                                           SheetId = testSheet.TestSheetId,
                                                                           HasNotes = testSheet.TestValues.ToList()
                                                                                               .Exists( value => value.TestValueNote.Count > 0 ),
                                                                           Notes = toDashboardNote( testSheet.TestValues )
                                                                           ,
                                                                           DashboardInfos = ToDashboardInfos( testSheet.TestValues ),
                                                                           ProductionOrderName = testSheet.FaNr,
                                                                           RwType = ToRwTypeAll( testSheet.TestValues.ToList() ),
                                                                           Action = "Edit",
                                                                           Controller = testSheet.ArticleType == ArticleType.BabyDiaper ? "LaborCreatorBaby" : "LaborCreatorInko"
                                                                       } );
            }
            return new LaborDashboardViewModel { DashboardItem = items };

            #endregion
        }

        private RwType CalcRwType( RwType before, RwType? rwType )
        {
            if ( rwType == null )
                return before;
            if ( ( before == RwType.Worse ) || ( rwType == RwType.Worse ) )
                return RwType.Worse;
            return before == RwType.SomethingWorse ? RwType.SomethingWorse : rwType.Value;
        }

        private DashboardInfo toDashboardInfo( String key, String value, RwType rw ) => new DashboardInfo { InfoValue = value, InfoKey = key, RwType = rw };

        private List<DashboardInfo> ToDashboardInfos( IEnumerable<TestValue> testValues )
        {
            var infos = new List<DashboardInfo>();
            foreach ( var testValue in testValues.Where( value => value.TestValueType == TestValueType.Average ) )
                infos.AddRange( testValue.ArticleTestType == ArticleType.BabyDiaper
                                    ? ToDashboardInfosBabyDiapers( testValue )
                                    : ToDashboardInfosIncontinencePad( testValue ) );

            return infos;
        }

        private List<DashboardInfo> ToDashboardInfosBabyDiapers( TestValue testValue )
        {
            var infos = new List<DashboardInfo>();
            switch ( testValue.BabyDiaperTestValue.TestType )
            {
                case TestTypeBabyDiaper.Rewet:
                    infos.Add( toDashboardInfo( "Rewet - 140 ml",
                                                Math.Round( testValue.BabyDiaperTestValue.Rewet140Value, 2 )
                                                    .ToString( CultureInfo.InvariantCulture ),
                                                testValue.BabyDiaperTestValue.Rewet140Rw.GetValueOrDefault() ) );
                    return infos;

                case TestTypeBabyDiaper.RewetAndPenetrationTime:
                    infos.Add( toDashboardInfo( "Rewet - 210 ml",
                                                Math.Round( testValue.BabyDiaperTestValue.Rewet210Value, 2 )
                                                    .ToString( CultureInfo.InvariantCulture ),
                                                testValue
                                                    .BabyDiaperTestValue.Rewet210Rw.GetValueOrDefault() ) );
                    infos.Add( toDashboardInfo( "Penetrationszeit - Zugabe 4",
                                                Math.Round( testValue.BabyDiaperTestValue.PenetrationTimeAdditionFourth, 2 )
                                                    .ToString( CultureInfo.InvariantCulture ),
                                                testValue.BabyDiaperTestValue.PenetrationRwType.GetValueOrDefault() ) );
                    return infos;

                case TestTypeBabyDiaper.Retention:
                    infos.Add( toDashboardInfo( "Retention - Nach Zentrifuge (g)",
                                                Math.Round( testValue.BabyDiaperTestValue.RetentionAfterZentrifugeValue, 2 )
                                                    .ToString( CultureInfo.InvariantCulture ),
                                                testValue.BabyDiaperTestValue.RetentionRw.GetValueOrDefault() ) );
                    return infos;
            }
            return infos;
        }

        private List<DashboardInfo> ToDashboardInfosIncontinencePad( TestValue testValue )
        {
            var infos = new List<DashboardInfo>();

            switch ( testValue.IncontinencePadTestValue.TestType )
            {
                case TestTypeIncontinencePad.Retention:

                    infos.Add( toDashboardInfo( "Retention",
                                                Math.Round( testValue.IncontinencePadTestValue.RetentionEndValue, 2 )
                                                    .ToString( CultureInfo.InvariantCulture ),
                                                testValue.IncontinencePadTestValue.RetentionRw ) );
                    return infos;

                case TestTypeIncontinencePad.RewetFree:
                    infos.Add( toDashboardInfo( "Rewet",
                                                Math.Round( testValue.IncontinencePadTestValue.RewetFreeDifference, 2 )
                                                    .ToString( CultureInfo.InvariantCulture ),
                                                testValue.IncontinencePadTestValue.RewetFreeRw ) );
                    return infos;
                case TestTypeIncontinencePad.AcquisitionTimeAndRewet:

                    infos.Add( toDashboardInfo( "Aquisitionszeit - Zugabe 1",
                                                Math.Round( testValue.IncontinencePadTestValue.AcquisitionTimeFirst, 2 )
                                                    .ToString( CultureInfo.InvariantCulture ),
                                                testValue.IncontinencePadTestValue.AcquisitionTimeFirstRw ) );
                    infos.Add( toDashboardInfo( "Aquisitionszeit - Zugabe 2",
                                                Math.Round( testValue.IncontinencePadTestValue.AcquisitionTimeSecond, 2 )
                                                    .ToString( CultureInfo.InvariantCulture ),
                                                testValue.IncontinencePadTestValue.AcquisitionTimeSecondRw ) );
                    infos.Add( toDashboardInfo( "Aquisitionszeit - Zugabe 3",
                                                Math.Round( testValue.IncontinencePadTestValue.AcquisitionTimeThird, 2 )
                                                    .ToString( CultureInfo.InvariantCulture ),
                                                testValue.IncontinencePadTestValue.AcquisitionTimeThirdRw ) );
                    infos.Add( toDashboardInfo( "Rewet nach Aquisition",
                                                Math.Round( testValue.IncontinencePadTestValue.RewetAfterAcquisitionTimeWeightDifference, 2 )
                                                    .ToString( CultureInfo.InvariantCulture ),
                                                testValue.IncontinencePadTestValue.RewetAfterAcquisitionTimeRw ) );
                    return infos;
            }

            return infos;
        }

        private List<DashboardNote> toDashboardNote( IEnumerable<TestValue> testValues )
        {
            var notes = new List<DashboardNote>();

            foreach ( var testValue in testValues.Where( testValue => testValue.TestValueNote.Count > 0 ) )
                notes.AddRange(
                    testValue.TestValueNote.Select(
                                 testValueNote =>
                                         new DashboardNote { ErrorMessage = testValueNote.Error.Value, Message = testValueNote.Message, Code = testValueNote.Error.ErrorCode } ) );

            return notes;
        }

        private RwType ToRwTypeAll( IEnumerable<TestValue> testValues )
        {
            var rwType = RwType.Ok;
            foreach (
                var testValue in
                testValues.Where( testValue => ( testValue.TestValueType == TestValueType.Average ) || ( testValue.TestValueType == TestValueType.StandardDeviation ) ) )
                if ( testValue.ArticleTestType == ArticleType.BabyDiaper )
                {
                    rwType = CalcRwType( rwType, testValue?.BabyDiaperTestValue?.RetentionRw );
                    rwType = CalcRwType( rwType, testValue?.BabyDiaperTestValue?.PenetrationRwType );
                    rwType = CalcRwType( rwType, testValue?.BabyDiaperTestValue?.Rewet140Rw );
                    rwType = CalcRwType( rwType, testValue?.BabyDiaperTestValue?.Rewet210Rw );
                    rwType = CalcRwType( rwType, testValue?.IncontinencePadTestValue?.RetentionRw );
                }
                else
                {
                    rwType = CalcRwType( rwType, testValue?.IncontinencePadTestValue?.AcquisitionTimeFirstRw );
                    rwType = CalcRwType( rwType, testValue?.IncontinencePadTestValue?.AcquisitionTimeSecondRw );
                    rwType = CalcRwType( rwType, testValue?.IncontinencePadTestValue?.AcquisitionTimeThirdRw );
                    rwType = CalcRwType( rwType, testValue?.IncontinencePadTestValue?.RewetAfterAcquisitionTimeRw );
                    rwType = CalcRwType( rwType, testValue?.IncontinencePadTestValue?.RewetFreeRw );
                }

            return rwType;
        }
    }
}