using Microsoft.Extensions.DependencyInjection.Extensions;
using SunTransfersApp.Infraestructure;
using SunTransfersApp.Logic;
using SunTransfersApp.Services;
using SunTransfersApp.Validations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddHttpClient();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(p => p.AddPolicy("corsapp", builder =>
{
    builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
}));

builder.Services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped<ClientsService>();
builder.Services.AddScoped<ClientsLogic> ();
builder.Services.AddScoped<ClientsValidations>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.MapEndpoint();

app.Run();