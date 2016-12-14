using System;
using System.Collections.Generic;
using Intranet.Labor.Model;

namespace Intranet.Labor.Definition
{
    /// <summary>
    ///     Interface representing the bll for for all tests edit/create
    /// </summary>
    public interface ITestBll
    {
        /// <summary>
        ///     Delete TestvalueNote from DB
        /// </summary>
        /// <param name="testValueNoteId">the Id of the Test value Note</param>
        TestValueNote DeleteNote( Int32 testValueNoteId );

        /// <summary>
        ///     Delete Testvalue from DB
        /// </summary>
        /// <param name="testValueId">the Id of the Test value</param>
        TestValue DeleteTestValue( Int32 testValueId );

        /// <summary>
        ///     Query for all Error Codes
        /// </summary>
        /// <returns>Collection of all Error Codes</returns>
        IEnumerable<Error> GetAllNoteCodes();

        /// <summary>
        ///     Query for the ProductionOrder
        /// </summary>
        /// <param name="productionOrderFa">the Id of the Production order</param>
        ProductionOrder GetProductionOrder( String productionOrderFa );

        /// <summary>
        ///     Query for the Test sheet info
        /// </summary>
        /// <param name="testSheetId">The ID of the test sheet</param>
        /// <returns>The test sheet</returns>
        TestSheet GetTestSheetInfo( Int32 testSheetId );

        /// <summary>
        ///     Query for a testvalue
        /// </summary>
        /// <param name="retentionTestId">The ID of the testvalue</param>
        /// <returns>The test value with the given Id</returns>
        TestValue GetTestValue( Int32 retentionTestId );

        /// <summary>
        ///     Saves a new testvalue in the db
        /// </summary>
        /// <param name="testValue">the test value which will be saved</param>
        TestValue SaveNewTestValue( TestValue testValue );

        /// <summary>
        ///     Updates the TestSheet
        /// </summary>
        Int32 UpdateTestSheet();

        /// <summary>
        ///     update an testvalue
        /// </summary>
        /// <param name="testValue">the testvalue which will be updated</param>
        TestValue UpdateTestValue( TestValue testValue );
    }
}