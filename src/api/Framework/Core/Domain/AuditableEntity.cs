using Skittles.Framework.Core.Domain.Contracts;

namespace Skittles.Framework.Core.Domain;

public abstract class AuditableEntity : BaseEntity, IAuditable
{
    public DateTimeOffset Created { get; set; }
    public Guid CreatedBy { get; set; }
    public DateTimeOffset? LastModified { get; set; }
    public Guid? LastModifiedBy { get; set; }
}
