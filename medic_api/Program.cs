using medic_api.Data;
using medic_api.Helpers.Auth;
using medic_api.Helpers.PasswordHash;
using medic_api.Helpers.Swagger;
using Microsoft.EntityFrameworkCore;

var config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", false)
    .Build();

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(config.GetConnectionString("db1")));
// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddSwaggerGen(x => x.OperationFilter<SwaggerHeader>());
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<MyAuthService>();

builder.Services.AddHttpContextAccessor();
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder
            .WithOrigins(new string[] { "http://localhost:4200", "https://localhost:7244" })
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseStaticFiles();

app.UseCors();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
