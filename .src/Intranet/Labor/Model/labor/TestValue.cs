#region Usings

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#endregion

namespace Intranet.Labor.Model
{
    /// <summary>
    ///     Class representing the test value
    /// </summary>
    public class TestValue
    {
        #region Properties

        /// <summary>
        ///     Gets or sets the id of the test value
        /// </summary>
        /// <value>the test value id </value>
        [Key]
        [DatabaseGenerated( DatabaseGeneratedOption.Identity )]
        public Int32 TestValueId { get; set; }

        /// <summary>
        ///     Gets or sets the date and time of the creation
        /// </summary>
        /// <value>the created date and time</value>
        [Column(TypeName = "datetime2")]
        public DateTime CreatedDateTime { get; set; }


        /// <summary>
        ///     Gets or sets the day in the year of the article creation
        /// </summary>
        /// <value>the day in the year</value>
        public Int32 DayInYearOfArticleCreation { get; set; }

        /// <summary>
        ///     Gets or sets the person who created the test value
        /// </summary>
        [Column(TypeName = "varchar")]
        [StringLength(255)]
        public String CreatedPerson { get; set; }

        /// <summary>
        ///     Gets or sets the date and time of the last edit
        /// </summary>
        /// <value>the date and time of the last edit</value>
        [Column(TypeName = "datetime2")]
        public DateTime LastEditedDateTime { get; set; }

        /// <summary>
        ///     Gets or sets the person who last edited the test value
        /// </summary>
        /// <value>the person who last edited the test value</value>
        [Column(TypeName = "varchar")]
        [StringLength(255)]
        public String LastEditedPerson { get; set; }

        /// <summary>
        ///     The article type of the test for the testvalue
        /// </summary>
        /// <value>the article type of the test</value>
        public ArticleType ArticleTestType { get; set; }

        /// <summary>
        ///     Gets or sets the notes to the testvalues
        /// </summary>
        /// <value>notes for the testvalue</value>
        public virtual ICollection<TestValueNote> TestValueNote { get; set; }

        /// <summary>
        ///     Gets or sets the baby diaper test value
        /// </summary>
        /// <value>the baby diaper test value</value>
        public virtual BabyDiaperTestValue BabyDiaperTestValue { get; set; }

        /// <summary>
        ///     Gets or sets the incontinence pad test value
        /// </summary>
        /// <value>the incontinence pad test value</value>
        public virtual IncontinencePadTestValue IncontinencePadTestValue { get; set; }

        /// <summary>
        ///     Gets or sets the type of the test value
        /// </summary>
        /// <value>the type of the test value</value>
        public virtual TestValueType TestValueType { get; set; }

        /// <summary>
        ///     Gets or sets the test sheet
        /// </summary>
        /// <value>the test sheet</value>
        
        public virtual TestSheet TestSheet { get; set; }

        /// <summary>
        ///     Gets or sets the ref id of the test sheet
        /// </summary>
        /// <value>the ref id of the test sheet</value>
        [ForeignKey("TestSheet")]
        [Index("IX_TestValue_TestSheet")]
        public Int32 TestSheetId { get; set; }

        #endregion
    }
}