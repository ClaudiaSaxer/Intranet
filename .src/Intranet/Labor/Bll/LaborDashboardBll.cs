using System;
using System.Collections.Generic;
using System.Linq;
using Intranet.Common;
using Intranet.Labor.Definition;
using Intranet.Labor.Model;

namespace Intranet.Labor.Bll
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
        public ICollection<TestSheet> GetTestSheetForActualAndLastThreeShifts() => GetTestSheetForShifts( ShiftHelper.GetLastXShiftSchedule( 4 ) );

        /// <summary>
        ///     Get the testsheets for the last shift
        /// </summary>
        /// <returns>the testsheets for the last shift</returns>
        public ICollection<TestSheet> GetTestSheetForShiftPerMachineNr( ShiftSchedule shiftSchedule, String machineNr )
        {
            var sheets = new List<TestSheet>();

            TestSheets.GetAll()
                      .Where( sheet => sheet.MachineNr.Equals( machineNr ) )
                      .ToList()
                      .ForEach( sheet =>
                                {
                                    if ( ShiftHelper.DateExistsInShift( sheet.CreatedDateTime, shiftSchedule ) )
                                        sheets.Add( sheet );
                                } );
            return sheets;
        }

        /// <summary>
        ///     Get the testsheets for the last shift.
        ///     Current = 0, Minus 1 shift = 1
        /// </summary>
        /// <returns>the testsheets for the last shift</returns>
        public ICollection<TestSheet> GetTestSheetForMinusXShiftPerMachineNr( Int32 lastCounter, String machineNr )
        {
            var shift = ShiftHelper.GetLastXShiftSchedule( lastCounter + 1 );
            return shift.Count < lastCounter + 1 ? null : GetTestSheetForShiftPerMachineNr( shift[lastCounter], machineNr );
        }

        /// <summary>
        ///     Get the testsheets for the given shift
        /// </summary>
        /// <returns>the testsheets for the given shift</returns>
        public ICollection<TestSheet> GetTestSheetForShifts( List<ShiftSchedule> shifts )
        {
            var sheets = new List<TestSheet>();
            TestSheets.GetAll()
                      .ToList()
                      .ForEach( sheet =>
                                {
                                    if ( ( DateTime.Now.AddDays( -shifts.Count ) <= sheet.CreatedDateTime ) && ShiftHelper.DateExistsInShifts( sheet.CreatedDateTime, shifts ) )
                                        sheets.Add( sheet );
                                } );
            return sheets;
        }

        #endregion
    }
}