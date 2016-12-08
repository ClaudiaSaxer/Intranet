#region Usings

using System;
using System.Collections.Generic;
using System.Linq;
using Extend;
using Intranet.Common;
using Intranet.Labor.Definition;
using Intranet.Labor.Model;
using Intranet.Labor.ViewModel;

#endregion

namespace Intranet.Labor.Bll
{
    /// <summary>
    ///     Class representing the history service
    /// </summary>
    public class HistoryService : ServiceBase, IHistoryService
    {
        #region Properties

        /// <summary>
        ///     Gets or sets the bll for the history.
        /// </summary>
        public IHistoryBll HistoryBll { get; set; }

        #endregion

        #region Ctor

        /// <summary>
        ///     Initialize a new instance of the <see cref="ServiceBase" /> class.
        /// </summary>
        /// <param name="loggerFactory">A <see cref="ILoggerFactory" />.</param>
        public HistoryService( ILoggerFactory loggerFactory )
            : base( loggerFactory.CreateLogger( typeof(HistoryService) ) )
        {
            Logger.Trace( "Enter Ctor - Exit." );
        }

        #endregion

        #region Implementation of IHistoryService

        /// <summary>
        ///     gets the HistoryViewModel
        /// </summary>
        /// <param name="faNr">the FaNr</param>
        /// <returns>The HistoryViewModel</returns>
        public HistoryViewModel GetHistoryViewModel( String faNr )
        {
            var viewModel = new HistoryViewModel
            {
                FaNr = faNr,
                Sheets = new List<HistoryItem>()
            };
            var dbTestSheets = HistoryBll.GetTestSheets( faNr );
            if ( dbTestSheets.IsNull() )
            {
                Logger.Error( "Datenbankproblem beim suchen nach Testsheets mit FaNr. " + faNr );
                viewModel.Message = "Es ist ein Problem aufgetreten. Bitte wenden Sie sich an ihren Administrator";
                return viewModel;
            }
            var testSheets = dbTestSheets.ToList();
            if ( !testSheets.Any() )
            {
                viewModel.Message = "Kein Eintrag mit Fertigunsnummer " + faNr + " gefunden.";
                return viewModel;
            }
            foreach ( var testSheet in testSheets )
            {
                var historyItem = new HistoryItem
                {
                    FaNr = testSheet.FaNr,
                    MachineNr = testSheet.MachineNr,
                    CreatedDateTime = testSheet.CreatedDateTime,
                    Shift = testSheet.ShiftType.ToFriendlyString(),
                    TestSheetId = testSheet.TestSheetId,
                    Action = "Edit"
                };
                if ( testSheet.ArticleType == ArticleType.BabyDiaper )
                {
                    historyItem.Controller = "LaborCreatorBaby";
                    historyItem.RwType = GetRwTypeBaby( testSheet );
                }
                else if ( testSheet.ArticleType == ArticleType.IncontinencePad )
                {
                    historyItem.Controller = "LaborCreatorInko";
                    historyItem.RwType = GetRwTypeInko( testSheet );
                }

                viewModel.Sheets.Add( historyItem );
            }

            return viewModel;
        }

        #endregion

        #region Private Methods

        private static RwType GetRwTypeBaby( TestSheet testSheet )
        {
            var result = RwType.Ok;
            foreach ( var babyDiaper in testSheet.TestValues.Where( testValue => testValue.TestValueType == TestValueType.Average )
                                                 .Select( testValue => testValue.BabyDiaperTestValue ) )
                switch ( babyDiaper.TestType )
                {
                    case TestTypeBabyDiaper.Retention:
                        if ( babyDiaper.RetentionRw == RwType.Worse )
                            return RwType.Worse;
                        if ( babyDiaper.RetentionRw == RwType.SomethingWorse )
                            result = RwType.SomethingWorse;
                        break;
                    case TestTypeBabyDiaper.Rewet:
                        if ( ( babyDiaper.Rewet140Rw == RwType.Worse ) || ( babyDiaper.Rewet210Rw == RwType.Worse ) )
                            return RwType.Worse;
                        if ( ( babyDiaper.Rewet140Rw == RwType.SomethingWorse ) || ( babyDiaper.Rewet210Rw == RwType.SomethingWorse ) )
                            result = RwType.SomethingWorse;
                        break;
                    case TestTypeBabyDiaper.RewetAndPenetrationTime:
                        if ( ( babyDiaper.Rewet140Rw == RwType.Worse ) || ( babyDiaper.Rewet210Rw == RwType.Worse ) || ( babyDiaper.PenetrationRwType == RwType.Worse ) )
                            return RwType.Worse;
                        if ( ( babyDiaper.Rewet140Rw == RwType.SomethingWorse ) || ( babyDiaper.Rewet210Rw == RwType.SomethingWorse )
                             || ( babyDiaper.PenetrationRwType == RwType.SomethingWorse ) )
                            result = RwType.SomethingWorse;
                        break;
                }

            return result;
        }

        private static RwType GetRwTypeInko( TestSheet testSheet )
        {
            var result = RwType.Ok;
            foreach ( var inko in testSheet.TestValues.Where( testValue => testValue.TestValueType == TestValueType.Average )
                                           .Select( testValue => testValue.IncontinencePadTestValue ) )
                switch ( inko.TestType )
                {
                    case TestTypeIncontinencePad.RewetFree:
                        if ( inko.RewetFreeRw == RwType.Worse )
                            return RwType.Worse;
                        if ( inko.RewetFreeRw == RwType.SomethingWorse )
                            result = RwType.SomethingWorse;
                        break;
                    case TestTypeIncontinencePad.Retention:
                        if ( inko.RetentionRw == RwType.Worse )
                            return RwType.Worse;
                        if ( inko.RetentionRw == RwType.SomethingWorse )
                            result = RwType.SomethingWorse;
                        break;
                    case TestTypeIncontinencePad.AcquisitionTimeAndRewet:
                        if ( ( inko.RewetAfterAcquisitionTimeRw == RwType.Worse ) || ( inko.AcquisitionTimeFirstRw == RwType.Worse )
                             || ( inko.AcquisitionTimeSecondRw == RwType.Worse ) || ( inko.AcquisitionTimeThirdRw == RwType.Worse ) )
                            return RwType.Worse;
                        if ( ( inko.RewetAfterAcquisitionTimeRw == RwType.SomethingWorse ) || ( inko.AcquisitionTimeFirstRw == RwType.SomethingWorse )
                             || ( inko.AcquisitionTimeSecondRw == RwType.SomethingWorse ) || ( inko.AcquisitionTimeThirdRw == RwType.SomethingWorse ) )
                            result = RwType.SomethingWorse;
                        break;
                }

            return result;
        }

        #endregion
    }
}