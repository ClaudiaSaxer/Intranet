using System.Linq;
using Intranet.Definition;
using Intranet.Model;
using Intranet.ViewModel;

namespace Intranet.Bll
{
    public class VMHelper : IVMHelper
    {
        #region Fields

        private readonly TestBll _testBll = new TestBll();

        #endregion

        public TestViewModel getTestVM()
        {
            var model = new Test();
            var tests = _testBll.Tests;

            var vm = new TestViewModel
            {
                Name = tests.First()
                            .TestString
            };
            return vm;
        }
    }
}