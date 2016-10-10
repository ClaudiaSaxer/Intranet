﻿using System;
using System.Diagnostics;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.SessionState;
using Intranet.Web.IoC;

namespace Intranet.Web.ControllerFactory
{
    /// <summary>
    ///     This Class handles the incoming Request and maps it to the specific controller
    /// </summary>
    public class CustomControllerFactory : IControllerFactory
    {
        #region Fields

        private readonly DefaultControllerFactory _defaultControllerFactory;

        #endregion

        #region Ctor

        /// <summary>
        ///     Inizialize the CustomController
        /// </summary>
        public CustomControllerFactory()
        {
            _defaultControllerFactory = new DefaultControllerFactory();
        }

        #endregion

        #region Public Methods

        /// <summary>Creates the specified controller by using the specified request context.</summary>
        /// <returns>The controller.</returns>
        /// <param name="requestContext">The request context.</param>
        /// <param name="controllerName">The name of the controller.</param>
        public IController CreateController(RequestContext requestContext, string controllerName)
        {
            controllerName = GetModuleName(requestContext) + controllerName;
            var controller = Bootstrapper.GetInstance<IController>(controllerName);

            return controller ?? _defaultControllerFactory.CreateController(requestContext, controllerName);
        }

        /// <summary>Gets the controller's session behavior.</summary>
        /// <returns>The controller's session behavior.</returns>
        /// <param name="requestContext">The request context.</param>
        /// <param name="controllerName">The name of the controller whose session behavior you want to get.</param>
        public SessionStateBehavior GetControllerSessionBehavior(RequestContext requestContext, string controllerName) => SessionStateBehavior.Default;

        /// <summary>Releases the specified controller.</summary>
        /// <param name="controller">The controller.</param>
        public void ReleaseController(IController controller)
        {
            var disposableController = controller as IDisposable;

            disposableController?.Dispose();
        }

        #endregion

        #region Private Methods

        private String GetModuleName(RequestContext requestContext)
        {
            if (requestContext.RouteData.DataTokens.Count == 0)
                return "";
            var moduleName = requestContext.HttpContext.Request.Url?.Segments[1].Replace("/", ".");
            return moduleName;
        }

        #endregion

    }
}