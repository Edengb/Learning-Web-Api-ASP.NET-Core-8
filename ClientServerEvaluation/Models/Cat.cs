using System;
using System.Collections.Generic;
using ClientServerEvaluation.Enums;

namespace ClientServerEvaluation.Models;

public partial class Cat
{
    public Guid Id { get; set; }

    public string Nickname { get; set; } = string.Empty;

    public int Age { get; set; }

    public CatBreeds Breed { get; set; }
}
