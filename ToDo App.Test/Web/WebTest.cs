using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Text;
using ToDo_App.Library.Calendar;
using ToDo_App.Library.Handler;
using ToDo_App.Library.Input;
using ToDo_App.Library.Models;
using ToDo_App.WebApp.Pages;


namespace ToDo_App.Test.Web
{
    internal class WebTest
    {
        private InputManager TestInputManager;

        [OneTimeSetUp]
        public void Setup(){
            ToDoHandler toDoHandler = new ToDoHandler();
            DataHandler dataHandler = new DataHandler();
            ToDoListSort toDoListSort = new ToDoListSort();
            TagFilter tagFilter = new TagFilter();
            CalendarManager calendarManager = new CalendarManager();

            TestInputManager = new InputManager(toDoHandler, dataHandler, toDoListSort, tagFilter, calendarManager);
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
        public void TodoesTest()
        {
            TodoesModel todoesmodel = new TodoesModel(TestInputManager);
        }

        [Test]
        public void GetATodoTest() {
            ToDo toDo = new ToDo();
            Assert.AreEqual("", toDo.Title);

            TestInputManager.ToDos = new List<ToDo>() { toDo };

            TodoModel Model = new TodoModel(TestInputManager);
            Model.OnGet(toDo.Id);

            Assert.AreEqual(Model.ToDo.Id, toDo.Id);
        }


        [Test]
        public void PostTodoTest() {
            SubmitTodoModel Model = new SubmitTodoModel(TestInputManager);
            DateTime Tommorow = DateTime.Today.AddDays(+1);
            RepeatInterval Weekdays = RepeatInterval.Weekdays;
            ActionResult result = Model.OnPost("Test", "this is a todo", Tommorow, Weekdays, 1, []);

            Assert.NotNull(result);
        }

        [Test]
        public void UpdateTodoTest()
        {
            ToDo toDo = new ToDo();
            Assert.AreEqual("", toDo.Title);

            TestInputManager.ToDos = new List<ToDo>() { toDo };

            UpdateModel Model = new UpdateModel(TestInputManager);
            Model.OnGet(toDo.Id);
            DateTime Tommorow = DateTime.Today.AddDays(+1);
            RepeatInterval Weekdays = RepeatInterval.Weekdays;
            ActionResult result = Model.OnPost(toDo.Id, "Test", "this is a todo", Tommorow, Weekdays, 1, []);
            
            Assert.AreEqual("Test", toDo.Title);

        }
    }
}
