using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Intranet.Common;
using Intranet.Labor.ViewModel;
using Intranet.Web.Areas.Labor.Controllers;
using Xunit;

namespace Intranet.Web.Test.Areas.Labor.Controllers
{
    /// <summary>
    ///     A test class for the babydiaper rewet controller
    /// </summary>
    public class BabyDiaperRewetControllerTest
    {
        /// <summary>
        ///     Tests if the AddNotes Action adds a note
        /// </summary>
        [Fact]
        public void AddNoteTest1()
        {
            var viewModel = new BabyDiaperRewetEditViewModel
            {
                Notes = new List<TestNote>()
            };
            var controller = new BabyDiaperRewetController(new NLogLoggerFactory());
            var result = controller.AddNote(viewModel) as ViewResult;
            var newViewModel = (BabyDiaperRewetEditViewModel)result?.ViewData.Model;
            Assert.NotNull(newViewModel);
            Assert.Equal(1, newViewModel.Notes.Count);
        }

        /// <summary>
        ///     Tests if the AddNotes Action adds a note if the old viewmodel Note list is NULL
        /// </summary>
        [Fact]
        public void AddNoteTest2()
        {
            var viewModel = new BabyDiaperRewetEditViewModel();
            var controller = new BabyDiaperRewetController(new NLogLoggerFactory());
            var result = controller.AddNote(viewModel) as ViewResult;
            var newViewModel = (BabyDiaperRewetEditViewModel)result?.ViewData.Model;
            Assert.NotNull(newViewModel);
            Assert.Equal(1, newViewModel.Notes.Count);
        }
    }
}
