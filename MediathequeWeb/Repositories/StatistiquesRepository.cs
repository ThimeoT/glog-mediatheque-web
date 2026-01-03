using Microsoft.EntityFrameworkCore;

public class StatistiquesRepository : IStatistiquesRepository
{
    private readonly DataContext _db;

    public StatistiquesRepository(DataContext db)
    {
        _db = db;
    }

    public IEnumerable<Statistiques> GetAllStatistiques()
    {
        return _db.Statistiques.ToList();
    }

    public Statistiques? GetStatistiquesFromId(int id)
    {
        return _db.Statistiques.FirstOrDefault(s => s.Id == id);
    }

    public Statistiques? GetStatistiquesFromDate(DateOnly date)
    {
        return _db.Statistiques.FirstOrDefault(s => s.Date == date);
    }

    public bool ExistsFromDate(DateOnly date)
    {
        return _db.Statistiques.Any(s => s.Date == date);
    }

    public void AddStatistiques(Statistiques statistiques)
    {
        _db.Statistiques.Add(statistiques);
    }

    public void RemoveStatistiques(Statistiques statistiques)
    {
        _db.Statistiques.Remove(statistiques);
    }

    public void SaveChanges()
    {
        _db.SaveChanges();
    }
}
