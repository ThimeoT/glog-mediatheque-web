using Microsoft.EntityFrameworkCore;

public class AdherentRepository : IAdherentRepository
{
    private readonly DataContext _db;

    public AdherentRepository(DataContext db)
    {
        _db = db;
    }

    public Adherent? GetAdherent(int id)
    {
        return _db.Adherents.FirstOrDefault(a => a.Id == id);
    }

    public IEnumerable<Adherent> GetAllAdherents()
    {
        return _db.Adherents.ToList();
    }

    public void AddAdherent(Adherent adherent)
    {
        _db.Adherents.Add(adherent);
    }

    public void RemoveAdherent(Adherent adherent)
    {
        _db.Adherents.Remove(adherent);
    }

    public Emprunt? GetEmprunt(int id)
    {
        return _db.Emprunts.FirstOrDefault(e => e.Id == id);
    }

    public IEnumerable<Emprunt> GetAllEmprunts()
    {
        return _db.Emprunts.ToList();
    }

    public void AddEmprunt(Emprunt emprunt)
    {
        _db.Emprunts.Add(emprunt);
    }

    public void RemoveEmprunt(Emprunt emprunt)
    {
        _db.Emprunts.Remove(emprunt);
    }

    public Reservation? GetReservation(int id)
    {
        return _db.Reservations.FirstOrDefault(r => r.Id == id);
    }

    public IEnumerable<Reservation> GetAllReservations()
    {
        return _db.Reservations.ToList();
    }

    public void AddReservation(Reservation reservation)
    {
        _db.Reservations.Add(reservation);
    }

    public void RemoveReservation(Reservation reservation)
    {
        _db.Reservations.Remove(reservation);
    }
}
