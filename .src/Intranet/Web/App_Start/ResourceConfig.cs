
#region Usings

using System.Web.Mvc;

#endregion

namespace Intranet.Web
{
    /// <summary>
    ///     Class configure all resource classes and keys.
    /// </summary>
    public static class ResourceConfig
    {
        /// <summary>
        ///     Configures the resources key and class.
        /// </summary>
        /// <remarks>
        ///     For more details see:
        ///     http://stackoverflow.com/questions/4828297/how-to-change-data-val-number-message-validation-in-mvc-while-it-is-generated
        /// </remarks>
        public static void Configurate()
        {
            ClientDataTypeModelValidatorProvider.ResourceClassKey = "GlobalResources";
            DefaultModelBinder.ResourceClassKey = "GlobalResources";
        }
    }
}