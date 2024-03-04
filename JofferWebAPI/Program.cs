using JofferWebAPI;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using MySql.Data.MySqlClient;
using System.Security.Claims;

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

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("read:messages", policy => policy.Requirements.Add(new
        HasScopeRequirement("read:messages", domain)));
});

builder.Services.AddSingleton<IAuthorizationHandler, HasScopeHandler>();

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseHttpsRedirection();

// Add these lines before UseEndpoints()
app.UseAuthentication();
app.UseAuthorization();

app.UseRouting(); // Add this line

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

// SQL connection
using (MySqlConnection connection = new MySqlConnection(builder.Configuration.GetConnectionString("DefaultConnection")))
{
    try
    {
        connection.Open();
        Console.WriteLine("Database connected!");
    }
    catch
    {
        throw new Exception("Database connection error. (Don't forget the VPN)");
    }

    connection.Close();
}

app.Run();
