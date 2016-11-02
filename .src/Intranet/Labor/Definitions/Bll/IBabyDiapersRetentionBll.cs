using System;

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
        void GetBabyDiapersRetetionTest(Int32 retentionTestId);
    }
}
