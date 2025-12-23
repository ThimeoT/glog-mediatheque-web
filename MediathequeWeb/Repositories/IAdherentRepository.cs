public interface IAdherentRepository
{
    Adherent? GetAdherent(int id);
    IEnumerable<Adherent> GetAllAdherents();
    void AddAdherent(Adherent adherent);
    void RemoveAdherent(Adherent adherent);
    void SaveChanges();
}
