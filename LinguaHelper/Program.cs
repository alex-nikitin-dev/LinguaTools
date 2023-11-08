using LinguaHelper.Properties;
using System;
using System.Windows.Forms;

namespace TestProj
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetHighDpiMode(HighDpiMode.PerMonitorV2);
            Application.SetCompatibleTextRenderingDefault(false);
            Settings.Default.Reset();
            Application.Run(new MainForm());
        }
    }
}
