using DepartmentEntity = EnterpriseHRMS.Domain.Entities.Department;

namespace EnterpriseHRMS.Application.Features.Department.Interfaces;

public interface IDepartmentRepository
{
    Task<IReadOnlyList<DepartmentEntity>> GetAllAsync(
        CancellationToken cancellationToken = default);
}