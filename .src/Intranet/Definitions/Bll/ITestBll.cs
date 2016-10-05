using System.Collections.Generic;
using Intranet.Model;

namespace Intranet.Definition

{
    public abstract class ITestBll
    {
        #region Fields

        public IEnumerable<Test> Tests;

        #endregion

        public abstract void AddTest( Test test );
        public abstract void RemoveTest( Test test );
        public abstract void UpdateTest( Test test, Test original );
    }
}