﻿#region Usings

using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

#endregion

namespace Intranet.Labor.ViewModel
{
    /// <summary>
    ///     Class representing the ViewModel for the InkoRetentionController
    /// </summary>
    public class InkoRetentionEditViewModel : BaseTestEditViewModel
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
        ///     Gets or sets the InkoWeight
        /// </summary>
        /// <value>
        ///     The InkoWeight
        /// </value>
        [DisplayName( "Prüflingsgewicht" )]
        [Required( ErrorMessage = "Das Prüflingsgewicht muss angegeben werden" )]
        [Range( 0, Double.MaxValue, ErrorMessage = "Die Zahl muss eine Positive Kommazahl sein" )]
        public Double InkoWeight { get; set; }

        /// <summary>
        ///     Gets or sets the InkoWeightWet
        /// </summary>
        /// <value>
        ///     The InkoWeightWet
        /// </value>
        [DisplayName( "Prüfling naß" )]
        [Required( ErrorMessage = "Das Prüflingsgewicht naß muss angegeben werden" )]
        [Range( 0, Double.MaxValue, ErrorMessage = "Die Zahl muss eine Positive Kommazahl sein" )]
        public Double InkoWeightWet { get; set; }

        /// <summary>
        ///     Gets or sets the InkoWeightAfterZentrifuge
        /// </summary>
        /// <value>
        ///     The InkoWeightAfterZentrifuge
        /// </value>
        [DisplayName( "Prüfling nach Zentrifuge" )]
        [Required( ErrorMessage = "Das Prüflingsgewicht nach Zentrifuge muss angegeben werden" )]
        [Range( 0, Double.MaxValue, ErrorMessage = "Die Zahl muss eine Positive Kommazahl sein" )]
        public Double InkoWeightAfterZentrifuge { get; set; }

        #endregion
    }
}