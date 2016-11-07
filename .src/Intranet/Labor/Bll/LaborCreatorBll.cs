using System.Collections.Generic;
using Intranet.Common;
using Intranet.Labor.Model;
using Intranet.Labor.ViewModel;
using Intranet.Labor.ViewModel.LaborCreator;

namespace Intranet.Web.Areas.Labor.Controllers
{
    /// <summary>
    ///     Class representing labor creator bll
    /// </summary>
    public class LaborCreatorBll : ServiceBase, ILaborCreatorBll
    {

        /// <summary>
        /// Gets or sets the repository for the production order
        /// </summary>
        /// <value>the production order repository</value>
        public IGenericRepository<ProductionOrder> ProductionOrderRepository { get; set; }


        #region Ctor

        /// <summary>
        ///     Initialize a new instance of the <see cref="ServiceBase" /> class.
        /// </summary>
        /// <param name="loggerFactory">A <see cref="ILoggerFactory" />.</param>
        public LaborCreatorBll( ILoggerFactory loggerFactory )
            : base( loggerFactory.CreateLogger( typeof(LaborCreatorBll) ) )
        {
        }

        #endregion

        #region Implementation of ILaborCreatorBll

        /// <summary>
        /// The Production Orders which are running at the moment
        /// </summary>
        /// <returns>the running production orders</returns>
        public ICollection<RunningProductionOrder> RunningProductionOrders()
        {

            throw new System.NotImplementedException();
        }

        #endregion
    }
}