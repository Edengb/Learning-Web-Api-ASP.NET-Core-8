using System;
using BasicEfCoreDemo.Models;
using Microsoft.EntityFrameworkCore;

namespace BasicEfCoreDemo.Data;

public class InvoiceDbContext(DbContextOptions<InvoiceDbContext> options, IConfiguration configuration) : DbContext(options)
{
    public DbSet<Invoice> Invoices => Set<Invoice>();

    public DbSet<InvoiceItem> InvoiceItems => Set<InvoiceItem>();

    // One-to-One
    public DbSet<Contact> Contact => Set<Contact>();
    public DbSet<Address> Addresse => Set<Address>();

    // Many-to-Many
    public DbSet<Movie> Movie => Set<Movie>();
    public DbSet<Actor> Actor => Set<Actor>();


    protected override void OnModelCreating(ModelBuilder modelBuilder )
    {
        modelBuilder.Entity<Invoice>().HasData(
            new Invoice
            {
                Id = Guid.NewGuid(),
                InvoiceNumber = "INV-001",
                ContactName = "Iron Man",
                Description = "Invoice for the first month",
                Amount = 100,
                InvoiceDate = new DateTimeOffset(2023, 1, 1, 0, 0, 0, TimeSpan.Zero),
                DueDate = new DateTimeOffset(2023, 1, 15, 0, 0, 0, TimeSpan.Zero),
                Status = InvoiceStatus.AwaitPayment
            },
            new Invoice
            {
                Id = Guid.NewGuid(),
                InvoiceNumber = "INV-002",
                ContactName = "Captain America",

                Description = "Invoice for the first month",
                Amount = 200,
                InvoiceDate = new DateTimeOffset(2021, 1, 1, 0, 0, 0, TimeSpan.Zero),
                DueDate = new DateTimeOffset(2021, 1, 15, 0, 0, 0, TimeSpan.Zero),
                Status = InvoiceStatus.AwaitPayment
            },
            new Invoice
            {
                Id = Guid.NewGuid(),
                InvoiceNumber = "INV-003",
                ContactName = "Thor",
                Description = "Invoice for the first month",
                Amount = 300,
                InvoiceDate = new DateTimeOffset(2021, 1, 1, 0, 0, 0, TimeSpan.Zero),
                DueDate = new DateTimeOffset(2021, 1, 15, 0, 0, 0, TimeSpan.Zero),
                Status = InvoiceStatus.Draft
            }
        );

        // modelBuilder.Entity<Invoice>(b => {
        //     b.ToTable("Invoices");
        //     b.HasKey(i => i.Id);
        //     b.Property(p => p.Id).HasColumnName("Id");
        //     b.Property(p => p.InvoiceNumber).HasColumnName("InvoiceNumber").HasColumnType("varchar(32)").IsRequired();
        //     b.Property(p => p.ContactName).HasColumnName("ContactName").HasMaxLength(32).IsRequired();
        //     b.Property(p => p.Description).HasColumnName("Description").HasMaxLength(256);
        //     // b.Property(p => p.Amount).HasColumnName("Amount").HasColumnType("decimal(18,2)").IsRequired();
        //     b.Property(p => p.Amount).HasColumnName("Amount").HasPrecision(18, 2);
        //     b.Property(p => p.InvoiceDate).HasColumnName("InvoiceDate").HasColumnType("datetimeoffset").IsRequired();
        //     b.Property(p => p.DueDate).HasColumnName("DueDate").HasColumnType("datetimeoffset").IsRequired();
        //     b.Property(p => p.Status).HasColumnName("Status").HasMaxLength(16).HasConversion(
        //             v => v.ToString(),
        //             v => (InvoiceStatus)Enum.Parse(typeof(InvoiceStatus), v));
        // });

        // Use extension methods to configure the entity
        //modelBuilder.ConfigureInvoice();

        // Use IEntityTypeConfiguration<TEntity> interface
        // modelBuilder.ApplyConfiguration(new InvoiceConfiguration());
        //OR
        // new InvoiceConfiguration().Configure(modelBuilder.Entity<Invoice>());

        // new InvoiceItemConfiguration().Configure(modelBuilder.Entity<InvoiceItem>());
        
        // new ContactConfiguration().Configure(modelBuilder.Entity<Contact>());

        // new AddressConfiguration().Configure(modelBuilder.Entity<Address>());

        // new ActorConfiguration().Configure(modelBuilder.Entity<Actor>());

        // new MovieConfiguration().Configure(modelBuilder.Entity<Movie>());
        // Grouping the configurations       
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(InvoiceDbContext).Assembly);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"), b => b.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery));
    }


}
