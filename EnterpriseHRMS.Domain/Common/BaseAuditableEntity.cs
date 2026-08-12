namespace EnterpriseHRMS.Domain.Common;

public abstract class BaseAuditableEntity : BaseEntity
{
    public DateTimeOffset CreatedOn { get; set; }

    public string CreatedBy { get; set; } = string.Empty;

    public DateTimeOffset? ModifiedOn { get; set; }

    public string? ModifiedBy { get; set; }
}