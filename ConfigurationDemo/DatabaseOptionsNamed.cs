using System;

namespace ConfigurationDemo;

public class DatabaseOptionsNamed
{
    public const string SectioName = "Databases";
    public const string SystemDatabaseSectionName = "System";
    public const string BusinessDatabaseSectionName = "Business";
    public string Type { get; set; } = string.Empty;
    public string ConnectionString { get; set; } = string.Empty;
}
