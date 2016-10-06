using System.Collections.Generic;
using Intranet.Model;

namespace Intranet.Definition

{
    /// <summary>
    /// </summary>
    public abstract class ITestBll
    {
        #region Fields

        /// <summary>
        /// </summary>
        public IEnumerable<Test> Tests;

        #endregion

        /// <summary>
        /// </summary>
        /// <param name="test"></param>
        public abstract void AddTest( Test test );

        /// <summary>
        /// </summary>
        /// <param name="test"></param>
        public abstract void RemoveTest( Test test );

        /// <summary>
        /// </summary>
        /// <param name="test"></param>
        /// <param name="original"></param>
        public abstract void UpdateTest( Test test, Test original );
    }
}