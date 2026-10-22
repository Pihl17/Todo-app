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
        public void IndexTest()
        {
            IndexModel indexmodel = new IndexModel();
            indexmodel.OnGet();
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

        /*[Test]
        public void PostTodoTest() {
            SubmitTodoModel Model = new SubmitTodoModel(TestInputManager);

            ActionResult result = Model.OnPost();

            Assert.NotNull(result);
        }*/

    }
}
