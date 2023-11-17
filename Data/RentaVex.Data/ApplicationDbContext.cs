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

        // Could be removed
        public DbSet<Setting> Settings { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<ProductAvailability> ProductAvailabilities { get; set; }

        public DbSet<ProductItem> ProductItems { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<ProductCategory> ProductCategories { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<UserInteraction> UserInteractions { get; set; }

        public DbSet<Follow> Follows { get; set; }

        public DbSet<Transaction> Transactions { get; set; }

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
                .HasKey(x => x.UserInteractionID);

            modelBuilder.Entity<UserInteraction>()
                .HasOne(x => x.User)
                .WithMany(y => y.UserInteractions)
                .HasForeignKey(z => z.UserID);

            modelBuilder.Entity<UserInteraction>()
                .HasOne(x => x.Product)
                .WithMany(y => y.UserInteractions)
                .HasForeignKey(z => z.ProductID);

            modelBuilder.Entity<Transaction>()
                .HasOne(x => x.User)
                .WithMany(y => y.Transactions)
                .HasForeignKey(z => z.UserID);

            modelBuilder.Entity<Transaction>()
                .HasOne(x => x.Product)
                .WithMany(y => y.Transactions)
                .HasForeignKey(z => z.ProductID);

            modelBuilder.Entity<Like>()
                .HasOne(x => x.User)
                .WithMany(y => y.Likes)
                .HasForeignKey(z => z.UserID);

            modelBuilder.Entity<Like>()
                .HasOne(x => x.Product)
                .WithMany(y => y.Likes)
                .HasForeignKey(z => z.ProductID);

            modelBuilder.Entity<ProductAvailability>()
                .HasKey(x => x.ProductAvailabilityID);

            modelBuilder.Entity<ProductAvailability>()
                .HasOne(x => x.Product)
                .WithMany(y => y.Availabilities)
                .HasForeignKey(z => z.ProductID);

            modelBuilder.Entity<ProductItem>()
                .HasKey(x => x.ProductItemID);

            modelBuilder.Entity<ProductItem>()
                .HasOne(x => x.Product)
                .WithMany(y => y.ProductItems)
                .HasForeignKey(z => z.ProductID);

            modelBuilder.Entity<Category>()
                .HasKey(x => x.Id);

            modelBuilder.Entity<ProductCategory>()
                .HasKey(x => new { x.ProductID, x.CategoryID });

            modelBuilder.Entity<ProductCategory>()
                .HasOne(x => x.Product)
                .WithMany(y => y.ProductCategories)
                .HasForeignKey(z => z.ProductID);

            modelBuilder.Entity<ProductCategory>()
              .HasOne(x => x.Category)
              .WithMany(y => y.ProductCategories)
              .HasForeignKey(z => z.CategoryID);

            modelBuilder.Entity<Image>()
                .HasKey(x => x.Id);

            modelBuilder.Entity<Product>()
                .HasMany(x => x.Images)
                .WithOne()
                .HasForeignKey(y => y.AddedByUserId);

            modelBuilder.Entity<Product>()
               .HasOne(x => x.Category)
               .WithMany(y => y.Products)
               .HasForeignKey(z => z.CategoryID);

            modelBuilder.Entity<Follow>()
                .HasKey(x => new { x.FollowerID, x.SellerID });

            modelBuilder.Entity<Follow>()
                .HasOne(x => x.Follower)
                .WithMany(y => y.Following)
                .HasForeignKey(z => z.FollowerID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Follow>()
                .HasOne(x => x.Seller)
                .WithMany(y => y.Followers)
                .HasForeignKey(z => z.SellerID)
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
