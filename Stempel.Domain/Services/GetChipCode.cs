﻿using Stempel.Domain.Model;
using Stempel.Domain.Repositories;

namespace Stempel.Domain.Services;
public class GetChipCode : IGetChipCode
{
    private INotifyChipFoundHandler _chipFoundHandler;
    private IMemberRepository _memberRepository;
    private IReciveKeyInput _reciveKeyInput;
    private List<int> _keyCode = new();

    public GetChipCode(INotifyChipFoundHandler chipFoundHandler, IMemberRepository memberRepository, IReciveKeyInput reciveKeyInput)
    {
        _chipFoundHandler = chipFoundHandler;
        _memberRepository = memberRepository;
        _reciveKeyInput = reciveKeyInput;
    }

    public void Enable()
    {
        _reciveKeyInput.CreateHook();
        _reciveKeyInput.CodeReaded += OnKeyRecived;
    }
    public void OnDisable()
    {
        _reciveKeyInput.CodeReaded -= OnKeyRecived;
    }
    private async Task OnKeyRecived(object? sender, string code)
    {
        var member = await _memberRepository.GetOrDefaultAsync(code);
        if(member == null)
            return;

        await _memberRepository.ChangeStateAsync(member);
        _chipFoundHandler.Notify(member);
    }
}
