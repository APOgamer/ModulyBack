using EntityFrameworkCore.CreatedUpdatedDate.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ModulyBack.Shared.Infraestructure.Persistences.EFC.Configuration
{

    public class AppDbContext : DbContext
    {
       

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

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

    
}
    }
}
