using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Intranet.Labor.Model.labor
{
    /// <summary>
    ///     Class representing incontinence pad test value
    /// </summary>
    public class IncontinencePadTestValue
    {
        #region Properties

        /// <summary>
        ///     Gets or sets the id of the incontinence pad test value
        /// </summary>
        /// <value>the incontinence pad test value id </value>
        [ForeignKey( "TestValue" )]
        public Int32 IncontinencePadTestValueId { get; set; }

        /// <summary>
        ///     Gets or sets the time the  incontinence pad was created
        /// </summary>
        /// <value>the time the  incontinence pad was created</value>
        public TimeSpan IncontinencePadTime { get; set; }

        /// <summary>
        ///     Gets or sets the month the incontinence pad expires
        /// </summary>
        /// <value>the expire montn</value>
        public Int32 ExpireMonth { get; set; }

        /// <summary>
        ///     Gets or sets the year the incontinence pad expires
        /// </summary>
        /// <value>the expire year</value>
        public Int32 ExpireYear { get; set; }

        /// <summary>
        ///     Gets or sets the value for the rewet dry
        /// </summary>
        /// <value>the value for the rewet dry</value>
        public Double RewetFreeDryValue { get; set; }

        /// <summary>
        ///     Gets or sets the RW for the rewet
        /// </summary>
        /// <value>the RW for the rewet</value>
        public RwType RewetFreeRw { get; set; }

        /// <summary>
        ///     Gets or sets the value for the rewet wet
        /// </summary>
        /// <value>the value for the rewet wet</value>
        public Double RewetFreeWetValue { get; set; }

        /// <summary>
        ///     Gets or sets the difference of the rewet wet and dry
        /// </summary>
        /// <value>the difference of the rewet</value>
        public Double RewetFreeDifference { get; set; }

        /// <summary>
        ///     Gets or sets the weight of the retention
        /// </summary>
        /// <value>the retention weight</value>
        public Double RetentionWeight { get; set; }

        /// <summary>
        ///     Gets or sets the weight of the retention wet
        /// </summary>
        /// <value>the weight of the retention wet</value>
        public Double RetentionWetValue { get; set; }

        /// <summary>
        ///     Gets or sets the value for the rentention after the zentrifuge
        /// </summary>
        /// <value>the retention value after zentrifuge</value>
        public Double RetentionAfterZentrifuge { get; set; }

        /// <summary>
        ///     Gets or sets the retention absorbation value
        /// </summary>
        /// <value>the retention absorbation value</value>
        public Double RetentionAbsorbtion { get; set; }

        /// <summary>
        ///     Gets or sets the retention end value
        /// </summary>
        /// <value>the retention end value</value>
        public Double RetentionEndValue { get; set; }

        /// <summary>
        ///     Gets or sets the RW for the retention
        /// </summary>
        /// <value>the RW for the retention</value>
        public RwType RetentionRw { get; set; }

        /// <summary>
        ///     Gets or sets the weight of the pad for the acquisition
        /// </summary>
        /// <value>the weight of the acquisition</value>
        public Double AcquisitionWeight { get; set; }

        /// <summary>
        ///     Gets or sets the acquisition after the first addition
        /// </summary>
        /// <value>the acquisition after the first addition</value>
        public Double AcquisitionTimeFirst { get; set; }

        /// <summary>
        ///     Gets or sets the acquisition after the second addition
        /// </summary>
        /// <value>the acquisition after the second addition</value>
        public Double AcquisitionTimeSecond { get; set; }

        /// <summary>
        ///     Gets or sets the acquisition after the third addition
        /// </summary>
        /// <value>the acquisition after the third addition</value>
        public Double AcquisitionTimeThird { get; set; }

        /// <summary>
        ///     Gets or sets the acquisition RW after the first addition
        /// </summary>
        /// <value>the acquisition RW after the first addition</value>
        public RwType AcquisitionTimeFirstRw { get; set; }

        /// <summary>
        ///     Gets or sets the acquisition RW after the second addition
        /// </summary>
        /// <value>the acquisition  RW after the second addition</value>
        public RwType AcquisitionTimeSecondRw { get; set; }

        /// <summary>
        ///     Gets or sets the acquisition  RW after the third addition
        /// </summary>
        /// <value>the acquisition RW after the third addition</value>
        public RwType AcquisitionTimeThirdRw { get; set; }

        /// <summary>
        ///     Gets or sets the weight of a wet pad for the rewet after the acquisition time
        /// </summary>
        /// <value>the weight of a wet pad for the rewet after the acquisition time</value>
        public Double RewetAfterAcquisitionTimeWetWeight { get; set; }

        /// <summary>
        ///     Gets or sets the weight of a dry pad for the rewet after the acquisition time
        /// </summary>
        /// <value>the weight of a dry pad for the rewet after the acquisition time</value>
        public Double RewetAfterAcquisitionTimeDryWeight { get; set; }

        /// <summary>
        ///     Gets or sets the weight difference of a  pad for the rewet after the acquisition time
        /// </summary>
        /// <value>the weight difference of a  pad for the rewet after the acquisition time</value>
        public Double RewetAfterAcquisitionTimeWeightDifference { get; set; }

        /// <summary>
        ///     Gets or sets the rw of a pad for the rewet after the acquisition time
        /// </summary>
        /// <value>the rw of a pad for the rewet after the acquisition time</value>
        public RwType RewetAfterAcquisitionTimeRw { get; set; }

        /// <summary>
        ///     Gets or sets the test sheet of the incontinence pad test value
        /// </summary>
        /// <value>gets or sets the test sheet</value>
        public virtual TestSheet TestSheet { get; set; }

        /// <summary>
        ///     Gets or sets the test value of the incontinence pad test value
        /// </summary>
        /// <value>the test value</value>
        public virtual TestValue TestValue { get; set; }

        /// <summary>
        ///     Gets or sets the test type of the incontinence pad test value
        /// </summary>
        /// <value>the test typof the incontinence pad test value</value>
        public TestTypeIncontinencePad TestType { get; set; }

        #endregion
    }
}