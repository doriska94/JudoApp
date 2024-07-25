namespace Stempel.Domain.Model;
public class StampTime : ModelBase
{
    public Member Member { get; set; }
    public DateTime Time { get; set; }
    public StampState StampState { get; set; }
}
