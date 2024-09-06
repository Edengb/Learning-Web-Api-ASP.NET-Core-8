using System;
using System.Text.Json.Serialization;

namespace BasicEfCoreDemo.Models;

public class Actor
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public List<Movie> Movies { get; set; } = new List<Movie>();
    [JsonIgnore]
    public List<MovieActor> MovieActors { get; set; } = new();
}
