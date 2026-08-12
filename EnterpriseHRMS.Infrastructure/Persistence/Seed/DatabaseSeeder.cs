using EnterpriseHRMS.Domain.Entities;
using EnterpriseHRMS.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace EnterpriseHRMS.Infrastructure.Persistence.Seed;

public static class DatabaseSeeder
{
    public static async Task SeedAsync(ApplicationDbContext context)
    {
        if (await context.Departments.AnyAsync())
            return;

        var departments = new List<Department>
        {
            new("HR", "Human Resources", "Human Resources Department"),
            new("IT", "Information Technology", "IT Department"),
            new("FIN", "Finance", "Finance Department"),
            new("ADM", "Administration", "Administration Department"),
            new("SAL", "Sales", "Sales Department")
        };

        await context.Departments.AddRangeAsync(departments);
        await context.SaveChangesAsync();
    }
}