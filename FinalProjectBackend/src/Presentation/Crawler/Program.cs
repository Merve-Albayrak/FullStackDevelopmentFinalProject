namespace Crawler
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]

        static void Main()
        {
            Thread.Sleep(5000);
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            //   Application.Run

            System.Windows.Forms. Application.Run(new CrawlMainForm());

          
        }
    }
}