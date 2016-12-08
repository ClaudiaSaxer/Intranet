using System.Collections.ObjectModel;
using System.Data.Entity.Migrations;
using Intranet.Model;

namespace Intranet.Dal
{
    internal sealed class Configuration : DbMigrationsConfiguration<IntranetContext>
    {
        #region Ctor

        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        #endregion

        protected override void Seed( IntranetContext context )
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            //  var admin = new Role { Name = "Administrator" };
            var admin = new Role { Name = "Everyone" };
            var laboruser = new Role { Name = "LaborUser" };
            var laborviewer = new Role { Name = "LaborViewer" };

            var roles = new Collection<Role> { admin, laboruser, laborviewer };
            var rolesadmin = new Collection<Role> { admin };

            context.Roles.AddOrUpdate( m => m.Name, admin, laboruser, laborviewer );

            var creator = new Module
            {
                Description = "Zum erfassen neuer Tests",
                Name = "Testerfassung",
                ControllerName = "LaborCreator",
                ActionName = "Index",
                AreaName = "Labor",
                Type = ModuleType.Sub,
                Visible = null,
                Roles = roles
            };

            var dashboard = new Module
            {
                Description = "Übersicht über die letzten 3 Schichten",
                Name = "Dashboard",
                ControllerName = "LaborDashboard",
                ActionName = "Index",
                AreaName = "Labor",
                Type = ModuleType.Sub,
                Visible = null,
                Roles = roles
            };

            var history = new Module
            {
                Description = "Zum finden alter Testwerte",
                Name = "History",
                ControllerName = "History",
                ActionName = "Index",
                AreaName = "Labor",
                Type = ModuleType.Sub,
                Visible = null,
                Roles = roles
            };

            var submodules = new Collection<Module> { creator, dashboard, history };

            var labor = new Module
            {
                Name = "Labor",
                Description = "Labor QS",
                ActionName = "Index",
                ControllerName = "LaborHome",
                Visible = true,
                AreaName = "Labor",
                Type = ModuleType.Main,
                Roles = roles,
                Submodules = submodules
            };
            var adminshell = new Module
            {
                Name = "Einstellungen",
                Description = "Einstellungen für die Shell",
                ActionName = "Index",
                ControllerName = "Settings",
                Visible = true,
                AreaName = "",
                Type = ModuleType.Setting,
                Roles = rolesadmin
            };

            context.Modules.AddOrUpdate( m => m.Name, labor, adminshell, creator, dashboard, history );
        }
    }
}