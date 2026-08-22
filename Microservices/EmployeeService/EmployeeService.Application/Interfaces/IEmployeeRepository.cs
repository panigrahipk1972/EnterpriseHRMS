using EmployeeService.Domain.Entities;

namespace EmployeeService.Application.Interfaces;

public interface IEmployeeRepository
{
    Task<Employee> CreateAsync(Employee employee);

    Task<Employee?> GetByIdAsync(int id);

    Task<List<Employee>> GetAllAsync();
}