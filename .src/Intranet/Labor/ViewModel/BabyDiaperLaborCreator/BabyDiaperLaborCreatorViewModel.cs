using System;
using System.Collections.Generic;

namespace Intranet.Labor.ViewModel
{
    /// <summary>
    ///     Class representing the view model for the labor creator
    /// </summary>
    public class BabyDiaperLaborCreatorViewModel
    {
        #region Properties

        /// <summary>
        ///     Gets or sets the producer
        /// </summary>
        /// <value>the producer</value>
        public String Producer { get; set; }

        /// <summary>
        ///     Gets or sets the shift
        /// </summary>
        /// <value>the shift</value>
        public String Shift { get; set; }

        /// <summary>
        ///     Gets or sets the production order number
        /// </summary>
        /// <value>the production order number</value>
        public String FaNr { get; set; }

        /// <summary>
        ///     Gets or sets the timestamp of the creation of the test sheet
        /// </summary>
        /// <value>the timestamp of the creation</value>
        public String CreatedDate { get; set; }

        /// <summary>
        ///     Gets or sets the name of the product
        /// </summary>
        /// <value>the name of the product</value>
        public String ProductName { get; set; }

        /// <summary>
        ///     Gets or sets the name of the size
        /// </summary>
        /// <value>the name of the size</value>
        public String SizeName { get; set; }

        /// <summary>
        ///     Gets or sets the rewets
        /// </summary>
        /// <value>collection of rewet</value>
        public ICollection<BabyDiaperRewetTestValue> Rewets { get; set; }

        /// <summary>
        ///     Gets or sets the average of the rewets
        /// </summary>
        /// <value>the average of the rewet</value>
        public BabyDiaperRewet BabyDiaperRewetAverage { get; set; }

        /// <summary>
        ///     Gets or sets the standard deviaion of the rewets
        /// </summary>
        /// <value>the standard deviation of the rewets</value>
        public BabyDiaperRewet BabyDiaperRewetStandardDeviation { get; set; }

        /// <summary>
        ///     Gets or sets the retentions
        /// </summary>
        /// <value>collection of retention</value>
        public ICollection<BabyDiaperRetentionTestValue> Retentions { get; set; }

        /// <summary>
        ///     Gets or sets the average of the retention
        /// </summary>
        /// <value>the average of the retention</value>
        public BabyDiaperRetention BabyDiaperRetentionAverage { get; set; }

        /// <summary>
        ///     Gets or sets the standard deviaion of the retention
        /// </summary>
        /// <value>the standard deviation of the retention</value>
        public BabyDiaperRetention BabyDiaperRetentionStandardDeviation { get; set; }

        /// <summary>
        ///     Gets or sets the PenetrationTimes
        /// </summary>
        /// <value>collection of penetrationtime</value>
        public ICollection<BabyDiaperPenetrationTimeTestValue> PenetrationTimes { get; set; }

        /// <summary>
        ///     Gets or sets the average of the penetration time
        /// </summary>
        /// <value>the average of the penetration time</value>
        public BabyDiaperPenetrationTime BabyDiaperPenetrationTimeAverage { get; set; }

        /// <summary>
        ///     Gets or sets the standard deviaion of the penetration time
        /// </summary>
        /// <value>the standard deviation of the penetration time</value>
        public BabyDiaperPenetrationTime BabyDiaperPenetrationTimeStandardDeviation { get; set; }

        /// <summary>
        /// Gets or sets the average of all tests
        /// </summary>
        /// <value>the average of all tests</value>
        public Double WeigthAverageAll { get; set; }        
        
        /// <summary>
        /// Gets or sets the standard Deviation of all tests
        /// </summary>
        /// <value>the standard deviation of all tests</value>
        public Double WeightStandardDeviationAll { get; set; }

        /// <summary>
        /// Gets or sets the id for the test sheet
        /// </summary>
        /// <value>the testsheet id</value>
        public Int32 TestSheetId { get; set; }

        /// <summary>
        /// Gets or sets the boolean, if the current user is allowed to edit the baby diaper
        /// </summary>
        /// <value>true if the current user is allowed to edit the baby diaper</value>
        public Boolean CanEdit { get; set; }
        #endregion
    }
}