using System.Collections.Generic;
using System.Linq;
using Intranet.Common;
using Intranet.Labor.Definition.Bll;
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
        ///     Gets or sets the Repository for TestSheets
        /// </summary>
        /// <value>the repository for testsheets</value>
        public IGenericRepository<TestSheet> TestSheets { get; set; }

        /// <summary>
        ///     Gets or sets the shifthelper
        /// </summary>
        /// <value>the shifthelper</value>
        public IShiftHelper ShiftHelper { get; set; }

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
            var shifts = ShiftHelper.GetLastXShiftSchedule( 4 );

            var testsheets =
                TestSheets.GetAll()
                          .ToList();
            var lastthreesheets = new List<TestSheet>();
            testsheets.ForEach( sheet =>
                                {
                                    if ( ShiftHelper.DateExistsInShifts( sheet.CreatedDateTime, shifts ) )
                                        lastthreesheets.Add( sheet );
                                } );
            return lastthreesheets;
        }

        #endregion
    }
}