#region Usings

using System.Collections.Generic;
using System.Web.Mvc;
using Intranet.Common;
using Intranet.Labor.Model;
using Intranet.Labor.ViewModel;
using Intranet.TestEnvironment;
using Intranet.Web.Areas.Labor.Controllers;
using Xunit;

#endregion

namespace Intranet.Web.Test
{
    /// <summary>
    ///     A test class for the babydiaper retention controller
    /// </summary>
    public class BabyDiaperRetentionControllerTest
    {
        /// <summary>
        ///     Tests if the AddNotes Action adds a note
        /// </summary>
        [Fact]
        public void AddNoteTest1()
        {
            var viewModel = new BabyDiaperRetentionEditViewModel
            {
                Notes = new List<TestNote>()
            };
            var controller = new BabyDiaperRetentionController( new NLogLoggerFactory() );
            var result = controller.AddNote( viewModel ) as ViewResult;
            var newViewModel = (BabyDiaperRetentionEditViewModel) result?.ViewData.Model;
            Assert.NotNull( newViewModel );
            Assert.Equal( 1, newViewModel.Notes.Count );
        }

        /// <summary>
        ///     Tests if the AddNotes Action adds a note if the old viewmodel Note list is NULL
        /// </summary>
        [Fact]
        public void AddNoteTest2()
        {
            var viewModel = new BabyDiaperRetentionEditViewModel();
            var controller = new BabyDiaperRetentionController( new NLogLoggerFactory() );
            var result = controller.AddNote( viewModel ) as ViewResult;
            var newViewModel = (BabyDiaperRetentionEditViewModel) result?.ViewData.Model;
            Assert.NotNull( newViewModel );
            Assert.Equal( 1, newViewModel.Notes.Count );
        }

        /// <summary>
        ///     Tests if an call for Create with no Id (or id = 0) retunrs an HttpNotFoundResult
        /// </summary>
        [Fact]
        public void CreateFailTest()
        {
            var babyDiaperRetentionService = MockHelperLaborControllerService.GetBabyDiaperRetentionService( new BabyDiaperRetentionEditViewModel() );
            var controller = new BabyDiaperRetentionController( new NLogLoggerFactory() )
            {
                BabyDiaperRetentionService = babyDiaperRetentionService
            };
            var result = controller.Create();
            Assert.IsType( typeof(HttpNotFoundResult), result );
        }

        /// <summary>
        ///     Tests if the returned view is the right view
        /// </summary>
        [Fact]
        public void CreateReturnsCorrectViewTest()
        {
            var viewModel = new BabyDiaperRetentionEditViewModel
            {
                TestValueId = -1,
                TestSheetId = 5
            };
            var babyDiaperRetentionService = MockHelperLaborControllerService.GetBabyDiaperRetentionService( viewModel );
            var controller = new BabyDiaperRetentionController( new NLogLoggerFactory() )
            {
                BabyDiaperRetentionService = babyDiaperRetentionService
            };
            var result = controller.Create( 5 ) as ViewResult;
            Assert.Equal( "Edit", result?.ViewName );
        }

        /// <summary>
        ///     Tests if an call for Create with an ID > 0 returns a viewmodel
        /// </summary>
        [Fact]
        public void CreateTest()
        {
            var viewModel = new BabyDiaperRetentionEditViewModel
            {
                TestValueId = -1,
                TestSheetId = 5
            };
            var babyDiaperRetentionService = MockHelperLaborControllerService.GetBabyDiaperRetentionService( viewModel );
            var controller = new BabyDiaperRetentionController( new NLogLoggerFactory() )
            {
                BabyDiaperRetentionService = babyDiaperRetentionService
            };
            var result = controller.Create( 5 ) as ViewResult;
            var newViewModel = (BabyDiaperRetentionEditViewModel) result?.ViewData.Model;
            Assert.Equal( viewModel, newViewModel );
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
                TestSheetId = 5
            };
            var babyDiaperRetentionService = MockHelperLaborControllerService.GetBabyDiaperRetentionServiceForDeleteAndSave( testVaule );
            var controller = new BabyDiaperRetentionController( new NLogLoggerFactory() )
            {
                BabyDiaperRetentionService = babyDiaperRetentionService
            };

            var result = controller.Delete( 4 ) as RedirectToRouteResult;
            Assert.NotNull( result );
            Assert.Equal( "Edit", result.RouteValues["action"] );
            Assert.Equal( "LaborCreatorBaby", result.RouteValues["controller"] );
            Assert.Equal( "Labor", result.RouteValues["area"] );
            Assert.Equal( 5, result.RouteValues["id"] );
        }

        /// <summary>
        ///     Tests if an call for Edit with no Id (or id = 0) retunrs an HttpNotFoundResult
        /// </summary>
        [Fact]
        public void EditFailTest()
        {
            var babyDiaperRetentionService = MockHelperLaborControllerService.GetBabyDiaperRetentionService( new BabyDiaperRetentionEditViewModel() );
            var controller = new BabyDiaperRetentionController( new NLogLoggerFactory() )
            {
                BabyDiaperRetentionService = babyDiaperRetentionService
            };
            var result = controller.Edit();
            Assert.IsType( typeof(HttpNotFoundResult), result );
        }

        /// <summary>
        ///     Tests if the returned view is the right view
        /// </summary>
        [Fact]
        public void EditReturnsCorrectViewTest()
        {
            var viewModel = new BabyDiaperRetentionEditViewModel
            {
                TestValueId = -1,
                TestSheetId = 5
            };
            var babyDiaperRetentionService = MockHelperLaborControllerService.GetBabyDiaperRetentionService( viewModel );
            var controller = new BabyDiaperRetentionController( new NLogLoggerFactory() )
            {
                BabyDiaperRetentionService = babyDiaperRetentionService
            };
            var result = controller.Edit( 5 ) as ViewResult;
            Assert.Equal( "Edit", result?.ViewName );
        }

        /// <summary>
        ///     Tests if an call for Edit with an ID > 0 returns a viewmodel
        /// </summary>
        [Fact]
        public void EditTest()
        {
            var viewModel = new BabyDiaperRetentionEditViewModel
            {
                TestValueId = -1,
                TestSheetId = 5
            };
            var babyDiaperRetentionService = MockHelperLaborControllerService.GetBabyDiaperRetentionService( viewModel );
            var controller = new BabyDiaperRetentionController( new NLogLoggerFactory() )
            {
                BabyDiaperRetentionService = babyDiaperRetentionService
            };
            var result = controller.Edit( 5 ) as ViewResult;
            var newViewModel = (BabyDiaperRetentionEditViewModel) result?.ViewData.Model;
            Assert.Equal( viewModel, newViewModel );
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
                TestSheetId = 5
            };
            var babyDiaperRetentionService = MockHelperLaborControllerService.GetBabyDiaperRetentionServiceForDeleteAndSave( testVaule );
            var controller = new BabyDiaperRetentionController( new NLogLoggerFactory() )
            {
                BabyDiaperRetentionService = babyDiaperRetentionService
            };

            var result = controller.Save( new BabyDiaperRetentionEditViewModel() ) as RedirectToRouteResult;
            Assert.NotNull( result );
            Assert.Equal( "Edit", result.RouteValues["action"] );
            Assert.Equal( "LaborCreatorBaby", result.RouteValues["controller"] );
            Assert.Equal( "Labor", result.RouteValues["area"] );
            Assert.Equal( 5, result.RouteValues["id"] );
        }
    }
}