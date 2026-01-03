public interface IAdherentRepository
{
    Adherent? GetAdherent(int id);
    bool AdherentExists(int adherentId);
    IEnumerable<Adherent> GetAllAdherents();
    int GetNombreEmpruntsEnCours(int adherentId);
    int GetNombreReservationsActives(int adherentId);
    int GetNombreEmpruntsTotal(int adherentId);
    void AddAdherent(Adherent adherent);
    void RemoveAdherent(Adherent adherent);
    void SaveChanges();
}
