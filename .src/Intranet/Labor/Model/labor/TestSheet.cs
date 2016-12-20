#region Usings

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#endregion

namespace Intranet.Labor.Model
{
    /// <summary>
    ///     Class representing the sheet for a test
    /// </summary>
    public class TestSheet
    {
        #region Properties

        /// <summary>
        ///     Gets or sets the id of the test sheet
        /// </summary>
        /// <value>the test sheet id </value>
        [Key]
        [DatabaseGenerated( DatabaseGeneratedOption.Identity )]
        public Int32 TestSheetId { get; set; }

        /// <summary>
        ///     Gets or sets the production order number
        /// </summary>
        /// <value>the production order number</value>
        [Column(TypeName = "varchar")]
        [StringLength(255)]
        public String FaNr { get; set; }

        /// <summary>
        ///     Gets or sets the Shift
        /// </summary>
        /// <value>the shift of the test sheet</value>
        public ShiftType ShiftType { get; set; }

        /// <summary>
        ///     Gets or sets the timestamp of the creation of the test sheet
        /// </summary>
        /// <value>the timestamp of the creation</value>
        [Column(TypeName = "datetime2")]
        public DateTime CreatedDateTime { get; set; }

        /// <summary>
        ///     Gets or sets the number of the machine
        /// </summary>
        /// <value>the number of the machine</value>
        [Column(TypeName = "varchar")]
        [StringLength(255)]
        public String MachineNr { get; set; }

        /// <summary>
        ///     Gets or sets the type of the SAP
        /// </summary>
        /// <value>the type of the SAP</value>
        [Column(TypeName = "varchar")]
        [StringLength(255)]
        public String SAPType { get; set; }

        /// <summary>
        ///     Gets or sets the number of the SAP
        /// </summary>
        /// <value>the number of the SAP</value>
        [Column(TypeName = "varchar")]
        [StringLength(255)]
        public String SAPNr { get; set; }

        /// <summary>
        ///     Gets or sets the name of the product
        /// </summary>
        /// <value>the name of the product</value>
        [Column(TypeName = "varchar")]
        [StringLength(255)]
        public String ProductName { get; set; }

        /// <summary>
        ///     Gets or sets the name of the size
        /// </summary>
        /// <value>the name of the size</value>
        [Column(TypeName = "varchar")]
        [StringLength(255)]
        public String SizeName { get; set; }

        /// <summary>
        ///     Gets or sets the type of the article
        /// </summary>
        /// <value>the type of the article</value>
        public ArticleType ArticleType { get; set; }

        /// <summary>
        ///     Gets or sets the values of the test sheet
        /// </summary>
        /// <value>the values of the test sheet</value>
        public virtual ICollection<TestValue> TestValues { get; set; }

        /// <summary>
        ///     Gets or sets the day in year of the test sheet
        /// </summary>
        /// <value>the day in the year</value>
        public Int32 DayInYear { get; set; }

        #endregion
    }
}