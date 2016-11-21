#region Usings

using System;
using System.Collections.Generic;
using System.Linq;
using Extend;
using Intranet.Common;
using Intranet.Labor.Dal.Repositories;
using Intranet.Labor.Definition;
using Intranet.Labor.Model;
using Intranet.Labor.Model.labor;

#endregion

namespace Intranet.Labor.Bll
{
    /// <summary>
    ///     Class representing the bll of the Baby diapers retention
    /// </summary>
    public class TestBll : ITestBll
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
        ///     TestValueRepository
        /// </summary>
        public IGenericRepository<TestValue> TestValueRepository { get; set; }

        /// <summary>
        ///     ProductionOrderRepository
        /// </summary>
        public IGenericRepository<ProductionOrder> ProductionOrderRepository { get; set; }

        /// <summary>
        ///     BabyDiaperTestValueRepository
        /// </summary>
        public IGenericRepository<BabyDiaperTestValue> BabyDiaperTestValueRepository { get; set; }

        /// <summary>
        ///     IncontinencePadTestValueRepository
        /// </summary>
        public IGenericRepository<IncontinencePadTestValue> IncontinencePadTestValueRepository { get; set; }

        /// <summary>
        ///     TestValueNoteRepository
        /// </summary>
        public IGenericRepository<TestValueNote> TestValueNoteRepository { get; set; }

        #endregion

        #region Implementation of ITestBll

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
        ///     Saves a new testvalue in the db
        /// </summary>
        /// <param name="testValue">the test value which will be saved</param>
        public TestValue SaveNewTestValue( TestValue testValue )
        {
            TestValueRepository.Add( testValue );
            TestValueRepository.SaveChanges();
            return testValue;
        }

        /// <summary>
        ///     update an testvalue
        /// </summary>
        /// <param name="testValue">the testvalue which will be updated</param>
        public TestValue UpdateTestValue(TestValue testValue)
        {
            TestSheetRepository.SaveChanges();
            return testValue;
        }

        /// <summary>
        ///     Updates the TestSheet
        /// </summary>
        public Int32 UpdateTestSheet() => TestSheetRepository.SaveChanges();

        /// <summary>
        ///     Query for the ProductionOrder
        /// </summary>
        /// <param name="productionOrderFa">the Id of the Production order</param>
        public ProductionOrder GetProductionOrder( String productionOrderFa )
        {
            var pO = ProductionOrderRepository.Where( p => p.FaNr == productionOrderFa )
                                     .FirstOrDefault();
            if ( pO != null )
                return ProductionOrderRepository.FindAsync( pO.FaId )
                                                .Result;
            return null;
        }

        /// <summary>
        ///     Delete Testvalue from DB
        /// </summary>
        /// <param name="testValueId">the Id of the Test value</param>
        public TestValue DeleteTestValue( Int32 testValueId )
        {
            var testValue = TestValueRepository.FindAsync( testValueId )
                               .Result;
            if ( testValue.IsNull() || testValue.TestValueType != TestValueType.Single )
                return null;
            if ( testValue.ArticleTestType == ArticleType.BabyDiaper )
            {
                BabyDiaperTestValueRepository.Attach( testValue.BabyDiaperTestValue );
                BabyDiaperTestValueRepository.Remove( testValue.BabyDiaperTestValue );
                BabyDiaperTestValueRepository.SaveChanges();
            }
            else
            {
                IncontinencePadTestValueRepository.Attach(testValue.IncontinencePadTestValue);
                IncontinencePadTestValueRepository.Remove(testValue.IncontinencePadTestValue);
                IncontinencePadTestValueRepository.SaveChanges();
            }
            while ( !testValue.TestValueNote.IsNullOrEmpty() )
            {
                var firstOrDefault = testValue.TestValueNote.FirstOrDefault();
                if ( firstOrDefault != null )
                    DeleteNote( firstOrDefault
                                         .TestValueNoteId );
            }
            TestValueRepository.Attach(testValue);
            var result = TestValueRepository.Remove( testValue );
            TestValueRepository.SaveChanges();
            return result;
        }

        /// <summary>
        ///     Delete TestvalueNote from DB
        /// </summary>
        /// <param name="testValueNoteId">the Id of the Test value Note</param>
        public TestValueNote DeleteNote( Int32 testValueNoteId )
        {
            var note = TestValueNoteRepository.FindAsync( testValueNoteId )
                                              .Result;
            if (note.IsNull())
                return null;
            var result = TestValueNoteRepository.Remove( note );
            TestValueNoteRepository.SaveChanges();
            return result;
        }

        #endregion
    }
}