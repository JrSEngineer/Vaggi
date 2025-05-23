using Amazon;
using Amazon.Runtime;
using Amazon.S3;
using Infra.Setup;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Vaggi.Infra.Context;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();

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
