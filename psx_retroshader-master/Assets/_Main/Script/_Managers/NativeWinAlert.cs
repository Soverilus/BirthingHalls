using System;
using System.Runtime.InteropServices;
using UnityEngine;
/// <see>https://docs.microsoft.com/en-us/windows/desktop/api/winuser/nf-winuser-messagebox</see>
/// 
public enum WindowType { Error, Warning, Info }

public static class NativeWinAlert
{
    [System.Runtime.InteropServices.DllImport("user32.dll")]
    private static extern System.IntPtr GetActiveWindow();

    public static System.IntPtr GetWindowHandle()
    {
        return GetActiveWindow();
    }

    [DllImport("user32.dll", SetLastError = true)]
    static extern int MessageBox(IntPtr hwnd, String lpText, String lpCaption, uint uType);

    /// <summary>
    /// Shows Error alert box with OK button.
    /// </summary>
    /// <param name="text">Main alert text / content.</param>
    /// <param name="caption">Message box title.</param>
    public static void PopUp(string text, string caption, WindowType type)
    {
        Screen.fullScreenMode = FullScreenMode.MaximizedWindow;
        switch (type)
        {
            case WindowType.Error:
                try
                {
                    MessageBox(GetWindowHandle(), text, caption, (uint)(0x00000000L | 0x00000010L));
                }
                catch (Exception ex) { }
                break;
            case WindowType.Warning:
                try
                {
                    MessageBox(GetWindowHandle(), text, caption, (uint)(0x00000000L | 0x00000030L));
                }
                catch (Exception ex) { }
                break;
            case WindowType.Info:
                try
                {
                    MessageBox(GetWindowHandle(), text, caption, (uint)(0x00000000L | 0x00000040L));
                }
                catch (Exception ex) { }
                break;
        }
    }
}