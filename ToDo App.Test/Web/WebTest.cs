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
        public void setuptest()
        {
            ToDoHandler toDoHandler = new ToDoHandler();
            DataHandler dataHandler = new DataHandler();
            ToDoListSort toDoListSort = new ToDoListSort();
            TagFilter tagFilter = new TagFilter();
            InputManager inputManager = new InputManager(toDoHandler, dataHandler, toDoListSort, tagFilter);

            IndexModel indexmode = new IndexModel(inputManager);
        }
    }
}
