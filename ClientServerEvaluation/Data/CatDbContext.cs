using System;
using System.Collections.Generic;
using ClientServerEvaluation.Models;
using Microsoft.EntityFrameworkCore;

namespace ClientServerEvaluation.Data;

public partial class CatDbContext(DbContextOptions<CatDbContext> options, IConfiguration configuration) : DbContext(options)
{


    public virtual DbSet<Cat> Cats { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer(configuration["Database:ConnectionString"]);

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cat>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
