using Microsoft.EntityFrameworkCore;

public class ReservationRepository : IReservationRepository
{
    private readonly DataContext _db;

    public ReservationRepository(DataContext db)
    {
        _db = db;
    }

    public Reservation? GetReservation(int id)
    {
        return _db
            .Reservations.Include(r => r.Adherent)
            .Include(r => r.Oeuvre)
            .FirstOrDefault(r => r.Id == id);
    }

    public IEnumerable<Reservation> GetAllReservations()
    {
        return _db
            .Reservations.Include(r => r.Adherent)
            .Include(r => r.Oeuvre)
            .Where(r => r.Statut == StatutReservation.Active)
            .ToList();
    }

    public IEnumerable<Reservation> GetAllReservationsFromAdherentId(int adherentId)
    {
        return _db
            .Reservations.Include(r => r.Adherent)
            .Include(r => r.Oeuvre)
            .Where(r => r.Adherent.Id == adherentId)
            .ToList();
    }

    public IEnumerable<Reservation> GetAllReservationsFromOeuvreId(int oeuvreId)
    {
        return _db
            .Reservations.Include(r => r.Adherent)
            .Include(r => r.Oeuvre)
            .Where(r => r.Oeuvre.Id == oeuvreId)
            .OrderBy(r => r.DateReservation)
            .ToList();
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
