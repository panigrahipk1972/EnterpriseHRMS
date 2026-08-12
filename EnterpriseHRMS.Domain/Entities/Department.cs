using EnterpriseHRMS.Domain.Common;
using EnterpriseHRMS.Domain.Exceptions;

namespace EnterpriseHRMS.Domain.Entities;

public class Department : BaseAuditableEntity
{
    private Department()
    {
        // Required by EF Core
    }

    public Department(
        string departmentCode,
        string name,
        string? description)
    {
        if (string.IsNullOrWhiteSpace(departmentCode))
            throw new DomainException("Department code is required.");

        if (string.IsNullOrWhiteSpace(name))
            throw new DomainException("Department name is required.");

        DepartmentCode = departmentCode;
        Name = name;
        Description = description;
        IsActive = true;
    }

    public string DepartmentCode { get; private set; } = string.Empty;

    public string Name { get; private set; } = string.Empty;

    public string? Description { get; private set; }

    public bool IsActive { get; private set; }

    // One Department -> Many Employees
    public virtual ICollection<Employee> Employees { get; private set; }
        = new List<Employee>();

    public void Activate() => IsActive = true;

    public void Deactivate() => IsActive = false;
}