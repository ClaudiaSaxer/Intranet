#region Usings

using System;
using System.Collections.Generic;
using System.Linq;
using Extend;
using Intranet.Common;
using Intranet.Labor.Definition;
using Intranet.Labor.Model;
using Intranet.Labor.Model.labor;
using Intranet.Labor.ViewModel;

#endregion

namespace Intranet.Labor.Bll
{
    /// <summary>
    /// </summary>
    public class TestServiceHelper : ServiceBase, ITestServiceHelper
    {
        #region Properties

        /// <summary>
        ///     Gets or sets the bll for the baby diapers retention test.
        /// </summary>
        public IBabyDiaperBll BabyDiaperBll { get; set; }

        #endregion

        #region Ctor

        /// <summary>
        ///     Initialize a new instance of the <see cref="ServiceBase" /> class.
        /// </summary>
        /// <param name="loggerFactory">A <see cref="ILoggerFactory" />.</param>
        public TestServiceHelper( ILoggerFactory loggerFactory )
            : base( loggerFactory.CreateLogger( typeof(TestServiceHelper) ) )
        {
            Logger.Trace( "Enter Ctor - Exit." );
        }

        #endregion

        #region Implementation of ITestServiceHelper

        /// <summary>
        ///     Creates the Production code string from the testsheet
        /// </summary>
        /// <param name="testSheet">the testSheet</param>
        /// <returns>the production code</returns>
        public String CreateProductionCode( TestSheet testSheet ) => "IT/" + testSheet.MachineNr.Substring(1) + "/" + testSheet.CreatedDateTime.Year.ToString().Substring(2) + "/";

        /// <summary>
        ///     Creates a new testvalue initialized with following values
        /// </summary>
        /// <param name="testSheetId">ID of the testsheet</param>
        /// <param name="testPerson">the Testperson</param>
        /// <param name="productionCodeDay">the production-day of the diaper</param>
        /// <param name="notes">the notes for the testvalue</param>
        /// <returns>the created testvalue</returns>
        public TestValue CreateNewTestValue( Int32 testSheetId, String testPerson, Int32 productionCodeDay, IList<TestNote> notes )
        {
            var testValue = new TestValue
            {
                TestSheetRefId = testSheetId,
                CreatedDateTime = DateTime.Now,
                LastEditedDateTime = DateTime.Now,
                CreatedPerson = testPerson,
                LastEditedPerson = testPerson,
                DayInYearOfArticleCreation = productionCodeDay
            };
            if (notes.IsNotNull())
                testValue.TestValueNote = notes.Select(error => new TestValueNote { ErrorRefId = error.ErrorCodeId, Message = error.Message, TestValue = testValue })
                                                   .ToList();
            return testValue;
        }

        #endregion
    }
}