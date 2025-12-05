public class EmpruntRepository : IEmpruntRepository
{
    private readonly DataContext _db;

    public EmpruntRepository(DataContext db)
    {
        _db = db;
    }

    public Emprunt? Get(int id)
    {
        return _db.Emprunts.FirstOrDefault(e => e.Id == id);
    }

    public IEnumerable<Emprunt> GetAll()
    {
        return _db.Emprunts.ToList();
    }

    public void Add(Emprunt emprunt)
    {
        _db.Emprunts.Add(emprunt);
    }

    public void Remove(Emprunt emprunt)
    {
        _db.Emprunts.Remove(emprunt);
    }
}
