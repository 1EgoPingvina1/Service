using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using social_network.backend.Entities;
using social_network.backend.Entities.Identity;
using System.Reflection.Emit;

namespace social_network.backend.Data
{
    public class DataContext : IdentityDbContext<User,
                                                 Role,
                                                 int,
                                                 IdentityUserClaim<int>,
                                                 UserRole,
                                                 IdentityUserLogin<int>,
                                                 IdentityRoleClaim<int>,
                                                 IdentityUserToken<int>>
    {
        public DataContext(DbContextOptions options) : base(options) 
        {
        }

        public DbSet<Post> Posts {  get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Groups> Groups { get; set; }
        public DbSet<GroupMembers> GroupMembers { get; set; }
        public DbSet<PostLike> PostLikes { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Notifications> Notifications { get; set; }
        public DbSet<Photo> Photos { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<User>()
                .HasMany(ur => ur.UserRoles)
                .WithOne(u => u.User)
                .HasForeignKey(ur => ur.UserId)
                .IsRequired();

            builder.Entity<Role>()
                .HasMany(ur => ur.UserRoles)
                .WithOne(u => u.Role)
                .HasForeignKey(ur => ur.RoleId)
                 .IsRequired();

            builder.Entity<Post>(p =>
            {
                p.HasOne(p => p.User)
                .WithMany(u => u.Posts)
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Cascade);

                p.HasMany(c => c.Comments)
                .WithOne(u => u.Post)
                .HasForeignKey(f => f.PostId)
                .OnDelete(DeleteBehavior.NoAction);
            });

            builder.Entity<PostLike>(like =>
            {
                like.HasOne(pl => pl.Post)
                   .WithMany(p => p.Likes)
                   .HasForeignKey(pl => pl.PostId)
                   .OnDelete(DeleteBehavior.NoAction);

                like.HasOne(pl => pl.User)
                   .WithMany(u => u.Likes)
                   .HasForeignKey(pl => pl.UserId)
                   .OnDelete(DeleteBehavior.Cascade);
            });

        }
    }
}
