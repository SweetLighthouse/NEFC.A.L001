using FA.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FA.Infrastructure.Context;

public class MainDbContext : DbContext
{
    public DbSet<Blog> Blogs { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Post> Posts { get; set; }
    public DbSet<Tag> Tags { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<User> Users { get; set; }

    public MainDbContext(DbContextOptions<MainDbContext> options) : base(options) 
    {
        this.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Blog>().ToTable(nameof(Blog));
        modelBuilder.Entity<Category>().ToTable(nameof(Category));
        modelBuilder.Entity<Post>().ToTable(nameof(Post));
        modelBuilder.Entity<Tag>().ToTable(nameof(Tag));
        modelBuilder.Entity<Comment>().ToTable(nameof(Comment));
        modelBuilder.Entity<User>().ToTable(nameof(User));
    }

    public override int SaveChanges()
    {
        AddTimestamps();
        return base.SaveChanges();
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        AddTimestamps();
        return base.SaveChangesAsync(cancellationToken);
    }

    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
    {
        AddTimestamps();
        return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }

    //private void AddTimestamps()
    //{
    //    var entities = ChangeTracker.Entries()
    //        .Where(x => x.Entity is BaseEntity && (x.State == EntityState.Added || x.State == EntityState.Modified));

    //    foreach (var entity in entities)
    //    {
    //        var now = DateTime.UtcNow; // current datetime

    //        if (entity.State == EntityState.Added)
    //        {
    //            ((BaseEntity)entity.Entity).CreatedAt = now;
    //        }
    //        ((BaseEntity)entity.Entity).UpdatedAt = now;
    //    }
    //}

    private void AddTimestamps()
    {
        var entries = ChangeTracker.Entries<BaseEntity>();
        foreach (var entry in entries)
        {
            if (entry.State == EntityState.Added)
            {
                entry.Entity.Id = Guid.NewGuid();
                entry.Entity.CreatedAt = DateTime.UtcNow;
                entry.Entity.UpdatedAt = DateTime.UtcNow;
            }
            else if (entry.State == EntityState.Modified)
            {
                entry.Entity.UpdatedAt = DateTime.UtcNow;
            }
        }
    }
}
