using System;
using System.Collections.Generic;
using Intranet.Labor.Model;
using Intranet.Labor.Model.labor;
using Error = Intranet.Labor.Model.Error; 

namespace Intranet.Labor.Definition
{
    /// <summary>
    ///     Interface representing the bll for the babydiapers value edit/create
    /// </summary>
    public interface IBabyDiaperBll
    {

        /// <summary>
        ///     Query for a testvalue
        /// </summary>
        /// <param name="retentionTestId">The ID of the testvalue</param>
        /// <returns>The test value with the given Id</returns>
        TestValue GetTestValue(Int32 retentionTestId);

        /// <summary>
        ///     Query for the Test sheet info
        /// </summary>
        /// <param name="testSheetId">The ID of the test sheet</param>
        /// <returns>The test sheet</returns>
        TestSheet GetTestSheetInfo(Int32 testSheetId);

        /// <summary>
        ///     Query for all Error Codes
        /// </summary>
        /// <returns>Collection of all Error Codes</returns>
        IEnumerable<Error> GetAllNoteCodes();

        /// <summary>
        ///     Saves a new testvalue in the db
        /// </summary>
        /// <param name="testValue">the test value which will be saved</param>
        TestValue SaveNewTestValue( TestValue testValue );

        /// <summary>
        ///     update an testvalue
        /// </summary>
        /// <param name="testValue">the testvalue which will be updated</param>
        TestValue UpdateTestValue(TestValue testValue);

        /// <summary>
        ///     Updates the TestSheet
        /// </summary>
        Int32 UpdateTestSheet();

        /// <summary>
        ///     Query for the ProductionOrder
        /// </summary>
        /// <param name="productionOrderFa">the Id of the Production order</param>
        ProductionOrder GetProductionOrder(String productionOrderFa);

        /// <summary>
        ///     Delete Testvalue from DB
        /// </summary>
        /// <param name="testValueId">the Id of the Test value</param>
        TestValue DeleteTestValue(Int32 testValueId);
    }
}
