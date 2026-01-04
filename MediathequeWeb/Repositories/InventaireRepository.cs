using Microsoft.EntityFrameworkCore;

public class InventaireRepository : IInventaireRepository
{
    private readonly DataContext _db;

    public InventaireRepository(DataContext db)
    {
        _db = db;
    }

    // Auteurs
    public Auteur? GetAuteur(int auteurId)
    {
        return _db.Auteurs.Include(a => a.Oeuvres).FirstOrDefault(a => a.Id == auteurId);
    }

    public IEnumerable<Auteur> GetAllAuteurs()
    {
        return _db.Auteurs.Include(a => a.Oeuvres).ToList();
    }

    public void AddAuteur(Auteur auteur)
    {
        _db.Auteurs.Add(auteur);
    }

    public void RemoveAuteur(Auteur auteur)
    {
        _db.Auteurs.Remove(auteur);
    }

    // Oeuvres
    public Oeuvre? GetOeuvre(int oeuvreId)
    {
        return _db
            .Oeuvres.Include(o => o.Auteur)
            .Include(o => o.Exemplaires)
                .ThenInclude(e => e.Emprunts)
            .Include(o => o.Reservations)
            .FirstOrDefault(o => o.Id == oeuvreId);
    }

    public bool OeuvreExists(int oeuvreId)
    {
        return _db.Oeuvres.Any(o => o.Id == oeuvreId);
    }

    public IEnumerable<Oeuvre> GetAllOeuvres()
    {
        return _db
            .Oeuvres.Include(o => o.Auteur)
            .Include(o => o.Exemplaires)
                .ThenInclude(e => e.Emprunts)
            .Include(o => o.Reservations)
            .ToList();
    }

    public int GetNombreEmpruntsTotalPourOeuvre(int oeuvreId)
    {
        return _db.Emprunts.Count(e => e.Exemplaire.Oeuvre.Id == oeuvreId);
    }

    public int GetReservationsActivesPourOeuvre(int oeuvreId)
    {
        return _db.Reservations.Count(r =>
            r.Oeuvre.Id == oeuvreId && r.Statut == StatutReservation.Active
        );
    }

    public void AddOeuvre(Oeuvre oeuvre)
    {
        _db.Oeuvres.Add(oeuvre);
    }

    public void RemoveOeuvre(Oeuvre oeuvre)
    {
        _db.Oeuvres.Remove(oeuvre);
    }

    // Exemplaires
    public Exemplaire? GetExemplaire(int id)
    {
        return _db
            .Exemplaires.Include(e => e.Oeuvre)
                .ThenInclude(o => o.Auteur)
            .FirstOrDefault(e => e.Id == id);
    }

    public IEnumerable<Exemplaire> GetAllExemplaires()
    {
        return _db.Exemplaires.Include(e => e.Oeuvre).ToList();
    }

    public IEnumerable<Exemplaire> GetAllExemplairesFromOeuvreId(int oeuvreId)
    {
        return _db.Exemplaires.Include(e => e.Oeuvre).Where(e => e.Oeuvre.Id == oeuvreId).ToList();
    }

    public void AddExemplaire(Exemplaire exemplaire)
    {
        _db.Exemplaires.Add(exemplaire);
    }

    public void RemoveExemplaire(Exemplaire exemplaire)
    {
        _db.Exemplaires.Remove(exemplaire);
    }

    public void SaveChanges()
    {
        _db.SaveChanges();
    }

    public bool ExisteExemplaireDisponiblePourOeuvre(int oeuvreId)
    {
        return _db.Exemplaires.Any(e =>
            e.Oeuvre.Id == oeuvreId && e.Statut == StatutExemplaire.Disponible
        );
    }
}
