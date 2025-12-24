using Microsoft.EntityFrameworkCore;

public class ReservationRepository : IReservationRepository
{
    private readonly DataContext _db;

    public ReservationRepository(DataContext db)
    {
        _db = db;
    }

    // Réservations
    public Reservation? GetReservation(int id)
    {
        return _db
            .Reservations.Include(r => r.Adherent)
            .Include(r => r.Oeuvre)
            .FirstOrDefault(r => r.Id == id);
    }

    public IEnumerable<Reservation> GetAllReservations()
    {
        return _db.Reservations.Include(r => r.Oeuvre).ToList();
    }
    public IEnumerable<Reservation> GetAllReservationsByAdherentId(int adherentId)
    {
        return _db.Reservations.Where(r => r.Adherent.Id == adherentId).ToList();
    }
    
    public IEnumerable<Reservation> GetAllReservationsByOeuvreId(int oeuvreId)
    {
        return _db.Reservations.Where(r => r.Oeuvre.Id == oeuvreId).ToList();
    }
    

    public void AddReservation(Reservation reservation)
    {
        _db.Reservations.Add(reservation);
    }

    public void RemoveReservation(Reservation reservation)
    {
        _db.Reservations.Remove(reservation);
    }

    public void SaveChanges()
    {
        _db.SaveChanges();
    }
}
