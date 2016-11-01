using System;
using System.Collections.Generic;
using Intranet.Labor.Model.labor;

namespace Intranet.Labor.ViewModel
{
    /// <summary>
    ///     Class representing the view model for the labor creator
    /// </summary>
    public class LaborCreatorViewModel
    {
        #region Properties

            /// <summary>
        /// Gets or sets the producer
        /// </summary>
        /// <value>the producer</value>
        public String Producer { get; set; }

        /// <summary>
        /// Gets or sets the shift
        /// </summary>
        /// <value>the shift</value>
        public String Shift { get; set; }

        /// <summary>
        ///     Gets or sets the production order number
        /// </summary>
        /// <value>the production order number</value>
        public String FaNr { get; set; }
    
        /// <summary>
        ///     Gets or sets the timestamp of the creation of the test sheet
        /// </summary>
        /// <value>the timestamp of the creation</value>
        public String CreatedDate { get; set; }
        
        /// <summary>
        ///     Gets or sets the name of the product
        /// </summary>
        /// <value>the name of the product</value>
        public String ProductName { get; set; }

        /// <summary>
        ///     Gets or sets the name of the size
        /// </summary>
        /// <value>the name of the size</value>
        public String SizeName { get; set; }

        /// <summary>
        /// Gets or sets the rewets
        /// </summary>
        /// <value>collection of rewet</value>
        public ICollection<RewetViewModel> Rewets { get; set; }
       /// <summary>
       /// Gets or sets the retentions
       /// </summary>
       /// <value>collection of retention</value>
        public ICollection<RetentionViewModel> Retentions { get; set; }
        /// <summary>
        /// Gets or sets the PenetrationTimes
        /// </summary>
        /// <value>collection of penetrationtime</value>
        public ICollection<PenetrationTimeViewModel> PenetrationTimes { get; set; }

        #endregion
    }
}