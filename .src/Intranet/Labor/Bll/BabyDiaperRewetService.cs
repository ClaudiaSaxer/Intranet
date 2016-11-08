using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Extend;
using Intranet.Common;
using Intranet.Labor.Definition;
using Intranet.Labor.Model.labor;
using Intranet.Labor.ViewModel;

namespace Intranet.Labor.Bll
{
    /// <summary>
    ///     Class representing the babydiaper rewet service
    /// </summary>
    public class BabyDiaperRewetService : ServiceBase, IBabyDiaperRewetService
    {
        #region Properties

        /// <summary>
        ///     Gets or sets the bll for the baby diapers retention test.
        /// </summary>
        public IBabyDiaperRetentionBll BabyDiaperRetentionBll { get; set; }

        /// <summary>
        ///     Gets or sets the baby diaper service helper.
        /// </summary>
        public IBabyDiaperServiceHelper BabyDiaperServiceHelper { get; set; }

        #endregion

        #region Ctor

        /// <summary>
        ///     Initialize a new instance of the <see cref="ServiceBase" /> class.
        /// </summary>
        /// <param name="loggerFactory">A <see cref="ILoggerFactory" />.</param>
        public BabyDiaperRewetService(ILoggerFactory loggerFactory)
            : base( loggerFactory.CreateLogger( typeof(BabyDiaperRewetService) ) )
        {
            Logger.Trace("Enter Ctor - Exit.");
        }

        #endregion

        #region Implementation of IBabyDiaperRewetService

        /// <summary>
        ///     deletes the testvalue
        /// </summary>
        /// <param name="testValueId">id of the testvalue</param>
        /// <returns>The deleted testvalue</returns>
        public TestValue Delete( Int32 testValueId )
        {
            var result = BabyDiaperRetentionBll.DeleteTestValue(testValueId);
            BabyDiaperServiceHelper.UpdateAverageAndStv(result.TestSheetRefId);
            return result;
        }

        /// <summary>
        ///     Gets a new BabyDiaperRewetEditViewModel or returns null if something is wrong
        /// </summary>
        /// <param name="rewetTestId">The Id of the Babydiaper rewet test which will be edited</param>
        /// <returns>The BabyDiaperRewetEditViewModel</returns>
        public BabyDiaperRewetEditViewModel GetBabyDiapersRetentionEditViewModel( Int32 rewetTestId )
        {
            var testValue = BabyDiaperRetentionBll.GetTestValue(rewetTestId);
            if (testValue.IsNull())
            {
                Logger.Error("TestValue mit id " + rewetTestId + "existiert nicht in DB!");
                return null;
            }
            var babyDiapersTestValue = testValue.BabyDiaperTestValue;
            if (babyDiapersTestValue.IsNull())
            {
                Logger.Error("BabyDiaperRetentionTestValue mit id " + testValue.TestValueId + "existiert nicht in DB!");
                return null;
            }
            if (babyDiapersTestValue.TestType != TestTypeBabyDiaper.Rewet)
            {
                Logger.Error("Requestet test was not an Retention Test. Id " + testValue.TestValueId);
                return null;
            }
            var testSheetInfo = testValue.TestSheet;
            if (testSheetInfo.IsNull())
            {
                Logger.Error("TestBlatt mit id " + testValue.TestSheetRefId + "existiert nicht in DB!");
                return null;
            }
            var notes = testValue.TestValueNote;
            var errors = BabyDiaperRetentionBll.GetAllNoteCodes();
            var errorCodes = errors.Select(error => new ErrorCode { ErrorId = error.ErrorId, Name = error.ErrorCode + " - " + error.Value })
                                   .ToList();
            if (notes.IsNull())
                notes = new List<TestValueNote>();
            var testNotes = notes.Select(note => new TestNote { Id = note.TestValueNoteId, ErrorCodeId = note.ErrorRefId, Message = note.Message })
                                 .ToList();

            var viewModel = new BabyDiaperRewetEditViewModel
            {
                TestValueId = rewetTestId,
                TestSheetId = testValue.TestSheetRefId,
                TestPerson = testValue.LastEditedPerson,
                ProductionCode = BabyDiaperServiceHelper.CreateProductionCode(testSheetInfo),
                ProductionCodeDay = testValue.DayInYearOfArticleCreation,
                ProductionCodeTime = babyDiapersTestValue.DiaperCreatedTime,
                DiaperWeight = babyDiapersTestValue.WeightDiaperDry,
                RewetAfter140 = babyDiapersTestValue.Rewet140Value,
                RewetAfter210 = babyDiapersTestValue.Rewet210Value,
                StrikeThrough = babyDiapersTestValue.StrikeTroughValue,
                Distribution = babyDiapersTestValue.DistributionOfTheStrikeTrough,
                PenetrationTime1 = babyDiapersTestValue.PenetrationTimeAdditionFirst,
                PenetrationTime2 = babyDiapersTestValue.PenetrationTimeAdditionSecond,
                PenetrationTime3 = babyDiapersTestValue.PenetrationTimeAdditionThird,
                PenetrationTime4 = babyDiapersTestValue.PenetrationTimeAdditionFourth,
                Notes = testNotes,
                NoteCodes = errorCodes
            };
            return viewModel;
        }

        /// <summary>
        ///     Gets the BabyDiaperRewetEditViewModel for edit
        /// </summary>
        /// <param name="testSheetId">The Id of the test sheet where the Babydiaper rewet test is for</param>
        /// <returns>The BabyDiaperRewetEditViewModel</returns>
        public BabyDiaperRewetEditViewModel GetNewBabyDiapersRetentionEditViewModel( Int32 testSheetId )
        {
            var testSheetInfo = BabyDiaperRetentionBll.GetTestSheetInfo(testSheetId);

            if (testSheetInfo.IsNull())
            {
                Logger.Error("TestBlatt mit id " + testSheetId + "existiert nicht in DB!");
                return null;
            }

            var errors = BabyDiaperRetentionBll.GetAllNoteCodes();
            var errorCodes = errors.Select(error => new ErrorCode { ErrorId = error.ErrorId, Name = error.ErrorCode + " - " + error.Value })
                                   .ToList();
            var viewModel = new BabyDiaperRewetEditViewModel
            {
                TestSheetId = testSheetId,
                TestValueId = -1,
                ProductionCode = BabyDiaperServiceHelper.CreateProductionCode(testSheetInfo),
                NoteCodes = errorCodes,
                Notes = new List<TestNote>()
            };

            return viewModel;
        }

        /// <summary>
        ///     Saves or updates the BabyDiaperRewetEditViewModel
        /// </summary>
        /// <param name="viewModel">The viewmodel which will be saved or updated</param>
        /// <returns>The saved or updated TestValue</returns>
        public TestValue Save( BabyDiaperRewetEditViewModel viewModel )
        {
            TestValue testValue = null;
            try
            {
                testValue = viewModel.TestValueId <= 0
                    ? BabyDiaperServiceHelper.SaveNewRewetTest(viewModel)
                    : BabyDiaperServiceHelper.UpdateRewetTest(viewModel);
                var testSheet = BabyDiaperServiceHelper.UpdateAverageAndStv(viewModel.TestSheetId);
            }
            catch (Exception e)
            {
                Logger.Error("Update oder Create new Test Value ist fehlgeschlagen: " + e.Message);
            }
            return testValue;
        }

        #endregion
    }
}
