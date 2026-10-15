using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Text;
using ToDo_App.Library.Handler;
using ToDo_App.Library.Input;
using ToDo_App.WebApp.Pages;


namespace ToDo_App.Test.Web
{
    internal class WebTest
    {
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
        public void TodoViewTest()
        {
            ToDoHandler toDoHandler = new ToDoHandler();
            DataHandler dataHandler = new DataHandler();
            ToDoListSort toDoListSort = new ToDoListSort();
            TagFilter tagFilter = new TagFilter();
            InputManager inputManager = new InputManager(toDoHandler, dataHandler, toDoListSort, tagFilter);

            TodoViewsModel todoviewsmodel = new TodoViewsModel(inputManager);
        }

        [Test]
        public void PostTodoTest() {
            ToDoHandler toDoHandler = new ToDoHandler();
            DataHandler dataHandler = new DataHandler();
            ToDoListSort toDoListSort = new ToDoListSort();
            TagFilter tagFilter = new TagFilter();
            InputManager inputManager = new InputManager(toDoHandler, dataHandler, toDoListSort, tagFilter);

            SubmitTodoModel Model = new SubmitTodoModel(inputManager);

            ActionResult result = Model.OnPost();

            Assert.NotNull(result);
        }

    }
}
