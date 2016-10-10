using System.Linq;
using Intranet.Model;

namespace Intranet.Definition

{
    /// <summary>
    /// </summary>
    public interface ITestBll
    {
        /// <summary>
        /// </summary>
        /// <param name="test"></param>
        void AddTest( Test test );

        /// <summary>
        /// </summary>
        IQueryable<Test> AllTests();

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