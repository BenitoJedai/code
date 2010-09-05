using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Interop;
using ScriptCoreLib.CSharp.Avalon.Extensions;

namespace ScriptCoreLib.Ultra.Components.Volatile.LogoAnimation.Library
{
    public static class MyExtensions
    {
        public static void MakeInteractive(this Window w, bool value)
        {
            IntPtr hwnd = new WindowInteropHelper(w).Handle;

            // Change the extended window style to include WS_EX_TRANSPARENT
            WindowExStyles extendedStyle = (WindowExStyles)Internal.GetWindowLong(hwnd, DesktopWindowManager.GWL_EXSTYLE);

            if (value)
            {
                extendedStyle &= ~(WindowExStyles.WS_EX_TRANSPARENT | WindowExStyles.WS_EX_NOACTIVATE);

            }
            else
            {
           
                extendedStyle |= WindowExStyles.WS_EX_TRANSPARENT | WindowExStyles.WS_EX_NOACTIVATE;

            }
            Internal.SetWindowLong(hwnd, DesktopWindowManager.GWL_EXSTYLE, extendedStyle);
        }
    }
}
