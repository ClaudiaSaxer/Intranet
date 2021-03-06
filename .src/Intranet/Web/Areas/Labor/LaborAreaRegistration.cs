﻿#region Usings

using System;
using System.Web.Mvc;

#endregion

namespace Intranet.Web.Areas.Labor
{
    /// <summary>
    ///     Registration for the Labor Area
    /// </summary>
    public class LaborAreaRegistration : AreaRegistration
    {
        #region Properties

        /// <inheritdoc />
        public override String AreaName { get; } = nameof( Labor );

        #endregion

        /// <inheritdoc />
        public override void RegisterArea( AreaRegistrationContext context )
            => context.MapRoute(
                "Labor_default",
                "Labor/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
    }
}