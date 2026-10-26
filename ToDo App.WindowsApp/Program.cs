using System.Runtime.CompilerServices;
using ToDo_App.Library.Calendar;
using ToDo_App.Library.Data;
using ToDo_App.Library.Handler;
using ToDo_App.Library.Input;
using ToDo_App.UI;

[assembly: InternalsVisibleTo("ToDo App.Test")]
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
            InputManager inputManager = CreateInputManager();
            Application.Run(new MainForm(inputManager));
        }
        internal static InputManager CreateInputManager()
        {
            return new InputManager(
                new ToDoHandler(),
                new JSONDataHandler(),
                new ToDoListSort(),
                new TagFilter(),
                new CalendarManager());
        }
    }
}