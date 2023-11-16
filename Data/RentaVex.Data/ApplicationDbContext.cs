namespace RentaVex.Data
{
    using System;
    using System.Linq;
    using System.Reflection;
    using System.Threading;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using RentaVex.Data.Common.Models;
    using RentaVex.Data.Models;

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
    {
        private static readonly MethodInfo SetIsDeletedQueryFilterMethod =
            typeof(ApplicationDbContext).GetMethod(
                nameof(SetIsDeletedQueryFilter),
                BindingFlags.NonPublic | BindingFlags.Static);

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Setting> Settings { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<Transaction> Transactions { get; set; }

        public DbSet<UserInteraction> UserInteractions { get; set; }

        public DbSet<Follow> Follows { get; set; }

        public DbSet<ProductAvailability> ProductAvailabilities { get; set; }

        public DbSet<ProductItem> ProductItems { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<ProductCategory> ProductCategories { get; set; }

        public DbSet<Image> Images { get; set; }


        public override int SaveChanges() => this.SaveChanges(true);

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            this.ApplyAuditInfoRules();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) =>
            this.SaveChangesAsync(true, cancellationToken);

        public override Task<int> SaveChangesAsync(
            bool acceptAllChangesOnSuccess,
            CancellationToken cancellationToken = default)
        {
            this.ApplyAuditInfoRules();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserInteraction>()
                .HasKey(ui => ui.UserInteractionID);

            modelBuilder.Entity<UserInteraction>()
                .HasOne(ui => ui.User)
                .WithMany(u => u.UserInteractions)
                .HasForeignKey(ui => ui.UserID);

            modelBuilder.Entity<UserInteraction>()
                .HasOne(ui => ui.Product)
                .WithMany(p => p.UserInteractions)
                .HasForeignKey(ui => ui.ProductID);

            modelBuilder.Entity<Transaction>()
                .HasOne(t => t.User)
                .WithMany(u => u.Transactions)
                .HasForeignKey(t => t.UserID);

            modelBuilder.Entity<Transaction>()
                .HasOne(t => t.Product)
                .WithMany(p => p.Transactions)
                .HasForeignKey(t => t.ProductID);

            modelBuilder.Entity<Like>()
                .HasOne(l => l.User)
                .WithMany(u => u.Likes)
                .HasForeignKey(l => l.UserID);

            modelBuilder.Entity<Like>()
                .HasOne(l => l.Product)
                .WithMany(p => p.Likes)
                .HasForeignKey(l => l.ProductID);

            modelBuilder.Entity<ProductAvailability>()
                .HasKey(pa => pa.ProductAvailabilityID);

            modelBuilder.Entity<ProductAvailability>()
                .HasOne(pa => pa.Product)
                .WithMany(p => p.Availabilities)
                .HasForeignKey(pa => pa.ProductID);

            modelBuilder.Entity<ProductItem>()
                .HasKey(pi => pi.ProductItemID);

            modelBuilder.Entity<ProductItem>()
                .HasOne(pi => pi.Product)
                .WithMany(p => p.ProductItems)
                .HasForeignKey(pi => pi.ProductID);

            modelBuilder.Entity<Category>()
                .HasKey(c => c.CategoryID);

            modelBuilder.Entity<ProductCategory>()
                .HasKey(pc => new { pc.ProductID, pc.CategoryID });

            modelBuilder.Entity<ProductCategory>()
                .HasOne(pc => pc.Product)
                .WithMany(p => p.ProductCategories)
                .HasForeignKey(pc => pc.ProductID);

            modelBuilder.Entity<ProductCategory>()
              .HasOne(pc => pc.Category)
              .WithMany(c => c.ProductCategories)
              .HasForeignKey(pc => pc.CategoryID);

            modelBuilder.Entity<Image>()
                .HasKey(img => img.Id);

            modelBuilder.Entity<Product>()
                .HasMany(p => p.Images)
                .WithOne()
                .HasForeignKey(img => img.AddedByUserId);

            modelBuilder.Entity<Product>()
               .HasOne(p => p.Category)
               .WithMany(c => c.Products)
               .HasForeignKey(p => p.CategoryID);

            modelBuilder.Entity<Follow>()
                .HasKey(f => new { f.FollowerID, f.SellerID });

            modelBuilder.Entity<Follow>()
                .HasOne(f => f.Follower)
                .WithMany(u => u.Following)
                .HasForeignKey(f => f.FollowerID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Follow>()
                .HasOne(f => f.Seller)
                .WithMany(u => u.Followers)
                .HasForeignKey(f => f.SellerID)
                .OnDelete(DeleteBehavior.Restrict);


            // Needed for Identity models configuration
            base.OnModelCreating(modelBuilder);

            this.ConfigureUserIdentityRelations(modelBuilder);

            EntityIndexesConfiguration.Configure(modelBuilder);

            var entityTypes = modelBuilder.Model.GetEntityTypes().ToList();

            // Set global query filter for not deleted entities only
            var deletableEntityTypes = entityTypes
                .Where(et => et.ClrType != null && typeof(IDeletableEntity).IsAssignableFrom(et.ClrType));
            foreach (var deletableEntityType in deletableEntityTypes)
            {
                var method = SetIsDeletedQueryFilterMethod.MakeGenericMethod(deletableEntityType.ClrType);
                method.Invoke(null, new object[] { modelBuilder });
            }

            // Disable cascade delete
            var foreignKeys = entityTypes
                .SelectMany(e => e.GetForeignKeys().Where(f => f.DeleteBehavior == DeleteBehavior.Cascade));
            foreach (var foreignKey in foreignKeys)
            {
                foreignKey.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }

        private static void SetIsDeletedQueryFilter<T>(ModelBuilder builder)
            where T : class, IDeletableEntity
        {
            builder.Entity<T>().HasQueryFilter(e => !e.IsDeleted);
        }

        // Applies configurations
        private void ConfigureUserIdentityRelations(ModelBuilder builder)
             => builder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);

        private void ApplyAuditInfoRules()
        {
            var changedEntries = this.ChangeTracker
                .Entries()
                .Where(e =>
                    e.Entity is IAuditInfo &&
                    (e.State == EntityState.Added || e.State == EntityState.Modified));

            foreach (var entry in changedEntries)
            {
                var entity = (IAuditInfo)entry.Entity;
                if (entry.State == EntityState.Added && entity.CreatedOn == default)
                {
                    entity.CreatedOn = DateTime.UtcNow;
                }
                else
                {
                    entity.ModifiedOn = DateTime.UtcNow;
                }
            }
        }
    }
}
