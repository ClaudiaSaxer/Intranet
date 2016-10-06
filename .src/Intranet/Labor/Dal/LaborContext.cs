using System.Data.Entity;

namespace Intranet.Labor.Dal
{
    public class LaborContext : DbContext
    {
        #region Properties

        public DbSet<Model.Labor> Tests { get; set; }

        #endregion
    }
}