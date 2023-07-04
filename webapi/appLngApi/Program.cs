using Services;
using Services.repo;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//builder.Services.AddScoped((f) =>
//{
//    return new AppData("lngapp.sqlite");
//});

builder.Services.AddScoped<IDbFactory>((f) => new DbFactory(@"..\db\lngapp.sqlite"));
builder.Services.AddScoped<INodeRepo, NodeRepo>();
builder.Services.AddScoped<IQuestionRepo, QuestionRepo>();
builder.Services.AddScoped<ITerrainRepo, TerrainRepo>();
builder.Services.AddScoped<IThExpressionRepo, ThExpressionRepo>();
builder.Services.AddScoped<IThoughtRepo, ThoughtRepo>();
builder.Services.AddScoped<IExamService, ExamService>();


builder.Services.AddCors(options => options.AddPolicy(name: "moyaDerevnya",
    policy =>
    {
        policy.WithOrigins("http://localhost:4200").AllowAnyMethod().AllowAnyHeader();
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
