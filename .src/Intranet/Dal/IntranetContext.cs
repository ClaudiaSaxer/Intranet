#region Usings

using System.Data.Entity;
using Intranet.Model;

#endregion

namespace Intranet.Dal
{
    /// <summary>
    ///     The DB Context for the IntranetDB
    /// </summary>
    public class IntranetContext : DbContext
    {
        #region Properties

        /// <summary>
        ///     Gets or sets the dbset of the model role
        /// </summary>
        public DbSet<Role> Roles { get; set; }

        /// <summary>
        ///     Gets or sets the dbset of the model module
        /// </summary>
        public DbSet<Module> Modules { get; set; }

        #endregion

        #region Overrides of DbContext

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
            //TPC
            modelBuilder.Entity<SubModule>()
                        .Map( m =>
                              {
                                  m.MapInheritedProperties();
                                  m.ToTable( "SubModule" );
                              } );

            modelBuilder.Entity<MainModule>()
                        .Map( m =>
                              {
                                  m.MapInheritedProperties();
                                  m.ToTable( "MainModule" );
                              } );
            //MainModule Role
            modelBuilder.Entity<MainModule>()
                        .HasMany( r => r.Roles )
                        .WithMany( s => s.MainModules )
                        .Map( mr =>
                              {
                                  mr.MapLeftKey( "MainModulRefId" );
                                  mr.MapRightKey( "RoleRefId" );
                                  mr.ToTable( "MainModuleRole" );
                              } );

            //SubModule Role
            modelBuilder.Entity<SubModule>()
                        .HasMany( r => r.Roles )
                        .WithMany( s => s.SubModules )
                        .Map( sr =>
                              {
                                  sr.MapLeftKey( "SubModulRefId" );
                                  sr.MapRightKey( "RoleRefId" );
                                  sr.ToTable( "SubModuleRole" );
                              } );

            //SubModule Submodule
            modelBuilder.Entity<SubModule>()
                        .HasMany( s => s.SubModules )
                        .WithOptional()
                        .Map( ss => ss.MapKey( "SubModulRefId" ) );

            //SubModule MainModul
            modelBuilder.Entity<SubModule>()
                        .HasOptional<MainModule>( s => s.MainModule )
                        .WithMany( s => s.SubModules )
                        .Map( s => s.MapKey( "MainModulRefId" ) );
        }

        #endregion
    }
}