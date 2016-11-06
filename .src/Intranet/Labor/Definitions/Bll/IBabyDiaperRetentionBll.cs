using System;
using System.Collections.Generic;
using Intranet.Labor.Model.labor;
using Error = Intranet.Labor.Model.Error; 

namespace Intranet.Labor.Definition
{
    /// <summary>
    ///     Interface representing the bll for the babydiapers retention value edit/create
    /// </summary>
    public interface IBabyDiaperRetentionBll
    {
        /// <summary>
        ///     Query for an babydiapers retention test
        /// </summary>
        /// <param name="retentionTestId">The ID of the retention Test</param>
        /// <returns>The retentiontest with the given Id</returns>
        BabyDiaperTestValue GetBabyDiapersRetetionTest(Int32 retentionTestId);

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
        ///     Query for all notes for the testValue
        /// </summary>
        /// /// <param name="testValueId">The ID of the test value</param>
        /// <returns>Collection of all notes for the testValue</returns>
        IEnumerable<TestValueNote> GetNotes(Int32 testValueId);

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
    }
}
