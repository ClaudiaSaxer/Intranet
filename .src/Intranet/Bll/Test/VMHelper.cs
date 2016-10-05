using System.Linq;
using Extend;
using Intranet.Bll.Test;
using Intranet.Definition;
using Intranet.Model;
using Intranet.ViewModel;

namespace Intranet.Bll
{
    public class VMHelper : IVMHelper
    {
        private readonly TestBll _testBll = new TestBll();
        public TestViewModel getTestVM()
        {

            var model = new Model.Test();
            var tests = _testBll.Tests;

            var vm = new TestViewModel
            {
                Name = tests.First().TestString
            };
            return vm;
        }
    }
}