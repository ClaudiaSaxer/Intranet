using Intranet.Definition;
using Intranet.ViewModel.Test;

namespace Intranet.Bll
{
    public class VMHelper : IVMHelper
    {
        public TestViewModel getTestVM()
        {
            var model = new Model.Test();
            
            var vm = new TestViewModel
            {
                Name = model.TestString
            };
            return vm;
        }
    }
}