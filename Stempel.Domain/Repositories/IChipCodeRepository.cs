namespace Stempel.Domain.Repositories;

public delegate Task EventHandlerAsync<Targs>(object sender, Targs args);
public interface IReciveKeyInput
{
    event EventHandlerAsync<string> CodeReaded;
    void CreateHook();
    void DisposeHook();
}
