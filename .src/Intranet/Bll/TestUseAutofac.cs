using System;
using Intranet.Definition;

namespace Intranet.Bll
{
    /// <summary>
    ///     Test the use of autofac
    ///     to be removed
    /// </summary>
    public class TestUseAutofac

    {
        #region Properties

        /// <summary>
        ///     Gets or sets TestAutofac
        /// </summary>
        public ITestAutofac TestAutofac { get; set; }

        #endregion

        /// <summary>
        ///     Return Hello world and a number
        /// </summary>
        /// <param name="world">the world name</param>
        /// <param name="number">the number</param>
        /// <returns></returns>
        public String GetHelloWorldAndNumer( String world, Int32 number )
            => TestAutofac.GetHelloWorld( world ) + " " + number;
    }
}