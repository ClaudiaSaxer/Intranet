using System.Web.Mvc;
using Intranet.Common;
using Intranet.Definition;
using Intranet.ViewModel;

namespace Intranet.Web.Controllers
{
    /// <summary>
    ///     Class representing the HomeController
    /// </summary>
    [Authorize]
    public class HomeController : BaseController
    {
        #region Properties

        /// <summary>
        ///     Gets or sets a <see cref="IHomeService" />
        /// </summary>
        /// <value>
        ///     <see cref="IHomeService" />
        /// </value>
        public IHomeService HomeService { get; set; }

        #endregion

        #region Ctor

        /// <summary>
        ///     Initialize a new instance of the <see cref="HomeController" /> class.
        /// </summary>
        /// <param name="loggerFactory">A <see cref="ILoggerFactory" />.</param>
        public HomeController( ILoggerFactory loggerFactory )
            : base( loggerFactory.CreateLogger( typeof(HomeController) ) )
        {
            Logger.Trace( "Enter Ctor - Exit." );
        }

        #endregion

        /// <summary>
        ///     Loads the index page of the HomeController
        /// </summary>
        /// <returns>The Index View filled with the viewModel</returns>
        public ActionResult Index()
        {
            var viewModel = HomeService.GetHomeViewModel();
            viewModel = new HomeViewModel();
            var moduleList = new System.Collections.Generic.List<Intranet.Model.Module>();
            moduleList.Add( new Intranet.Model.MainModule { Name = "Labor", Description = "Laborbeschreibung", ActionName = "Index", ControllerName = "LaborHome" } );
            moduleList.Add(new Intranet.Model.MainModule { Name = "Modul1", Description = "Modul1 beispiel", ActionName = "Index", ControllerName = "LaborHome" });
            moduleList.Add(new Intranet.Model.MainModule { Name = "Modul2", Description = "Modul2 beispiel", ActionName = "Index", ControllerName = "LaborHome" });
            moduleList.Add(new Intranet.Model.MainModule { Name = "Einstellungen", Description = "Einstellungen beispiel", ActionName = "Index", ControllerName = "LaborHome" });
            viewModel.Modules = moduleList;
            return View( viewModel );
        }

        #region Overrides of BaseController

        #endregion
    }
}