using Microsoft.EntityFrameworkCore;

public class SuiviActiviteRepository : ISuiviActiviteRepository
{
    private readonly DataContext _db;

    public SuiviActiviteRepository(DataContext db)
    {
        _db = db;
    }

    public IEnumerable<SuiviActivite> GetAllSuiviActivite()
    {
        return _db.SuiviActivite.ToList();
    }

    public SuiviActivite? GetSuiviActiviteFromId(int id)
    {
        return _db.SuiviActivite.FirstOrDefault(s => s.Id == id);
    }

    public SuiviActivite? GetSuiviActiviteFromDate(DateOnly date)
    {
        return _db.SuiviActivite.FirstOrDefault(s => s.Date == date);
    }

    public bool ExistsFromDate(DateOnly date)
    {
        return _db.SuiviActivite.Any(s => s.Date == date);
    }

    public void AddSuiviActivite(SuiviActivite suiviActivite)
    {
        _db.SuiviActivite.Add(suiviActivite);
    }

    public void DeleteSuiviActivite(SuiviActivite suiviActivite)
    {
        _db.SuiviActivite.Remove(suiviActivite);
    }

    public void SaveChanges()
    {
        _db.SaveChanges();
    }
}
