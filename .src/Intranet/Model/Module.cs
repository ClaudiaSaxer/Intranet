#region Usings

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#endregion

namespace Intranet.Model
{
    /// <summary>
    ///     Class representing a Module
    /// </summary>
    public abstract class Module
    {
        #region Properties

        /// <summary>
        ///     Gets or sets the name of the module
        /// </summary>
        /// <example>
        ///     name example: Labor
        /// </example>
        /// <value>The name of the module.</value>
        public String Name { get; set; }

        /// <summary>
        ///     Gets or sets a description of the module
        /// </summary>
        /// <value>The description of the module.</value>
        public String Description { get; set; }

        /// <summary>
        ///     Gets or sets the path to the module start page
        /// </summary>
        /// <value>The path of the module.</value>
        public String ActionName { get; set; }


        /// <summary>
        ///     Gets or sets the path to the module start page
        /// </summary>
        /// <value>The path of the module.</value>
        public String ControllerName { get; set; }

        #endregion
    }
}