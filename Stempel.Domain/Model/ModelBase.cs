namespace Stempel.Domain.Model;
public abstract class ModelBase
{
    public Guid Id { get; set; }
    public DateTime CreateTime { get; set; }
    public DateTime UpdateTime { get; set; }
    public bool IsDeleted { get; set; }
}
