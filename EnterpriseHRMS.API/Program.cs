using EnterpriseHRMS.Application.DependencyInjection;
using EnterpriseHRMS.Infrastructure.DependencyInjection;
using EnterpriseHRMS.Infrastructure.Persistence.Context;
using EnterpriseHRMS.Infrastructure.Persistence.Seed;
using EnterpriseHRMS.Application.Features.Employee.Mappings;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddAutoMapper(typeof(EmployeeProfile));


var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

    await DatabaseSeeder.SeedAsync(context);
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();