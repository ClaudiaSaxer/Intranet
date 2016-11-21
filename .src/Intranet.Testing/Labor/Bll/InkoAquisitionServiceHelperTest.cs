using System;
using System.Collections.Generic;
using System.Linq;
using Intranet.Labor.Model;
using Intranet.Labor.Model.labor;
using Intranet.Labor.ViewModel;
using Xunit;

namespace Intranet.Labor.Bll.Test
{
    /// <summary>
    ///     Class representing Tests for InkoAquisitionServiceHelper
    /// </summary>
    public class InkoAquisitionServiceHelperTest
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
                MaxHyTec1 = 20,
                MaxHyTec2 = 60,
                MaxHyTec3 = 85,
                MaxInkoRewetAfterAquisition = 2
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
                        TestType = TestTypeIncontinencePad.AcquisitionTimeAndRewet
                    }
                },
                new TestValue
                {
                    TestValueType = TestValueType.StandardDeviation,
                    ArticleTestType = ArticleType.IncontinencePad,
                    IncontinencePadTestValue = new IncontinencePadTestValue
                    {
                        TestType = TestTypeIncontinencePad.AcquisitionTimeAndRewet
                    }
                }
            }
        };

        private InkoAquisitionEditViewModel GetViewModelTestData() => new InkoAquisitionEditViewModel
        {
            TestPerson = "Hans",
            TestValueId = -1,
            TestSheetId = 1,
            ProductionCodeTime = new TimeSpan(12, 34, 0),
            ProductionCodeDay = 123,
            InkoWeight = 21.15,
            AquisitionAddition1 = 17.12,
            AquisitionAddition2 = 54.06,
            AquisitionAddition3 = 67.85,
            FPDry = 21.73,
            FPWet = 21.79,
            Notes = new List<TestNote> { new TestNote { ErrorCodeId = 1, Id = 1, Message = "Testnote" } }
        };

        private TestValue GetTestValueTestData() => new TestValue
        {
            TestSheetRefId = 1,
            CreatedDateTime = new DateTime(2016, 1, 2),
            LastEditedDateTime = new DateTime(2016, 1, 2),
            CreatedPerson = "Hans",
            LastEditedPerson = "Hans",
            DayInYearOfArticleCreation = 123,
            ArticleTestType = ArticleType.IncontinencePad,
            TestValueNote = new List<TestValueNote> { new TestValueNote { ErrorRefId = 1, Message = "Testnote" } }
        };

        #endregion

        #region SaveNewAquisitionTest Tests



        #endregion

        #region UpdateAquisitionTest Tests



        #endregion

        #region UpdateAquisitionAverageAndStv Tests



        #endregion
    }
}
