using Stempel.Domain.Model;

namespace Stempel.Domain.Services;
public class ChipFoundNotifeyer : INotifyChipFound, INotifyChipFoundHandler
{
    public event OnChipFound OnChipFound;

    public void AddSubscriber(ISubscriber subscriber)
    {
        OnChipFound += subscriber.Update;
    }

    public void Notify(Member member)
    {
        OnChipFound?.Invoke(member);
    }

    public void RemoveSubscriber(ISubscriber subscriber)
    {
        OnChipFound -= subscriber.Update;
    }
}
