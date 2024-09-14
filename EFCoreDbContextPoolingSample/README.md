# Entity Framework Core - Using DbContext Pooling

### DbContext pooling
>A DbContext is generally a light object: creating and disposing one doesn't involve a database operation, and most applications can do so without any noticeable impact on performance. However, each context instance does set up various internal services and objects necessary for performing its duties, and the overhead of continuously doing so may be significant in high-performance scenarios. For these cases, EF Core can pool your context instances: when you dispose your context, EF Core resets its state and stores it in an internal pool; when a new instance is next requested, that pooled instance is returned instead of setting up a new one. Context pooling allows you to pay context setup costs only once at program startup, rather than continuously.

Reference: [Learn Microsoft - DbContext pooling](https://learn.microsoft.com/en-us/ef/core/performance/advanced-performance-topics?tabs=with-di%2Cexpression-api-with-constant#dbcontext-pooling)


#### My Notes about lifecycle:

- The lifecycle of DbContext in EF Core is scoped by default
- The instances in the pool still have scoped lifecycle.
- The instances in the pool are reused across requests in a scoped manner (per request).
- The pool itself (the mechanism managing the reuse of DbContext instances) might be singleton, but the individual DbContext instances drawn from the pool are still scoped to each request.

### Managing state in pooled contexts

> Context pooling works by reusing the same context instance across requests; this means that it's effectively registered as a Singleton, and the same instance is reused across multiple requests (or DI scopes). This means that special care must be taken when the context involves any state that may change between requests. Crucially, the context's OnConfiguring is only invoked once - when the instance context is first created - and so cannot be used to set state which needs to vary (e.g. a tenant ID).

Reference: [Learn Microsoft - Managing state in pooled contexts](https://learn.microsoft.com/en-us/ef/core/performance/advanced-performance-topics?tabs=with-di%2Cexpression-api-with-constant#managing-state-in-pooled-contexts)
<br/><br/>
## Steps taken


First, create a minimal API for demo purposes:
```
dotnet new webapi -n EFCoreDbContextPoolingSample --use-minal-apis
```

Add the dependencies used in this demo:

```
dotnet add package Microsoft.EntityFrameworkCore.SqlServer --version 8.0.8

dotnet add package Microsoft.EntityFrameworkCore.Tools --version 8.0.8 
```

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

Finally, register DbContext pooling in the DI container using AddDbContextPool, and create a GET operation to list your entities:

```
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContextPool<YourEntityDbContext>(options =>
        options.UseSqlServer("StringConnection"));
```

```
app.MapGet("/api/yourEntities", async (YourEntityContext dbContext) =>
{
    return await dbContext.YourEntities
            .AsNoTracking()
            .ToListAsync();
});

```

## Testing

Run the .NET application and use K6 for performance testing:

```
    dotnet run
```
```
    cd k6
    k6 run script.js
```
Reference: [K6 Installation](https://grafana.com/docs/k6/latest/testing-guides/injecting-faults-with-xk6-disruptor/installation/)

Restart the application and replace AddDbContextPool with AddDbContext, then validate the results.

When you run the <b>dotnet run</b> command, your application is in development mode. It is not optimized for performance, but you should already observe an improvement in performance by using AddDbContextPool.

