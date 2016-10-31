
using Intranet.Common;
using Intranet.Common.Bll;
using Intranet.Labor.Definition.Bll;
using Intranet.Labor.ViewModel;

namespace Intranet.Bll
{
    /// <summary>
    /// 
    /// </summary>
    public class LaborHomeService : ServiceBase, ILaborHomeService
    {
        /// <summary>
        ///     Initialize a new instance of the <see cref="ServiceBase" /> class.
        /// </summary>
        /// <param name="logger">A <see cref="ILogger" />.</param>
        public LaborHomeService( ILogger loggerFactory)
            : base(loggerFactory.CreateLogger(typeof(LaborHomeService)))
        {
            Logger.Trace("Enter Ctor - Exit.");
        }

        #region Implementation of ILaborHomeService

        /// <summary>
        ///     Gets the LaborHomeViewModel
        /// </summary>
        /// <returns>The LaborHomeViewModel</returns>
        public LaborHomeViewModel GetLaborHomeViewModel()
        {
            throw new System.NotImplementedException();
        }

        #endregion
    }
}
