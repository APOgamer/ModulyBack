using EntityFrameworkCore.CreatedUpdatedDate.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ModulyBack.IAM.Domain.Model.Aggregates;
using ModulyBack.Moduly.Domain.Model.Aggregate;
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
        public DbSet<Module?> Modules { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<UserCompany> UserCompanies { get; set; }
        public DbSet<Being> Beings { get; set; }
        public DbSet<BeingModule> BeingModules { get; set; }
        public DbSet<UserCompanyPermission> UserCompanyPermissions { get; set; }
        public DbSet<PermissionType> PermissionTypes { get; set; }
        public DbSet<PermissionTypeAction> PermissionTypeActions { get; set; }

        // New DbSets for Inventory and InventoryItem
        public DbSet<Inventory> Inventories { get; set; }
        public DbSet<InventoryItem> InventoryItems { get; set; }

        public DbSet<Bank> Banks { get; set; }

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
            builder.Entity<User>(entity =>
            {
                entity.HasKey(u => u.Id);
                entity.Property(u => u.Id).HasColumnName("id_user");
                entity.Property(u => u.Username).HasColumnName("username");
                entity.Property(u => u.FullName).HasColumnName("full_name");
                entity.Property(u => u.Age).HasColumnName("age");
                entity.Property(u => u.Dni).HasColumnName("dni");
                entity.Property(u => u.PhoneNumber).HasColumnName("phone_number");
                entity.Property(u => u.Email).HasColumnName("email");
                entity.Property(u => u.PasswordHash).HasColumnName("password_hash");
                entity.Property(u => u.CreationDate).HasColumnName("creation_date");
            });

            // Company Entity
            builder.Entity<Company>(entity =>
            {
                entity.HasKey(c => c.Id);
                entity.HasMany(c => c.UserCompanies)
                    .WithOne(uc => uc.Company)
                    .HasForeignKey(uc => uc.CompanyId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(c => c.Modules)
                    .WithOne(m => m.Company)
                    .HasForeignKey(m => m.CompanyId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // UserCompany Entity
            builder.Entity<UserCompany>(entity =>
            {
                entity.HasKey(uc => uc.Id);
                entity.HasOne(uc => uc.User)
                    .WithMany()
                    .HasForeignKey(uc => uc.UserId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(uc => uc.Company)
                    .WithMany(c => c.UserCompanies)
                    .HasForeignKey(uc => uc.CompanyId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(uc => uc.UserCompanyPermissions)
                    .WithOne(ucp => ucp.UserCompany)
                    .HasForeignKey(ucp => ucp.UserCompanyId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // Module Entity
            builder.Entity<Module>(entity =>
            {
                entity.HasKey(m => m.Id);
                entity.HasMany(m => m.Invoices)
                    .WithOne(i => i.Module)
                    .HasForeignKey(i => i.ModuleId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(m => m.BeingModules)
                    .WithOne(bm => bm.Module)
                    .HasForeignKey(bm => bm.ModuleId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // Invoice Entity
            builder.Entity<Invoice>(entity =>
            {
                entity.HasKey(i => i.Id);
                entity.HasMany(i => i.Payments)
                    .WithOne(p => p.Invoice)
                    .HasForeignKey(p => p.InvoiceId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // Payment Entity
            builder.Entity<Payment>(entity =>
            {
                entity.HasKey(p => p.Id);
            });

            // Being Entity
            builder.Entity<Being>(entity =>
            {
                entity.HasKey(b => b.Id);
                entity.HasMany(b => b.BeingModules)
                    .WithOne(bm => bm.Being)
                    .HasForeignKey(bm => bm.BeingId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // BeingModule Entity
            builder.Entity<BeingModule>(entity =>
            {
                entity.HasKey(bm => new { bm.BeingId, bm.ModuleId });
            });

            // UserCompanyPermission Entity
            builder.Entity<UserCompanyPermission>(entity =>
            {
                entity.HasKey(ucp => ucp.Id);
                entity.HasOne(ucp => ucp.UserCompany)
                    .WithMany(uc => uc.UserCompanyPermissions)
                    .HasForeignKey(ucp => ucp.UserCompanyId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(ucp => ucp.Module)
                    .WithMany()
                    .HasForeignKey(ucp => ucp.ModuleId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(ucp => ucp.PermissionType)
                    .WithMany()
                    .HasForeignKey(ucp => ucp.PermissionTypeId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // PermissionType Entity
            builder.Entity<PermissionType>(entity =>
            {
                entity.HasKey(pt => pt.Id);
                entity.HasOne(pt => pt.Company)
                    .WithMany()
                    .HasForeignKey(pt => pt.CompanyId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(pt => pt.PermissionTypeActions)
                    .WithOne(pta => pta.PermissionType)
                    .HasForeignKey(pta => pta.PermissionTypeId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // PermissionTypeAction Entity
            builder.Entity<PermissionTypeAction>(entity =>
            {
                entity.HasKey(pta => pta.Id);
            });

            // Inventory Entity
            builder.Entity<Inventory>(entity =>
            {
                entity.HasKey(i => i.Id);
                entity.HasMany(i => i.Items)
                    .WithOne(ii => ii.Inventory)
                    .HasForeignKey(ii => ii.InventoryId)
                    .OnDelete(DeleteBehavior.Cascade);
                
                entity.Property(i => i.ModuleId).IsRequired();
                entity.Property(i => i.Name).IsRequired();
            });

            // InventoryItem Entity
            builder.Entity<InventoryItem>(entity =>
            {
                entity.HasKey(ii => ii.Id);
                entity.Property(ii => ii.Quantity).IsRequired();
                entity.HasOne(ii => ii.Being)
                    .WithMany()
                    .HasForeignKey(ii => ii.BeingId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // Bank Entity
            builder.Entity<Bank>(entity =>
            {
                entity.HasKey(b => b.Id);
                entity.Property(b => b.Name).IsRequired();
                entity.Property(b => b.TCEA).IsRequired();
                entity.HasOne(b => b.Company)
                    .WithMany(c => c.Banks)
                    .HasForeignKey(b => b.CompanyId)
                    .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
