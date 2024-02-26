using System;
using System.Security.Claims;
using JofferWebAPI;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using MySql.Data.MySqlClient;

var builder = WebApplication.CreateBuilder(args);

var domain = $"https://{builder.Configuration["Auth0:Domain"]}/";
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Authority = domain;
        options.Audience = builder.Configuration["Auth0:Audience"];
        options.TokenValidationParameters = new TokenValidationParameters
        {
            NameClaimType = ClaimTypes.NameIdentifier
        };
    });

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IAuthorizationHandler, HasScopeHandler>();

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("read:messages", policy => policy.Requirements.Add(new 
        HasScopeRequirement("read:messages", domain)));
});

var app = builder.Build();

// Middleware order correction
app.UseAuthentication();


app.UseHttpsRedirection();
app.UseRouting(); // Moved routing middleware up
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// SQL connection
using (MySqlConnection connection = new MySqlConnection(builder.Configuration.GetConnectionString("DefaultConnection")))
{
    try
    {
        connection.Open();
        Console.WriteLine("Database connected!");
    }
    catch (Exception ex)
    {
        // Log the exception or handle it accordingly
        Console.WriteLine($"Database connection error: {ex.Message}");
        throw; // Re-throwing the exception to halt execution if necessary
    }
    finally
    {
        connection.Close();
    }
}

app.Run();
