#region Usings

using System;
using System.ComponentModel.DataAnnotations.Schema;

#endregion

namespace Intranet.Labor.Model
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
        [ForeignKey( "TestValue" )]
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
        public Double WeightDiaperDry { get; set; }

        /// <summary>
        ///     Gets or sets the value for the revet after 140ml
        /// </summary>
        /// <value>the value for the revet after 140ml</value>
        public Double Rewet140Value { get; set; }

        /// <summary>
        ///     Gets or sets the value for revet after 120ml
        /// </summary>
        /// <value>the value for the revet after 120ml</value>
        public Double Rewet210Value { get; set; }

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
        ///     Gets or sets the RW of the revet for 140ml
        /// </summary>
        /// <value>the RW of the revet for 140ml</value>
        public RwType? Rewet140Rw { get; set; }

        /// <summary>
        ///     Gets or sets the RW of the revet for 210ml
        /// </summary>
        /// <value>the RW of the revet for 210ml</value>
        public RwType? Rewet210Rw { get; set; }

        /// <summary>
        ///     Gets or sets the RW of the retention
        /// </summary>
        /// <value>the RW of the retention</value>
        public RwType? RetentionRw { get; set; }

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
        ///     Gets or sets the retention wet weight of the diaper
        /// </summary>
        /// <value>the wet weight of the diaper</value>
        public Double RetentionWetWeight { get; set; }

        /// <summary>
        ///     Gets or sets the value for the SAP g/Höwi
        /// </summary>
        /// <value>the SAP g/Höwi value</value>
        public Double SapGHoewiValue { get; set; }

        /// <summary>
        ///     Gets or sets the sap type
        /// </summary>
        /// <value>the sap type</value>
        public String SapType { get; set; }

        /// <summary>
        ///     Gets or sets the sap number
        /// </summary>
        /// <value>the sap number</value>
        public String SapNr { get; set; }

        /// <summary>
        ///     Gets or sets the test value of the baby diaper test value
        /// </summary>
        /// <value>the test value</value>
        public virtual TestValue TestValue { get; set; }

        /// <summary>
        ///     Gets or sets the value of the penetrationTime of the first addition
        /// </summary>
        /// <value>penetration time value for the first addition</value>
        public Double PenetrationTimeAdditionFirst { get; set; }

        /// <summary>
        ///     Gets or sets the value of the penetrationTime of the second addition
        /// </summary>
        /// <value>penetration time value for the second addition</value>
        public Double PenetrationTimeAdditionSecond { get; set; }

        /// <summary>
        ///     Gets or sets the value of the penetrationTime of the third addition
        /// </summary>
        /// <value>penetration time value for the third addition</value>
        public Double PenetrationTimeAdditionThird { get; set; }

        /// <summary>
        ///     Gets or sets the value of the penetrationTime of the fourth addition
        /// </summary>
        /// <value>penetration time value for the fourth addition</value>
        public Double PenetrationTimeAdditionFourth { get; set; }

        /// <summary>
        ///     Gets or sets the RW of the Penetration test
        /// </summary>
        /// <value>the rw type of the penetration test</value>
        public RwType? PenetrationRwType { get; set; }

        /// <summary>
        ///     Gets or sets the test type of the baby diaper test value
        /// </summary>
        /// <value>the test type of the baby diaper test value</value>
        public TestTypeBabyDiaper TestType { get; set; }

        #endregion
    }
}