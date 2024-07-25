using Stempel.Domain.Model;

namespace Stempel.Domain.Repositories;
public interface IMemberRepository
{
    Task<Member> CreateAsync();
    Task<Member?> GetOrDefaultAsync(string code);
    Task SaveAsync(Member member);
    Task ChangeStateAsync(Member member);
    Task<Member[]> GetAllArrivedMembersAsync();
}
