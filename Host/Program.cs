using Host.Configurations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.Text;
using Tokyo.Core.Interfaces;
using Tokyo.Core.Middlewares;
using Tokyo.Core.Services;
using Tokyo.DomainPersistence.Entities;
using Tokyo.Infrastructure.Auth;
using Tokyo.Infrastructure.Caching;
using Tokyo.Infrastructure.Database;
using Tokyo.Infrastructure.Logging.Serilog;
using Tokyo.Infrastructure.ServicesScoped;
using Tokyo.Infrastructure.Swagger;

var builder = WebApplication.CreateBuilder(args);
builder.AddConfigurations().RegisterSerilog();
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddCaching(builder.Configuration)
    .InitializeDatabase(builder.Configuration)
    .InitializeAuth(builder.Configuration)
    .InitializeSwagger()
    .InitializeRequiredService();


var app = builder.Build();

app.UseTokyoSwagger();
app.UseAuth();
app.UseHttpsRedirection();
app.UseSerilogRequestLogging();
app.MapControllers();
app.Run();