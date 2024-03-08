using JofferWebAPI.Context;
using MySql.Data.MySqlClient;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
// Add services to the container.


builder.Services.AddControllers();
builder.Services.AddDbContext<MyDbContext>(options => options.UseMySQL(configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
     app.UseSwagger();
     app.UseSwaggerUI();
// }

//SQL connection
     using (MySqlConnection connection = new MySqlConnection(builder.Configuration.GetConnectionString("DefaultConnection")))
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

app.UseAuthorization();

app.MapControllers();

app.Run();

