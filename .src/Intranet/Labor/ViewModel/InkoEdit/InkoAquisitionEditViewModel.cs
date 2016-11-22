#region Usings

using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

#endregion

namespace Intranet.Labor.ViewModel
{
    /// <summary>
    ///     Class representing the ViewModel for the InkoAquisitionController
    /// </summary>
    public class InkoAquisitionEditViewModel : BaseTestEditViewModel
    {
        #region Properties

        /// <summary>
        ///     Gets or sets the AquisitionAddition1
        /// </summary>
        /// <value>
        ///     The AquisitionAddition1
        /// </value>
        [DisplayName( "1. Zugabe" )]
        [Range( 0, Double.MaxValue, ErrorMessage = "Die Zahl muss eine Positive Kommazahl sein" )]
        public Double AquisitionAddition1 { get; set; }

        /// <summary>
        ///     Gets or sets the AquisitionAddition2
        /// </summary>
        /// <value>
        ///     The AquisitionAddition2
        /// </value>
        [DisplayName( "2. Zugabe" )]
        [Range( 0, Double.MaxValue, ErrorMessage = "Die Zahl muss eine Positive Kommazahl sein" )]
        public Double AquisitionAddition2 { get; set; }

        /// <summary>
        ///     Gets or sets the AquisitionAddition3
        /// </summary>
        /// <value>
        ///     The AquisitionAddition3
        /// </value>
        [DisplayName( "3. Zugabe" )]
        [Range( 0, Double.MaxValue, ErrorMessage = "Die Zahl muss eine Positive Kommazahl sein" )]
        public Double AquisitionAddition3 { get; set; }

        /// <summary>
        ///     Gets or sets the InkoWeight
        /// </summary>
        /// <value>
        ///     The InkoWeight
        /// </value>
        [DisplayName( "Prüflingsgewicht" )]
        [Range( 0, Double.MaxValue, ErrorMessage = "Die Zahl muss eine Positive Kommazahl sein" )]
        public Double InkoWeight { get; set; }

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