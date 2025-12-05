using Microsoft.EntityFrameworkCore;

public class AdherentRepository : IAdherentRepository
{
    private readonly DataContext _db;

    public AdherentRepository(DataContext db)
    {
        _db = db;
    }

    public Adherent? Get(int id)
    {
        return _db.Adherents.FirstOrDefault(p => p.Id == id);
    }

    public IEnumerable<Adherent> GetAll()
    {
        return _db.Adherents.ToList();
    }

    public void Add(Adherent adherent)
    {
        _db.Adherents.Add(adherent);
    }

    public void Remove(Adherent adherent)
    {
        _db.Adherents.Remove(adherent);
    }
}
