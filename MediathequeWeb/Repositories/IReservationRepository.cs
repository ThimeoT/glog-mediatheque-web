public interface IReservationRepository
{
    Reservation? GetReservation(int id);
    IEnumerable<Reservation> GetAllReservations();
    IEnumerable<Reservation> GetAllReservationsByAdherentId(int adherentId);
    IEnumerable<Reservation> GetAllReservationsByOeuvreId(int oeuvreId);
    void AddReservation(Reservation reservation);
    void RemoveReservation(Reservation reservation);

    void SaveChanges();
}
