using Scalar.AspNetCore;

// Builder
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddOpenApi();
builder.Services.AddControllers();

// Application
var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
    SeedData.Initialize();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();
