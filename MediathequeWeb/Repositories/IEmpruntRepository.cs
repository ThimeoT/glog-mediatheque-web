public interface IEmpruntRepository
{
    Emprunt? Get(int id);
    IEnumerable<Emprunt> GetAll();
    void Add(Emprunt emprunt);
    void Remove(Emprunt emprunt);
}
