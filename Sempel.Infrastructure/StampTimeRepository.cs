using Stempel.Domain.Model;
using Stempel.Domain.Repositories;

namespace Stempel.Infrastructure;
public class StampTimeRepository(StampContext context) : IStampTimeRepository
{
    private StampContext _context = context;

    public async Task<StampTime[]> GetAll(Member member)
    {
        if (member == null)
            throw new ArgumentNullException("member");
        var stampTimes = _context.StampTime.Where(x => x.Member == member).OrderBy(x => x.Time).ToArray();

        await Task.CompletedTask;

        return stampTimes;
    }
    public Task AddNow(Member member)
    {
        var stampTime = new StampTime()
        {
            Member = member,
            CreateTime = DateTime.Now,
            Id = Guid.NewGuid(),
            IsDeleted = false,
            StampState = member.LastState,
            Time = DateTime.Now,
            UpdateTime = DateTime.Now,
        };

        _context.StampTime.Add(stampTime);
        _context.SaveChanges();

        return Task.CompletedTask;
    }
}
