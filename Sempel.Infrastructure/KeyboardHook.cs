using Stempel.Domain;
using Stempel.Domain.Repositories;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Stempel.Infrastructure;

public sealed class KeyboardHook
{
    [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    private static extern nint SetWindowsHookEx(int idHook, KeyboardProcess lpfn, nint hMod, uint dwThreadId);

    [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    private static extern nint CallNextHookEx(nint hhk, int nCode, nint wParam, nint lParam);

    [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    private static extern nint GetModuleHandle(string lpModuleName);

    [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool UnhookWindowsHookEx(nint hhk);

    private delegate nint KeyboardProcess(int nCode, nint wParam, nint lParam);

    public static event EventHandlerAsync<KeyPressedEventArgs> KeyPressed;
    private const int _wH_KEYBOARD = 13;
    private static readonly KeyboardProcess _keyboardProc = HookCallback;
    private static nint _hookID = nint.Zero;

    public static void CreateHook()
    {
        _hookID = SetHook(_keyboardProc);
    }

    public static void DisposeHook()
    {
        UnhookWindowsHookEx(_hookID);
    }

    private static nint SetHook(KeyboardProcess keyboardProc)
    {
        using (Process currentProcess = Process.GetCurrentProcess())
        using (ProcessModule currentProcessModule = currentProcess.MainModule)
        {
            return SetWindowsHookEx(_wH_KEYBOARD, keyboardProc, GetModuleHandle(currentProcessModule.ModuleName), 0);
        }
    }

    private static nint HookCallback(int nCode, nint wParam, nint lParam)
    {
        if (nCode >= 0)
        {
            int vkCode = Marshal.ReadInt32(lParam);

            if (KeyPressed != null)
                KeyPressed(null, new KeyPressedEventArgs(vkCode)).Wait();
        }
        return CallNextHookEx(_hookID, nCode, wParam, lParam);
    }
}