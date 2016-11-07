using System;
using System.Linq;
using Intranet.Common;
using Intranet.Labor.Bll;
using Intranet.Labor.Definition;
using Intranet.Labor.Model;
using Intranet.Labor.ViewModel;

namespace Intranet.Labor.Bll
{
    /// <summary>
    ///     Class representing the labor creator service
    /// </summary>
    public class BabyDiaperLaborCreatorService : ServiceBase, IBabyDiaperLaborCreatorService
    {
        #region Properties

        /// <summary>
        ///     Gets or sets the Labor creator bll
        /// </summary>
        /// <value>the labor creator bll</value>
        public IBabyDiaperLaborCreatorBll BabyDiaperLaborCreatorBll { get; set; }

        /// <summary>
        ///     Gets or sets the Helper for the Labor Creator Service
        /// </summary>
        /// <value>the helper for the Labor creator service</value>
        public IBabyDiaperLaborCreatorServiceHelper Helper { get; set; }

        #endregion

        #region Ctor

        /// <summary>
        ///     Initialize a new instance of the <see cref="ServiceBase" /> class.
        /// </summary>
        /// <param name="loggerFactory">A <see cref="ILoggerFactory" />.</param>
        public BabyDiaperLaborCreatorService( ILoggerFactory loggerFactory )
            : base( loggerFactory.CreateLogger( typeof(BabyDiaperLaborCreatorService) ) )
        {
        }

        #endregion

        /// <summary>
        ///     Get the labor creator view model for a specific id.
        /// </summary>
        /// <returns>the labor creator view model</returns>
        public LaborCreatorViewModel GetLaborCreatorViewModel( Int32 testSheetId )
        {
            var testSheet = BabyDiaperLaborCreatorBll.getTestSheetForId( testSheetId );
            var babydiaper = testSheet.TestValues.ToList()
                                      .Where( x => x.ArticleTestType == ArticleType.BabyDiaper )
                                      .ToList();
            var rewets = Helper.ToRewetTestValuesCollection( babydiaper );
            var rewetAverage = Helper.ToRewetAverage( babydiaper );
            var rewetStandardDeviation = Helper.ToRewetStandardDeviation( babydiaper );

            var retentions = Helper.ToRetentionTestValuesCollection( babydiaper );
            var retentionAverage = Helper.ToRetentionAverage( babydiaper );
            var retentionStandardDeviation = Helper.ToRetentionStandardDeviation( babydiaper );

            var penetrationTimes = Helper.ToPenetrationTimeTestValuesCollection( babydiaper );
            var penetrationTimeAverage = Helper.ToPenetrationTimeAverage( babydiaper );
            var penetrationTimeStandardDeviation = Helper.ToPenetrationTimeStandardDeviation( babydiaper );
            var vm = new LaborCreatorViewModel
            {
                Producer = "Intigena",
                Shift = testSheet.ShiftType.ToFriendlyString(),
                FaNr = testSheet.FaNr,
                ProductName = testSheet.ProductName,
                SizeName = testSheet.SizeName,
                CreatedDate = testSheet.CreatedDateTime.ToString("dd.MM.yyyy"),
                Rewets = rewets,
                RewetAverage = rewetAverage,
                RewetStandardDeviation = rewetStandardDeviation,
                Retentions = retentions,
                RetentionAverage = retentionAverage,
                RetentionStandardDeviation = retentionStandardDeviation,
                PenetrationTimes = penetrationTimes,
                PenetrationTimeStandardDeviation = penetrationTimeStandardDeviation,
                PenetrationTimeAverage = penetrationTimeAverage,
                WeightStandardDeviationAll = Helper.ComputeWeightStandardDeviationAll(babydiaper),
                WeigthAverageAll = Helper.ComputeWeightAverageAll(babydiaper)
            };
            return vm;
        }
    }
}