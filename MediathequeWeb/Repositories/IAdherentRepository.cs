public interface IAdherentRepository
{
    Adherent? GetAdherent(int id);
    IEnumerable<Adherent> GetAllAdherents();
    void AddAdherent(Adherent adherent);
    void RemoveAdherent(Adherent adherent);

    Emprunt? GetEmprunt(int id);
    IEnumerable<Emprunt> GetAllEmprunts();
    void AddEmprunt(Emprunt emprunt);
    void RemoveEmprunt(Emprunt emprunt);

    Reservation? GetReservation(int id);
    IEnumerable<Reservation> GetAllReservations();
    void AddReservation(Reservation reservation);
    void RemoveReservation(Reservation reservation);

    void SaveChanges();
}
