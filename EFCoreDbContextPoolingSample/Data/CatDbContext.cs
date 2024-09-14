using System;
using EFCoreDbContextPoolingSample.Entities;
using Microsoft.EntityFrameworkCore;

namespace EFCoreDbContextPoolingSample.Data;

public class CatDbContext(DbContextOptions<CatDbContext> options) : DbContext(options)
{
    public DbSet<Cat> Cats => Set<Cat>();
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cat>().HasData(
            new Cat
            {
                Id = Guid.NewGuid(),
                Nickname = "Miau",
                Age = 12,
                Breed = CatBreeds.Balinese
            },
            new Cat
            {
                Id = Guid.NewGuid(),
                Nickname = "Garfield",
                Age = 2,
                Breed = CatBreeds.Balinese
            },
            new Cat
            {
                Id = Guid.NewGuid(),
                Nickname = "Bilu",
                Age = 5,
                Breed = CatBreeds.Toyger
            },
            new Cat
            {
                Id = Guid.NewGuid(),
                Nickname = "Nanan",
                Age = 1,
                Breed = CatBreeds.Chausie
            },
            new Cat
            {
                Id = Guid.NewGuid(),
                Nickname = "Ghraarrr",
                Age = 7,
                Breed = CatBreeds.Donskoy
            },
            new Cat
            {
                Id = Guid.NewGuid(),
                Nickname = "Poof",
                Age = 4,
                Breed = CatBreeds.Abyssinian
            },
            new Cat
            {
                Id = Guid.NewGuid(),
                Nickname = "Chorão",
                Age = 8,
                Breed = CatBreeds.Toyger
            },
            new Cat
            {
                Id = Guid.NewGuid(),
                Nickname = "Briguento",
                Age = 9,
                Breed = CatBreeds.Donskoy
            },
            new Cat
            {
                Id = Guid.NewGuid(),
                Nickname = "Miau Safado",
                Age = 6,
                Breed = CatBreeds.Donskoy
            },
            new Cat
            {
                Id = Guid.NewGuid(),
                Nickname = "Desconhecido",
                Age = 1,
                Breed = CatBreeds.Donskoy
            },
            new Cat
            {
                Id = Guid.NewGuid(),
                Nickname = "Desconhecido",
                Age = 4,
                Breed = CatBreeds.Toyger
            },
            new Cat
            {
                Id = Guid.NewGuid(),
                Nickname = "Desconhecido",
                Age = 1,
                Breed = CatBreeds.Abyssinian
            },
            new Cat
            {
                Id = Guid.NewGuid(),
                Nickname = "Desconhecido",
                Age = 1,
                Breed = CatBreeds.Toyger
            },
            new Cat
            {
                Id = Guid.NewGuid(),
                Nickname = "Desconhecido",
                Age = 4,
                Breed = CatBreeds.Chausie
            },
            new Cat
            {
                Id = Guid.NewGuid(),
                Nickname = "Desconhecido",
                Age = 6,
                Breed = CatBreeds.Balinese
            }
        );
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CatDbContext).Assembly);
    }
}
