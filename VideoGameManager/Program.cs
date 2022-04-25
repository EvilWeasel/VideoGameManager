var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

/// previously in dotnet5:
/// this used the CreateHostBuilder(args).Build().Run();
/// Now the building of the WebHost is done in the multiple steps below,
/// which is a little more code, but much more readable and also allows us
/// to omit the Startup.cs (formally know as 'Spaghetti.cs') file
/// For this course, we won't change anything here!
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
