using Rookie.AssetManagement.DataAccessor.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using Rookie.AssetManagement.DataAccessor.Data.Seeds;

namespace Rookie.AssetManagement.DataAccessor.Data
{
    public class ApplicationDbContext : IdentityDbContext<User, IdentityRole<int>, int>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<User>(entity =>
            {
                entity.ToTable(name: "Users");
            });

            builder.Entity<IdentityRole<int>>(entity =>
            {
                entity.ToTable(name: "Roles");
            });

            builder.Entity<IdentityUserRole<int>>(entity =>
            {
                entity.ToTable("UserRoles");
            });

            builder.Entity<IdentityUserClaim<int>>(entity =>
            {
                entity.ToTable("UserClaims");
            });

            builder.Entity<IdentityUserLogin<int>>(entity =>
            {
                entity.ToTable("UserLogins");
            });

            builder.Entity<IdentityRoleClaim<int>>(entity =>
            {
                entity.ToTable("RoleClaims");
            });

            builder.Entity<IdentityUserToken<int>>(entity =>
            {
                entity.ToTable("UserTokens");
            });

            builder.Entity<Category>(entity =>
            {
                entity.ToTable("Categories");

                entity.Property(c => c.Name).IsRequired(true);
                entity.Property(c => c.Prefix).IsRequired(true);

                //entity.HasData(DefaultCategories.SeedCategories());
            });

            builder.Entity<State>(entity =>
            {
                entity.ToTable("States");

                entity.Property(c => c.Name).IsRequired(true);

                //entity.HasData(DefaultStates.SeedStates());
            });

            builder.Entity<Asset>(entity =>
            {
                entity.ToTable("Assets");

                entity.HasKey(a => a.AssetId);
                entity.Property(a => a.AssetId).ValueGeneratedOnAdd();

                entity.Property(a => a.AssetName).IsRequired(true);
                entity.Property(a => a.IsDisable).HasDefaultValue(false);

                //entity.HasData(DefaultAssets.SeedAssets());
            });

            builder.Entity<Assignment>(entity =>
            {
                entity.ToTable("Assignments");

                entity.HasKey(am => am.AssignmentId);
                entity.Property(am => am.AssignmentId).ValueGeneratedOnAdd();

                entity.HasOne<Asset>(am => am.Asset)
                    .WithOne(a => a.Assignment)
                    .HasForeignKey<Assignment>(am => am.AssetId);

                entity.HasOne<User>(am => am.AssignedToUser)
                    .WithMany(u => u.AssignmentsTo)
                    .HasForeignKey(am => am.AssignedToUserId)
                    .OnDelete(DeleteBehavior.NoAction);

                entity.HasOne<User>(am => am.AssignedByUser)
                    .WithMany(u => u.AssignmentsBy)
                    .HasForeignKey(am => am.AssignedByUserId)
                    .OnDelete(DeleteBehavior.NoAction);

                //entity.HasData(DefaultAssignments.SeedAssignments());
            });
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<Asset> Assets { get; set; }
        public DbSet<Assignment> Assignments { get; set; }
    }
}
