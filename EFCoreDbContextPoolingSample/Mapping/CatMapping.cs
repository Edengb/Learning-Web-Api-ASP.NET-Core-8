using System;
using EFCoreDbContextPoolingSample.Dtos;
using EFCoreDbContextPoolingSample.Entities;

namespace EFCoreDbContextPoolingSample.Mapping;

public static class CatMapping
{
    public static CatDto ToCatDto(this Cat cat)
    {
        return new(
            cat.Nickname,
            cat.Age,
            cat.Breed.ToString()
        );
    }
}
