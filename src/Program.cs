using System;
using System.Threading;
using System.Windows.Forms;
using SourceCode.Tools.Management.Helpers;

namespace SourceCode.Tools.Management
{
    internal static class Program
    {
        public static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            MessageBoxHelper.DisplayError(e.Exception);
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.ThreadException += new ThreadExceptionEventHandler(
               Application_ThreadException);

            Application.Run(new MainForm());
        }
    }
}