using System.Data.Entity;

namespace Intranet.Labor.Dal
{
    /// <summary>
    ///     The Context of the Module Labor
    /// </summary>
    public class LaborContext : DbContext
    {
        #region Properties

        /// <summary>
        ///     Gets or sets the Tests
        /// </summary>
        public DbSet<Model.Labor> Tests { get; set; }

        #endregion
    }
}