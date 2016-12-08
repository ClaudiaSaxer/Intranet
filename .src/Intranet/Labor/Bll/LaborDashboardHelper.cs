using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using Extend;
using Intranet.Common;
using Intranet.Labor.Definition;
using Intranet.Labor.Model;
using Intranet.Labor.ViewModel;

namespace Intranet.Labor.Bll
{
    /// <summary>
    ///     Class representing labor dashboard helper
    /// </summary>
    public class LaborDashboardHelper : ServiceBase, ILaborDashboardHelper
    {
        #region Ctor

        /// <summary>
        ///     Initialize a new instance of the <see cref="ServiceBase" /> class.
        /// </summary>
        /// <param name="loggerFactory">A <see cref="ILoggerFactory" />.</param>
        public LaborDashboardHelper( ILoggerFactory loggerFactory )
            : base( loggerFactory.CreateLogger( typeof(LaborDashboardHelper) ) )
        {
        }

        #endregion

        /// <summary>
        ///     Calculates the rwtype to get the more important of the before or actual
        /// </summary>
        /// <param name="before">the rwtype before</param>
        /// <param name="rwType">the rwtype at the moment</param>
        /// <returns>the rwtype that is more important</returns>
        public RwType CalcRwType( RwType before, RwType? rwType )
        {
            if ( rwType == null )
                return before;
            if ( ( before == RwType.Worse ) || ( rwType == RwType.Worse ) )
                return RwType.Worse;
            return before == RwType.SomethingWorse ? RwType.SomethingWorse : rwType.Value;
        }

        /// <summary>
        ///     Generates Dashboard infos with the given testvalues
        /// </summary>
        /// <param name="testValues">the testvalues containing infos</param>
        /// <returns>a list of dashboard infos with the information from the given testvalues</returns>
        public List<DashboardInfo> ToDashboardInfos( IEnumerable<TestValue> testValues )
        {
            var infos = new List<DashboardInfo>();
            foreach ( var testValue in testValues.Where( value => value.TestValueType == TestValueType.Average ) )
                infos.AddRange( testValue.ArticleTestType == ArticleType.BabyDiaper
                                    ? ToDashboardInfosBabyDiapers( testValue )
                                    : ToDashboardInfosIncontinencePad( testValue ) );

            return infos;
        }

        /// <summary>
        ///     generates a list of dashboard infos which are from the testvalue of baby diaper
        /// </summary>
        /// <param name="testValue">the testvalues containing baby diapers test value</param>
        /// <returns>a list of dashboard infos from the testvalue</returns>
        public List<DashboardInfo> ToDashboardInfosBabyDiapers( TestValue testValue )
        {
            var infos = new List<DashboardInfo>();
            if ( ( testValue.BabyDiaperTestValue.TestType == TestTypeBabyDiaper.Rewet ) || ( testValue.BabyDiaperTestValue.TestType == TestTypeBabyDiaper.RewetAndPenetrationTime ) )
            {
                infos.Add( ToDashboardInfo( "Rewet - 140 ml",
                                            Round( testValue.BabyDiaperTestValue.Rewet140Value )
                                                .ToString( CultureInfo.InvariantCulture ),
                                            testValue.BabyDiaperTestValue.Rewet140Rw.GetValueOrDefault() ) );
                infos.Add( ToDashboardInfo( "Rewet - 210 ml",
                                            Round( testValue.BabyDiaperTestValue.Rewet210Value )
                                                .ToString( CultureInfo.InvariantCulture ),
                                            testValue
                                                .BabyDiaperTestValue.Rewet210Rw.GetValueOrDefault() ) );
                if ( testValue.BabyDiaperTestValue.TestType == TestTypeBabyDiaper.RewetAndPenetrationTime )
                    infos.Add( ToDashboardInfo( "Penetrationszeit - Zugabe 4",
                                                Round( testValue.BabyDiaperTestValue.PenetrationTimeAdditionFourth )
                                                    .ToString( CultureInfo.InvariantCulture ),
                                                testValue.BabyDiaperTestValue.PenetrationRwType.GetValueOrDefault() ) );
            }
            else if ( testValue.BabyDiaperTestValue.TestType == TestTypeBabyDiaper.Retention )
            {
                infos.Add( ToDashboardInfo( "Retention - Nach Zentrifuge (g)",
                                            Round( testValue.BabyDiaperTestValue.RetentionAfterZentrifugeValue )
                                                .ToString( CultureInfo.InvariantCulture ),
                                            testValue.BabyDiaperTestValue.RetentionRw.GetValueOrDefault() ) );
            }

            return infos;
        }

