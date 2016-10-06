using System;
using Intranet.Definition;

namespace Intranet.Bll
{
    /// <summary>
    ///     Test File to be removed
    /// </summary>
    public class TestAutofac : ITestAutofac

    {
        #region Implementation of ITestAutofac

        /// <inheritdoc />
        public String GetHelloWorld( String worldname )
            => "Hello " + worldname;

        #endregion
    }
}