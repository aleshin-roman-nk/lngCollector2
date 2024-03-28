using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MySQLRepo;
using Swashbuckle.AspNetCore.Filters;
using System.Configuration;
using System.Net;
using System.Text;
using ThoughtzLand.Api.auth;
using ThoughtzLand.Core.Repos;
using ThoughtzLand.Core.Services;
using ThoughtzLand.Core.Services.FlashCards;
//using ThoughtzLand.ImplementRepo.SQLitePepo;
using ThoughtzLand.ImplementRepo.MySqlRepo;
using ThoughtzLand.ImplementRepo.SQLitePepo;
using UserRegistry;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
	.AddJsonOptions(options =>
	{
		options.JsonSerializerOptions.PropertyNamingPolicy = null;
	});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(
	options =>
{
	options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
	{
		Description = "Standard Authorization header using the Bearer scheme (\"bearer {token}\")",
		In = ParameterLocation.Header,
		Name = "Authorization",
		Type = SecuritySchemeType.ApiKey
	});

	options.OperationFilter<SecurityRequirementsOperationFilter>();
}
);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
	.AddJwtBearer(options =>
	{
		options.TokenValidationParameters = new TokenValidationParameters
		{
			ValidateIssuerSigningKey = true,
			IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
				.GetBytes(builder.Configuration.GetSection("AppSettings:Token").Value)),
			ValidateIssuer = false,
			ValidateAudience = false
		};
	});

builder.Host.ConfigureDefaults(args)
			.ConfigureLogging(logging =>
			{
				//logging.ClearProviders();

				// Set the logging level for Entity Framework Core
				logging.AddFilter("Microsoft.EntityFrameworkCore.Database.Command", LogLevel.Warning);
				// Add other logging filters if needed
			});


builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped<TerrainService>();
builder.Services.AddScoped<NodeService>();
builder.Services.AddScoped<LanguagesService>();
builder.Services.AddScoped<FlashCardService>();
builder.Services.AddScoped<FlashCardExamService>();
builder.Services.AddScoped<ResearchTextService>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<IAuthorizedUserService, AuthorizedUserService>();

dataBaseRepositoriesMySql(builder);

builder.Services.AddSingleton<CardParametersSchemeProvider>();

builder.Services.AddCors(options => options.AddPolicy(name: "moyaDerevnya",
	policy =>
	{
		var allowedOrigins = builder.Configuration.GetSection("Cors:AllowedOrigins").Get<string[]>();
		if (allowedOrigins == null) allowedOrigins = new string[] { "localhost" };

		Console.WriteLine("Allowed cors origins:");
		foreach (var allowedOrigin in allowedOrigins)
            Console.WriteLine(allowedOrigin);

		policy.WithOrigins(allowedOrigins).AllowAnyMethod().AllowAnyHeader();
	}));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseExceptionHandler(errorApp =>
{
	errorApp.Run(async context =>
	{
		context.Response.StatusCode = StatusCodes.Status500InternalServerError;
		//context.Response.ContentType = "application/problem+json";
		context.Response.ContentType = "text/plain";

		var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();

		if (exceptionHandlerPathFeature?.Error != null)
		{
			var logger = context.RequestServices.GetRequiredService<ILogger<Program>>();

			// Log only the exception message
			//logger.LogError(exceptionHandlerPathFeature.Error.Message);

			//var problemDetails = new ProblemDetails
			//{
			//	Status = StatusCodes.Status500InternalServerError,
			//	Title = "An error occurred while processing your request.",
			//	Detail = exceptionHandlerPathFeature.Error.Message // Or a generic error message
			//};

			//var json = System.Text.Json.JsonSerializer.Serialize(problemDetails);
			//await context.Response.WriteAsync(json);
			await context.Response.WriteAsync(exceptionHandlerPathFeature.Error.Message);
		}
	});
});


app.UseCors("moyaDerevnya");

//app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

void dataBaseRepositoriesMySql(WebApplicationBuilder? builder)
{
	if (builder == null) return;

	builder.Services.AddDbContext<AppDataMySql>(options =>
	{
		var dbIp = builder.Configuration["DB_IP"] != null ? builder.Configuration["DB_IP"] : "45.131.41.112";
		var dbPort = builder.Configuration["DB_PORT"] != null ? builder.Configuration["DB_PORT"] : "3306";
		var dbRootPsw = builder.Configuration["DB_ROOT_PSW"] != null ? builder.Configuration["DB_ROOT_PSW"] : "tratata900";

		var connection = $"Server={dbIp};Database=lng2;port={dbPort};user=root;password={dbRootPsw};";

		options.UseMySql(connection, new MySqlServerVersion(new Version(8, 0, 25)));

		//options.LogTo(Console.WriteLine);
		//options.EnableSensitiveDataLogging(true);
		//options.UseLoggerFactory(null);
	});

	builder.Services.AddScoped<ITerrainRepo, TerrainRepoMySql>();
	builder.Services.AddScoped<INodeRepo, NodeRepoMySql>();
	builder.Services.AddScoped<ILanguageRepo, LanguageRepoMySql>();
	builder.Services.AddScoped<IFlashCardRepo, FlashCardRepoMySql>();
	builder.Services.AddScoped<IFlashCardAnswerRepo, FlashCardAnswerRepoMySql>();
	builder.Services.AddScoped<IResearchTextRepo, ResearchTextRepoMySql>();
	builder.Services.AddScoped<IUserRepo, UserRepoMySql>();
}

//void dataBaseRepositoriesSQLite(WebApplicationBuilder? builder)
//{
//	if (builder == null) return;

//builder.Services.AddDbContext<AppDataSQLite>(options =>
//{
//	options.UseSqlite(builder.Configuration.GetConnectionString("cnSqlite"), b => b.MigrationsAssembly("appLngApi"));
//	//options.LogTo(Console.WriteLine);
//	//options.EnableSensitiveDataLogging(true);
//	//options.UseLoggerFactory(null);
//});

//	builder.Services.AddScoped<ITerrainRepo, TerrainRepoSQLite>();
//	builder.Services.AddScoped<INodeRepo, NodeRepoSQLite>();
//	builder.Services.AddScoped<ILanguageRepo, LanguageRepoSQLite>();
//	builder.Services.AddScoped<IFlashCardRepo, FlashCardRepoSQLite>();
//	builder.Services.AddScoped<IFlashCardAnswerRepo, FlashCardAnswerRepoSQLite>();
//	builder.Services.AddScoped<IResearchTextRepo, ResearchTextRepoSQLite>();
//	builder.Services.AddScoped<IUserRepo, UserRepoSQLite>();
//}