        /// <summary>
        ///     generates a list of dashboard infos which are from the testvalue of  incontinenec pad
        /// </summary>
        /// <param name="testValue">the testvalues containing incontinence pod test value</param>
        /// <returns>a list of dashboard infos from the testvalue</returns>
        public List<DashboardInfo> ToDashboardInfosIncontinencePad( TestValue testValue )
        {
            var infos = new List<DashboardInfo>();

            switch ( testValue.IncontinencePadTestValue.TestType )
            {
                case TestTypeIncontinencePad.Retention:

                    infos.Add( ToDashboardInfo( "Retention",
                                                Round( testValue.IncontinencePadTestValue.RetentionEndValue )
                                                    .ToString( CultureInfo.InvariantCulture ),
                                                testValue.IncontinencePadTestValue.RetentionRw ) );
                    return infos;

                case TestTypeIncontinencePad.RewetFree:
                    infos.Add( ToDashboardInfo( "Rewet",
                                                Round( testValue.IncontinencePadTestValue.RewetFreeDifference )
                                                    .ToString( CultureInfo.InvariantCulture ),
                                                testValue.IncontinencePadTestValue.RewetFreeRw ) );
                    return infos;
                case TestTypeIncontinencePad.AcquisitionTimeAndRewet:

                    infos.Add( ToDashboardInfo( "Aquisitionszeit - Zugabe 1",
                                                Round( testValue.IncontinencePadTestValue.AcquisitionTimeFirst )
                                                    .ToString( CultureInfo.InvariantCulture ),
                                                testValue.IncontinencePadTestValue.AcquisitionTimeFirstRw ) );
                    infos.Add( ToDashboardInfo( "Aquisitionszeit - Zugabe 2",
                                                Round( testValue.IncontinencePadTestValue.AcquisitionTimeSecond )
                                                    .ToString( CultureInfo.InvariantCulture ),
                                                testValue.IncontinencePadTestValue.AcquisitionTimeSecondRw ) );
                    infos.Add( ToDashboardInfo( "Aquisitionszeit - Zugabe 3",
                                                Round( testValue.IncontinencePadTestValue.AcquisitionTimeThird )
                                                    .ToString( CultureInfo.InvariantCulture ),
                                                testValue.IncontinencePadTestValue.AcquisitionTimeThirdRw ) );
                    infos.Add( ToDashboardInfo( "Rewet nach Aquisition",
                                                Round( testValue.IncontinencePadTestValue.RewetAfterAcquisitionTimeWeightDifference )
                                                    .ToString( CultureInfo.InvariantCulture ),
                                                testValue.IncontinencePadTestValue.RewetAfterAcquisitionTimeRw ) );
                    return infos;
            }

            return infos;
        }

        /// <summary>
        ///     Generates a list od dashboard notes with the information from the given test values
        /// </summary>
        /// <param name="testValues">a list of testvalues containing attributes for the notes</param>
        /// <returns></returns>
        public List<DashboardNote> ToDashboardNote( IEnumerable<TestValue> testValues )
        {
            var notes = new List<DashboardNote>();

            foreach ( var testValue in testValues.Where( testValue => testValue.TestValueNote.Count > 0 ) )
                notes.AddRange(
                    testValue.TestValueNote.Select(
                                 testValueNote =>
                                         new DashboardNote { ErrorMessage = testValueNote.Error.Value, Message = testValueNote.Message, Code = testValueNote.Error.ErrorCode } ) );

            return notes;
        }

        /// <summary>
        ///     Creates a productionOrder item with the values from the testsheet
        /// </summary>
        /// <param name="testSheet">the testsheet</param>
        /// <returns>a production order item</returns>
        public ProductionOrderItem ToProductionOrderItem( TestSheet testSheet )
            => new ProductionOrderItem
            {
                SheetId = testSheet.TestSheetId,
                HasNotes = testSheet.TestValues?.ToList()
                                    .Exists( value => value.TestValueNote.Count > 0 ) ?? false,
                Notes = testSheet.TestValues != null ? ToDashboardNote( testSheet.TestValues ) : null,
                DashboardInfos = testSheet.TestValues != null ? ToDashboardInfos( testSheet.TestValues ) : null,
                ProductionOrderName = testSheet.FaNr,
                RwType = testSheet.TestValues != null ? ToRwTypeAll( testSheet.TestValues.ToList() ) : RwType.Ok,
                Action = "Edit",
                Controller = testSheet.ArticleType == ArticleType.BabyDiaper ? "LaborCreatorBaby" : "LaborCreatorInko"
            };

        /// <summary>
        ///     Creates a list of production order items from a collection of test sheets
        /// </summary>
        /// <param name="testSheets">a collection of test sheets</param>
        /// <returns></returns>
        public ICollection<ProductionOrderItem> ToProductionOrderItems( ICollection<TestSheet> testSheets )
        {
            var productionorderitems = new Collection<ProductionOrderItem>();
            testSheets?.ForEach( x => productionorderitems.Add( ToProductionOrderItem( x ) ) );
            return productionorderitems;
        }

        /// <summary>
        ///     collectes all rw data from the testvalues to a production order and calculates the most important one for the whole
        /// </summary>
        /// <param name="testValues">a list of testvalues</param>
        /// <returns>a rw type representing the whole lot of testvalues</returns>
        public RwType ToRwTypeAll( IEnumerable<TestValue> testValues )
        {
            var rwType = RwType.Ok;
            if ( testValues != null )
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

        /// <summary>
        ///     Round double to value to show on viewmodel
        /// </summary>
        /// <param name="value">the double before round</param>
        /// <returns>the double after Round</returns>
        private static Double Round( Double value ) => Math.Round( value, 2 );

        /// <summary>
        ///     Generates a Dashboard Info with the given param values
        /// </summary>
        /// <param name="key">the key for the info as a explaining text</param>
        /// <param name="value">the value for the info</param>
        /// <param name="rw">the rw type for the info</param>
        /// <returns></returns>
        private DashboardInfo ToDashboardInfo( String key, String value, RwType rw ) => new DashboardInfo { InfoValue = value, InfoKey = key, RwType = rw };
    }
}