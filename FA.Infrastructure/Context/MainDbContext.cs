using FA.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;



namespace FA.Infrastructure.Context;

public class MainDbContext : DbContext
{
    public DbSet<Blog> Blogs { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Post> Posts { get; set; }
    public DbSet<Tag> Tags { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<User> Users { get; set; }

    Guid? _id;
    public MainDbContext(DbContextOptions<MainDbContext> options, IHttpContextAccessor httpContextAccessor) : base(options)
    {
        this.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        if (Guid.TryParse(httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value, out Guid id))
        {
            _id = id;
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasMany(user => user.UsersICreated)
            .WithOne(entity => entity.Creator)
            .HasForeignKey(entity => entity.CreatorId);

        modelBuilder.Entity<User>()
            .HasMany(user => user.UsersIUpdated)
            .WithOne(entity => entity.Updator)
            .HasForeignKey(entity => entity.UpdatorId);

        modelBuilder.Entity<User>()
            .HasMany(user => user.BlogsICreated)
            .WithOne(entity => entity.Creator)
            .HasForeignKey(entity => entity.CreatorId);

        modelBuilder.Entity<User>()
            .HasMany(user => user.BlogsIUpdated)
            .WithOne(entity => entity.Updator)
            .HasForeignKey(entity => entity.UpdatorId);

        modelBuilder.Entity<User>()
            .HasMany(user => user.CategoriesICreated)
            .WithOne(entity => entity.Creator)
            .HasForeignKey(entity => entity.CreatorId);

        modelBuilder.Entity<User>()
            .HasMany(user => user.CategoriesIUpdated)
            .WithOne(entity => entity.Updator)
            .HasForeignKey(entity => entity.UpdatorId);

        modelBuilder.Entity<User>()
            .HasMany(user => user.PostsICreated)
            .WithOne(entity => entity.Creator)
            .HasForeignKey(entity => entity.CreatorId);

        modelBuilder.Entity<User>()
            .HasMany(user => user.PostsIUpdated)
            .WithOne(entity => entity.Updator)
            .HasForeignKey(entity => entity.UpdatorId);

        modelBuilder.Entity<User>()
            .HasMany(user => user.TagsICreated)
            .WithOne(entity => entity.Creator)
            .HasForeignKey(entity => entity.CreatorId);

        modelBuilder.Entity<User>()
            .HasMany(user => user.TagsIUpdated)
            .WithOne(entity => entity.Updator)
            .HasForeignKey(entity => entity.UpdatorId);

        modelBuilder.Entity<User>()
            .HasMany(user => user.CommentsICreated)
            .WithOne(entity => entity.Creator)
            .HasForeignKey(entity => entity.CreatorId);

        modelBuilder.Entity<User>()
            .HasMany(user => user.CommentsIUpdated)
            .WithOne(entity => entity.Updator)
            .HasForeignKey(entity => entity.UpdatorId)
            .OnDelete(DeleteBehavior.SetNull);



        // One Blog has many Posts
        modelBuilder.Entity<Blog>().HasMany(b => b.Posts).WithOne(p => p.Blog).HasForeignKey(p => p.BlogId);

        // One Category has many Posts
        modelBuilder.Entity<Category>().HasMany(c => c.Posts).WithOne(p => p.Category).HasForeignKey(p => p.CategoryId);

        // Many to Many between Post and Tag
        modelBuilder.Entity<PostTag>().HasKey(pt => new { pt.PostId, pt.TagId });
        modelBuilder.Entity<Post>().HasMany(p => p.Tags).WithMany(t => t.Posts)
        .UsingEntity<PostTag>(
            j => j.HasOne(pt => pt.Tag).WithMany(t => t.PostTags).HasForeignKey(pt => pt.TagId),
            j => j.HasOne(pt => pt.Post).WithMany(p => p.PostTags).HasForeignKey(pt => pt.PostId)
        );

        // One Post has many Comments
        modelBuilder.Entity<Post>().HasMany(p => p.Comments).WithOne(c => c.Post).HasForeignKey(c => c.PostId);

        foreach (var foreignKey in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
        {
            foreignKey.DeleteBehavior = DeleteBehavior.NoAction;
        }
    }

    public override int SaveChanges(bool acceptAllChangesOnSuccess)
    {
        AddMetadata();
        return base.SaveChanges(acceptAllChangesOnSuccess);
    }

    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
    {
        AddMetadata();
        return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }

    private void AddMetadata()
    {
        var entries = ChangeTracker.Entries<BaseEntity>();

        // if authorized: trust the above layer that they have privelege to do tehe data manupilation.
        if (_id != null)
        {
            foreach (var entry in entries)
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedAt = DateTime.UtcNow;
                    entry.Entity.CreatorId = _id.Value; // they are the creator
                    entry.Entity.UpdatedAt = DateTime.UtcNow;
                    entry.Entity.UpdatorId = _id.Value; // and updator 
                }
                else if (entry.State == EntityState.Modified)
                {
                    entry.Entity.UpdatedAt = DateTime.UtcNow;
                    entry.Entity.UpdatorId = _id.Value; // same
                }
            }
        }

        // else if unauthorized, only action allowed is create new User record
        else
        {
            if (!entries.All(e => e.Entity is User && e.State == EntityState.Added))
            {
                throw new InvalidOperationException("Found non-User type record being modified unauthorizly");
            }

            foreach (var user in entries)
            {
                Guid id = Guid.NewGuid();
                user.Entity.Id = id; // set the id manually
                user.Entity.CreatedAt = DateTime.UtcNow;
                user.Entity.CreatorId = id; // the registered user will have the creator is themselves
                user.Entity.UpdatedAt = DateTime.UtcNow;
                user.Entity.UpdatorId = id; // the registered user will have the updator is themselves
            }
        }
    }
}
