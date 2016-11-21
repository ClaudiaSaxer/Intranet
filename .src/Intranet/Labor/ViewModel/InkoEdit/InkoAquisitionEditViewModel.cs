#region Usings

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

#endregion

namespace Intranet.Labor.ViewModel
{
    /// <summary>
    ///     Class representing the ViewModel for the InkoAquisitionController
    /// </summary>
    public class InkoAquisitionEditViewModel
    {
        #region Properties

        /// <summary>
        ///     Gets or sets the ID of the Inko test
        /// </summary>
        /// <value>
        ///     The ID of the Inko test
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
        [DisplayName( "Tag im Jahr" )]
        [Required( ErrorMessage = "Der Tag muss angegeben werden" )]
        [Range( 0, 366, ErrorMessage = "Die Anzahl Tage dürfen ein Jahr nicht überschreiten" )]
        public Int32 ProductionCodeDay { get; set; }

        /// <summary>
        ///     Gets or sets the ProductionCodeTime
        /// </summary>
        /// <value>
        ///     The ProductionCodeTime
        /// </value>
        public TimeSpan ProductionCodeTime { get; set; }

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

        /// <summary>
        ///     Gets or sets the Collection of Notes
        /// </summary>
        /// <value>
        ///     The Collection of Notes
        /// </value>
        public IList<TestNote> Notes { get; set; }

        /// <summary>
        ///     Gets or sets the Collection of NoteCodes
        /// </summary>
        /// <value>
        ///     The Collection of NoteCodes
        /// </value>
        public IList<ErrorCode> NoteCodes { get; set; }

        #endregion
    }
}