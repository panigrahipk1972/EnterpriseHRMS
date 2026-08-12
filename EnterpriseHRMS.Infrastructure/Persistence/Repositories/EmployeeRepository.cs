using EnterpriseHRMS.Application.Features.Employee.Interfaces;
using EnterpriseHRMS.Domain.Entities;
using EnterpriseHRMS.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace EnterpriseHRMS.Infrastructure.Persistence.Repositories;
public sealed class EmployeeRepository : IEmployeeRepository
{
    private readonly ApplicationDbContext _context;

    public EmployeeRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Employee?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        return await _context.Employees
            .Include(x => x.Department)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<Employee?> GetByEmployeeCodeAsync(
        string employeeCode,
        CancellationToken cancellationToken = default)
    {
        return await _context.Employees
            .FirstOrDefaultAsync(
                x => x.EmployeeCode == employeeCode,
                cancellationToken);
    }

    public async Task<Employee?> GetByEmailAsync(
        string email,
        CancellationToken cancellationToken = default)
    {
        return await _context.Employees
            .FirstOrDefaultAsync(
                x => x.Email == email,
                cancellationToken);
    }

    public async Task<IReadOnlyList<Employee>> GetAllAsync(
        CancellationToken cancellationToken = default)
    {
        return await _context.Employees
            .AsNoTracking()
            .OrderBy(x => x.FirstName)
            .ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<Employee>> GetByDepartmentAsync(
        Guid departmentId,
        CancellationToken cancellationToken = default)
    {
        return await _context.Employees
            .Where(x => x.DepartmentId == departmentId)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<bool> ExistsByEmailAsync(
        string email,
        CancellationToken cancellationToken = default)
    {
        return await _context.Employees
            .AnyAsync(x => x.Email == email, cancellationToken);
    }

    public async Task AddAsync(
        Employee employee,
        CancellationToken cancellationToken = default)
    {
        await _context.Employees.AddAsync(employee, cancellationToken);
    }

    public void Update(Employee employee)
    {
        _context.Employees.Update(employee);
    }

    public void Delete(Employee employee)
    {
        _context.Employees.Remove(employee);
    }

    public async Task SaveChangesAsync(
        CancellationToken cancellationToken = default)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }
}