using System;
using BasicEfCoreDemo.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BasicEfCoreDemo.Data;

public class AddressConfiguration : IEntityTypeConfiguration<Address>
{
    public void Configure(EntityTypeBuilder<Address> builder)
    {
        builder.ToTable("Addresses");
        builder.HasKey(a => a.Id);
        builder.Property(a => a.Street).IsRequired();
        builder.Property(a => a.City).IsRequired();
        builder.Property(a => a.State).IsRequired();
        builder.Property(a => a.ZipCode).IsRequired();
        builder.Property(a => a.Country);
        builder.Property(a => a.ContactId);
        builder.Ignore(a => a.Contact);
        builder.HasOne(a => a.Contact)
            .WithOne(c => c.Address)
            .HasForeignKey<Address>(a => a.ContactId);
    }
}
