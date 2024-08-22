using System;

namespace ConfigurationDemo;

public static class OptionsCollectionExtensions
{
    public static IServiceCollection AddConfig(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<DatabaseOption>(configuration.GetSection(DatabaseOption.SectioName));
        services.Configure<DatabaseOptionsNamed>(DatabaseOptionsNamed.SystemDatabaseSectionName, configuration.GetSection($"{DatabaseOptionsNamed.SectioName}:{DatabaseOptionsNamed.SystemDatabaseSectionName}"));
        services.Configure<DatabaseOptionsNamed>(DatabaseOptionsNamed.BusinessDatabaseSectionName, configuration.GetSection($"{DatabaseOptionsNamed.SectioName}:{DatabaseOptionsNamed.BusinessDatabaseSectionName}"));
        return services;
    }
}
