using MySql.Data.MySqlClient;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

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
               
               throw new Exception("Database connection error. (Don't forget the VPN)");
          }

          connection.Close();
     }

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

