public interface IReservationRepository
{
    Reservation? Get(int id);
    IEnumerable<Reservation> GetAll();
    void Add(Reservation reservation);
    void Remove(Reservation reservation);
}
