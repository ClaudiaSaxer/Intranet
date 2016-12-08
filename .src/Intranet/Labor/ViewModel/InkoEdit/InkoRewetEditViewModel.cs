#region Usings

using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

#endregion

namespace Intranet.Labor.ViewModel
{
    /// <summary>
    ///     Class representing the ViewModel for the InkoRewetController
    /// </summary>
    public class InkoRewetEditViewModel : BaseTestEditViewModel
    {
        #region Properties

        /// <summary>
        ///     Gets or sets the ExpireMonth
        /// </summary>
        /// <value>
        ///     The ExpireMonth
        /// </value>
        [DisplayName( "Ablauf-Monat" )]
        [Required( ErrorMessage = "Der Ablauf-Monat muss angegeben werden" )]
        [Range( 1, 12, ErrorMessage = "Die Zahl muss zwischen 1 und 12 liegen." )]
        public Int32 ExpireMonth { get; set; }

        /// <summary>
        ///     Gets or sets the ExpireYear
        /// </summary>
        /// <value>
        ///     The ExpireYear
        /// </value>
        [DisplayName( "Ablauf-Jahr" )]
        [Required( ErrorMessage = "Das Ablauf-Jahr muss angegeben werden" )]
        [Range( 0, 9999, ErrorMessage = "Die Zahl muss zwischen 0 und 9999 liegen." )]
        public Int32 ExpireYear { get; set; }

        /// <summary>
        ///     Gets or sets the FPDry
        /// </summary>
        /// <value>
        ///     The FPDry
        /// </value>
        [DisplayName( "FP trocken" )]
        [Range( 0, Double.MaxValue, ErrorMessage = "Die Zahl muss eine Positive Kommazahl sein" )]
        public Double FPDry { get; set; }

        /// <summary>
        ///     Gets or sets the FPWet
        /// </summary>
        /// <value>
        ///     The FPWet
        /// </value>
        [DisplayName( "FP feucht" )]
        [Range( 0, Double.MaxValue, ErrorMessage = "Die Zahl muss eine Positive Kommazahl sein" )]
        public Double FPWet { get; set; }

        #endregion
    }
}