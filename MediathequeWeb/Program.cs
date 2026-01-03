using Scalar.AspNetCore;

// Builder
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddOpenApi();
builder.Services.AddControllers();
builder.Services.AddDbContext<DataContext>();
builder.Services.AddScoped<IAdherentRepository, AdherentRepository>();
builder.Services.AddScoped<IInventaireRepository, InventaireRepository>();
builder.Services.AddScoped<IEmpruntRepository, EmpruntRepository>();
builder.Services.AddScoped<IReservationRepository, ReservationRepository>();
builder.Services.AddScoped<IStatistiquesRepository, StatistiquesRepository>();
builder.Services.AddScoped<AdherentService>();
builder.Services.AddScoped<InventaireService>();
builder.Services.AddScoped<EmpruntService>();
builder.Services.AddScoped<ReservationService>();
builder.Services.AddScoped<StatistiquesService>();

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
