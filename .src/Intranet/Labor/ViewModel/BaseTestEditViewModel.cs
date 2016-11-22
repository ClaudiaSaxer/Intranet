#region Usings

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

#endregion

namespace Intranet.Labor.ViewModel
{
    /// <summary>
    ///     Class representing the Base ViewModel for all TestEdit ViewModels
    /// </summary>
    public class BaseTestEditViewModel
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
        [DisplayName("Test Person")]
        [Required(ErrorMessage = "Der Name darf nicht leer sein")]
        [DataType(DataType.Text, ErrorMessage = "Name muss ein Text sein")]
        [StringLength(1024, ErrorMessage = "Der Name darf nicht länger als 1024 Zeichen sein")]
        [MinLength(2, ErrorMessage = "Der Name muss mindestens 2 Zeichen lang sein")]
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