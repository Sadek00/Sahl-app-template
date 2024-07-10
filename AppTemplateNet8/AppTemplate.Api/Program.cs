using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using System.Text.Json.Serialization;
using System.Text.Json;
using AppTemplate.Api.Helper;
using AppTemplate.Domain.DBContexts;
using AppTemplate.Dto.Mappers;
using AppTemplate.Service.Implementation.ApacheKafka;
using AppTemplate.Service.Helper;
using AppTemplate.Infrastructure.Helper;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using AppTemplate.Domain.Entities.Admin;
using Microsoft.AspNetCore.Identity;
using AppTemplate.Domain.DbContexts;
using AppTemplate.Domain.Utilities;
using AppTemplate.Domain.AppConstants;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles)
    .AddJsonOptions(x => x.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull)
    .AddJsonOptions(x => x.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase)
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
        options.SerializerSettings.ContractResolver = new DefaultContractResolver { NamingStrategy = new CamelCaseNamingStrategy() };
    });

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")).UseLowerCaseNamingConvention();

    //options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
});

builder.Services.AddCors(p => p.AddDefaultPolicy(builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));
//builder.Services.AddHttpClient("CentralAuthenticationService", client => client.BaseAddress = new Uri(builder.Configuration["JWT:CentralAuthUrl"].ToString()));

// Project service registration
builder.Services.RegisterServices();
builder.Services.RegisterInfrastructure();
builder.Services.AddAutoMapper(typeof(MappingProfile));
//builder.Services.AddHttpContextAccessor();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
//builder.Services.AddHostedService<KafkaConsumerService>();
builder.Services.AddScoped<IDapperContext, DapperContext>();
builder.Services.AddScoped<DataSeeder>();
builder.Services.AddSingleton<IPasswordHasher, PasswordHasher>();


// Adding Authentication
builder.Services.ConfigureAuthentication(AppConstants.JwtSecretKey, builder.Configuration["JWT:ValidIssuer"], builder.Configuration["JWT:ValidAudience"]);
//builder.Services.ConfigureAuthentication(builder.Configuration["SLCRM:JwtAccTokenSecret"]);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = ".NET 8 Template Api", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "This site uses Bearer token for authentication. format: Bearer<<space>>token",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {{
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        },
                        Scheme = "oauth2",
                        Name = "Bearer",
                        In = ParameterLocation.Header
                    },
                    new List<string>(){}
                 }
                });
    c.EnableAnnotations();
});


// ==========================================================================================
var app = builder.Build();

app.AddGlobalErrorHandler();
app.UseRouting();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None));
}

app.UseHttpsRedirection();

app.UseDefaultFiles();
app.UseStaticFiles();
app.UseCors();

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

// Seed the database
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    try
    {
        var context = services.GetRequiredService<AppDbContext>();
        await context.Database.MigrateAsync();

        var seeder = services.GetRequiredService<DataSeeder>();
        await seeder.SeedAsync();
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred seeding the DB.");
    }
}

app.Run();
