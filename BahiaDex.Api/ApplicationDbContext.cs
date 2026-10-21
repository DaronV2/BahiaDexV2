using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Collections;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;

namespace BahiaDex.Api
{
    public class ApplicationDbContext : DbContext
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationDbContext"/> class.
        /// </summary>
        /// <param name="options">The parameter.</param>
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            this.Database.SetCommandTimeout(180);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationDbContext"/> class.
        /// </summary>
        public ApplicationDbContext()
        {
            this.Database.SetCommandTimeout(180);
        }

        /// <summary>
        /// Gets or sets the IdentifiantType DbSet, representing the collection of identifier types in the database context, allowing for querying and manipulation of identifier type entities within the application. This DbSet provides access to the underlying database table that stores identifier type information, enabling operations such as adding, updating, and retrieving identifier type records as part of the application's data management functionality.
        /// </summary>
        // public DbSet<Andon> Andon { get; set; }
        public DbSet<Card> Card { get; set; }
        public DbSet<Serie> Serie { get; set; }
        public DbSet<Set> Set { get; set; }
        public DbSet<Variant> Variant { get; set; }

        private string? connexion;

        /// <summary>
        /// The function to save changes to the database.
        /// </summary>
        /// <returns>The base value.</returns>
        public override int SaveChanges()
        {
            this.NormalizeDateTimesToUtc();
            return base.SaveChanges();
        }

        /// <summary>
        /// Asynchronously saves all changes made in this context to the underlying database.
        /// </summary>
        /// <param name="cancellationToken">A cancellation token that can be used to cancel the asynchronous save operation.</param>
        /// <returns>A task that represents the asynchronous save operation. The task result contains the number of state entries written to the database.</returns>
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            this.NormalizeDateTimesToUtc();
            return base.SaveChangesAsync(cancellationToken);
        }

        /// <summary>
        /// Configures the database context to use a PostgreSQL database with the provided connection string if it has not already been configured. This method is called by the framework when the context is being initialized, allowing for dynamic configuration of the database connection based on the presence of a connection string. If the context has already been configured or if the connection string is null or empty, this method will not modify the existing configuration.
        /// </summary>
        /// <param name="optionsBuilder">The options builder.</param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured && !string.IsNullOrEmpty(this.connexion))
            {
                optionsBuilder.UseNpgsql(this.connexion);
            }
        }

        /// <summary>
        /// Configures the model to apply global value converters that ensure all DateTime and nullable DateTime properties are stored and retrieved as UTC.
        /// </summary>
        /// <remarks>This configuration enforces UTC handling for all DateTime properties, preventing issues with ambiguous or unspecified DateTime kinds.</remarks>
        /// <param name="modelBuilder">The builder used to construct the model for the context.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            ConfigureNavigationRelationships(modelBuilder);
            // Apply global UTC converters for DateTime and DateTime?
            ValueConverter<DateTime, DateTime> dateTimeConverter = new(
                v => DateTime.SpecifyKind(v.Kind == DateTimeKind.Unspecified ? v : v.ToUniversalTime(), DateTimeKind.Utc),
                v => DateTime.SpecifyKind(v, DateTimeKind.Utc));

            ValueConverter<DateTime?, DateTime?> nullableDateTimeConverter = new(
                v => v.HasValue ? DateTime.SpecifyKind(v.Value.Kind == DateTimeKind.Unspecified ? v.Value : v.Value.ToUniversalTime(), DateTimeKind.Utc) : v,
                v => v.HasValue ? DateTime.SpecifyKind(v.Value, DateTimeKind.Utc) : v);

            foreach (IMutableEntityType entityType in modelBuilder.Model.GetEntityTypes())
            {
                foreach (IMutableProperty property in entityType.GetProperties())
                {
                    if (property.ClrType == typeof(DateTime))
                    {
                        property.SetValueConverter(dateTimeConverter);
                    }
                    else if (property.ClrType == typeof(DateTime?))
                    {
                        property.SetValueConverter(nullableDateTimeConverter);
                    }
                }
            }
        }

        private static void ConfigureNavigationRelationships(ModelBuilder modelBuilder)
        {
            foreach (IMutableEntityType entityType in modelBuilder.Model.GetEntityTypes())
            {
                Type clrType = entityType.ClrType;
                PropertyInfo[] properties = clrType.GetProperties(BindingFlags.Instance | BindingFlags.Public);

                foreach (PropertyInfo property in properties)
                {
                    if (property.GetCustomAttribute<NotMappedAttribute>() != null)
                    {
                        continue;
                    }

                    Type propertyType = property.PropertyType;
                    if (propertyType == typeof(string))
                    {
                        continue;
                    }

                    if (typeof(IEnumerable).IsAssignableFrom(propertyType) && propertyType != typeof(byte[]))
                    {
                        continue;
                    }

                    if (!propertyType.IsClass)
                    {
                        continue;
                    }

                    string foreignKeyName = $"Id{property.Name}";
                    PropertyInfo? foreignKey = clrType.GetProperty(foreignKeyName);
                    if (foreignKey == null)
                    {
                        continue;
                    }

                    modelBuilder.Entity(clrType)
                        .HasOne(propertyType, property.Name)
                        .WithMany()
                        .HasForeignKey(foreignKeyName)
                        .OnDelete(DeleteBehavior.Cascade);
                }
            }
        }

        /// <summary>
        /// Ensure all DateTime/DateTime? values are normalized to UTC before persistence.
        /// </summary>
        private void NormalizeDateTimesToUtc()
        {
            foreach (Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry entry in this.ChangeTracker.Entries())
            {
                if (entry.State != EntityState.Added && entry.State != EntityState.Modified)
                {
                    continue;
                }

                foreach (Microsoft.EntityFrameworkCore.ChangeTracking.PropertyEntry prop in entry.Properties)
                {
                    Type clrType = prop.Metadata.ClrType;

                    if (clrType == typeof(DateTime))
                    {
                        if (prop.CurrentValue is DateTime dt)
                        {
                            if (dt.Kind == DateTimeKind.Local)
                            {
                                prop.CurrentValue = dt.ToUniversalTime();
                            }
                            else if (dt.Kind == DateTimeKind.Unspecified)
                            {
                                prop.CurrentValue = DateTime.SpecifyKind(dt, DateTimeKind.Utc);
                            }
                        }
                    }
                    else if (clrType == typeof(DateTime?))
                    {
                        if (prop.CurrentValue is DateTime dt2)
                        {
                            if (dt2.Kind == DateTimeKind.Local)
                            {
                                prop.CurrentValue = dt2.ToUniversalTime();
                            }
                            else if (dt2.Kind == DateTimeKind.Unspecified)
                            {
                                prop.CurrentValue = DateTime.SpecifyKind(dt2, DateTimeKind.Utc);
                            }
                        }
                    }
                }
            }
        }


    }
}


