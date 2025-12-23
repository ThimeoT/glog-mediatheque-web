using Microsoft.EntityFrameworkCore;

public class AdherentRepository : IAdherentRepository
{
    private readonly DataContext _db;

    public AdherentRepository(DataContext db)
    {
        _db = db;
    }

    // Adhérents
    public Adherent? GetAdherent(int id)
    {
        return _db.Adherents
            .Include(a => a.Emprunts)
                .ThenInclude(e => e.Exemplaire)
                    .ThenInclude(ex => ex.Oeuvre)
            .Include(a => a.Reservations)
                .ThenInclude(r => r.Oeuvre)
            .FirstOrDefault(a => a.Id == id);
    }

    public IEnumerable<Adherent> GetAllAdherents()
    {
        return _db.Adherents
            .Include(a => a.Emprunts)
            .ToList();
    }

    public void AddAdherent(Adherent adherent)
    {
        _db.Adherents.Add(adherent);
    }

    public void RemoveAdherent(Adherent adherent)
    {
        _db.Adherents.Remove(adherent);
    }

    // Emprunts
    public Emprunt? GetEmprunt(int id)
    {
        return _db.Emprunts
            .Include(e => e.Exemplaire)
                .ThenInclude(ex => ex.Oeuvre)
            .Include(e => e.Adherent)
            .FirstOrDefault(e => e.Id == id);
    }

    public IEnumerable<Emprunt> GetAllEmprunts()
    {
        return _db.Emprunts
            .Include(e => e.Exemplaire)
                .ThenInclude(ex => ex.Oeuvre)
            .ToList();
    }

    public void AddEmprunt(Emprunt emprunt)
    {
        _db.Emprunts.Add(emprunt);
    }

    public void RemoveEmprunt(Emprunt emprunt)
    {
        _db.Emprunts.Remove(emprunt);
    }

    // Réservations
    public Reservation? GetReservation(int id)
    {
        return _db.Reservations
            .Include(r => r.Adherent)
            .Include(r => r.Oeuvre)
            .FirstOrDefault(r => r.Id == id);
    }

    public IEnumerable<Reservation> GetAllReservations()
    {
        return _db.Reservations
            .Include(r => r.Oeuvre)
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
