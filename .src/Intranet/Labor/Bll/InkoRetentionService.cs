#region Usings

using System;
using Intranet.Common;
using Intranet.Labor.Definition;
using Intranet.Labor.Model.labor;
using Intranet.Labor.ViewModel;

#endregion

namespace Intranet.Labor.Bll
{
    /// <summary>
    ///     Class representing the inko retention service
    /// </summary>
    public class InkoRetentionService : ServiceBase, IInkoRetentionService
    {
        #region Properties

        /// <summary>
        ///     Gets or sets the bll for the incontinence retention test.
        /// </summary>
        public ITestBll TestBll { get; set; }

        /// <summary>
        ///     Gets or sets the incontinence retention service helper.
        /// </summary>
        public IInkoRetentionServiceHelper InkoRewetServiceHelper { get; set; }

        /// <summary>
        ///     Gets or sets the test service helper.
        /// </summary>
        public ITestServiceHelper TestServiceHelper { get; set; }

        #endregion

        #region Ctor

        /// <summary>
        ///     Initialize a new instance of the <see cref="ServiceBase" /> class.
        /// </summary>
        /// <param name="loggerFactory">A <see cref="ILoggerFactory" />.</param>
        public InkoRetentionService( ILoggerFactory loggerFactory )
            : base( loggerFactory.CreateLogger( typeof(InkoRetentionService) ) )
        {
            Logger.Trace( "Enter Ctor - Exit." );
        }

        #endregion

        #region Implementation of IInkoRetentionService

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
        ///     Gets a new InkoRetentionEditViewModel
        /// </summary>
        /// <param name="rewetTestId">The Id of the Inko rewet test which will be edited</param>
        /// <returns>The InkoRewetEditViewModel</returns>
        public InkoRetentionEditViewModel GetInkoRetentionEditViewModel( Int32 rewetTestId )
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Gets the InkoRetentionEditViewModel for edit
        /// </summary>
        /// <param name="testSheetId">The Id of the test sheet where the Inko rewet test is for</param>
        /// <returns>The InkoRewetEditViewModel</returns>
        public InkoRetentionEditViewModel GetNewInkoRetentionEditViewModel( Int32 testSheetId )
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Saves or updates the InkoRetentionEditViewModel
        /// </summary>
        /// <param name="viewModel">The viewmodel which will be saved or updated</param>
        /// <returns>The saved or updated TestValue</returns>
        public TestValue Save( InkoRetentionEditViewModel viewModel )
        {
            TestValue testValue = null;
            try
            {
                testValue = viewModel.TestValueId <= 0
                    ? InkoRewetServiceHelper.SaveNewRetentionTest(viewModel)
                    : InkoRewetServiceHelper.UpdateRetentionTest(viewModel);
                var testSheet = InkoRewetServiceHelper.UpdateRetentionAverageAndStv(viewModel.TestSheetId);
            }
            catch (Exception e)
            {
                Logger.Error("Update oder Create new Test Value ist fehlgeschlagen: " + e.Message);
                testValue = null;
            }
            return testValue;
        }

        #endregion
    }
}