using System.Configuration;
using JofferWebAPI;
using JofferWebAPI.Swagger;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MySql.Data.MySqlClient;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Security.Claims;
using JofferWebAPI.Context;
﻿// using JofferWebAPI.Context;
using MySql.Data.MySqlClient;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using Microsoft.OpenApi.Models;
using System.Reflection;
//TEST
var builder = WebApplication.CreateBuilder(args);
var domain = $"https://{builder.Configuration["Auth0:Domain"]}/";
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Authority = domain;
        options.Audience = builder.Configuration["Auth0:Audience"];
        options.TokenValidationParameters = new TokenValidationParameters
        {
            NameClaimType = ClaimTypes.NameIdentifier,
            RoleClaimType = "http://www.joffer.com/roles",
        };
    });

builder.Services.AddCors(options =>
{
    options.AddPolicy("CORSPolicy",
        builder =>
        {
            builder
                .AllowAnyMethod()
                .AllowAnyHeader()
                .WithOrigins("http://localhost:3000", "http://localhost:5173", "https://appname.azurestaticapps.net")
                .AllowCredentials(); // Allow credentials if your frontend sends cookies or authorization headers
        });
});

var configuration = builder.Configuration;

builder.Services.AddControllers();
builder.Services.AddDbContext<DbContextRender>(options => options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "API Documentation",
        Version = "v1.0",
        Description = "Your API Description"
    });

    //Add the OAuth2 security scheme definition
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.OAuth2,
        Flows = new OpenApiOAuthFlows
        {
            Implicit = new OpenApiOAuthFlow
            {
                TokenUrl = new Uri($"https://{configuration["Auth0:Domain"]}/oauth/token"),
                AuthorizationUrl = new Uri($"https://{configuration["Auth0:Domain"]}/authorize?audience={configuration["Auth0:Audience"]}"),

                Scopes = new Dictionary<string, string>
                  {
                      { "friet", "saus" } // Modify scopes as needed
                  }
            }
        }
    });

    //Add security requirement to endpoints
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
      {
          {
              new OpenApiSecurityScheme
              {
                  Reference = new OpenApiReference
                  {
                      Type = ReferenceType.SecurityScheme,
                      Id = "oauth2"
                  }
              },
              new[] { "friet" }
          }
      });
});




builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();

var app = builder.Build();

// Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
app.UseSwagger();
app.UseSwaggerUI(settings =>
{
    settings.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1.0");
    settings.OAuthClientId(configuration["Auth0:ClientId"]);
    settings.OAuthClientSecret(configuration["Auth0:ClientSecret"]);
    settings.OAuthUsePkce();
});
// }

//SQL connection
using (NpgsqlConnection connection = new NpgsqlConnection(builder.Configuration.GetConnectionString("DefaultConnection")))
     {
          try
          {
               connection.Open();
               Console.WriteLine("Database connected!");
          }
          catch 
          {
               
               Console.WriteLine("Database connection error. (Don't forget the VPN)");
          }

          connection.Close();
     }
app.UseCors("CORSPolicy"); 
app.UseHttpsRedirection();

// Add these lines before UseEndpoints()

app.UseAuthentication();
app.UseRouting(); 
app.UseAuthorization();


app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.Run();
