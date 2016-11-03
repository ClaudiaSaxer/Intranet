#region Usings

using System;
using System.Collections.Generic;

#endregion

namespace Intranet.Labor.ViewModel
{
    /// <summary>
    ///     Class representing the ViewModel for the BabyDiapersRetentionController
    /// </summary>
    public class BabyDiapersRetentionEditViewModel
    {
        #region Properties

        /// <summary>
        ///     Gets or sets the ID of the Babydapers test
        /// </summary>
        /// <value>
        ///     The ID of the Babydapers test
        /// </value>
        public Int32 Id { get; set; }

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
        ///     Gets or sets the ProductionCodeTime
        /// </summary>
        /// <value>
        ///     The ProductionCodeTime
        /// </value>
        public DateTime ProductionCodeTime { get; set; }
        /// <summary>
        ///     Gets or sets the DiaperWeight value
        /// </summary>
        /// <value>
        ///     The DiaperWeight value
        /// </value>
        public Double DiaperWeight { get; set; }
        /// <summary>
        ///     Gets or sets the WeightRetentionWet value
        /// </summary>
        /// <value>
        ///     The WeightRetentionWet value
        /// </value>
        public Double WeightRetentionWet { get; set; }
        /// <summary>
        ///     Gets or sets the Collection of Notes
        /// </summary>
        /// <value>
        ///     The Collection of Notes
        /// </value>
        public ICollection<TestNote> Notes { get; set; }
        /// <summary>
        ///     Gets or sets the Collection of NoteCodes
        /// </summary>
        /// <value>
        ///     The Collection of NoteCodes
        /// </value>
        public ICollection<String> NoteCodes { get; set; }


        #endregion
    }
}