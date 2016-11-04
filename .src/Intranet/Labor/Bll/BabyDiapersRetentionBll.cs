#region Usings

using System;
using System.Collections.Generic;
using System.Linq;
using Intranet.Common;
using Intranet.Labor.Definition;
using Intranet.Labor.Model;
using Intranet.Labor.Model.labor;

#endregion

namespace Intranet.Labor.Bll
{
    /// <summary>
    ///     Class representing the bll of the Baby diapers retention
    /// </summary>
    public class BabyDiapersRetentionBll : IBabyDiapersRetentionBll
    {
        #region Properties

        /// <summary>
        ///     TestSheetRepository
        /// </summary>
        public IGenericRepository<TestSheet> TestSheetRepository { get; set; }

        /// <summary>
        ///     ErrorRepository
        /// </summary>
        public IGenericRepository<Error> ErrorRepository { get; set; }

        /// <summary>
        ///     BabyDiaperTestValueRepository
        /// </summary>
        public IGenericRepository<TestValue> TestValueRepository { get; set; }

        /// <summary>
        ///     BabyDiaperTestValueRepository
        /// </summary>
        public IGenericRepository<BabyDiaperTestValue> BabyDiaperTestValueRepository { get; set; }

        /// <summary>
        ///     TestValueNoteRepository
        /// </summary>
        public IGenericRepository<TestValueNote> TestValueNoteRepository { get; set; }

        #endregion

        #region Implementation of IBabyDiapersRetentionBll

        /// <summary>
        ///     Query for an babydiapers retention test
        /// </summary>
        /// <param name="retentionTestId">The ID of the retention Test</param>
        /// <returns>The retentiontest with the given Id</returns>
        public BabyDiaperTestValue GetBabyDiapersRetetionTest( Int32 retentionTestId )
        {
            var babyDiaperTestValue = BabyDiaperTestValueRepository.Where( tv => tv.BabyDiaperTestValueId == retentionTestId )
                                                                   .FirstOrDefault();
            return babyDiaperTestValue;
        }

        /// <summary>
        ///     Query for a testvalue
        /// </summary>
        /// <param name="retentionTestId">The ID of the testvalue</param>
        /// <returns>The test value with the given Id</returns>
        public TestValue GetTestValue( Int32 retentionTestId )
        {
            var testValue = TestValueRepository.FindAsync(retentionTestId)
                                               .Result;
            return testValue;
        }

        /// <summary>
        ///     Query for the Test sheet info
        /// </summary>
        /// <param name="testSheetId">The ID of the test sheet</param>
        /// <returns>The test sheet</returns>
        public TestSheet GetTestSheetInfo( Int32 testSheetId )
        {
            var testSheet = TestSheetRepository.FindAsync( testSheetId )
                                               .Result;
            return testSheet;
        }

        /// <summary>
        ///     Query for all Error Codes
        /// </summary>
        /// <returns>Collection of all Error Codes</returns>
        public IEnumerable<Error> GetAllNoteCodes()
        {
            var errors = ErrorRepository.GetAll()
                                        .ToList();
            return errors;
        }

        /// <summary>
        ///     Query for all notes for the testValue
        /// </summary>
        /// ///
        /// <param name="testValueId">The ID of the test value</param>
        /// <returns>Collection of all notes for the testValue</returns>
        public IEnumerable<TestValueNote> GetNotes( Int32 testValueId )
        {
            var notes = TestValueNoteRepository.Where( n => n.TestValueRefId == testValueId )
                                               .ToList();
            return notes;
        }

        /// <summary>
        ///     Saves a new testvalue in the db
        /// </summary>
        /// <param name="testValue">the test value which will be saved</param>
        public void SaveNewTestValue( TestValue testValue )
        {
            TestValueRepository.Add( testValue );
            TestValueRepository.SaveChanges();
        }

        /// <summary>
        ///     update an testvalue
        /// </summary>
        /// <param name="testValue">the testvalue which will be updated</param>
        public void UpdateTestValue( TestValue testValue ) => TestSheetRepository.SaveChanges();

        #endregion
    }
}