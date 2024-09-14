using System;
using TrackingNoTrackingSample.Enums;

namespace TrackingNoTrackingSample.Models;

public class Cat
{
    public Guid Id { get; set;}
    public string Nickname { get; set; } = string.Empty;
    public int Age { get; set; } = 0;
    public CatBreeds Breed { get; set; }
}
