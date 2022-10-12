using GrpcRestExample.Services;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.ConfigureLogging(log => log.AddConsole().SetMinimumLevel(LogLevel.Information));

builder.Services.AddGrpc();
builder.Services.AddGrpcHttpApi();
builder.Services.AddGrpcReflection();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
    c.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.,
        Flows =   new OpenApiOAuthFlows
        {
            Implicit = new OpenApiOAuthFlow
            {
                AuthorizationUrl = new Uri("https://qa1-admin.deltatreaxis.com/login"),
            }
        },
        In = ParameterLocation.,
        Scheme = JwtBearerDefaults.AuthenticationScheme,
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = JwtBearerDefaults.AuthenticationScheme
                }
            },
            new string[] {}
        }
    });
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