using Intranet.Labor.Definition;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Intranet.Common;
using Intranet.Labor.Model.labor;

namespace Intranet.Labor.Bll
{
    /// <summary>
    ///     Class representing the bll of the Baby diapers retention
    /// </summary>
    public class BabyDiapersRetentionBll : IBabyDiapersRetentionBll
    {
        #region Properties

        /// <summary>
        ///     TestSheetRepository
        /// </summary>
        public IGenericRepository<TestSheet> TestSheetRepository { get; set; }

        #endregion

        #region Implementation of IBabyDiapersRetentionBll

        /// <summary>
        ///     Query for an babydiapers retention test
        /// </summary>
        /// <param name="retentionTestId">The ID of the retention Test</param>
        /// <returns>The retentiontest with the given Id</returns>
        public BabyDiaperTestValue GetBabyDiapersRetetionTest( Int32 retentionTestId )
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Query for a testvalue
        /// </summary>
        /// <param name="retentionTestId">The ID of the testvalue</param>
        /// <returns>The test value with the given Id</returns>
        public TestValue GetTestValue( Int32 retentionTestId )
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Query for the Test sheet info
        /// </summary>
        /// <param name="testSheetId">The ID of the test sheet</param>
        /// <returns>The test sheet</returns>
        public TestSheet GetTestSheetInfo( Int32 testSheetId )
        {
            var testSheet = TestSheetRepository.Where( ts => ts.TestSheetId == testSheetId )
                                      .FirstOrDefault();
            return testSheet;
        }

        #endregion
    }
}
