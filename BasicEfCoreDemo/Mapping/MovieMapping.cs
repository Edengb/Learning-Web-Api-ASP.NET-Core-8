using System;
using BasicEfCoreDemo.Models;

namespace BasicEfCoreDemo.Mapping;

public static class MovieMapping
{
    public static List<MovieDto> ToDto(this List<Movie> movies)
    {
        return (List<MovieDto>)movies.Select(movie => 
        {
            return new MovieDto()
            {
                Id = movie.Id,
                Title =  movie.Title,
                Description = movie.Description,
                ReleaseYear = movie.ReleaseYear
            };
        }).ToList();
    }
}
