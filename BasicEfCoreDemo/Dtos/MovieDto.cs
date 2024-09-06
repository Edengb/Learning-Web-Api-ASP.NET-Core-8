namespace BasicEfCoreDemo.Models;

public class MovieDto
{
    public MovieDto()
    {
    }

    public MovieDto(Guid id, string title, string? description, int releaseYear)
    {
        Id = id;
        Title = title;
        Description = description;
        ReleaseYear = releaseYear;
    }

    public Guid Id { get; set; }
    public string Title { get; set; }
    public string? Description { get; set; }
    public int ReleaseYear { get; set; }
}
