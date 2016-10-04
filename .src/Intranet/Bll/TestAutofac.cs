using System;
using Intranet.Definition;

namespace Intranet.Bll
{
    public class TestAutofac : ITestAutofac

    {
        #region Implementation of ITestAutofac

        public String GetHelloWorld( String worldname )
            => "Hello " + worldname;

        #endregion
    }
}