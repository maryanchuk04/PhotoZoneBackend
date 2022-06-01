using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PhotoZone.Entities;
using PhotoZone.Services;

namespace PhotoZone.EF;

public class AppDbContext : DbContext
{
    private readonly ISecurityContext _securityContext;
    private readonly IConfiguration Configuration;
    public AppDbContext(DbContextOptions<AppDbContext> options,IConfiguration configuration ,ISecurityContext securityContext): base(options)
    {
        _securityContext = securityContext;
        Configuration = configuration;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }



    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Place> Places { get; set; }

    public virtual DbSet<Images> Images { get; set; }

    public DbSet<Subscribers> Subscribes { get; set; }

    public DbSet<Location> Locations { get; set; }

    public DbSet<Subscribtions> Subscribtions { get; set; }

    public DbSet<Comment> Comments { get; set; }

    public override int SaveChanges(bool acceptAllChangesOnSuccess)
    {

        return base.SaveChanges(acceptAllChangesOnSuccess);
    }

    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
    {
        return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }

}