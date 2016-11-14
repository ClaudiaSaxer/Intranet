using System;
using System.Collections.Generic;

namespace Intranet.Labor.ViewModel
{
    /// <summary>
    ///     Class representing the view model for the labor creator
    /// </summary>
    public class IncontinencePadLaborCreatorViewModel
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
        ///     Gets or sets the free rewets
        /// </summary>
        /// <value>collection of free rewet</value>
        public ICollection<IncontinencePadRewetTestValue> FreeRewets { get; set; }

        /// <summary>
        ///     Gets or sets the average of the free rewets
        /// </summary>
        /// <value>the average of the free rewet</value>
        public IncontinencePadRewet IncontinencePadFreeRewetAverage { get; set; }

        /// <summary>
        ///     Gets or sets the standard deviaion of the free rewets
        /// </summary>
        /// <value>the standard deviation of the free rewets</value>
        public IncontinencePadRewet IncontinencePadFreeRewetStandardDeviation { get; set; }

        /// <summary>
        ///     Gets or sets the AcquisitionTime rewets
        /// </summary>
        /// <value>collection of AcquisitionTime rewet</value>
        public ICollection<IncontinencePadRewetTestValue> AfterAcquisitionTimeRewets { get; set; }

        /// <summary>
        ///     Gets or sets the average of the AcquisitionTime rewets
        /// </summary>
        /// <value>the average of the AcquisitionTime rewet</value>
        public IncontinencePadRewet IncontinencePadAcquisitionTimeRewetAverage { get; set; }

        /// <summary>
        ///     Gets or sets the standard deviaion of the AcquisitionTime rewets
        /// </summary>
        /// <value>the standard deviation of the AcquisitionTime rewets</value>
        public IncontinencePadRewet IncontinencePadAcquisitionTimeRewetStandardDeviation { get; set; }


        /// <summary>
        ///     Gets or sets the retentions
        /// </summary>
        /// <value>collection of retention</value>
        public ICollection<IncontinencePadRetentionTestValue> Retentions { get; set; }

        /// <summary>
        ///     Gets or sets the average of the retention
        /// </summary>
        /// <value>the average of the retention</value>
        public
            IncontinencePadRetention IncontinencePadRetentionAverage { get; set; }

        /// <summary>
        ///     Gets or sets the standard deviaion of the retention
        /// </summary>
        /// <value>the standard deviation of the retention</value>
        public IncontinencePadRetention IncontinencePadRetentionStandardDeviation { get; set; }


        /// <summary>
        ///     Gets or sets the aquisation time
        /// </summary>
        /// <value>collection of aquisation time</value>
        public ICollection<IncontinencePadAcquisitionTime> AcquisitionTimes { get; set; }

        /// <summary>
        ///     Gets or sets the average of the aquisation time
        /// </summary>
        /// <value>the average of the aquisation time</value>
        public IncontinencePadAcquisitionTime AcquisitionTimeAverage { get; set; }

        /// <summary>
        ///     Gets or sets the standard deviaion of the penetration time
        /// </summary>
        /// <value>the standard deviation of the penetration time</value>
        public IncontinencePadAcquisitionTime AcquisitionTimeStandardDeviation { get; set; }

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
        #endregion
    }
}