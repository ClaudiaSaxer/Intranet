using System;
using System.Web.Mvc;
using Extend;
using Intranet.Common;
using Intranet.Labor.Definition;
using Intranet.Labor.ViewModel;

namespace Intranet.Web.Areas.Labor.Controllers
{
    /// <summary>
    ///     Class representing the BabyWindelnRetentionController
    /// </summary>
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
        public BabyDiaperRetentionController(ILoggerFactory loggerFactory)
            : base( loggerFactory.CreateLogger( typeof(BabyDiaperRetentionController) ) )
        {
            Logger.Trace("Enter Ctor - Exit.");
        }

        #endregion

        /// <summary>
        ///     Loads the BabywindelnRetetion Edit View with a new Item for the Test-Sheet
        /// </summary>
        /// /// <param name="id">The Id of the test-sheet which this Test-Data is for</param>
        /// <returns>The Index View filled with the viewModel</returns>
        public ActionResult Create(Int32 id = 0)
        {
            if (id.IsNull())
                return HttpNotFound();

            var viewModel = BabyDiaperRetentionService.GetNewBabyDiapersRetentionEditViewModel(id);
            return View("Edit", viewModel);
        }

        /// <summary>
        ///     Loads the BabywindelnRetetion Edit View with an Item form the Test-Sheet which will be edited 
        /// </summary>
        /// /// <param name="id">The Id of the test-sheet which this Test-Data is for</param>
        /// <returns>The Index View filled with the viewModel</returns>
        public ActionResult Edit(Int32 id = 0)
        {
            if (id.IsNull())
                return HttpNotFound();

            var viewModel = BabyDiaperRetentionService.GetBabyDiapersRetentionEditViewModel(id);
            return View("Edit", viewModel);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Save( BabyDiaperRetentionEditViewModel viewModel )
        {
            var savedModel = BabyDiaperRetentionService.Save( viewModel );
            return View();
        }
    }
}