using System;
using System.Collections.Generic;
using ProcedureAndQueriesSample.Enum;

namespace ProcedureAndQueriesSample.Models;

public partial class Cat
{
    public Guid Id { get; set; }

    public string Nickname { get; set; } = null!;

    public int Age { get; set; }

    public CatBreeds Breed { get; set; }
}
