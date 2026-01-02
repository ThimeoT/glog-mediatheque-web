using Microsoft.EntityFrameworkCore;

public class DataContext : DbContext
{
    private const string DbPath = "data.sqlite";

    public DbSet<Adherent> Adherents { get; set; }
    public DbSet<Auteur> Auteurs { get; set; }
    public DbSet<Emprunt> Emprunts { get; set; }
    public DbSet<Exemplaire> Exemplaires { get; set; }
    public DbSet<Oeuvre> Oeuvres { get; set; }
    public DbSet<Reservation> Reservations { get; set; }
    public DbSet<SuiviActiviteQuotidien> SuiviActiviteQuotidien { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseSqlite($"Data Source={DbPath}");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<SuiviActiviteQuotidien>().HasIndex(m => m.DateId).IsUnique();
    }
}
