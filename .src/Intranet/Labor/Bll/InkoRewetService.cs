using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Intranet.Common;
using Intranet.Labor.Definition;
using Intranet.Labor.Model.labor;
using Intranet.Labor.ViewModel;

namespace Intranet.Labor.Bll
{
    /// <summary>
    ///     Class representing the inko rewet service
    /// </summary>
    public class InkoRewetService : ServiceBase, IInkoRewetService
    {
        #region Properties

        /// <summary>
        ///     Gets or sets the bll for the incontinence rewet test.
        /// </summary>
        public IBabyDiaperBll TestBll { get; set; }


        #endregion


        #region Ctor

        /// <summary>
        ///     Initialize a new instance of the <see cref="ServiceBase" /> class.
        /// </summary>
        /// <param name="loggerFactory">A <see cref="ILoggerFactory" />.</param>
        public InkoRewetService(ILoggerFactory loggerFactory)
            : base( loggerFactory.CreateLogger( typeof(InkoRewetService) ) )
        {
            Logger.Trace("Enter Ctor - Exit.");
        }

        #endregion

        #region Implementation of IInkoRewetService

        /// <summary>
        ///     deletes the testvalue
        /// </summary>
        /// <param name="testValueId">id of the testvalue</param>
        /// <returns>The deleted testvalue</returns>
        public TestValue Delete( Int32 testValueId )
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Gets a new InkoRewetEditViewModel
        /// </summary>
        /// <param name="rewetTestId">The Id of the Inko rewet test which will be edited</param>
        /// <returns>The InkoRewetEditViewModel</returns>
        public InkoRewetEditViewModel GetInkoRewetEditViewModel( Int32 rewetTestId )
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Gets the InkoRewetEditViewModel for edit
        /// </summary>
        /// <param name="testSheetId">The Id of the test sheet where the Inko rewet test is for</param>
        /// <returns>The InkoRewetEditViewModel</returns>
        public InkoRewetEditViewModel GetNewInkoRewetEditViewModel( Int32 testSheetId )
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Saves or updates the InkoRewetEditViewModel
        /// </summary>
        /// <param name="viewModel">The viewmodel which will be saved or updated</param>
        /// <returns>The saved or updated TestValue</returns>
        public TestValue Save( InkoRewetEditViewModel viewModel )
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
