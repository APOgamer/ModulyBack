
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using ModulyBack.IAM.Application.Internal.CommandServices;
using ModulyBack.IAM.Application.Internal.OutboundServices;
using ModulyBack.IAM.Application.Internal.QueryServices;
using ModulyBack.IAM.Domain.Repositories;
using ModulyBack.IAM.Domain.Services;
using ModulyBack.IAM.Infrastructure.Hashing.BCrypt.Services;
using ModulyBack.IAM.Infrastructure.Persistence.EFC.Repositories;
using ModulyBack.IAM.Infrastructure.Tokens.JWT.Configuration;
using ModulyBack.IAM.Infrastructure.Tokens.JWT.Services;
using ModulyBack.Moduly.Application.Internal.CommandServices;
using ModulyBack.Moduly.Application.Internal.QueryServices;
using ModulyBack.Moduly.Domain.Repositories;
using ModulyBack.Moduly.Domain.Services;
using ModulyBack.Moduly.Infraestructure.Persistence.EFC.Repositories;
using ModulyBack.Shared.Domain.Repositories;
using ModulyBack.Shared.Infraestructure.Interfaces.ASP.Configuration;
using ModulyBack.Shared.Infraestructure.Persistences.EFC.Configuration;
using ModulyBack.Shared.Infraestructure.Persistences.EFC.Repositories;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(options =>
{

    options.Conventions.Add(new KebabCaseRouteNamingConvention());
});

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(
    c =>
    {
        c.SwaggerDoc("v1",
            new OpenApiInfo
            {
                Title = "ModulyBack API",
                Version = "v1",
                Description = "ModulyBack Platform API",
                TermsOfService = new Uri("http://coquettecraft.duckdns.org:4200/"),
                Contact = new OpenApiContact
                {
                    Name = "ModulyRG",
                    Email = "u202210066@upc.edu.pe"
                },
                License = new OpenApiLicense
                {
                    Name = "Apache 2.0",
                    Url = new Uri("https://www.apache.org/licenses/LICENSE-2.0.html")
                }
            });
        c.EnableAnnotations();
     
    });

// ADD DATABASE CONNECTION
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// CONFIGURE DATABASE CONTEXT AND LOGGING LEVELS
builder.Services.AddDbContext<AppDbContext>(
    options =>
    {
        if(connectionString != null)
        {
            if (builder.Environment.IsDevelopment())
            {
                options.UseNpgsql(connectionString)
                    .LogTo(Console.WriteLine, LogLevel.Information)
                    .EnableSensitiveDataLogging()
                    .EnableDetailedErrors();
            }
            else if (builder.Environment.IsProduction())
            {
                options.UseNpgsql(connectionString)
                    .LogTo(Console.WriteLine, LogLevel.Error)
                    .EnableSensitiveDataLogging()
                    .EnableDetailedErrors();
            }
        }
    });

// CONFIGURE LOWERCASE URLS 
builder.Services.AddRouting(options => options.LowercaseUrls = true);

// ADD CONTROLLERS
builder.Services.AddControllers(options =>
{
    options.Conventions.Add(new KebabCaseRouteNamingConvention());
});
// ADD CORS
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(corsPolicyBuilder =>
    {
        corsPolicyBuilder.WithOrigins(
                "http://coquettecraft.duckdns.org:4200" // Dominio permitido
            )
            .AllowAnyHeader()       // Permitir cualquier encabezado
            .AllowAnyMethod()       // Permitir cualquier método HTTP (GET, POST, etc.)
            .AllowCredentials();    // Permitir el envío de cookies o credenciales
    });
});


// ADD REPOSITORIES AND SERVICES

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ICompanyRepository, CompanyRepository>();
builder.Services.AddScoped<IInvoiceRepository, InvoiceRepository>();
builder.Services.AddScoped<IModuleRepository, ModuleRepository>();
builder.Services.AddScoped<IUserCompanyRepository, UserCompanyRepository>();
builder.Services.AddScoped<IUserCompanyPermissionRepository, UserCompanyPermissionRepository>();
builder.Services.AddScoped<IPermissionTypeRepository, PermissionTypeRepository>();
builder.Services.AddScoped<IBeingRepository, BeingRepository>();
builder.Services.AddScoped<IInventoryRepository, InventoryRepository>();




builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IHashingService, HashingService>();

builder.Services.AddScoped<IUserCommandService, UserCommandService>();
builder.Services.AddScoped<ICompanyCommandService, CompanyCommandService>();
builder.Services.AddScoped<IInvoiceCommandService, InvoiceCommandService>();
builder.Services.AddScoped<IModuleCommandService, ModuleCommandService>();
builder.Services.AddScoped<IPermissionAssignmentCommandService, PermissionAssignmentCommandService>();
builder.Services.AddScoped<IPermissionTypeCommandService, PermissionTypeCommandService>();
builder.Services.AddScoped<IBeingCommandService, BeingCommandService>();
builder.Services.AddScoped<IInventoryCommandService, InventoryCommandService>();


builder.Services.AddScoped<IUserQueryServices, UserQueryService>();
builder.Services.AddScoped<ICompanyQueryService, CompanyQueryService>();
builder.Services.AddScoped<IInvoiceQueryService, InvoiceQueryService>();
builder.Services.AddScoped<IModuleQueryService, ModuleQueryService>();
builder.Services.AddScoped<IUserCompanyQueryService, UserCompanyQueryService>();
builder.Services.AddScoped<IBeingQueryService, BeingQueryService>();
builder.Services.AddScoped<IInventoryQueryService, IInventoryQueryService>();


builder.Services.Configure<TokenSettings>(builder.Configuration.GetSection("TokenSettings"));


// Register additional repositories and services here

var app = builder.Build();

// VERIFY DATABASE OBJECTS ARE CREATED
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<AppDbContext>();
    context.Database.EnsureCreated();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
    });
}

app.UseHttpsRedirection();
app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.Run();
