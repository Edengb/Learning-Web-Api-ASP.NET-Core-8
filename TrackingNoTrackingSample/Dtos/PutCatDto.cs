using System.ComponentModel.DataAnnotations;
using TrackingNoTrackingSample.Enums;

namespace TrackingNoTrackingSample.Dtos;

public record class PutCatDto
(
    Guid Id,
    string Nickname,
    [Required] int Age,
    [Required] CatBreeds Breed
);