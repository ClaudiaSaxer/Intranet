using System;
using Intranet.Common;
using Intranet.Labor.Model.labor;
using Intranet.Labor.TestEnvironment;
using Xunit;

namespace Intranet.Labor.Bll.Test
{
    /// <summary>
    ///     Class representing Tests for BabyDiaperRetentionService
    /// </summary>
    public class BabyDiaperRetentionServiceTest
    {
        /// <summary>
        ///     Tests if it get a new correct viewModel if the testSheet exists in the db
        /// </summary>
        [Fact]
        public void GetNewBabyDiapersRetentionEditViewModelFromExistingTestSheet()
        {
            var testSheetInDb = new TestSheet
            {
                TestSheetId = 1,
                MachineNr = "M11",
                CreatedDateTime = new DateTime(2016,5,5)
            };
            var babyDiaperRetentionBll =
                MockHelperBll.GetBabyDiaperRetentionBll(
                    testSheetInDb
                );

            var target = new BabyDiaperRetentionService(new NLogLoggerFactory())
            {
                BabyDiaperRetentionBll = babyDiaperRetentionBll
            };

            var actual = target.GetNewBabyDiapersRetentionEditViewModel(1);

            Assert.Equal(testSheetInDb.TestSheetId, actual.TestSheetId);
            Assert.Equal(-1, actual.TestValueId);
            Assert.Equal("IT/11/16/", actual.ProductionCode);
        }

        /// <summary>
        ///     Tests if it get null if the testSheet doesnt exist in the db
        /// </summary>
        [Fact]
        public void GetNewBabyDiapersRetentionEditViewModelFromNotExistingTestSheet()
        {
            var babyDiaperRetentionBll =
                MockHelperBll.GetBabyDiaperRetentionBll(
                    new TestSheet { TestSheetId = 1}
                );

            var target = new BabyDiaperRetentionService(new NLogLoggerFactory())
            {
                BabyDiaperRetentionBll = babyDiaperRetentionBll
            };

            var actual = target.GetNewBabyDiapersRetentionEditViewModel(2);

            Assert.Equal(null, actual);
        }
    }
}
