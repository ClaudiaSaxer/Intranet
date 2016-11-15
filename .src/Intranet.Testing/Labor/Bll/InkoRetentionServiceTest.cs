using System;
using System.Collections.Generic;
using System.Linq;
using Intranet.Common;
using Intranet.Labor.Model;
using Intranet.Labor.Model.labor;
using Intranet.Labor.TestEnvironment;
using Xunit;

namespace Intranet.Labor.Bll.Test
{
    /// <summary>
    ///     Class representing Tests for InkoRetentionService
    /// </summary>
    public class InkoRetentionServiceTest
    {
        /// <summary>
        ///     Tests if it get a new correct viewModel if the testSheet exists in the db
        /// </summary>
        [Fact]
        public void GetNewInkoRetentionEditViewModelFromExistingTestSheetTest()
        {
            var testSheetInDb = new TestSheet
            {
                TestSheetId = 2,
                MachineNr = "M49",
                CreatedDateTime = new DateTime(2016, 5, 5),
                ArticleType = ArticleType.IncontinencePad
            };
            var testBll =
                MockHelperBll.GetTestBll(
                    testSheetInDb
                );

            var testServiceHelper = MockHelperTestServiceHelper.GetTestServiceHelper("IT/49/16/");

            var target = new InkoRetentionService(new NLogLoggerFactory())
            {
                TestBll = testBll,
                TestServiceHelper = testServiceHelper
            };

            var actual = target.GetNewInkoRetentionEditViewModel(2);

            Assert.Equal(testSheetInDb.TestSheetId, actual.TestSheetId);
            Assert.Equal(-1, actual.TestValueId);
            Assert.Equal("IT/49/16/", actual.ProductionCode);
        }

        /// <summary>
        ///     Tests if it get null if the testSheet doesnt exist in the db
        /// </summary>
        [Fact]
        public void GetNewInkoRetentionEditViewModelFromNotExistingTestSheetTest()
        {
            var testBll =
                MockHelperBll.GetTestBll(
                    new TestSheet { TestSheetId = 1 }
                );

            var target = new InkoRetentionService(new NLogLoggerFactory())
            {
                TestBll = testBll
            };

            var actual = target.GetNewInkoRetentionEditViewModel(2);

            Assert.Equal(null, actual);
        }
    }
}
