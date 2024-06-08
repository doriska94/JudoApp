namespace Stempel.Domain.Repositories;
public interface IReciveKeyInput
{
    event EventHandler<string> CodeReaded;
    void CreateHook();
    void DisposeHook();
}
