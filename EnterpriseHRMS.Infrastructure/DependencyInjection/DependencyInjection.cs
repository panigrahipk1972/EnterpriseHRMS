using EnterpriseHRMS.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using EnterpriseHRMS.Application.Features.Employee.Interfaces;
using EnterpriseHRMS.Infrastructure.Persistence.Repositories;
using EnterpriseHRMS.Application.Features.Department.Interfaces;

namespace EnterpriseHRMS.Infrastructure.DependencyInjection;

public static class DependencyInjection
{
   public static IServiceCollection AddInfrastructure(
    this IServiceCollection services,
    IConfiguration configuration)
{
    services.AddDbContext<ApplicationDbContext>(options =>
        options.UseSqlServer(
            configuration.GetConnectionString("DefaultConnection")));

    //services.AddScoped<IApplicationDbContext>(provider =>
      //  provider.GetRequiredService<ApplicationDbContext>());

    services.AddScoped<IEmployeeRepository, EmployeeRepository>();

    services.AddScoped<IDepartmentRepository, DepartmentRepository>();

    return services;
}
}