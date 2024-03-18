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

var configuration = builder.Configuration;

builder.Services.AddControllers();
builder.Services.AddDbContext<DbContextRender>(options => options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();

var app = builder.Build();

// Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
     app.UseSwagger();
     app.UseSwaggerUI();
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
