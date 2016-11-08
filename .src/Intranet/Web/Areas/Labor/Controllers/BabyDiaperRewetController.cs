using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Extend;
using Intranet.Common;
using Intranet.Labor.Model.labor;
using Intranet.Labor.ViewModel;

namespace Intranet.Web.Areas.Labor.Controllers
{
    /// <summary>
    ///     Class representing the BabyDiaperRewetController
    /// </summary>
    public class BabyDiaperRewetController : BaseController
    {
        #region Ctor

        /// <summary>
        ///     Initialize a new instance of the <see cref="BabyDiaperRewetController" /> class.
        /// </summary>
        /// <param name="loggerFactory">A <see cref="ILoggerFactory" />.</param>
        public BabyDiaperRewetController(ILoggerFactory loggerFactory)
            : base( loggerFactory.CreateLogger( typeof(BabyDiaperRewetController) ) )
        {
            Logger.Trace("Enter Ctor - Exit.");
        }

        #endregion

        /// <summary>
        ///     Loads the BabywindelnRewet Edit View with a new Item for the Test-Sheet
        /// </summary>
        /// /// <param name="id">The Id of the test-sheet which this Test-Data is for</param>
        /// <returns>The Edit View filled with the viewModel</returns>
        public ActionResult Create( Int32 id = 0 )
        {
            if (id.IsNull())
                return HttpNotFound();
            var viewModel = new BabyDiaperRewetEditViewModel();
            viewModel.NoteCodes = new List<ErrorCode>();
            if (viewModel.IsNull()) return new HttpNotFoundResult("Das TestSheet ist entwerder kein Baby Windel Testsheet oder existiert nicht.");
            return View("Edit", viewModel);
        }


        /// <summary>
        ///     Saves the new test or update it
        /// </summary>
        /// <param name="viewModel">the testdata whicht will be saved</param>
        /// <returns>The Edit View</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(BabyDiaperRewetEditViewModel viewModel)
        {
            var savedModel = new TestValue {TestSheetRefId = 1};//BabyDiaperRetentionService.Save(viewModel);
            return RedirectToAction("Edit", "LaborCreatorBaby", new { area = "Labor", id = savedModel.TestSheetRefId });
        }

        /// <summary>
        ///     adds a new note to the test
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns>The Edit View</returns>
        [HttpPost]
        public ActionResult AddNote(BabyDiaperRewetEditViewModel viewModel)
        {
            if (viewModel.Notes.IsNull())
                viewModel.Notes = new List<TestNote>();
            viewModel.Notes.Add(new TestNote());
            return View("Edit", viewModel);
        }
    }
}