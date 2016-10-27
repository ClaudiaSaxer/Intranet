using System.Data.Entity;
using Intranet.Labor.Model.fa;

namespace Intranet.Labor.Dal
{
    /// <summary>
    ///     The Context of the Module Labor
    /// </summary>
    public class LaborContext : DbContext
    {
        #region Properties

        /// <summary>
        ///     Gets or sets the articles for the labor
        /// </summary>
        /// <value>the articles</value>
        public DbSet<Article> Articles { get; set; }

        /// <summary>
        ///     Gets or sets the components for the labor
        /// </summary>
        /// <value>the components</value>
        public DbSet<Component> Components { get; set; }

        /// <summary>
        ///     Gets or sets the errors for the labor
        /// </summary>
        /// <value>the errors</value>
        public DbSet<Error> Errors { get; set; }

        /// <summary>
        ///     Gets or sets the production orders for the labor
        /// </summary>
        /// <value>the production orders</value>
        public DbSet<ProductionOrder> ProductionOrders { get; set; }

        #endregion

        /// <summary>
        ///     This method is called when the model for a derived context has been initialized, but
        ///     before the model has been locked down and used to initialize the context.  The default
        ///     implementation of this method does nothing, but it can be overridden in a derived class
        ///     such that the model can be further configured before it is locked down.
        /// </summary>
        /// <remarks>
        ///     Typically, this method is called only once when the first instance of a derived context
        ///     is created.  The model for that context is then cached and is for all further instances of
        ///     the context in the app domain.  This caching can be disabled by setting the ModelCaching
        ///     property on the given ModelBuidler, but note that this can seriously degrade performance.
        ///     More control over caching is provided through use of the DbModelBuilder and DbContextFactory
        ///     classes directly.
        /// </remarks>
        /// <param name="modelBuilder"> The builder that defines the model for the context being created. </param>
        protected override void OnModelCreating( DbModelBuilder modelBuilder )
        {
            modelBuilder.Entity<ProductionOrder>()
                        .HasOptional( x => x.Component );
        }
    }
}