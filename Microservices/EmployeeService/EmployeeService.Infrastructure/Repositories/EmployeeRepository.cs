using EmployeeService.Application.Interfaces;
using EmployeeService.Domain.Entities;
using EmployeeService.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace EmployeeService.Infrastructure.Repositories;

public class EmployeeRepository : IEmployeeRepository
{
    private readonly ApplicationDbContext _context;

    public EmployeeRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Employee> CreateAsync(Employee employee)
    {
        await _context.Employees.AddAsync(employee);
        await _context.SaveChangesAsync();

        return employee;
    }

    public async Task<Employee?> GetByIdAsync(int id)
    {
        return await _context.Employees
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.Id == id);
    }

    public async Task<List<Employee>> GetAllAsync()
    {
        return await _context.Employees
            .AsNoTracking()
            .ToListAsync();
    }
}