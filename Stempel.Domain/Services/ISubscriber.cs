using Stempel.Domain.Model;

namespace Stempel.Domain.Services;
public interface ISubscriber
{
    void Update(Member member);
}
