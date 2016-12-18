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
    ///     Class representing the BabyWindelnRetentionController
    /// </summary>
    [CheckDisable( ModuleName = "Labor" )]
    [Authorize( Roles = RoleSettings.LaborAdmin + "," + RoleSettings.LaborUser )]
    public class BabyDiaperRetentionController : BaseController
    {
        #region Properties

        /// <summary>
        ///     Gets or sets a <see cref="IBabyDiaperRetentionService" />
        /// </summary>
        /// <value>
        ///     <see cref="IBabyDiaperRetentionService" />
        /// </value>
        public IBabyDiaperRetentionService BabyDiaperRetentionService { get; set; }

        #endregion

        #region Ctor

        /// <summary>
        ///     Initialize a new instance of the <see cref="BabyDiaperRetentionController" /> class.
        /// </summary>
        /// <param name="loggerFactory">A <see cref="ILoggerFactory" />.</param>
        public BabyDiaperRetentionController( ILoggerFactory loggerFactory )
            : base( loggerFactory.CreateLogger( typeof(BabyDiaperRetentionController) ) )
        {
            Logger.Trace( "Enter Ctor - Exit." );
        }

        #endregion

        /// <summary>
        ///     adds a new note to the test
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AddNote( BabyDiaperRetentionEditViewModel viewModel )
        {
            if ( viewModel.Notes.IsNull() )
                viewModel.Notes = new List<TestNote>();
            viewModel.Notes.Add( new TestNote() );
            return View( "Edit", viewModel );
        }

        /// <summary>
        ///     Loads the BabywindelnRetetion Edit View with a new Item for the Test-Sheet
        /// </summary>
        /// ///
        /// <param name="id">The Id of the test-sheet which this Test-Data is for</param>
        /// <returns>The Edit View filled with the viewModel</returns>
        public ActionResult Create( Int32 id = 0 )
        {
            if ( id.IsNull() )
                return HttpNotFound();

            var viewModel = BabyDiaperRetentionService.GetNewBabyDiapersRetentionEditViewModel( id );
            if ( viewModel.IsNull() )
                return new HttpNotFoundResult( "Das TestSheet ist entweder kein Baby Windel Testsheet oder existiert nicht." );
            return View( "Edit", viewModel );
        }

        /// <summary>
        ///     Deletes the Testvalue
        /// </summary>
        /// <param name="id">The Id of the TestValue</param>
        /// <returns></returns>
        public ActionResult Delete( Int32 id )
        {
            var deletedTest = BabyDiaperRetentionService.Delete( id );
            return RedirectToAction( "Edit", "LaborCreatorBaby", new { area = "Labor", id = deletedTest.TestSheetId } );
        }

        /// <summary>
        ///     Loads the BabywindelnRetetion Edit View with an Item form the Test-Sheet which will be edited
        /// </summary>
        /// ///
        /// <param name="id">The Id of the test-sheet which this Test-Data is for</param>
        /// <returns>The Index View filled with the viewModel</returns>
        public ActionResult Edit( Int32 id = 0 )
        {
            if ( id.IsNull() )
                return HttpNotFound();

            var viewModel = BabyDiaperRetentionService.GetBabyDiapersRetentionEditViewModel( id );
            if ( viewModel.IsNull() )
                return new HttpNotFoundResult( "Der Angeforderte Test existiert entweder nicht oder war kein BabyDiaperRetention Test." );
            return View( "Edit", viewModel );
        }

        /// <summary>
        ///     Saves the new test or update it
        /// </summary>
        /// <param name="viewModel">the testdata whicht will be saved</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save( BabyDiaperRetentionEditViewModel viewModel )
        {
            var savedModel = BabyDiaperRetentionService.Save( viewModel );
            return RedirectToAction( "Edit", "LaborCreatorBaby", new { area = "Labor", id = savedModel.TestSheetId } );
        }
    }
}