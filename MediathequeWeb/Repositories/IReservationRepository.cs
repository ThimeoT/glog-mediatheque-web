public interface IReservationRepository
{
    Reservation? GetReservation(int id);
    IEnumerable<Reservation> GetAllReservations();
    void AddReservation(Reservation reservation);
    void RemoveReservation(Reservation reservation);

    void SaveChanges();
}
