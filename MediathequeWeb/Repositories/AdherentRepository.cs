using Microsoft.EntityFrameworkCore;

public class AdherentRepository : IAdherentRepository
{
    private readonly DataContext _db;

    public AdherentRepository(DataContext db)
    {
        _db = db;
    }

    public Adherent? GetAdherent(int id)
    {
        return _db
            .Adherents.Include(a => a.Emprunts)
            .ThenInclude(e => e.Exemplaire)
            .ThenInclude(ex => ex.Oeuvre)
            .FirstOrDefault(a => a.Id == id);
    }

    public bool AdherentExists(int adherentId)
    {
        return _db.Adherents.Any(a => a.Id == adherentId);
    }

    public IEnumerable<Adherent> GetAllAdherents()
    {
        return _db
            .Adherents.Include(a => a.Emprunts)
            .ThenInclude(e => e.Exemplaire)
            .ThenInclude(ex => ex.Oeuvre)
            .ToList();
    }

    public void AddAdherent(Adherent adherent)
    {
        _db.Adherents.Add(adherent);
    }

    public void RemoveAdherent(Adherent adherent)
    {
        _db.Adherents.Remove(adherent);
    }

    public void SaveChanges()
    {
        _db.SaveChanges();
    }
}
