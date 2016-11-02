using System;
using System.Web.Mvc;
using Extend;
using Intranet.Common;
using Intranet.Labor.ViewModel;

namespace Intranet.Web.Areas.Labor.Controllers
{
    /// <summary>
    ///     Class representing the BabyWindelnRetentionController
    /// </summary>
    public class BabyDiapersRetentionController : BaseController
    {
        #region Ctor

        /// <summary>
        ///     Initialize a new instance of the <see cref="BabyDiapersRetentionController" /> class.
        /// </summary>
        /// <param name="loggerFactory">A <see cref="ILoggerFactory" />.</param>
        public BabyDiapersRetentionController(ILoggerFactory loggerFactory)
            : base( loggerFactory.CreateLogger( typeof(BabyDiapersRetentionController) ) )
        {
            Logger.Trace("Enter Ctor - Exit.");
        }

        #endregion

        /// <summary>
        ///     Loads the BabywindelnRetetion Edit View with a new Item for the Test-Sheet
        /// </summary>
        /// /// <param name="testSheetId">The Id of the test-sheet which this Test-Data is for</param>
        /// <returns>The Index View filled with the viewModel</returns>
        public ActionResult Create(Int32 testSheetId = 0)
        {
            if (testSheetId.IsNull())
                return HttpNotFound();

            var viewModel = new BabyDiapersRetentionEditViewModel { Id = 5, TestPerson = "hans", ProductionCode = "IT/11/16/158/"};
            return View("Edit", viewModel);
        }

        /// <summary>
        ///     Loads the BabywindelnRetetion Edit View with an Item form the Test-Sheet which will be edited 
        /// </summary>
        /// /// <param name="testValueId">The Id of the test-sheet which this Test-Data is for</param>
        /// <returns>The Index View filled with the viewModel</returns>
        public ActionResult Edit(Int32 testValueId = 0)
        {
            if (testValueId.IsNull())
                return HttpNotFound();

            var viewModel = new BabyDiapersRetentionEditViewModel { Id = 5, TestPerson = "hans", ProductionCode = "IT/11/16/158/" };
            return View("Edit", viewModel);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Save( BabyDiapersRetentionEditViewModel viewModel )
        {
            return View();
        }
    }
}