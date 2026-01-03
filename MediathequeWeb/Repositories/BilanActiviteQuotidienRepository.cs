using Microsoft.EntityFrameworkCore;

public class BilanActiviteQuotidienRepository : IBilanActiviteQuotidienRepository
{
    private readonly DataContext _db;

    public BilanActiviteQuotidienRepository(DataContext db)
    {
        _db = db;
    }

    public IEnumerable<BilanActiviteQuotidien> GetAllBilansActiviteQuotidiens()
    {
        return _db.BilanActiviteQuotidien.ToList();
    }

    public BilanActiviteQuotidien? GetBilanActiviteQuotidienFromId(int id)
    {
        return _db.BilanActiviteQuotidien.FirstOrDefault(s => s.Id == id);
    }

    public BilanActiviteQuotidien? GetBilanActiviteQuotidienFromDate(DateOnly date)
    {
        return _db.BilanActiviteQuotidien.FirstOrDefault(s => s.Date == date);
    }

    public bool ExistsFromDate(DateOnly date)
    {
        return _db.BilanActiviteQuotidien.Any(s => s.Date == date);
    }

    public void AddBilanActiviteQuotidien(BilanActiviteQuotidien bilanActiviteQuotidien)
    {
        _db.BilanActiviteQuotidien.Add(bilanActiviteQuotidien);
    }

    public void RemoveBilanActiviteQuotidien(BilanActiviteQuotidien bilanActiviteQuotidien)
    {
        _db.BilanActiviteQuotidien.Remove(bilanActiviteQuotidien);
    }

    public void SaveChanges()
    {
        _db.SaveChanges();
    }
}
