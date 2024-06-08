

using Stempel.Domain.Model;

namespace Stempel.Domain.Services;

public interface INotifyChipFoundHandler
{
    void Notify(Member member);
}