using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Extend;
using Intranet.Common;
using Intranet.Labor.Bll;
using Intranet.Labor.Model;
using Intranet.Labor.Model.labor;
using Intranet.Labor.ViewModel;

namespace Intranet.Web.Areas.Labor.Controllers
{
    /// <summary>
    ///     Class representing the labor creator service
    /// </summary>
    public class LaborCreatorService : ServiceBase, ILaborCreatorService
    {
        #region Properties

        /// <summary>
        ///     Gets or sets the Labor creator bll
        /// </summary>
        /// <value>the labor creator bll</value>
        public ILaborCreatorBll LaborCreatorBll { get; set; }

        #endregion

        #region Ctor

        /// <summary>
        ///     Initialize a new instance of the <see cref="ServiceBase" /> class.
        /// </summary>
        /// <param name="loggerFactory">A <see cref="ILoggerFactory" />.</param>
        public LaborCreatorService( ILoggerFactory loggerFactory )
            : base( loggerFactory.CreateLogger( typeof(LaborCreatorService) ) )
        {
        }

        #endregion

        #region Implementation of ILaborCreatorService

        /// <summary>
        ///     Get the labor creator view model for a specific id.
        /// </summary>
        /// <returns>the labor creator view model</returns>
        public LaborCreatorViewModel GetLaborCreatorViewModel( Int32 testSheetId )
        {
            var testSheet = LaborCreatorBll.getTestSheetForId( testSheetId );
            var babydiaper = testSheet.TestValues.ToList()
                                      .Where( x => x.ArticleTestType== ArticleType.BabyDiaper  );
            var rewets = ToRewetTestValuesCollection( babydiaper );

            var vm = new LaborCreatorViewModel
            {
                Producer = "Intigena",
                Shift = testSheet.ShiftType.ToFriendlyString(),
                FaNr = testSheet.FaNr,
                ProductName = testSheet.ProductName,
                SizeName = testSheet.SizeName,
                CreatedDate = testSheet.CreatedDateTime.ToShortDateString(),
                Rewets = rewets
            };
            return vm;
        }

        private ICollection<RewetTestValue> ToRewetTestValuesCollection( IEnumerable<TestValue> testValue )
        {
            var rewets = testValue.ToList()
                                  .Where( x => x.TestValueType == TestValueType.Single && (x.BabyDiaperTestValue.TestType == TestTypeBabyDiaper.Rewet  ||  x.BabyDiaperTestValue.TestType==TestTypeBabyDiaper.PenetrationTime)).ForEach( x => ToRewetTestValue( x.BabyDiaperTestValue, x.LastEditedPerson) );
            
        }

        private RewetTestValue ToRewetTestValue(BabyDiaperTestValue rewet,String testPerson )
        {
            var vm = new RewetTestValue
            {
                TestInfo = new TestInfo
                {
                    TestPerson = testPerson,
                    ProductionCode = rewet.
                    WeightyDiaperDry = rewet
                },
                Rewet = new Rewet()
            }
            return null;
        }

        #endregion

        #region Implementation of ILaborCreatorService

        #endregion
    }
}