using System;
using System.Collections.Generic;
using System.Linq;
using Intranet.Common;
using Intranet.Labor.Definition;
using Intranet.Labor.Model;

namespace Intranet.Labor.Bll
{
    /// <summary>
    ///     Class representing the bll of the history
    /// </summary>
    public class HistoryBll : IHistoryBll
    {
        #region Properties

        /// <summary>
        ///     TestSheetRepository
        /// </summary>
        public IGenericRepository<TestSheet> TestSheetRepository { get; set; }

        #endregion

        #region Implementation of IHistoryBll

        /// <summary>
        ///     Gets all testsheet with the given faNr from the database
        /// </summary>
        /// <param name="faNr">the faNr for the testsheets</param>
        /// <returns>a list of testsheets</returns>
        public IEnumerable<TestSheet> GetTestSheets( String faNr ) => TestSheetRepository.GetAll()
                                                                                         .Where( ts => ts.FaNr.Equals( faNr ) )
                                                                                         .ToList();

        #endregion
    }
}