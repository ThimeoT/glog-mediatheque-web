public interface IAdherentRepository
{
    Adherent? Get(int id);
    IEnumerable<Adherent> GetAll();
    void Add(Adherent adherent);
    void Remove(Adherent adherent);
}
