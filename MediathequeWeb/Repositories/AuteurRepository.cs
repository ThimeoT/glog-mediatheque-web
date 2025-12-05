public class AuteurRepository : IAuteurRepository
{
    private readonly DataContext _db;

    public AuteurRepository(DataContext db)
    {
        _db = db;
    }

    public Auteur? Get(int id)
    {
        return _db.Auteurs.FirstOrDefault(a => a.Id == id);
    }

    public IEnumerable<Auteur> GetAll()
    {
        return _db.Auteurs.ToList();
    }

    public void Add(Auteur auteur)
    {
        _db.Auteurs.Add(auteur);
    }

    public void Remove(Auteur auteur)
    {
        _db.Auteurs.Remove(auteur);
    }
}
