using System;

namespace MyFirstApi.Services;

public class SillyServiceRandom(ILogger<SillyServiceRandom> logger, Random random) : ISillyService 
{
    private readonly Guid _serviceId = Guid.NewGuid();

    public void DoSillyThing()
    {
        logger.LogInformation($"Silly Stuff happening: {_serviceId}");
        logger.LogInformation($"Using Random Range[1, 100]: {random.Next(1, 100)}");
        logger.LogInformation("Silly Stuff ending");
    }

    public string GetData()
    {
        return "This is my Silly Service Random!";
    }
}
