
namespace Stempel.Domain.Model;
public class Member : ModelBase
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public State State { get; set; }
    public string? Key { get; set; }
}
