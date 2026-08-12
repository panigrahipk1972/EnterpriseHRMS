using EnterpriseHRMS.Application.Features.Department.Interfaces;
using DepartmentEntity = EnterpriseHRMS.Domain.Entities.Department;
using EnterpriseHRMS.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace EnterpriseHRMS.Infrastructure.Persistence.Repositories;

public sealed class DepartmentRepository : IDepartmentRepository
{
    private readonly ApplicationDbContext _context;

    public DepartmentRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IReadOnlyList<DepartmentEntity>> GetAllAsync(
        CancellationToken cancellationToken = default)
    {
        return await _context.Departments
            .AsNoTracking()
            .OrderBy(x => x.Name)
            .ToListAsync(cancellationToken);
    }
}