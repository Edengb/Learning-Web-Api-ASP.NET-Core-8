using System;

namespace MyFirstApi.Services;

public class SillyService(ILogger<SillyService> logger) : ISillyService 
{
    private readonly Guid _serviceId = Guid.NewGuid();

    public void DoSillyThing()
    {
        logger.LogInformation($"Silly Stuff happening: {_serviceId}");
        logger.LogInformation("Silly Stuff ending");
    }

    public string GetData()
    {
        return "This is my Silly Service!";
    }
}
