using Stempel.Domain.Model;

namespace Stempel.Domain.Repositories;
public interface IMemberRepository
{
    Member Get(string code);
}
