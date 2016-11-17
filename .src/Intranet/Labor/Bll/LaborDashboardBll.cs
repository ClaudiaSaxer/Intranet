using System;
using System.Collections.Generic;
using Intranet.Common;
using Intranet.Labor.Model.labor;

namespace Intranet.Web.Areas.Labor.Controllers
{
    /// <summary>
    ///     Class representing labor dashboard bll
    /// </summary>
    public class LaborDashboardBll : ServiceBase, ILaborDashboardBll
    {
        #region Properties

        /// <summary>
        ///     Repository fpr TestSheets
        /// </summary>
        public IGenericRepository<TestSheet> TestSheets { get; set; }

        #endregion

        #region Ctor

        /// <summary>
        ///     Initialize a new instance of the <see cref="ServiceBase" /> class.
        /// </summary>
        /// <param name="loggerFactory">A <see cref="ILoggerFactory" />.</param>
        public LaborDashboardBll( ILoggerFactory loggerFactory )
            : base( loggerFactory.CreateLogger( typeof(LaborDashboardBll) ) )
        {
        }

        #endregion

        #region Implementation of ILaborDashboardBll

        /// <summary>
        ///     Get the testsheets for the actual shift and the last 3 shifts
        /// </summary>
        /// <returns>the testsheets for the actual and last three shifts</returns>
        public ICollection<TestSheet> GetTestSheetForActualAndLastThreeShifts()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}