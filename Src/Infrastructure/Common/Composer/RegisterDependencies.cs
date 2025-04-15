using Domain.Interfaces;
using Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Common.Composer;

public static class RegisterDependencies
{
    public static IServiceCollection RegisterInfrastructure(this IServiceCollection services)
    {
        services.AddSingleton<MyRepository>();
        services.AddSingleton<IRepository<object>, MyRepository>(x => x.GetRequiredService<MyRepository>());
        services.AddSingleton<IInMemoryMeetingRepository, InMemoryMeetingRepository>();
        return services;
    }
}