#region Usings

using System;

#endregion

namespace Intranet.Labor.ViewModel
{
    /// <summary>
    ///     Class representing the test info for the labor creator
    /// </summary>
    public class BabyDiaperTestInfo
    {
        #region Properties

        /// <summary>
        ///     Gets or sets the Person who did the test
        /// </summary>
        /// <value>the test person</value>
        public String TestPerson { get; set; }

        /// <summary>
        ///     Gets or sets the production code from the diaper
        /// </summary>
        /// <value>the production code from the diaper</value>
        public String ProductionCode { get; set; }

        /// <summary>
        ///     Gets or sets the weight the diaper has if it is dry
        /// </summary>
        /// <value>the weight of the dry diaper</value>
        public Double WeightyDiaperDry { get; set; }

        /// <summary>
        ///     Gets or setst the testvalue id
        /// </summary>
        /// <value>the testvalue id</value>
        public Int32 TestValueId { get; set; }

        #endregion
    }
}