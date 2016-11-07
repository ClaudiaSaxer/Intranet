using System.Collections.Generic;
using System.Reflection;
using Intranet.Common;
using Intranet.Labor.ViewModel;
using Intranet.Labor.ViewModel.LaborCreator;

namespace Intranet.Web.Areas.Labor.Controllers
{
    /// <summary>
    ///     Class representing labor creator service
    /// </summary>
    public class LaborCreatorService : ServiceBase, ILaborCreatorService
    {
        /// <summary>
        /// Gets or sets the bll for the labor creator service
        /// </summary>
        /// <value>the bll</value>
        public ILaborCreatorBll LaborCreatorBll { get; set; }
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

        /// <summary>
        ///     Labor creator view model
        /// </summary>
        /// <returns>the LaborCreatorViewModel</returns>
        public LaborCreatorViewModel GetLaborCreatorViewModel()
        {
            return new LaborCreatorViewModel
            {
                ProductionOrders = LaborCreatorBll.RunningProductionOrders()
                
            };
        }
    }
}