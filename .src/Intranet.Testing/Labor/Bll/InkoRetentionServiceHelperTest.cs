using System;
using System.Collections.Generic;
using System.Linq;
using Intranet.Labor.Model;
using Intranet.Labor.Model.labor;
using Xunit;

namespace Intranet.Labor.Bll.Test
{
    /// <summary>
    ///     Class representing Tests for InkoRetentionServiceHelper
    /// </summary>
    public class InkoRetentionServiceHelperTest
    {
        #region BaseTestData

        private TestSheet GetTestSheetTestData() => new TestSheet
        {
            FaNr = "FA654321"
        };

        private ProductionOrder GetProductionOrderTestData() => new ProductionOrder
        {
            Article = new Article
            {
                MinInkoRetention = 180
            }
        };

        private TestSheet GetTestSheetTestDataWithAvgAndStDev() => new TestSheet
        {
            TestValues = new List<TestValue>
            {
                new TestValue
                {
                    TestValueType = TestValueType.Average,
                    ArticleTestType = ArticleType.IncontinencePad,
                    IncontinencePadTestValue = new IncontinencePadTestValue
                    {
                        TestType = TestTypeIncontinencePad.Retention
                    }
                },
                new TestValue
                {
                    TestValueType = TestValueType.StandardDeviation,
                    ArticleTestType = ArticleType.IncontinencePad,
                    IncontinencePadTestValue = new IncontinencePadTestValue
                    {
                        TestType = TestTypeIncontinencePad.Retention
                    }
                }
            }
        };

        #endregion

        #region SaveNewRetentionTest Tests



        #endregion

        #region UpdateRetentionTest Tests



        #endregion

        #region UpdateRetentionAverageAndStv Tests



        #endregion
    }
}
