public class ExemplaireRepository : IExemplaireRepository
{
    private readonly DataContext _db;

    public ExemplaireRepository(DataContext db)
    {
        _db = db;
    }

    public Exemplaire? Get(int id)
    {
        return _db.Exemplaires.FirstOrDefault(e => e.Id == id);
    }

    public IEnumerable<Exemplaire> GetAll()
    {
        return _db.Exemplaires.ToList();
    }

    public void Add(Exemplaire exemplaire)
    {
        _db.Exemplaires.Add(exemplaire);
    }

    public void Remove(Exemplaire exemplaire)
    {
        _db.Exemplaires.Remove(exemplaire);
    }
}
