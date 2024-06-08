using static System.Runtime.CompilerServices.RuntimeHelpers;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Stempel.Domain;
using Stempel.Domain.Repositories;

namespace Sempel.Infrastructure;

public sealed class KeyboardHook 
{
    [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    private static extern IntPtr SetWindowsHookEx(int idHook, KeyboardProcess lpfn, IntPtr hMod, uint dwThreadId);

    [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

    [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    private static extern IntPtr GetModuleHandle(string lpModuleName);

    [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool UnhookWindowsHookEx(IntPtr hhk);

    private delegate IntPtr KeyboardProcess(int nCode, IntPtr wParam, IntPtr lParam);

    public static event EventHandler<KeyPressedEventArgs> KeyPressed;
    private const int _wH_KEYBOARD = 13;
    private static readonly KeyboardProcess keyboardProc = HookCallback;
    private static IntPtr _hookID = IntPtr.Zero;

    public static void CreateHook()
    {
        _hookID = SetHook(keyboardProc);
    }

    public static void DisposeHook()
    {
        UnhookWindowsHookEx(_hookID);
    }

    private static IntPtr SetHook(KeyboardProcess keyboardProc)
    {
        using (Process currentProcess = Process.GetCurrentProcess())
        using (ProcessModule currentProcessModule = currentProcess.MainModule)
        {
            return SetWindowsHookEx(_wH_KEYBOARD, keyboardProc, GetModuleHandle(currentProcessModule.ModuleName), 0);
        }
    }

    private static IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
    {
        if (nCode >= 0)
        {
            int vkCode = Marshal.ReadInt32(lParam);

            if (KeyPressed != null)
                KeyPressed(null, new KeyPressedEventArgs(vkCode));
        }
        return CallNextHookEx(_hookID, nCode, wParam, lParam);
    }
}