public class OeuvreRepository : IOeuvreRepository
{
    private readonly DataContext _db;

    public OeuvreRepository(DataContext db)
    {
        _db = db;
    }

    public Oeuvre? Get(int id)
    {
        return _db.Oeuvres.FirstOrDefault(o => o.Id == id);
    }

    public IEnumerable<Oeuvre> GetAll()
    {
        return _db.Oeuvres.ToList();
    }

    public void Add(Oeuvre oeuvre)
    {
        _db.Oeuvres.Add(oeuvre);
    }

    public void Remove(Oeuvre oeuvre)
    {
        _db.Oeuvres.Remove(oeuvre);
    }
}
