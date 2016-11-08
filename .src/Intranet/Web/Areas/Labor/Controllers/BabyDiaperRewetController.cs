#region Usings

using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Extend;
using Intranet.Common;
using Intranet.Labor.Definition;
using Intranet.Labor.Model.labor;
using Intranet.Labor.ViewModel;

#endregion

namespace Intranet.Web.Areas.Labor.Controllers
{
    /// <summary>
    ///     Class representing the BabyDiaperRewetController
    /// </summary>
    public class BabyDiaperRewetController : BaseController
    {
        #region Properties

        /// <summary>
        ///     Gets or sets a <see cref="IBabyDiaperRewetService" />
        /// </summary>
        /// <value>
        ///     <see cref="IBabyDiaperRewetService" />
        /// </value>
        public IBabyDiaperRewetService BabyDiaperRewetService { get; set; }

        #endregion

        #region Ctor

        /// <summary>
        ///     Initialize a new instance of the <see cref="BabyDiaperRewetController" /> class.
        /// </summary>
        /// <param name="loggerFactory">A <see cref="ILoggerFactory" />.</param>
        public BabyDiaperRewetController( ILoggerFactory loggerFactory )
            : base( loggerFactory.CreateLogger( typeof(BabyDiaperRewetController) ) )
        {
            Logger.Trace( "Enter Ctor - Exit." );
        }

        #endregion

        /// <summary>
        ///     Loads the BabywindelnRewet Edit View with a new Item for the Test-Sheet
        /// </summary>
        /// ///
        /// <param name="id">The Id of the test-sheet which this Test-Data is for</param>
        /// <returns>The Edit View filled with the viewModel</returns>
        public ActionResult Create( Int32 id = 0 )
        {
            if ( id.IsNull() )
                return HttpNotFound();
            var viewModel = BabyDiaperRewetService.GetNewBabyDiapersRetentionEditViewModel( id );
            if ( viewModel.IsNull() )
                return new HttpNotFoundResult( "Das TestSheet ist entweder kein Baby Windel Testsheet oder existiert nicht." );
            return View( "Edit", viewModel );
        }

        /// <summary>
        ///     Loads the BabywindelnRewet Edit View with an Item form the Test-Sheet which will be edited 
        /// </summary>
        /// /// <param name="id">The Id of the test-sheet which this Test-Data is for</param>
        /// <returns>The Index View filled with the viewModel</returns>
        public ActionResult Edit(Int32 id = 0)
        {
            if (id.IsNull())
                return HttpNotFound();

            var viewModel = BabyDiaperRewetService.GetBabyDiapersRetentionEditViewModel(id);
            if (viewModel.IsNull())
                return new HttpNotFoundResult("Der Angeforderte Test existiert entweder nicht oder war kein Retention Test.");
            return View("Edit", viewModel);
        }

        /// <summary>
        ///     Deletes the Testvalue
        /// </summary>
        /// <param name="id">The Id of the TestValue</param>
        /// <returns></returns>
        public ActionResult Delete(Int32 id)
        {
            var deletedTest = BabyDiaperRewetService.Delete(id);
            return RedirectToAction("Edit", "LaborCreatorBaby", new { area = "Labor", id = deletedTest.TestSheetRefId });
        }

        /// <summary>
        ///     Saves the new test or update it
        /// </summary>
        /// <param name="viewModel">the testdata whicht will be saved</param>
        /// <returns>The Edit View</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save( BabyDiaperRewetEditViewModel viewModel )
        {
            var savedModel = new TestValue { TestSheetRefId = 1 }; //BabyDiaperRetentionService.Save(viewModel);
            return RedirectToAction( "Edit", "LaborCreatorBaby", new { area = "Labor", id = savedModel.TestSheetRefId } );
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