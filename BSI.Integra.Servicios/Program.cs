using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository;
using BSI.Integra.Repositorio.UnitOfWork;
using BSI.Integra.Servicios.Configurations;
using BSI.Integra.Servicios.Helpers;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

//Add Cors
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsVista",
        builder =>
        {
            builder.WithOrigins("https://integrav4-prepublicacion-interfaz.bsginstitute.com", "http://localhost:4200", "http://localhost:51260", "https://integrav5.bsginstitute.com", "https://integrav5-servicios.bsginstitute.com", "https://integrav5p.bsginstitute.com", "https://integrav4.bsginstitute.com", "https://integrav4-prepublicacion-interfaz.bsginstitute.com", "https://integrav5mejora.bsginstitute.com", "https://integrav5-mejora-servicios.bsginstitute.com", "https://integrav5prepublicacion.bsginstitute.com", "https://integrav5-prepublicacion-servicios.bsginstitute.com", "https://integrav5publicacion.bsginstitute.com", "https://integrav5-publicacion-servicios.bsginstitute.com", "https://integrav5pruebainterfaz.bsginstitute.com", "https://integrav5-3cx.bsginstitute.com")
                .AllowAnyHeader().AllowAnyMethod();
        });
});

// Add services to the container.

builder.Services.AddControllers()
    .ConfigureApiBehaviorOptions(options =>
    {
        options.SuppressModelStateInvalidFilter = true;
    });

var tokenKey = builder.Configuration["Jwt:Key"]!;
var key = Encoding.UTF8.GetBytes(tokenKey);

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        LifetimeValidator = LifetimeValidator,
        TokenDecryptionKey = new SymmetricSecurityKey(key),
    };
});
static bool LifetimeValidator(DateTime? notBefore,
    DateTime? expires,
    SecurityToken securityToken,
    TokenValidationParameters validationParameters) => expires != null && expires > DateTime.UtcNow;

//Add Contexts
builder.Services.AddScoped<IConnectionFactory, ConnectionFactory>(cn => new ConnectionFactory(builder.Configuration.GetConnectionString("IntegraDB")));
builder.Services.AddTransient<IDapperRepository, DapperRepository>();
builder.Services.AddDbContext<IntegraDBContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("IntegraDB")));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<ITokenManager, TokenManager>();


builder.Services.AddHttpContextAccessor();
builder.Services.AddFluentValidationAutoValidation();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddValidatorsFromAssemblyContaining<Program>();
builder.Services.AddFluentValidationAutoValidation();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors();

app.AddGlobalErrorHandler();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
