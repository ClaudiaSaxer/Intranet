using System;
using System.Collections.Generic;
using System.Linq;
using Intranet.Labor.Model;
using Intranet.Labor.Model.labor;
using Xunit;

namespace Intranet.Labor.Bll.Test
{
    /// <summary>
    ///     Class representing Tests for BabyDiaperRewetServiceHelper
    /// </summary>
    public class BabyDiaperRewetServiceHelperTest
    {
        #region BaseTestData

        private TestSheet GetTestSheetTestData()
        {
            return new TestSheet
            {
                FaNr = "FA123456",
                SAPType = "EKX",
                SAPNr = "EN67"
            };
        }
        private ProductionOrder GetProductionOrderTestData()
        {
            return new ProductionOrder
            {
                Article = new Article
                {
                    Rewet140Max = 0.4,
                    Rewet210Max = 0.5,
                    MaxPenetrationAfter4Time = 250
                }
            };
        }

        private TestSheet GetTestSheetTestDataWithAvgAndStDev()
        {
            return new TestSheet
            {
                TestValues = new List<TestValue>
                {
                    new TestValue
                    {
                        TestValueType = TestValueType.Average,
                        ArticleTestType = ArticleType.BabyDiaper,
                        BabyDiaperTestValue = new BabyDiaperTestValue
                        {
                            TestType = TestTypeBabyDiaper.Rewet
                        }
                    },
                    new TestValue
                    {
                        TestValueType = TestValueType.StandardDeviation,
                        ArticleTestType = ArticleType.BabyDiaper,
                        BabyDiaperTestValue = new BabyDiaperTestValue
                        {
                            TestType = TestTypeBabyDiaper.Rewet
                        }
                    },
                    new TestValue
                    {
                        TestValueType = TestValueType.Average,
                        ArticleTestType = ArticleType.BabyDiaper,
                        BabyDiaperTestValue = new BabyDiaperTestValue
                        {
                            TestType = TestTypeBabyDiaper.RewetAndPenetrationTime
                        }
                    },
                    new TestValue
                    {
                        TestValueType = TestValueType.StandardDeviation,
                        ArticleTestType = ArticleType.BabyDiaper,
                        BabyDiaperTestValue = new BabyDiaperTestValue
                        {
                            TestType = TestTypeBabyDiaper.RewetAndPenetrationTime
                        }
                    }
                }
            };
        }

        #endregion
    }
}
