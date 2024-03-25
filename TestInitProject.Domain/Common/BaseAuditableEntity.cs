namespace TestInitProject.Domain.Common;

public abstract class BaseAuditableEntity<T> : BaseEntity<T>
{
    protected BaseAuditableEntity(T id) : base(id)
    {
    }

    public DateTimeOffset Created { get; set; }

    public T? CreatedBy { get; set; }

    public DateTimeOffset LastModified { get; set; }
    
    public T? LastModifiedBy { get; set; } 
}