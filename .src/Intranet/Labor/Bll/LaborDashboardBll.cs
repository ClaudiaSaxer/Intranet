﻿using System;
using System.Collections.Generic;
using System.Linq;
using Intranet.Common;
using Intranet.Labor.Definition.Bll;
using Intranet.Labor.Model;
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

            var lastthreesheets = new List<TestSheet>();
            TestSheets.GetAll()
                      .ToList()
                      .ForEach( sheet =>
                                {
                                    if ( ShiftHelper.DateExistsInShifts( sheet.CreatedDateTime, shifts ) )
                                        lastthreesheets.Add( sheet );
                                } );
            return lastthreesheets;
        }

        /// <summary>
        ///     Get the testsheets for the last shift
        /// </summary>
        /// <returns>the testsheets for the last shift</returns>
        public ICollection<TestSheet> GetTestSheetForShiftPerMachineNr( ShiftSchedule shiftSchedule, String machineNr )
        {
            var sheets = new List<TestSheet>();

            TestSheets.GetAll().Where( sheet => sheet.MachineNr.Equals( machineNr ))
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
        public ICollection<TestSheet> GetTestSheetForMinusXShiftPerMachineNr( Int32 lastCounter,String machineNr )
        {
            var shift = ShiftHelper.GetLastXShiftSchedule( lastCounter+1 );
            return shift.Count < lastCounter+1 ? null : GetTestSheetForShiftPerMachineNr( shift[lastCounter] ,machineNr);
        }

        #endregion
    }
}