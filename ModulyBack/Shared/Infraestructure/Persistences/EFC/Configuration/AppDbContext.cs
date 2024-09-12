using EntityFrameworkCore.CreatedUpdatedDate.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ModulyBack.IAM.Domain.Model.Aggregates;
using ModulyBack.Moduly.Domain.Model.Entities;

namespace ModulyBack.Shared.Infraestructure.Persistences.EFC.Configuration
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<Module> Modules { get; set; }
        public DbSet<ModulePermission> ModulePermissions { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<UserCompany> UserCompanies { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder.UseLoggerFactory(LoggerFactory.Create(builder => builder.AddConsole()));
            builder.EnableSensitiveDataLogging();
            builder.AddCreatedUpdatedInterceptor();
            //builder.UseSnakeCaseNamingConvention();

            base.OnConfiguring(builder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // User entity
            builder.Entity<User>()
                .HasKey(u => u.Id);

            // Company entity
            builder.Entity<Company>()
                .HasKey(c => c.Id);

            builder.Entity<Company>()
                .HasMany(c => c.UserCompanies)
                .WithOne(uc => uc.Company)
                .HasForeignKey(uc => uc.CompanyId);

            builder.Entity<Company>()
                .HasMany(c => c.Modules)
                .WithOne(m => m.Company)
                .HasForeignKey(m => m.CompanyId);

            // UserCompany entity
            builder.Entity<UserCompany>()
                .HasKey(uc => uc.Id);

            builder.Entity<UserCompany>()
                .HasMany(uc => uc.ModulePermissions)
                .WithOne(mp => mp.UserCompany)
                .HasForeignKey(mp => mp.UserCompanyId);

            // Module entity
            builder.Entity<Module>()
                .HasKey(m => m.Id);

            builder.Entity<Module>()
                .HasMany(m => m.Invoices)
                .WithOne(i => i.Module)
                .HasForeignKey(i => i.ModuleId);

            builder.Entity<Module>()
                .HasMany(m => m.Permissions)
                .WithOne(mp => mp.Module)
                .HasForeignKey(mp => mp.ModuleId);

            // Invoice entity
            builder.Entity<Invoice>()
                .HasKey(i => i.Id);

            builder.Entity<Invoice>()
                .HasMany(i => i.Payments)
                .WithOne(p => p.Invoice)
                .HasForeignKey(p => p.InvoiceId);

            // ModulePermission entity
            builder.Entity<ModulePermission>()
                .HasKey(mp => mp.Id);

            // Payment entity
            builder.Entity<Payment>()
                .HasKey(p => p.Id);

            // Configurations for the `User` table to match the database schema
            builder.Entity<User>()
                .Property(u => u.Id)
                .HasColumnName("id_user");

            builder.Entity<User>()
                .Property(u => u.Username)
                .HasColumnName("username");

            builder.Entity<User>()
                .Property(u => u.FullName)
                .HasColumnName("full_name");

            builder.Entity<User>()
                .Property(u => u.Age)
                .HasColumnName("age");

            builder.Entity<User>()
                .Property(u => u.Dni)
                .HasColumnName("dni");

            builder.Entity<User>()
                .Property(u => u.PhoneNumber)
                .HasColumnName("phone_number");

            builder.Entity<User>()
                .Property(u => u.Email)
                .HasColumnName("email");

            builder.Entity<User>()
                .Property(u => u.PasswordHash)
                .HasColumnName("password_hash");

            builder.Entity<User>()
                .Property(u => u.CreationDate)
                .HasColumnName("creation_date");

            // Similar configurations can be added for other entities if needed
        }
    }
}
