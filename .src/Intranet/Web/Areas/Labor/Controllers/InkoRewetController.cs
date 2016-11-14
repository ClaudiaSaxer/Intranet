using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Extend;
using Intranet.Common;
using Intranet.Labor.Bll;
using Intranet.Labor.Definition;
using Intranet.Labor.ViewModel;

namespace Intranet.Web.Areas.Labor.Controllers
{
    /// <summary>
    ///     Class representing the BabyDiaperRewetController
    /// </summary>
    public class InkoRewetController : BaseController
    {
        #region Properties

        /// <summary>
        ///     Gets or sets a <see cref="IInkoRewetService" />
        /// </summary>
        /// <value>
        ///     <see cref="IInkoRewetService" />
        /// </value>
        public IInkoRewetService InkoRewetService { get; set; }

        #endregion

        #region Ctor

        /// <summary>
        ///     Initialize a new instance of the <see cref="InkoRewetController" /> class.
        /// </summary>
        /// <param name="loggerFactory">A <see cref="ILoggerFactory" />.</param>
        public InkoRewetController(ILoggerFactory loggerFactory)
            : base( loggerFactory.CreateLogger( typeof(InkoRewetController) ) )
        {
            Logger.Trace("Enter Ctor - Exit.");
        }

        #endregion

        /// <summary>
        ///     Loads the InkoRewet Edit View with a new Item for the Test-Sheet
        /// </summary>
        /// ///
        /// <param name="id">The Id of the test-sheet which this Test-Data is for</param>
        /// <returns>The Edit View filled with the viewModel</returns>
        public ActionResult Create(Int32 id = 0)
        {
            if (id.IsNull())
                return HttpNotFound();
            var viewModel = InkoRewetService.GetNewInkoRewetEditViewModel( id );
            if (viewModel.IsNull())
                return new HttpNotFoundResult("Das TestSheet ist entweder kein Baby Windel Testsheet oder existiert nicht.");
            return View("Edit",viewModel);
        }

        /// <summary>
        ///     Loads the InkoRewet Edit View with an Item form the Test-Sheet which will be edited 
        /// </summary>
        /// /// <param name="id">The Id of the test-sheet which this Test-Data is for</param>
        /// <returns>The Index View filled with the viewModel</returns>
        public ActionResult Edit(Int32 id = 0)
        {
            if (id.IsNull())
                return HttpNotFound();

            var viewModel = InkoRewetService.GetNewInkoRewetEditViewModel(id);
            if (viewModel.IsNull())
                return new HttpNotFoundResult("Der Angeforderte Test existiert entweder nicht oder war kein BabyDiaperRetention Test.");
            return View("Edit", viewModel);
        }

        /// <summary>
        ///     Deletes the Testvalue
        /// </summary>
        /// <param name="id">The Id of the TestValue</param>
        /// <returns></returns>
        public ActionResult Delete(Int32 id)
        {
            var deletedTest = InkoRewetService.Delete(id);
            return RedirectToAction("Edit", "LaborCreatorBaby", new { area = "Labor", id = deletedTest.TestSheetRefId });
        }

        /// <summary>
        ///     Saves the new test or update it
        /// </summary>
        /// <param name="viewModel">the testdata whicht will be saved</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(InkoRewetEditViewModel viewModel)
        {
            var savedModel = InkoRewetService.Save(viewModel);
            return RedirectToAction("Edit", "LaborCreatorBaby", new { area = "Labor", id = savedModel.TestSheetRefId });
        }

        /// <summary>
        ///     adds a new note to the test
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns>The Edit View</returns>
        [HttpPost]
        public ActionResult AddNote(InkoRewetEditViewModel viewModel)
        {
            if (viewModel.Notes.IsNull())
                viewModel.Notes = new List<TestNote>();
            viewModel.Notes.Add(new TestNote());
            return View("Edit", viewModel);
        }
    }
}