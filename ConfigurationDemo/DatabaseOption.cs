using System;

namespace ConfigurationDemo;

public class DatabaseOption
{
    public const string SectioName = "Database";
    public string Type { get; set; } = string.Empty;
    public string ConnectionString { get; set; } = string.Empty;
}
