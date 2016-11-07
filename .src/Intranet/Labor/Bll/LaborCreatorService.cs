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
                ProductionOrders = new List<RunningProductionOrder>
                {
                    new RunningProductionOrder
                    {
                        ControllerName = "asdf",ActionName = "asf",PoId = 123,AreaName = "asf",PoName = "asgsyf"
                    }
                }
                
            };
        }
    }
}