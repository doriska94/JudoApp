
namespace Stempel.Domain.Model;
public class Member : ModelBase
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public StampState LastState { get; set; }
    public string? Key { get; set; }
}
