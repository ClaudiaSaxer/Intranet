using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Intranet.Labor.Model.labor
{
    /// <summary>
    ///     Class representing the baby diaper test value
    /// </summary>
    public class BabyDiaperTestValue
    {
        #region Properties

        /// <summary>
        ///     Gets or sets the id of the baby diaper test value
        /// </summary>
        /// <value>the baby diaper test value id </value>
        [Key]
        [DatabaseGenerated( DatabaseGeneratedOption.Identity )]
        public Int32 BabyDiaperTestValueId { get; set; }

        /// <summary>
        ///     Gets or sets the time the diaper was created
        /// </summary>
        /// <value>the time the diaper was created</value>
        public TimeSpan DiaperCreatedTime { get; set; }

        /// <summary>
        ///     Gets or sets the weight the diaper has if it is dry
        /// </summary>
        /// <value>the weight of the dry diaper</value>
        public Double WeightyDiaperDry { get; set; }

        /// <summary>
        ///     Gets or sets the value for the revet after 140ml
        /// </summary>
        /// <value>the value for the revet after 140ml</value>
        public Double Revert140Value { get; set; }

        /// <summary>
        ///     Gets or sets the value for revet after 120ml
        /// </summary>
        /// <value>the value for the revet after 120ml</value>
        public Double Revet120Value { get; set; }

        /// <summary>
        ///     Gets or sets the value after the strike trough (with 210ml in g)
        /// </summary>
        /// <value>the value after the strike through</value>
        public Double StrikeTroughValue { get; set; }

        /// <summary>
        ///     Gets or sets the distribution after the strikte trough (in mm)
        /// </summary>
        /// <value>the distribution after the strikte trough</value>
        public Double DistributionOfTheStrikeTrough { get; set; }

        /// <summary>
        ///     Gets or sets the weight of the diaper, wet after the retention
        /// </summary>
        /// <value>the weight of a wet diaper after retention</value>
        public Double RetentionWetWeight { get; set; }

        /// <summary>
        ///     Gets or sets the RW of the revet for 140ml
        /// </summary>
        /// <value>the RW of the revet for 140ml</value>
        public RWType Revet140RW { get; set; }

        /// <summary>
        ///     Gets or sets the RW of the revet for 210ml
        /// </summary>
        /// <value>the RW of the revet for 210ml</value>
        public RWType Revet210RW { get; set; }

        /// <summary>
        ///     Gets or sets the RW of the retention
        /// </summary>
        /// <value>the RW of the retention</value>
        public RWType RetentionRW { get; set; }

        /// <summary>
        ///     Gets or sets the rentention value after the zentrifuge
        /// </summary>
        /// <value>the retention value after zentrifuge</value>
        public Double RetentionAfterZentrifugeValue { get; set; }

        /// <summary>
        ///     Gets or sets the rentention percent after the zentrifuge
        /// </summary>
        /// <value>the retention percent after the zentrifuge</value>
        public Double RetentionAfterZentrifugePercent { get; set; }

        /// <summary>
        ///     Gets or sets the value for the SAP g/Höwi
        /// </summary>
        /// <value>the SAP g/Höwi value</value>
        public Double SAPgHoewiValue { get; set; }

        /// <summary>
        ///     Gets or sets the test sheet of the baby diaper test value
        /// </summary>
        /// <value>gets or sets the test sheet</value>
        public TestSheet TestSheet { get; set; }

        #endregion
    }
}