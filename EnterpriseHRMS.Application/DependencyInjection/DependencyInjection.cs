using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using MediatR;
using EnterpriseHRMS.Application.Behaviors;

namespace EnterpriseHRMS.Application.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(
    this IServiceCollection services)
{
    Assembly assembly = Assembly.GetExecutingAssembly();

    services.AddMediatR(cfg =>
    {
        cfg.RegisterServicesFromAssembly(assembly);
    });

    services.AddTransient(
        typeof(IPipelineBehavior<,>),
        typeof(ValidationBehavior<,>));

    services.AddAutoMapper(assembly);

    services.AddValidatorsFromAssembly(assembly);

    return services;
}
}