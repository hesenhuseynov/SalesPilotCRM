using Google.Cloud.SecretManager.V1;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.IdentityModel.Tokens;
using SalesPilotCRM.API.Middlewares;
using SalesPilotCRM.API.Services;
using SalesPilotCRM.Application;
using SalesPilotCRM.Application.Common.Settings;
using SalesPilotCRM.Infrastructure;
using SalesPilotCRM.Persistence;
using SalesPilotCRM.Persistence.Contexts;
using SalesPilotCRM.Persistence.Seed;
using Serilog;
using System.Text;
var builder = WebApplication.CreateBuilder(args);


//Google Secret Manager Confgration   

#region 🔐 Google Secret Manager + JWT Secret

var configuration = builder.Configuration;




var relativePath = configuration["GoogleSecrets:CredentialsPath"];
var solutionRoot = Directory.GetParent(AppContext.BaseDirectory)?
                          .Parent?.Parent?.Parent?.Parent?.FullName;

var defaultSecretPath = Path.Combine(solutionRoot!, "secrets", "salespilotcrm-e62b0a5554e53.json");
var credentialsPath = Path.Combine(solutionRoot!, relativePath!);

if (!File.Exists(credentialsPath))
{
    throw new FileNotFoundException("❌ Secret faylı tapılmadı!", credentialsPath);
}

var secretClient = new SecretManagerServiceClientBuilder
{
    CredentialsPath = credentialsPath
}.Build();

var secretService = new GoogleSecretService(secretClient);

builder.Services.AddSingleton(secretService);

var jwtSettings = configuration.GetSection("JwtSettings").Get<JwtSettings>() ?? new JwtSettings();
var jwtSecretFromGcp = secretService.GetSecret("jwt-secret2", "salespilotcrm");



if (string.IsNullOrWhiteSpace(jwtSecretFromGcp))
    throw new Exception("❌ JWT secret null və ya boş gəldi!");

jwtSettings.Key = jwtSecretFromGcp;

// ✅ DÜZGÜN BUDUR:
builder.Services.Configure<JwtSettings>(opts =>
{
    opts.Key = jwtSettings.Key;
    opts.Issuer = jwtSettings.Issuer;
    opts.Audience = jwtSettings.Audience;
    opts.AccessTokenExpirationMinutes = jwtSettings.AccessTokenExpirationMinutes;
    opts.RefreshTokenExpirationDays = jwtSettings.RefreshTokenExpirationDays;
});

builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtSettings.Issuer,
            ValidAudience = jwtSettings.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key))
        };
    });

builder.Services.AddAuthorization();

#endregion


builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddPersistenceServices(builder.Configuration);
builder.Services.AddInfrastructureServices();
builder.Services.AddControllers();




builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Host.UseSerilog((context, services, configuration) =>
{
    configuration
          .ReadFrom.Configuration(context.Configuration)
          .ReadFrom.Services(services)
          .Enrich.FromLogContext()
          .Enrich.WithProperty("Application", "SalesPilotCRM")
          .WriteTo.Seq("http://localhost:5341");
});



builder.Services.AddApiVersioning(opt =>
{
    opt.AssumeDefaultVersionWhenUnspecified = true;
    opt.DefaultApiVersion = new ApiVersion(1, 0);
    opt.ReportApiVersions = true;
    opt.ApiVersionReader = new QueryStringApiVersionReader("api-version");
});

//builder.Services.AddOpenApi();
builder.Services.AddOpenApiDocument(options =>
{
    options.Title = "SalesPilot CRM API";
    options.Version = "v1";
    options.Description = "SalesPilot CRM üçün ASP.NET Core + Scalar sənədləşməsi";
});

var app = builder.Build();


using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppWriteDbContext>();
    await DbInitializer.SeedAsync(context);
}


app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseMiddleware<RequestLoggingMiddleware>();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();


if (app.Environment.IsDevelopment())
{
    //app.MapOpenApi();              ///CI CD den otru comitlerdim scalari qayiadacam .NET 9 a 
    //app.MapScalarApiReference();
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseCors("AllowAll");
}
else
{
    app.UseCors(policy =>
       policy.WithOrigins(configuration.GetSection("Cors:AllowedOrigins").Get<string[]>()!)
             .AllowAnyHeader()
             .AllowAnyMethod());
}

app.MapControllers();

app.Run();
