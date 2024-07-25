using Stempel.Domain;
using Stempel.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;


namespace Stempel.Infrastructure;
public class ReciveKeyInput : IReciveKeyInput
{
    public event EventHandler<string> CodeReaded;
    private List<char> _chars = new();
    public void CreateHook()
    {

        KeyboardHook.CreateHook();
        KeyboardHook.KeyPressed += KeyPressed;
    }

    private void KeyPressed(object? sender, KeyPressedEventArgs e)
    {
        char digit = Convert.ToChar(GetKeyValueNormalized(e.Info));

        if (digit == '\r')
        {
            var code = new StringBuilder();
            for (int i = 0; i < _chars.Count; i += 2)
            {
                code.Append(_chars[i]);
            }

            CodeReaded?.Invoke(sender, code.ToString());
            _chars.Clear();
        }
        else
        {
            _chars.Add(digit);
        }
    }
    private int GetKeyValueNormalized(int keyValue)
    {
        if (keyValue >= 96 && keyValue <= 105)
        {
            return keyValue - 48; //numpad
        }
        else
        {
            return keyValue;
        }
    }

    public void DisposeHook()
    {
        KeyboardHook.KeyPressed -= KeyPressed;
        KeyboardHook.DisposeHook();
    }
}
