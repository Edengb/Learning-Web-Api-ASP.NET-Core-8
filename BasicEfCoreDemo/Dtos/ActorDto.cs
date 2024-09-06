namespace BasicEfCoreDemo.Models;

public class ActorDto
{
    public ActorDto()
    {
    }

    public ActorDto(Guid id, string name, List<MovieDto> movies)
    {
        Id = id;
        Name = name;
        Movies = movies;
    }

    public Guid Id { get; set; }
    public string Name { get; set; }
    public List<MovieDto> Movies { get; set; }
}
