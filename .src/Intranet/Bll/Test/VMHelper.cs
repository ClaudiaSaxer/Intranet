using Intranet.Definition;
using Intranet.Model;
using Intranet.ViewModel;

namespace Intranet.Bll
{
    public class VMHelper : IVMHelper
    {
        public TestViewModel getTestVM()
        {
            var model = new Model.Test();

            var vm = new TestViewModel
            {
                // Name = model.TestString
                Name = "to be replaced by db"
            };
            return vm;
        }
    }
}