﻿using System;
using Intranet.Common;
using Intranet.Labor.Definition;
using Intranet.Labor.Model.labor;
using Intranet.Web.Areas.Labor.Controllers;

namespace Intranet.Labor.Bll
{
    /// <summary>
    ///     Class representing Labro creator bll
    /// </summary>
    public class LaborCreatorBll : ILaborCreatorBll
    {
        #region Implementation of ILaborCreatorBll

        /// <summary>
        ///     Repository fpr TestSheets
        /// </summary>
        public IGenericRepository<TestSheet> TestSheets { get; set; }

        /// <summary>
        ///     Gets the testsheet for the given id from the database
        /// </summary>
        /// <param name="id">the unique identifier for the test sheet</param>
        /// <returns>a testsheet with the given data, or null if not found</returns>
        public TestSheet getTestSheetForId( Int32 id ) => TestSheets.FindAsync( id ).Result;

        #endregion
    }
}