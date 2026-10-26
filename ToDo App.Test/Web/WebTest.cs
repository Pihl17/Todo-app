using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using ToDo_App.Library.Calendar;
using ToDo_App.Library.Data;
using ToDo_App.Library.Handler;
using ToDo_App.Library.Input;
using ToDo_App.Library.Models;
using ToDo_App.WebApp.Pages;


namespace ToDo_App.Test.Web
{
    internal class WebTest
    {
        private Mock<IInputHandling> mock;
        private ModelMetadataProvider modelMetadataProvider;
        private ViewDataDictionary viewData;

        [SetUp]
        public void Setup(){
            mock = new Mock<IInputHandling>();

            modelMetadataProvider = new EmptyModelMetadataProvider();

            //Creating View data instance
            viewData = new ViewDataDictionary(modelMetadataProvider, new ModelStateDictionary());
        }

        [Test]
        public void PrivacyTest()
        {
            PrivacyModel privacymodel = new PrivacyModel();
            privacymodel.OnGet();
        }

        [Test]
        public void ErrorTest()
        {
            ErrorModel errormodel = new ErrorModel();

            var httpContext = new DefaultHttpContext();

            errormodel.PageContext = new PageContext
            {
                HttpContext = httpContext
            };

            errormodel.OnGet();

            Assert.NotNull(errormodel.RequestId);
            Assert.True(errormodel.ShowRequestId);
        }

        [Test]
        public void TodoesPostTest()
        {
            ToDo toDo = new ToDo();
            List<ToDo> listofTodoes = new List<ToDo>() { toDo };
            Assert.IsNotEmpty(listofTodoes);

            mock.Setup(x => x.GetToDoList()).Returns(listofTodoes);
            mock.Setup(x => x.GetToDo(toDo.Id)).Returns(toDo);

            TodoesModel Model = new TodoesModel(mock.Object);

            Model.OnPost(toDo.Id);

            mock.Verify(x => x.DeleteToDo(toDo));
        }

        [Test]
        public void GetATodoTest() {
            ToDo toDo = new ToDo();
            Assert.AreEqual("", toDo.Title);

            mock.Setup(x => x.GetToDoList()).Returns(new List<ToDo>() { toDo });
            mock.Setup(x => x.GetToDo(toDo.Id)).Returns(toDo);

            TodoModel Model = new TodoModel(mock.Object)
            {
                PageContext = new PageContext
                {
                    ViewData = viewData
                },
                MetadataProvider = modelMetadataProvider,
            };

            Model.OnGet(toDo.Id);

            mock.Verify(x => x.GetToDo(toDo.Id));
        }

        [Test]
        public void GetATodoTestError()
        {
            Guid test = new Guid("e7e64692-6ac4-4d8d-baa1-ab8be334fd6d");

            TodoModel Model = new TodoModel(mock.Object)
            {
                PageContext = new PageContext
                {
                    ViewData = viewData
                },
                MetadataProvider = modelMetadataProvider,
            };

            Model.OnGet(test);

            Assert.IsNull(Model.ToDo);
            Assert.NotNull(Model.ErrorMessage);
        }

        [Test]
        public void PostTodoTest() {
            SubmitTodoModel Model = new SubmitTodoModel(mock.Object);
            DateTime Tommorow = DateTime.Today.AddDays(+1);
            RepeatInterval Weekdays = RepeatInterval.Weekdays;
            ActionResult result = Model.OnPost("Test", "this is a todo", Tommorow, Weekdays, 1, []);

            Assert.NotNull(result);
        }

        [Test]
        public void OnGetUpdateTodoTest()
        {
            ToDo toDo = new ToDo()
            {
                Deadline = new DateTime(2026, 3, 1),
            };

            mock.Setup(x => x.GetToDoList()).Returns(new List<ToDo>() { toDo  });
            mock.Setup(x => x.GetToDo(toDo.Id)).Returns(toDo);

            UpdateModel Model = new UpdateModel(mock.Object)
            {
                PageContext = new PageContext
                {
                    ViewData = viewData
                },
                MetadataProvider = modelMetadataProvider,
            };

            Model.OnGet(toDo.Id);
            mock.Verify(x => x.GetToDo(toDo.Id));

            Assert.AreEqual("2026-03-01T00:00", Model.HtmlDateTime);
        }

        [Test]
        public void OnGetUpdateTodoErrorTest()
        {
            Guid test = new Guid("e7e64692-6ac4-4d8d-baa1-ab8be334fd6d");

            UpdateModel Model = new UpdateModel(mock.Object)
            {
                PageContext = new PageContext
                {
                    ViewData = viewData
                },
                MetadataProvider = modelMetadataProvider,
            };

            Model.OnGet(test);

            Assert.IsNull(Model.ToDo);
            Assert.NotNull(Model.ErrorMessage);

        }


        [Test]
        public void UpdateTodoTest()
        {
            ToDo toDo = new ToDo();
            mock.Setup(x => x.GetToDoList()).Returns(new List<ToDo>() { toDo });
            mock.Setup(x => x.GetToDo(toDo.Id)).Returns(toDo);

            UpdateModel Model = new UpdateModel(mock.Object)
            {
                PageContext = new PageContext
                {
                    ViewData = viewData
                },
                MetadataProvider = modelMetadataProvider,
            };

            Model.OnGet(toDo.Id);
            mock.Verify(x => x.GetToDo(toDo.Id));

            DateTime Tommorow = DateTime.Today.AddDays(+1);
            RepeatInterval Weekdays = RepeatInterval.Weekdays;
            ActionResult result = Model.OnPost(toDo.Id, "Test", "this is a todo", Tommorow, Weekdays, 1, null);
            
            mock.Verify(x => x.UpdateToDo(toDo.Id, "Test", "this is a todo", Tommorow, null, Weekdays, null, 1, null));
        }
    }
}
