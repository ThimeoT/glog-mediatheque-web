using Microsoft.EntityFrameworkCore;

public class EmpruntRepository : IEmpruntRepository
{
    private readonly DataContext _db;

    public EmpruntRepository(DataContext db)
    {
        _db = db;
    }

    public Emprunt? GetEmprunt(int id)
    {
        return _db
            .Emprunts.Include(e => e.Exemplaire)
                .ThenInclude(ex => ex.Oeuvre)
            .Include(e => e.Adherent)
            .FirstOrDefault(e => e.Id == id);
    }

    public IEnumerable<Emprunt> GetAllEmprunts()
    {
        return _db
            .Emprunts.Include(e => e.Adherent)
            .Include(e => e.Exemplaire)
                .ThenInclude(ex => ex.Oeuvre)
            .ToList();
    }

    public void AddEmprunt(Emprunt emprunt)
    {
        _db.Emprunts.Add(emprunt);
    }

    public void RemoveEmprunt(Emprunt emprunt)
    {
        _db.Emprunts.Remove(emprunt);
    }

    public void SaveChanges()
    {
        _db.SaveChanges();
    }
}
