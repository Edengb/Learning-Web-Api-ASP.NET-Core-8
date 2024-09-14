# Entity Framework Core - tracking versus no-tracking queries

Beyond general ORM functionalities, EF Core includes a tracking feature, which is a significant benefit as it allows tracking changes to entities and generating the appropriate SQL queries to update the database. When retrieving an entity that was previously returned by the database, EF Core will, by default, return the entity from the context instead of making another query to the database. This avoids unnecessary database operations.


### Microsoft Tracking vs. No-Tracking Queries
>Tracking behavior controls if Entity Framework Core keeps information about an entity instance in its change tracker. If an entity is tracked, any changes detected in the entity are persisted to the database during SaveChanges. EF Core also fixes up navigation properties between the entities in a tracking query result and the entities that are in the change tracker.

>Keyless entity types are never tracked. Wherever this article mentions entity types, it refers to entity types which have a key defined.


Reference: [Tracking vs No-Tracking](https://learn.microsoft.com/en-us/ef/core/querying/tracking)


### My demo samples

* GET tests/tracking/first-find-single:
        Methods like FirstAsync and SingleAsync always make a request to the database, regardless of the entity's state. However, the FindAsync method will return the tracked entity if it was previously retrieved.



* GET tests/no-tracking/first-find-single:

        Using AsNoTracking() forces no-tracking behavior. Since there is no tracked entity, FindAsync will make a request to the database.

        You can also modify the default behavior in the DbContext's OnConfiguration method.

* GET tracking-demo/outdated-cat:

        The ToListAsync() method has the same behavior than FindAsync(), it won't make request to database if it is already retrieved.

        Take care of update operations in the moment of retrieving because it can get outdated entity.

        The ToListAsync() method behaves like FindAsync() —it will not make a database request if the entity has already been retrieved.

        Be careful when updating entities after retrieval, as you might get an outdated entity.

* GET no-tracking-demo/updated-cat:

        Using the AsNoTracking() method while retrieving the entity ensures that a database request is made, fetching the most up-to-date data.

        Disabling tracking can enhance performance and save memory, making it very useful for read-only operations.

* GET tracking-demo/outdated-new-cat

        This demonstrates retrieving data from the database, deleting it, modifying it in memory, and attempting to save it. If you call SaveChangesAsync() on an entity that no longer exists, it will result in an EF Core update exception. To resolve this, modify the entity's state to "added" before calling SaveChangesAsync() for a successful insert.

* GET states-demo/attached-cat:

        Here, you can observe entity states during retrieval and manage them. This example attaches an entity that was retrieved using AsNoTracking().

* GET states-demo/detached-cat

        This is the reverse of the previous example. It detaches an entity that was initially retrieved with tracking enabled.


## Initial steps taken

First, create a Web API with a controller:
```
dotnet new webapi -n TrackingNoTrackingSample -controllers

```

Add the dependencies used in this demo:

```
dotnet add package Microsoft.EntityFrameworkCore.SqlServer --version 8.0.8

dotnet add package Microsoft.EntityFrameworkCore.Tools --version 8.0.8 

dotnet add package Microsoft.VisualStudio.Web.CodeGeneration.Design --version 8.0.5

dotnet tool install -g dotnet-aspnet-codegenerator
```

(The last one is installed globally.)

Next, define an entity. Create the folder .\Entities and the YourEntity class:

```
public class YourEntity
{
    public Guid Id { get; set;}
    add other properties ...
}
```
Use Fluent API for the table definition. Create the folder .\Data and the YourEntityConfiguration class. This class inherits from IEntityTypeConfiguration<T>:

```
public class YourEntityConfiguration : IEntityTypeConfiguration<YourEntity>
{
    public void Configura(EntityTypeBuilder<YourEntity> builder)
    {
        builder.ToTable("YourEntitiesTableName);
        builder.HasKey(e => e.Id );
        builder.Property(p => p.YourProperty).IsRequired();
    }
}
```
Reference: [Fluent API Configuration](https://www.learnentityframeworkcore.com/configuration/fluent-api)



Then, create the DbContext class in the same folder:

```
public class EntityNameDbContext(DbContextOptions<EntityNameDbContext> options) : Dbcontext(options)
{
    public DbSet<YourEntity> YourEntities => Set<YourEntity>();
}
```

Your EntityNameDbContext class should be derived from DbContext. You can override the OnModelCreating method to seed data and group the Fluent API configurations:


```
   protected override OnModelCreating(ModelBuilder modelBuilder)
   {
        modelBuilder.Entity<YourEntity>().HasData(
            new YourEntity
            {
                ...
            }
        )
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(EntityNameDbContext).Assembly);
   }
```

Finally, register DbContext in the DI container using AddDbContext:

```
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<EntityDbContext>(options => options.UseSqlServer(configuration["Database:ConnectionString"]));
```
