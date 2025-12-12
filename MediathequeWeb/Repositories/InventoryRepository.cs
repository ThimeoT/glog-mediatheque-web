public class InventoryRepository : IInventoryRepository
{
    private readonly DataContext _db;

    public InventoryRepository(DataContext db)
    {
        _db = db;
    }

    public Auteur? GetAuteur(int id)
    {
        return _db.Auteurs.FirstOrDefault(a => a.Id == id);
    }

    public IEnumerable<Auteur> GetAllAuteurs()
    {
        return _db.Auteurs.ToList();
    }

    public void AddAuteur(Auteur auteur)
    {
        _db.Auteurs.Add(auteur);
    }

    public void RemoveAuteur(Auteur auteur)
    {
        _db.Auteurs.Remove(auteur);
    }

    public Oeuvre? GetOeuvre(int id)
    {
        return _db.Oeuvres.FirstOrDefault(o => o.Id == id);
    }

    public IEnumerable<Oeuvre> GetAllOeuvres()
    {
        return _db.Oeuvres.ToList();
    }

    public void AddOeuvre(Oeuvre oeuvre)
    {
        _db.Oeuvres.Add(oeuvre);
    }

    public void RemoveOeuvre(Oeuvre oeuvre)
    {
        _db.Oeuvres.Remove(oeuvre);
    }

    public Exemplaire? GetExemplaire(int id)
    {
        return _db.Exemplaires.FirstOrDefault(e => e.Id == id);
    }

    public IEnumerable<Exemplaire> GetAllExemplaires()
    {
        return _db.Exemplaires.ToList();
    }

    public void AddExemplaire(Exemplaire exemplaire)
    {
        _db.Exemplaires.Add(exemplaire);
    }

    public void RemoveExemplaire(Exemplaire exemplaire)
    {
        _db.Exemplaires.Remove(exemplaire);
    }
}
