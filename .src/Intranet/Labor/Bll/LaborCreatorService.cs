using System;
using System.Globalization;
using Intranet.Common;
using Intranet.Labor.Bll;
using Intranet.Labor.Model;
using Intranet.Labor.Model.labor;
using Intranet.Labor.ViewModel;

namespace Intranet.Web.Areas.Labor.Controllers
{
    /// <summary>
    /// Class representing the labor creator service
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
            //   TestSheet = LaborCreatorBll.getTestSheetForId( testSheetId )

            var vm = new LaborCreatorViewModel
            {
                Producer = "Intigena",
                Shift =  ShiftType.Morning.ToFriendlyString(),
                FaNr = "Fa123",
                ProductName = "Babydream",
                SizeName = "Maxi-Plus",
                CreatedDate= "12.12.2015"
               
            };
            return vm;
        }

        #endregion

        #region Implementation of ILaborCreatorService

        #endregion
    }
}