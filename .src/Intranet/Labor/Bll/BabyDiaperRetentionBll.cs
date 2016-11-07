#region Usings

using System;
using System.Collections.Generic;
using System.Linq;
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
    public class BabyDiaperRetentionBll : IBabyDiaperRetentionBll
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
        ///     ProductionOrderRepository
        /// </summary>
        public IGenericRepository<ProductionOrder> ProductionOrderRepository { get; set; }

        #endregion

        #region Implementation of IBabyDiaperRetentionBll

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
        #endregion
    }
}