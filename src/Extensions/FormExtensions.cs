using System;
using System.Windows.Forms;
using Tools.Management.Helpers;
using Tools.Management.Properties;

namespace Tools.Management.Extensions
{
    /// <summary>
    /// Form extention methods
    /// </summary>
    internal static class FormExtensions
    {
        private const int WM_SETREDRAW = 11;

        public static void Busy(this Form form)
        {
            if (form == null)
            {
                return;
            }

            form.Cursor = Cursors.WaitCursor;
        }

        public static void DisplayError(this Form form, string message, string caption)
        {
            form.Stop();
            MessageBoxHelper.DisplayError(message, caption);
        }

        public static void DisplayError(this Form form, Exception ex)
        {
            form.DisplayError(ex.Message, Resources.Error);
        }

        public static void DisplayInfo(this Form form, string message, string caption)
        {
            form.Stop();
            MessageBoxHelper.DisplayInfo(message, caption);
        }

        public static void Draw(this Form form, Action action)
        {
            form.StartRedraw();

            try
            {
                action();
            }
            finally
            {
                form.StopRedraw();
            }
        }

        public static int StartRedraw(this Form form)
        {
            form.SuspendLayout();

            return (int)UnsafeNativeMethods.SendMessage(form.Handle, WM_SETREDRAW, (IntPtr)0, (IntPtr)0);
        }

        public static void Stop(this Form form)
        {
            if (form == null)
            {
                return;
            }

            form.Cursor = Cursors.Default;
        }

        public static int StopRedraw(this Form form)
        {
            var returnVal = UnsafeNativeMethods.SendMessage(form.Handle, WM_SETREDRAW, (IntPtr)1, (IntPtr)0);

            form.ResumeLayout();
            form.Refresh();

            return (int)returnVal;
        }
    }
}