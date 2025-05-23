using Amazon;
using Amazon.Runtime;
using Amazon.S3;
using Infra.Setup;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using Vaggi.Infra.Context;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.WebHost.ConfigureKestrel(options =>
{
    options.Limits.MaxRequestBodySize = builder.Configuration.GetValue<int>("MaxSizeRequest");
});

builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Vaggi",
        Version = "v1",
        Description = "Server side application for Vaggi Mobile App"
    });

    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Insira o token JWT no campo abaixo"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

builder.Services.AddAuthorization();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
       .AddJwtBearer(options =>
       {
           options.RequireHttpsMetadata = false;
           options.Challenge = JwtBearerDefaults.AuthenticationScheme;
           options.TokenValidationParameters = new TokenValidationParameters
           {
               IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SecretKey"]!)),
               ValidIssuer = builder.Configuration["Jwt:Issuer"],
               ValidAudience = builder.Configuration["Jwt:Audience"],
               ValidateIssuer = true,
               ValidateAudience = true,
               ValidateIssuerSigningKey = true,
               ValidateLifetime = true,
               ClockSkew = TimeSpan.FromSeconds(0)
           };

           options.Events = new JwtBearerEvents
           {
               OnMessageReceived = context =>
               {
                   var accessToken = context.Request.Query["access_token"];

                   var path = context.HttpContext.Request.Path;
                   if (!string.IsNullOrEmpty(accessToken) && path.Value!.Contains("/chat-hub"))
                   {
                       context.Token = accessToken;
                   }

                   return Task.CompletedTask;
               },
               OnAuthenticationFailed = context =>
               {
                   var logger = context.HttpContext.RequestServices.GetRequiredService<ILoggerFactory>()
                   .CreateLogger("JWT");

                   logger.LogWarning("Falha na autenticação do token.");
                   return Task.CompletedTask;
               },
               OnTokenValidated = context =>
               {
                   var logger = context.HttpContext.RequestServices.GetRequiredService<ILoggerFactory>()
                    .CreateLogger("JWT");

                   logger.LogInformation("Token validado.");
                   return Task.CompletedTask;
               },
               OnChallenge = context =>
               {
                   var logger = context.HttpContext.RequestServices.GetRequiredService<ILoggerFactory>()
                       .CreateLogger("JWT");

                   logger.LogWarning("Desafio JWT falhou: {ErrorDescription}", context.ErrorDescription);
                   return Task.CompletedTask;
               },
           };
       });

builder.Services.Configure<S3Setup>(builder.Configuration.GetSection("S3Setup"));

builder.Services.AddDbContext<AppDbContext>(options =>
{
    string connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? "";

    options.UseNpgsql(connectionString);
});

builder.Services.AddSingleton<IAmazonS3>(options =>
{
    S3Setup settings = options.GetRequiredService<IOptions<S3Setup>>().Value;

    var credentials = new BasicAWSCredentials(settings.AccessKey, settings.SecretKey);
    var config = new AmazonS3Config
    {
        RegionEndpoint = RegionEndpoint.GetBySystemName(settings.Region),
    };
    return new AmazonS3Client(credentials, config);
});

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

await app.RunAsync();
