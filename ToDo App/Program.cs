using ToDo_App.Input;
using ToDo_App.UI;

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
            InputManager inputManager = new InputManager();
            Application.Run(new MainForm(inputManager));
        }
    }
}