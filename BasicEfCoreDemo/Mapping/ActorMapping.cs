using System;
using BasicEfCoreDemo.Models;

namespace BasicEfCoreDemo.Mapping;

public static class ActorMapping
{
    public static List<ActorDto> ToDto(this List<Actor> actors)
    {
        return (List<ActorDto>) actors.Select(actor => 
        {
            return new ActorDto()
            {
                Id = actor.Id,
                Name = actor.Name,
                Movies = actor.Movies.ToDto().ToList() 
            };
        }).ToList();
    }
}
