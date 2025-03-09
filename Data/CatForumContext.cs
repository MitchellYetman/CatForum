using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CatForum.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace CatForum.Data
{
    public class CatForumContext : IdentityDbContext<ApplicationUser>
    {
        public CatForumContext (DbContextOptions<CatForumContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure the relationship between Comment and Discussion
            modelBuilder.Entity<Comment>()
                .HasOne(c => c.Discussion)
                .WithMany(d => d.Comments)
                .HasForeignKey(c => c.DiscussionId)
                .OnDelete(DeleteBehavior.Restrict);  // Prevent cascading delete

            // Configure the relationship between Comment and ApplicationUser (if needed)
            modelBuilder.Entity<Comment>()
                .HasOne(c => c.ApplicationUser)
                .WithMany() // Assuming one-to-many relation with ApplicationUser
                .HasForeignKey(c => c.ApplicationUserId)
                .OnDelete(DeleteBehavior.Restrict);  // Prevent cascading delete
        }

        public DbSet<CatForum.Models.Discussion> Discussion { get; set; } = default!;
        public DbSet<CatForum.Models.Comment> Comment { get; set; } = default!;
    }
}
