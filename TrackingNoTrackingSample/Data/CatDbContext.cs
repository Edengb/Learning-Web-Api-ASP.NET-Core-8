using System;
using Microsoft.EntityFrameworkCore;
using TrackingNoTrackingSample.Models;

namespace TrackingNoTrackingSample.Data;

public class CatDbContext(DbContextOptions<CatDbContext> options, IConfiguration configuration) : DbContext(options)
{
    public DbSet<Cat> Cats => Set<Cat>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseSqlServer(configuration["Database:ConnectionString"]);
    }
}
