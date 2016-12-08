using System;

namespace Intranet.ViewModel
{
    /// <summary>
    ///     Class representing ModuleSettings
    /// </summary>
    public class ModuleSetting
    {
        #region Properties

        /// <summary>
        ///     Gets or sets the id of the module
        /// </summary>
        /// <value>The name of the module</value>
        public Int32 Id { get; set; }

        /// <summary>
        ///     Gets or sets the name of the module
        /// </summary>
        /// <value>The name of the module</value>
        public String Name { get; set; }

        /// <summary>
        ///     Gets or sets the visibility of the module
        /// </summary>
        /// <value>The visibility status of the module</value>
        public Boolean Visible { get; set; }

        #endregion
    }
}