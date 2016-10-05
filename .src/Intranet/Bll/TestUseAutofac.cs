using System;
using Intranet.Definition;

namespace Intranet.Bll
{
    public class TestUseAutofac

    {
        #region Properties

        public ITestAutofac TestAutofac { get; set; }

        #endregion

        public String GetHelloWorldAndNumer( String world, Int32 number ) 
            => TestAutofac.GetHelloWorld( world ) + " " + number;
    }
}