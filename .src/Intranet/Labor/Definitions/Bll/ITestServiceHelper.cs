#region Usings

using System;
using System.Collections.Generic;
using Intranet.Labor.Model.labor;
using Intranet.Labor.ViewModel;

#endregion

namespace Intranet.Labor.Definition
{
    /// <summary>
    ///     The Interface for the baby diaper service helper
    /// </summary>
    public interface ITestServiceHelper
    {
        /// <summary>
        ///     Creates a new testvalue initialized with following values
        /// </summary>
        /// <param name="testSheetId">ID of the testsheet</param>
        /// <param name="testPerson">the Testperson</param>
        /// <param name="productionCodeDay">the production-day of the diaper</param>
        /// <param name="notes">the notes for the testvalue</param>
        /// <returns>the created testvalue</returns>
        TestValue CreateNewTestValue( Int32 testSheetId, String testPerson, Int32 productionCodeDay, IList<TestNote> notes );

        /// <summary>
        ///     Creates the Production code string from the testsheet
        /// </summary>
        /// <param name="testSheet">the testSheet</param>
        /// <returns>the production code</returns>
        String CreateProductionCode( TestSheet testSheet );
    }
}