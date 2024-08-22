using MyFirstApi.Services;
using System;

namespace MyFirstApi;

public static class LifeTimeServicesCollectionExtension
{
    public static IServiceCollection AddLifeTimeServices(this IServiceCollection services)
    {
        services.AddScoped<IPostService, PostsService>();
        services.AddKeyedSingleton<ISillyService, SillyServiceRandom>("SillyServiceRandom");
        services.AddKeyedTransient<ISillyService, SillyServiceFixed>("SillyServiceFixedContent");
        services.AddSingleton<ISillyService, SillyService>();
        services.AddSingleton<Random, Random>();
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        return services;
    }
}
