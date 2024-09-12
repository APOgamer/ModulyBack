using EntityFrameworkCore.CreatedUpdatedDate.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ModulyBack.IAM.Domain.Model.Aggregates;
using ModulyBack.Moduly.Domain.Model.Aggregate;
using ModulyBack.Moduly.Domain.Model.Entities;
using ModulyBack.Moduly.Domain.Model.ValueObjects;

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
        public DbSet<Being> Beings { get; set; }
        public DbSet<BeingModule> BeingModules { get; set; }
        public DbSet<UserCompanyPermission> UserCompanyPermissions { get; set; }
        public DbSet<PermissionType> PermissionTypes { get; set; }

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

            // User Entity
            builder.Entity<User>()
                .HasKey(u => u.Id);

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

            // Company Entity
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

            // UserCompany Entity
            builder.Entity<UserCompany>()
                .HasKey(uc => uc.Id);

            builder.Entity<UserCompany>()
                .HasOne(uc => uc.User)
                .WithMany()
                .HasForeignKey(uc => uc.UserId);
            builder.Entity<PermissionType>()
                .Property(pt => pt.AllowedActions)
                .HasConversion(
                    v => string.Join(",", v),    // Serializa la lista de enums a una cadena de texto
                    v => v.Split(",", StringSplitOptions.RemoveEmptyEntries)
                        .Select(e => Enum.Parse<AllowedActionEnum>(e))
                        .ToList()              // Convierte la cadena de vuelta a la lista de enums
                );
            builder.Entity<UserCompany>()
                .HasOne(uc => uc.Company)
                .WithMany(c => c.UserCompanies)
                .HasForeignKey(uc => uc.CompanyId);

            builder.Entity<UserCompany>()
                .HasMany(uc => uc.ModulePermissions)
                .WithOne()
                .HasForeignKey(mp => mp.ModuleId);

            // Module Entity
            builder.Entity<Module>()
                .HasKey(m => m.Id);

            builder.Entity<Module>()
                .HasMany(m => m.Invoices)
                .WithOne(i => i.Module)
                .HasForeignKey(i => i.ModuleId);

            builder.Entity<Module>()
                .HasMany(m => m.ModulePermissions)
                .WithOne(mp => mp.Module)
                .HasForeignKey(mp => mp.ModuleId);

            builder.Entity<Module>()
                .HasMany(m => m.BeingModules)
                .WithOne(bm => bm.Module)
                .HasForeignKey(bm => bm.ModuleId);

            // Invoice Entity
            builder.Entity<Invoice>()
                .HasKey(i => i.Id);

            builder.Entity<Invoice>()
                .HasMany(i => i.Payments)
                .WithOne(p => p.Invoice)
                .HasForeignKey(p => p.InvoiceId);

            // ModulePermission Entity
            builder.Entity<ModulePermission>()
                .HasKey(mp => mp.Id);

            builder.Entity<ModulePermission>()
                .HasOne(mp => mp.Module)
                .WithMany(m => m.ModulePermissions)
                .HasForeignKey(mp => mp.ModuleId);

            builder.Entity<ModulePermission>()
                .HasOne(mp => mp.PermissionType)
                .WithMany()
                .HasForeignKey(mp => mp.PermissionTypeId);

            // Payment Entity
            builder.Entity<Payment>()
                .HasKey(p => p.Id);

            // Being Entity
            builder.Entity<Being>()
                .HasKey(b => b.Id);

            builder.Entity<Being>()
                .HasMany(b => b.BeingModules)
                .WithOne(bm => bm.Being)
                .HasForeignKey(bm => bm.BeingId);

            // BeingModule Entity
            builder.Entity<BeingModule>()
                .HasKey(bm => new { bm.BeingId, bm.ModuleId });

            // UserCompanyPermission Entity
            builder.Entity<UserCompanyPermission>()
                .HasKey(ucp => ucp.Id);

            builder.Entity<UserCompanyPermission>()
                .HasOne(ucp => ucp.UserCompany)
                .WithMany()
                .HasForeignKey(ucp => ucp.UserCompanyId);

            builder.Entity<UserCompanyPermission>()
                .HasOne(ucp => ucp.Module)
                .WithMany()
                .HasForeignKey(ucp => ucp.ModuleId);

            builder.Entity<UserCompanyPermission>()
                .HasOne(ucp => ucp.PermissionType)
                .WithMany()
                .HasForeignKey(ucp => ucp.PermissionTypeId);

            // PermissionType Entity
            builder.Entity<PermissionType>()
                .HasKey(pt => pt.Id);

            builder.Entity<PermissionType>()
                .HasOne(pt => pt.Company)
                .WithMany()
                .HasForeignKey(pt => pt.CompanyId);
        }
    }
}