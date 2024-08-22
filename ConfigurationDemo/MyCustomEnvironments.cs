using System;

namespace ConfigurationDemo;

public static class MyCustomEnvironments
{
    public static bool IsMyCustomHawkTuah(this IWebHostEnvironment environment)
    {
        return environment.EnvironmentName == "MyCustomHawkTuah";
    }
}
