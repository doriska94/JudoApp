using Stempel.Domain.Repositories;

namespace Stempel.Domain.Services;
public class RFIDChipCode : IGetChipCode
{
    private readonly INotifyChipFoundHandler _chipFoundHandler;
    private readonly IMemberRepository _memberRepository;
    private readonly IReciveKeyInput _reciveKeyInput;
    public bool IsRunning { get; private set; }
    public RFIDChipCode(INotifyChipFoundHandler chipFoundHandler, 
                        IMemberRepository memberRepository, 
                        IReciveKeyInput reciveKeyInput)
    {
        _chipFoundHandler = chipFoundHandler;
        _memberRepository = memberRepository;
        _reciveKeyInput = reciveKeyInput;
    }

    public void Enable()
    {
        _reciveKeyInput.CreateHook();
        _reciveKeyInput.CodeReaded += OnKeyRecived;
        IsRunning = true;
    }

    public void Disable()
    {
        _reciveKeyInput.CodeReaded -= OnKeyRecived;
        _reciveKeyInput.DisposeHook();
        IsRunning = false;
    }

    private async Task OnKeyRecived(object? sender, string code)
    {
        var member = await _memberRepository.GetOrDefaultAsync(code);
        if (member == null)
            return;

        await _memberRepository.ChangeStateAsync(member);
        _chipFoundHandler.Notify(member);
    }
}
