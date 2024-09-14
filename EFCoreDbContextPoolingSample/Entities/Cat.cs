using System;

namespace EFCoreDbContextPoolingSample.Entities;

public class Cat
{
    public Guid Id { get; set;}
    public string Nickname { get; set; } = string.Empty;
    public int Age { get; set; } = 0;
    public CatBreeds Breed { get; set; }
}
