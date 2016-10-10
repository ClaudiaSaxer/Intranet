using System.Data.Entity;
using Intranet.Model;

namespace Intranet.Dal
{
    /// <summary>
    ///     The DB Context for the IntranetDB
    /// </summary>
    public class IntranetContext : DbContext
    {
        #region Properties

        /// <summary>
        ///     Test set
        ///     to be removed
        /// </summary>
        public DbSet<Test> Tests { get; set; }

        #endregion
    }
}