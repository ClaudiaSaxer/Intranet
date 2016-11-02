using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Intranet.Common;
using Intranet.Labor.Definition;
using Intranet.Labor.ViewModel;

namespace Intranet.Labor.Bll
{
    /// <summary>
    /// 
    /// </summary>
    public class BabyDiapersRetentionService : ServiceBase, IBabyDiapersRetentionService
    {
        #region Ctor

        /// <summary>
        ///     Initialize a new instance of the <see cref="ServiceBase" /> class.
        /// </summary>
        /// <param name="loggerFactory">A <see cref="ILoggerFactory" />.</param>
        public BabyDiapersRetentionService(ILoggerFactory loggerFactory)
            : base( loggerFactory.CreateLogger( typeof(BabyDiapersRetentionService) ) )
        {
            Logger.Trace("Enter Ctor - Exit.");
        }

        #endregion

        #region Implementation of IBabyDiapersRetentionService

        /// <summary>
        ///     Gets a new BabyDiapersRetentionEditViewModel
        /// </summary>
        /// <param name="retentionTestId">The Id of the Babydiaper retention test which will be edited</param>
        /// <returns>The BabyDiapersRetentionEditViewModel</returns>
        public BabyDiapersRetentionEditViewModel GetBabyDiapersRetentionEditViewModel( Int32 retentionTestId )
        {
            return new BabyDiapersRetentionEditViewModel { Id = 5, TestPerson = "New Hans", ProductionCode = "IT/11/16/158/" };
        }

        /// <summary>
        ///     Gets the BabyDiapersRetentionEditViewModel for edit
        /// </summary>
        /// <param name="testSheetId">The Id of the test sheet where the Babydiaper retention test is for</param>
        /// <returns>The BabyDiapersRetentionEditViewModel</returns>
        public BabyDiapersRetentionEditViewModel GetNewBabyDiapersRetentionEditViewModel( Int32 testSheetId )
        {
            return new BabyDiapersRetentionEditViewModel { Id = 5, TestPerson = "Edit Hans", ProductionCode = "IT/11/16/158/"};
        }

        /// <summary>
        ///     Saves or updates the BabyDiapersRetentionEditViewModel
        /// </summary>
        /// <param name="babyDiapersRetentionEditViewModel">The viewmodel which will be saved or updated</param>
        /// <returns>The saved or updated BabyDiapersRetentionEditViewModel</returns>
        public BabyDiapersRetentionEditViewModel Save( BabyDiapersRetentionEditViewModel babyDiapersRetentionEditViewModel )
        {
            return babyDiapersRetentionEditViewModel;
        }

        #endregion
    }
}
