using CorpResource.API.Configurations;
using CorpResource.Application.Contants;
using CorpResource.Application.Interfaces.Repositories;
using CorpResource.Application.Interfaces.Services;
using CorpResource.Application.Services;
using CorpResource.Infrastructure.Persistance.Repositories;
using Microsoft.AspNetCore.Authentication.Negotiate;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddCustomDatabaseConfiguration(builder.Configuration);

builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ReportApiVersions = true;
});

builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddScoped<IDepartmentsService, DepartmentsService>();
builder.Services.AddScoped<IUsersService, UsersService>();

builder.Services.AddAuthentication(NegotiateDefaults.AuthenticationScheme).AddNegotiate();

builder.Services.AddAuthorization(options =>
{
    options.FallbackPolicy = options.DefaultPolicy;
});

builder.Services.AddCors(options =>
{
    options.AddPolicy(ApiConstants.AllowLocalhost,
        builder =>
        {
            builder.WithOrigins(ApiConstants.ClientOrigin)
                   .AllowAnyHeader()
                   .AllowAnyMethod()
                   .SetPreflightMaxAge(TimeSpan.FromMinutes(10));
        });
});

var app = builder.Build();

app.UseCors(ApiConstants.AllowLocalhost);
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
