using Application.Features.ProgrammingLanguages.Rules;
using Application.Features.Technologies.Rules;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Application;

public static class ApplicationServiceRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddMediatR(Assembly.GetExecutingAssembly());
        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        services.AddScoped<ProgrammingLanguageBusinessRule>();
        services.AddScoped<TechnologyBusinessRule>();

        return services;
    }
}