using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Stempel.Domain.Model;
using Stempel.Domain.Repositories;
using System.Diagnostics;

namespace Stempel.Infrastructure;
public class MemberRepository : IMemberRepository
{
    private readonly StampContext _memberContext;
    private DbSet<Member> _membersDbSet;

    public MemberRepository(StampContext memberContext)
    {
        _memberContext = memberContext;
        _membersDbSet = _memberContext.Members;
    }

    public async Task<Member?> GetOrDefaultAsync(string code)
    {
        Debug.Print("Code: " + code);
        await Task.CompletedTask;
        var lsit = _membersDbSet.Where(x => x.Key == code).ToList();
        return lsit.SingleOrDefault();
    }

    public async Task<Member> CreateAsync()
    {
        var member = new Member()
        {
            Id = Guid.NewGuid(),
            CreateTime = DateTime.Now,
            IsDeleted= false,
            UpdateTime= DateTime.Now,
        };
        await Task.CompletedTask;
        _membersDbSet.Add(member);
        return member;
    }

    public async Task SaveAsync(Member member)
    {
        await Task.CompletedTask;
        _memberContext.SaveChanges();
    }

    public async Task ChangeStateAsync(Member member)
    {
        if (member.LastState == StampState.Levead)
            member.LastState = StampState.Arrived;
        else
            member.LastState = StampState.Levead;

        await SaveAsync(member);
    }
}
