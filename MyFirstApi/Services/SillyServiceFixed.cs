using System;

namespace MyFirstApi.Services;

public class SillyServiceFixed(ILogger<SillyServiceFixed> logger, Random random) : ISillyService 
{
    private readonly Guid _serviceId = Guid.NewGuid();
    private readonly string fixedContent = "Fixed Content";

    public void DoSillyThing()
    {
        logger.LogInformation($"Silly Stuff happening: {_serviceId}");
        logger.LogInformation($"Using Fixed Content: {fixedContent}");
        logger.LogInformation("Silly Stuff ending");
    }

    public string GetData()
    {
        return "This is my Silly Service Fixed Content!";
    }
}
