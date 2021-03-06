﻿#region Usings

using System;
using System.Linq;
using Intranet.Common;
using Intranet.Labor.Definition;
using Intranet.Labor.Model;
using Intranet.Labor.ViewModel;

#endregion

namespace Intranet.Labor.Bll
{
    /// <summary>
    ///     Class representing the babydiaper labor creator service
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
        ///     Get the babydiaper labor creator view model for a specific id.
        /// </summary>
        /// <returns>the labor creator view model</returns>
        public BabyDiaperLaborCreatorViewModel GetLaborCreatorViewModel( Int32 testSheetId )
        {
            try
            {
                var testSheet = BabyDiaperLaborCreatorBll.GetTestSheetForId( testSheetId );
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

                var canEdit = Helper.CanUserEdit();
                var vm = new BabyDiaperLaborCreatorViewModel
                {
                    Producer = "Intigena",
                    Shift = testSheet.ShiftType.ToFriendlyString(),
                    FaNr = testSheet.FaNr,
                    ProductName = testSheet.ProductName,
                    SizeName = testSheet.SizeName,
                    CreatedDate = testSheet.CreatedDateTime.ToString( "dd.MM.yyyy" ),
                    Rewets = rewets,
                    BabyDiaperRewetAverage = rewetAverage,
                    BabyDiaperRewetStandardDeviation = rewetStandardDeviation,
                    Retentions = retentions,
                    BabyDiaperRetentionAverage = retentionAverage,
                    BabyDiaperRetentionStandardDeviation = retentionStandardDeviation,
                    PenetrationTimes = penetrationTimes,
                    BabyDiaperPenetrationTimeStandardDeviation = penetrationTimeStandardDeviation,
                    BabyDiaperPenetrationTimeAverage = penetrationTimeAverage,
                    WeightStandardDeviationAll = Helper.ComputeWeightStandardDeviationAll( babydiaper ),
                    WeigthAverageAll = Helper.ComputeWeightAverageAll( babydiaper ),
                    TestSheetId = testSheetId,
                    CanEdit = canEdit
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