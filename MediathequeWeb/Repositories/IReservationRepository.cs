public interface IReservationRepository
{
    Reservation? GetReservation(int id);
    IEnumerable<Reservation> GetAllReservations();
    IEnumerable<Reservation> GetAllReservationsFromAdherentId(int adherentId);
    IEnumerable<Reservation> GetAllReservationsFromOeuvreId(int oeuvreId);
    void AddReservation(Reservation reservation);
    void RemoveReservation(Reservation reservation);

    void SaveChanges();
}
