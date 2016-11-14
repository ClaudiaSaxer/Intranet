using System;
using System.Linq;
using Intranet.Common;
using Intranet.Labor.Definition;
using Intranet.Labor.Model;
using Intranet.Labor.ViewModel;

namespace Intranet.Labor.Bll
{
    /// <summary>
    ///     Class representing the labor creator service
    /// </summary>
    public class IncontinencePadLaborCreatorService : ServiceBase, IIncontinencePadLaborCreatorService
    {
        #region Properties

        /// <summary>
        ///     Gets or sets the Labor creator bll
        /// </summary>
        /// <value>the labor creator bll</value>
        public IIncontinencePadLaborCreatorBll IncontinencePadLaborCreatorBll { get; set; }

        /// <summary>
        ///     Gets or sets the Helper for the Labor Creator Service
        /// </summary>
        /// <value>the helper for the Labor creator service</value>
        public IIncontinencePadLaborCreatorServiceHelper Helper { get; set; }

        #endregion

        #region Ctor

        /// <summary>
        ///     Initialize a new instance of the <see cref="ServiceBase" /> class.
        /// </summary>
        /// <param name="loggerFactory">A <see cref="ILoggerFactory" />.</param>
        public IncontinencePadLaborCreatorService( ILoggerFactory loggerFactory )
            : base( loggerFactory.CreateLogger( typeof(IncontinencePadLaborCreatorService) ) )
        {
        }

        #endregion

        /// <summary>
        ///     Get the labor creator view model for a specific id.
        /// </summary>
        /// <returns>the labor creator view model</returns>
        public IncontinencePadLaborCreatorViewModel GetLaborCreatorViewModel( Int32 testSheetId )
        {
            try
            {
                var testSheet = IncontinencePadLaborCreatorBll.GetTestSheetForId( testSheetId );
                var incontinencePad = testSheet.TestValues.ToList()
                                               .Where( x => x.ArticleTestType == ArticleType.IncontinencePad )
                                               .ToList();
                var rewets = Helper.ToRewetTestValuesCollection( incontinencePad );
                var rewetAverage = Helper.ToRewetAverage( incontinencePad );
                var rewetStandardDeviation = Helper.ToRewetStandardDeviation( incontinencePad );

                var retentions = Helper.ToRetentionTestValuesCollection( incontinencePad );
                var retentionAverage = Helper.ToRetentionAverage( incontinencePad );
                var retentionStandardDeviation = Helper.ToRetentionStandardDeviation( incontinencePad );

                var acquisitionTimes = Helper.ToAcquisitionTimeTestValuesCollection( incontinencePad );
                var acquisitionTimeAverage = Helper.ToAcquisitionTimeAverage( incontinencePad );
                var acquisitionTimeStandardDeviation = Helper.ToAcquisitionTimeStandardDeviation( incontinencePad );

                var rewetAfterAcquisitionTimeAverage = Helper.ToRewetAfterAcquisitionTimeAverage( incontinencePad );
                var rewetAfterAcquisitionTimeStandardDeviation = Helper.ToRewetAfterAcquisitionTimeStandardDeviation( incontinencePad );

                var vm = new IncontinencePadLaborCreatorViewModel
                {
                    Producer = "Intigena",
                    Shift = testSheet.ShiftType.ToFriendlyString(),
                    FaNr = testSheet.FaNr,
                    ProductName = testSheet.ProductName,
                    SizeName = testSheet.SizeName,
                    CreatedDate = testSheet.CreatedDateTime.ToString( "dd.MM.yyyy" ),
                    Rewets = rewets,
                    RewetAverage = rewetAverage,
                    RewetStandardDeviation = rewetStandardDeviation,
                    Retentions = retentions,
                    RetentionAverage = retentionAverage,
                    RetentionStandardDeviation = retentionStandardDeviation,
                    AcquisitionTimes = acquisitionTimes,
                    AcquisitionTimeStandardDeviation = acquisitionTimeStandardDeviation,
                    AcquisitionTimeAverage = acquisitionTimeAverage,
                    RewetAfterAcquisitionTimeAverage = rewetAfterAcquisitionTimeAverage,
                    RewetAfterAcquisitionTimeStandardDeviation = rewetAfterAcquisitionTimeStandardDeviation
                };
                return vm;
            }
            catch ( Exception exception )
            {
                Logger.Error( "a exception acured: " + exception.Message );
                return null;
            }
        }
    }
}