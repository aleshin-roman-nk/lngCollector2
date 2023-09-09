using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using ThoughtzLand.Core.Repos;
using ThoughtzLand.Core.Services;
using ThoughtzLand.ImplementRepo.SQLitePepo;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = null;
    });
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//builder.Services.AddScoped((f) =>
//{
//    return new AppData("lngapp.sqlite");
//});

//builder.Services.AddScoped<IDbFactory>((f) => new DbFactory(@"..\db\lngapp.sqlite"));

builder.Services.AddDbContext<AppData>(options =>
{
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("appLngApi"));
    //options.LogTo(Console.WriteLine);
    //options.EnableSensitiveDataLogging(true);
    //options.UseLoggerFactory(null);
});

builder.Host.ConfigureDefaults(args)
            .ConfigureLogging(logging =>
            {
                //logging.ClearProviders();

                // Set the logging level for Entity Framework Core
                logging.AddFilter("Microsoft.EntityFrameworkCore.Database.Command", LogLevel.Warning);
                // Add other logging filters if needed
            });

builder.Services.AddScoped<ITerrainRepo, TerrainRepo>();
builder.Services.AddScoped<TerrainService>();

builder.Services.AddScoped<INodeRepo, NodeRepo>();
builder.Services.AddScoped<NodeService>();

builder.Services.AddScoped<IThoughtRepo, ThoughtRepo>();
builder.Services.AddScoped<ThoughtService>();

builder.Services.AddScoped<IThExpressionRepo, ThExpressionRepo>();
builder.Services.AddScoped<ThExpressionService>();

builder.Services.AddScoped<ILanguageRepo, LanguageRepo>();
builder.Services.AddScoped<LanguagesService>();

builder.Services.AddScoped<SRBoxService>();


builder.Services.AddCors(options => options.AddPolicy(name: "moyaDerevnya",
    policy =>
    {
        policy.WithOrigins("http://localhost:4200").AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin();
    }));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("moyaDerevnya");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
