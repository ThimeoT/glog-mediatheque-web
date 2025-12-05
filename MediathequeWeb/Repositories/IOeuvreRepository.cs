public interface IOeuvreRepository
{
    Oeuvre? Get(int id);
    IEnumerable<Oeuvre> GetAll();
    void Add(Oeuvre oeuvre);
    void Remove(Oeuvre oeuvre);
}
