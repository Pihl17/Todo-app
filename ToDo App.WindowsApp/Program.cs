using ToDo_App.Library.Handler;
using ToDo_App.Library.Input;
using ToDo_App.UI;
using ToDo_App.Library.Calendar;

namespace ToDo_App
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            ToDoHandler toDoHandler = new ToDoHandler();
            DataHandler dataHandler = new DataHandler();
            ToDoListSort toDoListSort = new ToDoListSort();
            CalendarManager calendar = new CalendarManager();
            TagFilter tagFilter = new TagFilter();
            InputManager inputManager = new InputManager(toDoHandler, dataHandler, toDoListSort, tagFilter, calendar);
            Application.Run(new MainForm(inputManager));
        }
    }
}