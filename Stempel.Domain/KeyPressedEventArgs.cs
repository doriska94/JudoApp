namespace Stempel.Domain;
public class KeyPressedEventArgs
{
    public int Info { get; set; }
    public KeyPressedEventArgs(int Key)
    {
        Info = Key;
    }
}
