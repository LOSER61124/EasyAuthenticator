namespace EasyAuthenticator
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
            // PerMonitorV2：高DPI下按缩放重排版（清晰），而非位图拉伸（发虚）；须在Initialize之前调用才生效
            Application.SetHighDpiMode(HighDpiMode.PerMonitorV2);
            ApplicationConfiguration.Initialize();
            Application.Run(new Form1());
        }
    }
}