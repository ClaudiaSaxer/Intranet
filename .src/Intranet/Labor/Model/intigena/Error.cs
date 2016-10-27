using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intranet.Labor.Model
{
    /// <summary>
    /// Class representing Errors
    /// </summary>
    public class Error
    {
        /// <summary>
        /// Gets or sets the id of the error
        /// </summary>
        /// <value>the id of the error</value>
        [Key]
        public Int32 ErrorId { get; set; }

        /// <summary>
        /// Gets or sets the value of the error
        /// </summary>
        /// <value>the value or a description of the error</value>
        public String Value { get; set; }



    }
}
