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
    ///     Class representing the inko retention service helper
    /// </summary>
    public class InkoRetentionServiceHelper : ServiceBase, IInkoRetentionServiceHelper
    {
        #region Properties

        /// <summary>
        ///     Gets or sets the bll for the all testvalues.
        /// </summary>
        public ITestBll TestBll { get; set; }

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
        public InkoRetentionServiceHelper( ILoggerFactory loggerFactory )
            : base( loggerFactory.CreateLogger( typeof(InkoRetentionServiceHelper) ) )
        {
            Logger.Trace( "Enter Ctor - Exit." );
        }

        #endregion

        #region Implementation of IInkoRetentionServiceHelper

        /// <summary>
        ///     Creates an new TestValue from the view model
        /// </summary>
        /// <param name="viewModel">the data from the view</param>
        /// <returns>The created test value</returns>
        public TestValue SaveNewRetentionTest( InkoRetentionEditViewModel viewModel )
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Updates the Average and standard deviation values of the testsheet for retention values
        /// </summary>
        /// <param name="testSheetId">id of the test sheet</param>
        /// <returns>the updated test sheet</returns>
        public TestSheet UpdateRetentionAverageAndStv( Int32 testSheetId )
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Updates an given Testvalue from the view model
        /// </summary>
        /// <param name="viewModel">the data from the view</param>
        /// <returns>the updated test value</returns>
        public TestValue UpdateRetentionTest( InkoRetentionEditViewModel viewModel )
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Private Methods

        #endregion
    }
}