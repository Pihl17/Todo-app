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
            DataHandler TestHandler = new DataHandler(1, 2);
            TestHandler.CreateJSON();
            Application.Run(new Form1());
        }
    }
}