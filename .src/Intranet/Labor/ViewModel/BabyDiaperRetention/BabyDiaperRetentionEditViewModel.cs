#region Usings

using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

#endregion

namespace Intranet.Labor.ViewModel
{
    /// <summary>
    ///     Class representing the ViewModel for the BabyDiapersRetentionController
    /// </summary>
    public class BabyDiaperRetentionEditViewModel : BaseTestEditViewModel
    {
        #region Properties

        /// <summary>
        ///     Gets or sets the DiaperWeight value
        /// </summary>
        /// <value>
        ///     The DiaperWeight value
        /// </value>
        [DisplayName( "Windeln Gewicht" )]
        [Required( ErrorMessage = "Das Windeln Gewicht muss angegeben werden" )]
        [Range( 0, Double.MaxValue, ErrorMessage = "Die Zahl muss eine Positive Kommazahl sein" )]
        public Double DiaperWeight { get; set; }

        /// <summary>
        ///     Gets or sets the WeightRetentionWet value
        /// </summary>
        /// <value>
        ///     The WeightRetentionWet value
        /// </value>
        [DisplayName( "Windeln Gewicht Nass" )]
        [Required( ErrorMessage = "Das nasse Windeln Gewicht muss angegeben werden" )]
        [Range( 0, Double.MaxValue, ErrorMessage = "Die Zahl muss eine Positive Kommazahl sein" )]
        public Double WeightRetentionWet { get; set; }

        #endregion
    }
}