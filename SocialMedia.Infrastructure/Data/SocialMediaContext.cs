using Microsoft.EntityFrameworkCore;
using SocialMedia.Infrastructure.Data.Configurations;

namespace SocialMedia.Core.Entities;

public partial class SocialMediaContext : DbContext
{
    public SocialMediaContext()
    {
    }

    public SocialMediaContext(DbContextOptions<SocialMediaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Comment> Comments { get; set; }

    public virtual DbSet<Post> Posts { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Fluent API
        // se agrega la configuracion de cada una de las propiedades
        // que conforman nuestras tablas
        //modelBuilder.Entity<Comment>(entity =>
        //{

        //});

        //modelBuilder.Entity<Post>(entity =>
        //{

        //});
        
        modelBuilder.ApplyConfiguration(new PostConfiguration());
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new CommentConfiguration());

    }
}
