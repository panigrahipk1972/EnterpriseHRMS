using EmployeeEntity = EnterpriseHRMS.Domain.Entities.Employee;

namespace EnterpriseHRMS.Application.Features.Employee.Interfaces;

public interface IEmployeeRepository
{
Task<EmployeeEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

Task<EmployeeEntity?> GetByEmployeeCodeAsync(string employeeCode, CancellationToken cancellationToken = default);

Task<EmployeeEntity?> GetByEmailAsync(string email, CancellationToken cancellationToken = default);
Task<IReadOnlyList<EmployeeEntity>> GetAllAsync(CancellationToken cancellationToken = default);

Task<IReadOnlyList<EmployeeEntity>> GetByDepartmentAsync(Guid departmentId, CancellationToken cancellationToken = default);

Task AddAsync(EmployeeEntity employee, CancellationToken cancellationToken = default);

void Update(EmployeeEntity employee);

void Delete(EmployeeEntity employee);
Task<bool> ExistsByEmailAsync(
    string email,
    CancellationToken cancellationToken = default);

    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}