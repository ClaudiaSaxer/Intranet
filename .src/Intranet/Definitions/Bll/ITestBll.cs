using System.Collections.Generic;
using Intranet.Model;

namespace Intranet.Definition

{
    /// <summary>
    /// </summary>
    public interface ITestBll
    {
        #region Fields

        /// <summary>
        /// </summary>
        IEnumerable<Test> AllTests();

        #endregion

        /// <summary>
        /// </summary>
        /// <param name="test"></param>
        void AddTest( Test test );

        /// <summary>
        /// </summary>
        /// <param name="test"></param>
        void RemoveTest( Test test );

        /// <summary>
        /// </summary>
        /// <param name="test"></param>
        /// <param name="original"></param>
        void UpdateTest( Test test, Test original );
    }
}