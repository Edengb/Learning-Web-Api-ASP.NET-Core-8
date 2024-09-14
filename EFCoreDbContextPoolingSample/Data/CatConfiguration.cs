using System;
using EFCoreDbContextPoolingSample.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFCoreDbContextPoolingSample.Data;

public class CatConfiguration : IEntityTypeConfiguration<Cat>
{
    public void Configure(EntityTypeBuilder<Cat> builder)
    {
        builder.ToTable("Cats");
        builder.HasKey(c => c.Id); 
        builder.Property(p => p.Id).HasColumnName("Id");
        builder.Property(p => p.Nickname).HasColumnName("Nickname").HasMaxLength(32);
        builder.Property(p => p.Age).HasColumnName("Age");
        builder.Property(p => p.Breed).HasColumnName("Breed").IsRequired();

    }
}
