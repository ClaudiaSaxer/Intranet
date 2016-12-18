#region Usings

using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Extend;
using Intranet.Common;
using Intranet.Labor.Definition;
using Intranet.Labor.ViewModel;
using Intranet.Web.Filter;

#endregion

namespace Intranet.Web.Areas.Labor.Controllers
{
    /// <summary>
    ///     Class representing the InkoAquisitionController
    /// </summary>
    [CheckDisable( ModuleName = "Labor" )]
    [Authorize( Roles = RoleSettings.LaborAdmin + "," + RoleSettings.LaborUser )]
    public class InkoAquisitionController : BaseController
    {
        #region Properties

        /// <summary>
        ///     Gets or sets a <see cref="IInkoAquisitionService" />
        /// </summary>
        /// <value>
        ///     <see cref="IInkoAquisitionService" />
        /// </value>
        public IInkoAquisitionService InkoAquisitionService { get; set; }

        #endregion

        #region Ctor

        /// <summary>
        ///     Initialize a new instance of the <see cref="InkoRetentionController" /> class.
        /// </summary>
        /// <param name="loggerFactory">A <see cref="ILoggerFactory" />.</param>
        public InkoAquisitionController( ILoggerFactory loggerFactory )
            : base( loggerFactory.CreateLogger( typeof(InkoAquisitionController) ) )
        {
            Logger.Trace( "Enter Ctor - Exit." );
        }

        #endregion

        /// <summary>
        ///     adds a new note to the test
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns>The Edit View</returns>
        [HttpPost]
        public ActionResult AddNote( InkoAquisitionEditViewModel viewModel )
        {
            if ( viewModel.Notes.IsNull() )
                viewModel.Notes = new List<TestNote>();
            viewModel.Notes.Add( new TestNote() );
            return View( "Edit", viewModel );
        }

        /// <summary>
        ///     Loads the InkoAquisition Edit View with a new Item for the Test-Sheet
        /// </summary>
        /// ///
        /// <param name="id">The Id of the test-sheet which this Test-Data is for</param>
        /// <returns>The Edit View filled with the viewModel</returns>
        public ActionResult Create( Int32 id = 0 )
        {
            if ( id.IsNull() )
                return HttpNotFound();
            var viewModel = InkoAquisitionService.GetNewInkoAquisitionEditViewModel( id );
            if ( viewModel.IsNull() )
                return new HttpNotFoundResult( "Das TestSheet ist entweder kein Inko Testsheet oder existiert nicht." );
            return View( "Edit", viewModel );
        }

        /// <summary>
        ///     Deletes the Testvalue
        /// </summary>
        /// <param name="id">The Id of the TestValue</param>
        /// <returns></returns>
        public ActionResult Delete( Int32 id )
        {
            var deletedTest = InkoAquisitionService.Delete( id );
            return RedirectToAction( "Edit", "LaborCreatorInko", new { area = "Labor", id = deletedTest.TestSheetId } );
        }

        /// <summary>
        ///     Loads the InkoAquisition Edit View with an Item form the Test-Sheet which will be edited
        /// </summary>
        /// ///
        /// <param name="id">The Id of the test-sheet which this Test-Data is for</param>
        /// <returns>The Index View filled with the viewModel</returns>
        public ActionResult Edit( Int32 id = 0 )
        {
            if ( id.IsNull() )
                return HttpNotFound();

            var viewModel = InkoAquisitionService.GetInkoAquisitionEditViewModel( id );
            if ( viewModel.IsNull() )
                return new HttpNotFoundResult( "Der Angeforderte Test existiert entweder nicht oder war kein Inko Aquisition Test." );
            return View( "Edit", viewModel );
        }

        /// <summary>
        ///     Saves the new test or update it
        /// </summary>
        /// <param name="viewModel">the testdata whicht will be saved</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save( InkoAquisitionEditViewModel viewModel )
        {
            var savedModel = InkoAquisitionService.Save( viewModel );
            return RedirectToAction( "Edit", "LaborCreatorInko", new { area = "Labor", id = savedModel.TestSheetId } );
        }
    }
}