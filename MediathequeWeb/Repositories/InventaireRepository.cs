using Microsoft.EntityFrameworkCore;

public class InventaireRepository : IInventaireRepository
{
    private readonly DataContext _db;

    public InventaireRepository(DataContext db)
    {
        _db = db;
    }

    // Auteurs
    public Auteur? GetAuteur(int id)
    {
        return _db.Auteurs.Include(a => a.Oeuvres).FirstOrDefault(a => a.Id == id);
    }

    public IEnumerable<Auteur> GetAllAuteurs()
    {
        return _db.Auteurs.Include(a => a.Oeuvres).ToList();
    }

    public void AddAuteur(Auteur auteur)
    {
        _db.Auteurs.Add(auteur);
    }

    public void RemoveAuteur(Auteur auteur)
    {
        _db.Auteurs.Remove(auteur);
    }

    // Oeuvres
    public Oeuvre? GetOeuvre(int id)
    {
        return _db
            .Oeuvres.Include(o => o.Auteur)
            .Include(o => o.Exemplaires)
            .FirstOrDefault(o => o.Id == id);
    }

    public IEnumerable<Oeuvre> GetAllOeuvres()
    {
        return _db.Oeuvres.Include(o => o.Auteur).Include(o => o.Exemplaires).ToList();
    }

    public void AddOeuvre(Oeuvre oeuvre)
    {
        _db.Oeuvres.Add(oeuvre);
    }

    public void RemoveOeuvre(Oeuvre oeuvre)
    {
        _db.Oeuvres.Remove(oeuvre);
    }

    // Exemplaires
    public Exemplaire? GetExemplaire(int id)
    {
        return _db
            .Exemplaires.Include(e => e.Oeuvre)
            .ThenInclude(o => o.Auteur)
            .FirstOrDefault(e => e.Id == id);
    }

    public IEnumerable<Exemplaire> GetAllExemplaires()
    {
        return _db.Exemplaires.Include(e => e.Oeuvre).ToList();
    }

    public void AddExemplaire(Exemplaire exemplaire)
    {
        _db.Exemplaires.Add(exemplaire);
    }

    public void RemoveExemplaire(Exemplaire exemplaire)
    {
        _db.Exemplaires.Remove(exemplaire);
    }

    public void SaveChanges()
    {
        _db.SaveChanges();
    }
}
