

using Stempel.Domain.Model;

namespace Stempel.Domain.Services;
public delegate void OnChipFound(Member member);
public interface INotifyChipFound
{
    event OnChipFound OnChipFound;
    void AddSubscriber(ISubscriber subscriber);
    void RemoveSubscriber(ISubscriber subscriber);
}
