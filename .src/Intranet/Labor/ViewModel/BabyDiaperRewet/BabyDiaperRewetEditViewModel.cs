#region Usings

using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Intranet.Labor.Model.labor;

#endregion

namespace Intranet.Labor.ViewModel
{
    /// <summary>
    ///     Class representing the ViewModel for the BabyDiaperRewetController
    /// </summary>
    public class BabyDiaperRewetEditViewModel : BaseTestEditViewModel
    {
        #region Properties

        /// <summary>
        ///     Gets or sets the DiaperWeight value
        /// </summary>
        /// <value>
        ///     The DiaperWeight value
        /// </value>
        [DisplayName("Windeln Gewicht")]
        [Required(ErrorMessage = "Das Windeln Gewicht muss angegeben werden")]
        [Range(0, Double.MaxValue, ErrorMessage = "Die Zahl muss eine Positive Kommazahl sein")]
        public Double DiaperWeight { get; set; }

        /// <summary>
        ///     Gets or sets the RewetAfter140 value
        /// </summary>
        /// <value>
        ///     The RewetAfter140 value
        /// </value>
        [DisplayName("Rewet nach 140ml")]
        [Required(ErrorMessage = "Der Rewet-Wert nach 140ml muss angegeben werden")]
        [Range(0, Double.MaxValue, ErrorMessage = "Die Zahl muss eine Positive Kommazahl sein")]
        public Double RewetAfter140 { get; set; }

        /// <summary>
        ///     Gets or sets the RewetAfter210 value
        /// </summary>
        /// <value>
        ///     The RewetAfter210 value
        /// </value>
        [DisplayName("Rewet nach 210ml")]
        [Required(ErrorMessage = "Der Rewet-Wert nach 210ml muss angegeben werden")]
        [Range(0, Double.MaxValue, ErrorMessage = "Die Zahl muss eine Positive Kommazahl sein")]
        public Double RewetAfter210 { get; set; }

        /// <summary>
        ///     Gets or sets the StrikeThrough value
        /// </summary>
        /// <value>
        ///     The StrikeThrough value
        /// </value>
        [DisplayName("Strike Through (g)")]
        [Required(ErrorMessage = "Der Strike Through muss angegeben werden")]
        [Range(0, Double.MaxValue, ErrorMessage = "Die Zahl muss eine Positive Kommazahl sein")]
        public Double StrikeThrough { get; set; }

        /// <summary>
        ///     Gets or sets the Distribution value
        /// </summary>
        /// <value>
        ///     The Distribution value
        /// </value>
        [DisplayName("Verteilung (mm)")]
        [Required(ErrorMessage = "Die Verteilung muss angegeben werden")]
        [Range(0, Double.MaxValue, ErrorMessage = "Die Zahl muss eine Positive Zahl sein")]
        public Double Distribution { get; set; }

        /// <summary>
        ///     Gets or sets the PenetrationTime1 value
        /// </summary>
        /// <value>
        ///     The PenetrationTime1 value
        /// </value>
        [DisplayName("Penetrationszeit nach 1. Zugabe")]
        [Required(ErrorMessage = "Die Penetrationszeit nach der 1. Zugabe muss angegeben werden")]
        [Range(0, Double.MaxValue, ErrorMessage = "Die Zahl muss eine Positive Kommazahl sein")]
        public Double PenetrationTime1 { get; set; }

        /// <summary>
        ///     Gets or sets the PenetrationTime2 value
        /// </summary>
        /// <value>
        ///     The PenetrationTime2 value
        /// </value>
        [DisplayName("Penetrationszeit nach 2. Zugabe")]
        [Required(ErrorMessage = "Die Penetrationszeit nach der 2. Zugabe muss angegeben werden")]
        [Range(0, Double.MaxValue, ErrorMessage = "Die Zahl muss eine Positive Kommazahl sein")]
        public Double PenetrationTime2 { get; set; }

        /// <summary>
        ///     Gets or sets the PenetrationTime3 value
        /// </summary>
        /// <value>
        ///     The PenetrationTime3 value
        /// </value>
        [DisplayName("Penetrationszeit nach 3. Zugabe")]
        [Required(ErrorMessage = "Die Penetrationszeit nach der 3. Zugabe muss angegeben werden")]
        [Range(0, Double.MaxValue, ErrorMessage = "Die Zahl muss eine Positive Kommazahl sein")]
        public Double PenetrationTime3 { get; set; }

        /// <summary>
        ///     Gets or sets the PenetrationTime4 value
        /// </summary>
        /// <value>
        ///     The PenetrationTime4 value
        /// </value>
        [DisplayName("Penetrationszeit nach 4. Zugabe")]
        [Required(ErrorMessage = "Die Penetrationszeit nach der 4. Zugabe muss angegeben werden")]
        [Range(0, Double.MaxValue, ErrorMessage = "Die Zahl muss eine Positive Kommazahl sein")]
        public Double PenetrationTime4 { get; set; }

        /// <summary>
        ///     Gets or sets the TestType for the babydiaper
        /// </summary>
        /// <value>
        ///     The TestType value
        /// </value>
        public TestTypeBabyDiaper TestType { get; set; }

        #endregion
    }
}