﻿using System;
using System.Runtime.InteropServices;
using System.Security;

namespace SourceCode.Tools.Management.Helpers
{
    [SuppressUnmanagedCodeSecurityAttribute]
    internal static class UnsafeNativeMethods
    {
        [DllImport("user32.dll")]
        public static extern IntPtr SendMessage(IntPtr hWnd, UInt32 Msg, IntPtr wParam, IntPtr lParam);
    }
}