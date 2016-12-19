#region Usings

using System;
using System.Collections.Generic;
using System.Linq;
using Intranet.Common;
using Intranet.Labor.Definition;
using Intranet.Labor.Model;

#endregion

namespace Intranet.Labor.Bll
{
    /// <summary>
    ///     Class representing labor creator bll
    /// </summary>
    public class LaborCreatorBll : ServiceBase, ILaborCreatorBll
    {
        #region Properties

        /// <summary>
        ///     Gets or sets the repository for the production order
        /// </summary>
        /// <value>the production order repository</value>
        public IGenericRepository<TestSheet> TestSheetRepository { get; set; }

        /// <summary>
        ///     Gets or sets the repository for the production order
        /// </summary>
        /// <value>the production order repository</value>
        public IGenericRepository<ProductionOrder> ProductionOrderRepository { get; set; }

        /// <summary>
        ///     Gets or sets the shifthelper
        /// </summary>
        /// <value>the shifthelper</value>
        public IShiftHelper ShiftHelper { get; set; }

        #endregion

        #region Ctor

        /// <summary>
        ///     Initialize a new instance of the <see cref="ServiceBase" /> class.
        /// </summary>
        /// <param name="loggerFactory">A <see cref="ILoggerFactory" />.</param>
        public LaborCreatorBll( ILoggerFactory loggerFactory )
            : base( loggerFactory.CreateLogger( typeof(LaborCreatorBll) ) )
        {
        }

        #endregion

        private TestValue CreateDefaultTestValueBabyDiaper(
                TestValueType testValueType,
                TestTypeBabyDiaper
                    testTypeBabyDiaper
            )
            => new TestValue
            {
                ArticleTestType = ArticleType.BabyDiaper,
                CreatedDateTime = DateTime.Now,
                CreatedPerson = "system",
                LastEditedDateTime = DateTime.Now,
                TestValueType = testValueType,
                LastEditedPerson = "system",
                DayInYearOfArticleCreation = DateTime.Now.DayOfYear,
                BabyDiaperTestValue = new BabyDiaperTestValue
                {
                    TestType = testTypeBabyDiaper,
                    RetentionRw = RwType.Ok,
                    Rewet140Rw = RwType.Ok,
                    Rewet210Rw = RwType.Ok,
                    PenetrationRwType = RwType.Ok
                }
            };

        private TestValue CreateDefaultTestValueIncontinencePad(
                TestValueType testValueType,
                TestTypeIncontinencePad
                    testTypeIncontinencePad
            )
            => new TestValue
            {
                ArticleTestType = ArticleType.IncontinencePad,
                CreatedDateTime = DateTime.Now,
                CreatedPerson = "system",
                LastEditedDateTime = DateTime.Now,
                TestValueType = testValueType,
                LastEditedPerson = "system",
                DayInYearOfArticleCreation = DateTime.Now.DayOfYear,
                IncontinencePadTestValue = new IncontinencePadTestValue
                {
                    TestType = testTypeIncontinencePad,
                    AcquisitionTimeFirstRw = RwType.Ok,
                    AcquisitionTimeSecondRw = RwType.Ok,
                    AcquisitionTimeThirdRw = RwType.Ok,
                    RetentionRw = RwType.Ok,
                    RewetFreeRw = RwType.Ok
                }
            };

        #region Implementation of ILaborCreatorBll

        /// <summary>
        ///     The TestSheets which are running at the moment
        /// </summary>
        /// <returns>the running production orders</returns>
        public ICollection<TestSheet> RunningTestSheets()
        {
            var today = DateTime.Today;
            var shift = ShiftHelper.GetCurrentShift();
            if ( shift == null )
                return null;
            return TestSheetRepository.GetAll()
                                      .Where( sheet => sheet.DayInYear.Equals( today.DayOfYear ) && ( sheet.ShiftType == shift ) )
                                      .ToList();
        }

