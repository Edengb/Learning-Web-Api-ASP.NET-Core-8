using System;
using BasicEfCoreDemo.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BasicEfCoreDemo.Data;

public class ActorConfiguration : IEntityTypeConfiguration<Actor>
{
    public void Configure(EntityTypeBuilder<Actor> builder)
    {
        builder.ToTable("Actors");
        builder.HasKey(a => a.Id);
        builder.Property(p => p.Name).HasColumnName("Name").HasMaxLength(32).IsRequired();
        builder.HasIndex(p => p.Name).IsUnique();
    }
}