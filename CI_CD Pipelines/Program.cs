using CI_CD_Pipelines.Entities;
using CI_CD_Pipelines.Services;
using CI_CD_Pipelines.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddTransient<IProductService, ProductService>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddPolicy("ZiadAngular",
        corsBuilder => corsBuilder.WithOrigins(builder.Configuration.GetSection("CorsSettings:AllowedOrigins").Get<string[]>())
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials());
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("ZiadAngular");

app.UseAuthorization();

app.MapControllers();

app.Run();