        /// <summary>
        ///     The current existing testsheet for given fanr or null if not existing
        /// </summary>
        /// <param name="faNr">the production order number</param>
        /// <returns>the current testsheet</returns>
        public TestSheet GetTestSheetForFaNr( String faNr )
        {
            var today = DateTime.Today;
            var shift = ShiftHelper.GetCurrentShift();
            if ( shift == null )
                return null;
            var testsheet = TestSheetRepository.GetAll()
                                               .Where( sheet => sheet.FaNr.Equals( faNr ) && sheet.DayInYear.Equals( today.DayOfYear ) && ( sheet.ShiftType == shift ) )
                                               .ToList();
            return testsheet.ToList()
                            .Count == 1
                ? testsheet[0]
                : null;
        }

        /// <summary>
        ///     Initializes a new testsheet for the faNr and the current date
        /// </summary>
        /// <param name="faNr">the production order number</param>
        /// <returns>the initialized testsheet</returns>
        public TestSheet InitTestSheetForFaNr( String faNr )
        {
            var productionOrder = ProductionOrderRepository.GetAll()
                                                           .FirstOrDefault( order => order.FaNr == faNr );

            if ( productionOrder == null )
            {
                Logger.Error( "Fanr " + faNr + " not found in Production Order" );
                return null;
            }
            var shift = ShiftHelper.GetCurrentShift();

            if ( shift == null )
                return null;

            var testvalues = new List<TestValue>();
            if ( productionOrder.Article.ArticleType == ArticleType.BabyDiaper )
                testvalues = new List<TestValue>
                {
                    CreateDefaultTestValueBabyDiaper( TestValueType.Average, TestTypeBabyDiaper.Retention ),
                    CreateDefaultTestValueBabyDiaper( TestValueType.Average, TestTypeBabyDiaper.Rewet ),
                    CreateDefaultTestValueBabyDiaper( TestValueType.Average, TestTypeBabyDiaper.RewetAndPenetrationTime ),
                    CreateDefaultTestValueBabyDiaper( TestValueType.StandardDeviation, TestTypeBabyDiaper.Retention ),
                    CreateDefaultTestValueBabyDiaper( TestValueType.StandardDeviation, TestTypeBabyDiaper.Rewet ),
                    CreateDefaultTestValueBabyDiaper( TestValueType.StandardDeviation, TestTypeBabyDiaper.RewetAndPenetrationTime )
                };
            if ( productionOrder.Article.ArticleType == ArticleType.IncontinencePad )

                testvalues = new List<TestValue>
                {
                    CreateDefaultTestValueIncontinencePad( TestValueType.StandardDeviation, TestTypeIncontinencePad.AcquisitionTimeAndRewet ),
                    CreateDefaultTestValueIncontinencePad( TestValueType.StandardDeviation, TestTypeIncontinencePad.Retention ),
                    CreateDefaultTestValueIncontinencePad( TestValueType.StandardDeviation, TestTypeIncontinencePad.RewetFree ),
                    CreateDefaultTestValueIncontinencePad( TestValueType.Average, TestTypeIncontinencePad.AcquisitionTimeAndRewet ),
                    CreateDefaultTestValueIncontinencePad( TestValueType.Average, TestTypeIncontinencePad.Retention ),
                    CreateDefaultTestValueIncontinencePad( TestValueType.Average, TestTypeIncontinencePad.RewetFree )
                };

            var testSheet = new TestSheet
            {
                ArticleType = productionOrder.Article.ArticleType,
                ShiftType = shift.Value,
                FaNr = productionOrder.FaNr,
                TestValues = testvalues,
                CreatedDateTime = DateTime.Now,
                SizeName = productionOrder.Article.SizeName,
                ProductName = productionOrder.Article.ProductName,
                DayInYear = DateTime.Now.DayOfYear,
                MachineNr = productionOrder.Machine.MachineNr,
                SAPNr = productionOrder.Component?.ComponentNr,
                SAPType = productionOrder.Component?.ComponentType
            };
            TestSheetRepository.Add( testSheet );
            TestSheetRepository.SaveChanges();

            return testSheet;
        }

        #endregion

        #region Implementation of ILaborCreatorBll

        #endregion
    }
}