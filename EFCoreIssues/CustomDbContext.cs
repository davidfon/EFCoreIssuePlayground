using Microsoft.EntityFrameworkCore;

namespace EFCoreIssues
{
    public class CustomDbContext : DbContext
    {
        public CustomDbContext(DbContextOptions<CustomDbContext> options) : base(options) { }

        public DbSet<Person> Employees { get; set; }

        public DbSet<PersonPermission> PersonPermissions { get; set; }

        public DbSet<PermissionType> PermissionTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>(builder =>
            {
                builder.OwnsOne(e => e.PersonName);
            });

            modelBuilder.Entity<PersonPermission>(builder =>
            {
                builder.HasOne(e => e.Person)
                    .WithMany()
                    .HasForeignKey(f => f.PersonId);
            });

            modelBuilder.Entity<PermissionType>(builder =>
            {
                builder.OwnsOne(e => e.PermissionTypeData);
            });
        }
    }
}
