using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Intranet.Model;

namespace Intranet.Dal
{
    /// <summary>
    /// The DB Context for the IntranetDB
    /// </summary>
    public class IntranetContext : DbContext
    {
        /// <summary>
        /// Test set
        /// to be removed
        /// </summary>
        public DbSet<Test> Tests { get; set; }
    }

}
