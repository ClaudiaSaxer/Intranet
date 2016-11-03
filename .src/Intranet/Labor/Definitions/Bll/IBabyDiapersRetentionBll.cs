using System;
using Intranet.Labor.Model.labor;

namespace Intranet.Labor.Definition
{
    /// <summary>
    ///     Interface representing the bll for the babydiapers retention value edit/create
    /// </summary>
    public interface IBabyDiapersRetentionBll
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
    }
}
