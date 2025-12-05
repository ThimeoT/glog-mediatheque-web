public interface IExemplaireRepository
{
    Exemplaire? Get(int id);
    IEnumerable<Exemplaire> GetAll();
    void Add(Exemplaire exemplaire);
    void Remove(Exemplaire exemplaire);
}
