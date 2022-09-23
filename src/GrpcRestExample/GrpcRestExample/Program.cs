using GrpcRestExample.Services;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.ConfigureLogging(log => log.AddConsole().SetMinimumLevel(LogLevel.Information));

builder.Services.AddGrpc();
builder.Services.AddGrpcHttpApi();
builder.Services.AddGrpcReflection();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
});
builder.Services.AddGrpcSwagger();

var app = builder.Build();

//app.UseDeveloperExceptionPage();
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "gRPC HTTP API Example V1");
});

app.UseRouting();
app.MapGrpcService<GreeterService>();
app.MapGrpcReflectionService();

app.Run();