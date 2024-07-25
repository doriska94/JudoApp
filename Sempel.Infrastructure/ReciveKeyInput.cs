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
    public event EventHandlerAsync<string> CodeReaded;
    private List<char> _chars = new();
    public void CreateHook()
    {

        KeyboardHook.CreateHook();
        KeyboardHook.KeyPressed += KeyPressedAsync;
    }

    private async Task KeyPressedAsync(object? sender, KeyPressedEventArgs e)
    {
        char digit = Convert.ToChar(GetKeyValueNormalized(e.Info));

        if (_chars.Count > 0 && _chars[_chars.Count -1] == '\r' && digit == '\r')
        {
            var code = new StringBuilder();
            for (int i = 0; i < _chars.Count - 1; i += 2)
            {
                code.Append(_chars[i]);
            }

            _chars.Clear();
            await CodeReaded?.Invoke(sender, code.ToString());
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
        KeyboardHook.KeyPressed -= KeyPressedAsync;
        KeyboardHook.DisposeHook();
    }
}
