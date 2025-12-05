public class ReservationRepository : IReservationRepository
{
    private readonly DataContext _db;

    public ReservationRepository(DataContext db)
    {
        _db = db;
    }

    public Reservation? Get(int id)
    {
        return _db.Reservations.FirstOrDefault(r => r.Id == id);
    }

    public IEnumerable<Reservation> GetAll()
    {
        return _db.Reservations.ToList();
    }

    public void Add(Reservation reservation)
    {
        _db.Reservations.Add(reservation);
    }

    public void Remove(Reservation reservation)
    {
        _db.Reservations.Remove(reservation);
    }
}
