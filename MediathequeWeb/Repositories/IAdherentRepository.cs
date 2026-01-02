public interface IAdherentRepository
{
    Adherent? GetAdherent(int id);
    bool AdherentExists(int adherentId);
    IEnumerable<Adherent> GetAllAdherents();
    void AddAdherent(Adherent adherent);
    void RemoveAdherent(Adherent adherent);
    void SaveChanges();
}
