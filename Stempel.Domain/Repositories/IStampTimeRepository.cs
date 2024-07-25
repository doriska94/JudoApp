using Stempel.Domain.Model;

namespace Stempel.Domain.Repositories;
public interface IStampTimeRepository
{
    public Task AddNow(Member member);
    Task<StampTime[]> GetAll(Member member);
}
