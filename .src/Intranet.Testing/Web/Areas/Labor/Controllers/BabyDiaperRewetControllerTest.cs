using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Intranet.Common;
using Intranet.Labor.Model.labor;
using Intranet.Labor.ViewModel;
using Intranet.TestEnvironment;
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

        /// <summary>
        ///     Tests if an call for Create with no Id (or id = 0) retunrs an HttpNotFoundResult
        /// </summary>
        [Fact]
        public void CreateFailTest()
        {
            var babyDiaperRewetService = MockHelperLaborControllerService.GetBabyDiaperRewetService(new BabyDiaperRewetEditViewModel());
            var controller = new BabyDiaperRewetController(new NLogLoggerFactory())
            {
                BabyDiaperRewetService = babyDiaperRewetService
            };
            var result = controller.Create();
            Assert.IsType(typeof(HttpNotFoundResult), result);
        }

        /// <summary>
        ///     Tests if an call for Create with an ID > 0 returns a viewmodel
        /// </summary>
        [Fact]
        public void CreateTest()
        {
            var viewModel = new BabyDiaperRewetEditViewModel
            {
                TestValueId = -1,
                TestSheetId = 5,
            };
            var babyDiaperRewetService = MockHelperLaborControllerService.GetBabyDiaperRewetService(viewModel);
            var controller = new BabyDiaperRewetController(new NLogLoggerFactory())
            {
                BabyDiaperRewetService = babyDiaperRewetService
            };
            var result = controller.Create(5) as ViewResult;
            var newViewModel = (BabyDiaperRewetEditViewModel)result?.ViewData.Model;
            Assert.Equal(viewModel, newViewModel);
        }

        /// <summary>
        ///     Tests if the returned view is the right view
        /// </summary>
        [Fact]
        public void CreateReturnsCorrectViewTest()
        {
            var viewModel = new BabyDiaperRewetEditViewModel
            {
                TestValueId = -1,
                TestSheetId = 5,
            };
            var babyDiaperRewetService = MockHelperLaborControllerService.GetBabyDiaperRewetService(viewModel);
            var controller = new BabyDiaperRewetController(new NLogLoggerFactory())
            {
                BabyDiaperRewetService = babyDiaperRewetService
            };
            var result = controller.Create(5) as ViewResult;
            Assert.Equal("Edit", result?.ViewName);
        }

        /// <summary>
        ///     Tests if an call for Edit with no Id (or id = 0) retunrs an HttpNotFoundResult
        /// </summary>
        [Fact]
        public void EditFailTest()
        {
            var babyDiaperRewetService = MockHelperLaborControllerService.GetBabyDiaperRewetService(new BabyDiaperRewetEditViewModel());
            var controller = new BabyDiaperRewetController(new NLogLoggerFactory())
            {
                BabyDiaperRewetService = babyDiaperRewetService
            };
            var result = controller.Edit();
            Assert.IsType(typeof(HttpNotFoundResult), result);
        }

        /// <summary>
        ///     Tests if an call for Edit with an ID > 0 returns a viewmodel
        /// </summary>
        [Fact]
        public void EditTest()
        {
            var viewModel = new BabyDiaperRewetEditViewModel
            {
                TestValueId = -1,
                TestSheetId = 5,
            };
            var babyDiaperRewetService = MockHelperLaborControllerService.GetBabyDiaperRewetService(viewModel);
            var controller = new BabyDiaperRewetController(new NLogLoggerFactory())
            {
                BabyDiaperRewetService = babyDiaperRewetService
            };
            var result = controller.Edit(5) as ViewResult;
            var newViewModel = (BabyDiaperRewetEditViewModel)result?.ViewData.Model;
            Assert.Equal(viewModel, newViewModel);
        }

        /// <summary>
        ///     Tests if the returned view is the right view
        /// </summary>
        [Fact]
        public void EditReturnsCorrectViewTest()
        {
            var viewModel = new BabyDiaperRewetEditViewModel
            {
                TestValueId = -1,
                TestSheetId = 5,
            };
            var babyDiaperRewetService = MockHelperLaborControllerService.GetBabyDiaperRewetService(viewModel);
            var controller = new BabyDiaperRewetController(new NLogLoggerFactory())
            {
                BabyDiaperRewetService = babyDiaperRewetService
            };
            var result = controller.Edit(5) as ViewResult;
            Assert.Equal("Edit", result?.ViewName);
        }

        /// <summary>
        ///     Tests of Deleting an Test returns you to the correct view
        /// </summary>
        [Fact]
        public void DeleteTest()
        {
            var testVaule = new TestValue
            {
                TestValueId = 4,
                TestSheetRefId = 5
            };
            var babyDiaperRewetService = MockHelperLaborControllerService.GetBabyDiaperRewetServiceForDeleteAndSave(testVaule);
            var controller = new BabyDiaperRewetController(new NLogLoggerFactory())
            {
                BabyDiaperRewetService = babyDiaperRewetService
            };

            var result = controller.Delete(4) as RedirectToRouteResult;
            Assert.NotNull(result);
            Assert.Equal("Edit", result.RouteValues["action"]);
            Assert.Equal("LaborCreatorBaby", result.RouteValues["controller"]);
            Assert.Equal("Labor", result.RouteValues["area"]);
            Assert.Equal(5, result.RouteValues["id"]);
        }

        /// <summary>
        ///     Tests of Saving an Test returns you to the correct view
        /// </summary>
        [Fact]
        public void SaveTest()
        {
            var testVaule = new TestValue
            {
                TestValueId = 4,
                TestSheetRefId = 5
            };
            var babyDiaperRewetService = MockHelperLaborControllerService.GetBabyDiaperRewetServiceForDeleteAndSave(testVaule);
            var controller = new BabyDiaperRewetController(new NLogLoggerFactory())
            {
                BabyDiaperRewetService = babyDiaperRewetService
            };

            var result = controller.Save(new BabyDiaperRewetEditViewModel()) as RedirectToRouteResult;
            Assert.NotNull(result);
            Assert.Equal("Edit", result.RouteValues["action"]);
            Assert.Equal("LaborCreatorBaby", result.RouteValues["controller"]);
            Assert.Equal("Labor", result.RouteValues["area"]);
            Assert.Equal(5, result.RouteValues["id"]);
        }
    }
}
