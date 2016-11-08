using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Intranet.Labor.Model.labor;

namespace Intranet.Labor.ViewModel
{
    /// <summary>
    ///     Class representing the ViewModel for the BabyDiaperRewetController
    /// </summary>
    public class BabyDiaperRewetEditViewModel
    {
        #region Properties

        /// <summary>
        ///     Gets or sets the ID of the Babydapers test
        /// </summary>
        /// <value>
        ///     The ID of the Babydapers test
        /// </value>
        public Int32 TestValueId { get; set; }

        /// <summary>
        ///     Gets or sets the ID of the Test Sheet
        /// </summary>
        /// <value>
        ///     The ID of the Test Sheet
        /// </value>
        public Int32 TestSheetId { get; set; }

        /// <summary>
        ///     Gets or sets the TestPerson
        /// </summary>
        /// <value>
        ///     The TestPerson
        /// </value>
        public String TestPerson { get; set; }

        /// <summary>
        ///     Gets or sets the ProductionCode
        /// </summary>
        /// <value>
        ///     The ProductionCode
        /// </value>
        public String ProductionCode { get; set; }

        /// <summary>
        ///     Gets or sets the ProductionCodeDay
        /// </summary>
        /// <value>
        ///     The ProductionCodeDay
        /// </value>
        public Int32 ProductionCodeDay { get; set; }

        /// <summary>
        ///     Gets or sets the ProductionCodeTime
        /// </summary>
        /// <value>
        ///     The ProductionCodeTime
        /// </value>
        ///         [DisplayName( "Tag im Jahr" )]
        [Required(ErrorMessage = "Der Tag muss angegeben werden")]
        [Range(0, 366, ErrorMessage = "Die Anzahl Tage dürfen ein Jahr nicht überschreiten")]
        public TimeSpan ProductionCodeTime { get; set; }

        /// <summary>
        ///     Gets or sets the DiaperWeight value
        /// </summary>
        /// <value>
        ///     The DiaperWeight value
        /// </value>
        public Double DiaperWeight { get; set; }

        /// <summary>
        ///     Gets or sets the RewetAfter140 value
        /// </summary>
        /// <value>
        ///     The RewetAfter140 value
        /// </value>
        public Double RewetAfter140 { get; set; }

        /// <summary>
        ///     Gets or sets the RewetAfter210 value
        /// </summary>
        /// <value>
        ///     The RewetAfter210 value
        /// </value>
        public Double RewetAfter210 { get; set; }

        /// <summary>
        ///     Gets or sets the StrikeThrough value
        /// </summary>
        /// <value>
        ///     The StrikeThrough value
        /// </value>
        public Double StrikeThrough { get; set; }

        /// <summary>
        ///     Gets or sets the Distribution value
        /// </summary>
        /// <value>
        ///     The Distribution value
        /// </value>
        public Double Distribution { get; set; }

        /// <summary>
        ///     Gets or sets the PenetrationTime1 value
        /// </summary>
        /// <value>
        ///     The PenetrationTime1 value
        /// </value>
        public Double PenetrationTime1 { get; set; }

        /// <summary>
        ///     Gets or sets the PenetrationTime2 value
        /// </summary>
        /// <value>
        ///     The PenetrationTime2 value
        /// </value>
        public Double PenetrationTime2 { get; set; }

        /// <summary>
        ///     Gets or sets the PenetrationTime3 value
        /// </summary>
        /// <value>
        ///     The PenetrationTime3 value
        /// </value>
        public Double PenetrationTime3 { get; set; }

        /// <summary>
        ///     Gets or sets the PenetrationTime4 value
        /// </summary>
        /// <value>
        ///     The PenetrationTime4 value
        /// </value>
        public Double PenetrationTime4 { get; set; }

        /// <summary>
        ///     Gets or sets the TestType for the babydiaper
        /// </summary>
        /// <value>
        ///     The TestType value
        /// </value>
        public TestTypeBabyDiaper TestType { get; set; }

        /// <summary>
        ///     Gets or sets the Collection of Notes
        /// </summary>
        /// <value>
        ///     The Collection of Notes
        /// </value>
        public List<TestNote> Notes { get; set; }
        /// <summary>
        ///     Gets or sets the Collection of NoteCodes
        /// </summary>
        /// <value>
        ///     The Collection of NoteCodes
        /// </value>
        public List<ErrorCode> NoteCodes { get; set; }

        #endregion
    }
}
