public class AdherentService
{
    private readonly IAdherentRepository _repo;
    private readonly InventaireService _inventoryService;

    public AdherentService(IAdherentRepository repo)
    {
        _repo = repo;
    }

    public Adherent? GetAdherentByID(int id)
    {
        var adherent = _repo.GetAdherent(id) ?? throw new NotFoundException($"L'adherent avec l'Id {id} n'existe pas.");
        return adherent;
    }

    public IEnumerable<Adherent> GetAllAdherents()
    {
        return _repo.GetAllAdherents();
    }

    public void CreateAdherent(string nom, string prenom, string email)
    {
        var newAdherent = new Adherent
        {
            Nom = nom,
            Prenom = prenom,
            Email = email
        };

        _repo.AddAdherent(newAdherent);
    }

    public Adherent UpdateAdherent(int id, string nom, string prenom, string email)
    {
        var adherentToUpdate = GetAdherentByID(id);

        adherentToUpdate.Nom = nom;
        adherentToUpdate.Prenom = prenom;
        adherentToUpdate.Email = email;

        return adherentToUpdate;
    }

    public void DeleteAdherent(int id)
    {
        var adherentToDelete = GetAdherentByID(id);
        _repo.RemoveAdherent(adherentToDelete);

    }

    public Emprunt? GetEmpruntById(int id)
    {
        var emprunt = _repo.GetEmprunt(id);
        if (emprunt == null)
        {
            throw new NotFoundException($"L'emprunt avec l'Id {id} n'existe pas.");
        }
        return emprunt;
    }

    public IEnumerable<Emprunt> GetAllEmprunts()
    {
        return _repo.GetAllEmprunts();
    }

    public void CreateEmprunt(int exemplaireID, int adherentID, string DateRetourPrevue)
    {
        var exemplaire = _inventoryService.GetExemplaireById(exemplaireID); 
        var adherent = GetAdherentByID(adherentID);

        var dt = DateTime.Parse(DateRetourPrevue);              
        var dateRetourPrevue = DateOnly.FromDateTime(dt);      

        var newEmprunt = new Emprunt
        {
            Exemplaire = exemplaire,
            Adherent = adherent,
            DateRetourPrevue = dateRetourPrevue
        };

        _repo.AddEmprunt(newEmprunt);
    }

    public Emprunt UpdateEmprunt(int id, int exemplaireID, int adherentID, string DateRetourPrevue, string DateRetourReelle)
    {
        var empruntToUpdate = GetEmpruntById(id);

        var dt = DateTime.Parse(DateRetourPrevue);
        var dateRetourPrevue = DateOnly.FromDateTime(dt);

        var dtr = DateTime.Parse(DateRetourReelle);
        var dateRetourReelle = DateOnly.FromDateTime(dtr);

        empruntToUpdate.Exemplaire = _inventoryService.GetExemplaireById(exemplaireID);
        empruntToUpdate.Adherent = GetAdherentByID(adherentID);
        empruntToUpdate.DateRetourPrevue = dateRetourPrevue;
        empruntToUpdate.DateRetourReelle = dateRetourReelle;

        return empruntToUpdate;
    }

    public void DeleteEmprunt(int id)
    {
        var empruntToDelete = GetEmpruntById(id);
        _repo.RemoveEmprunt(empruntToDelete);
    }

    public Reservation? GetReservationById(int id)
    {
        var reservation = _repo.GetReservation(id);

        if (reservation == null)
        {
            throw new NotFoundException($"Le reservation avec l'Id {id} n'existe pas.");
        }

        return reservation;
    }

    public IEnumerable<Reservation> GetAllReservations()
    {
        return _repo.GetAllReservations();
    }

    public void CreateReservation(int adherentID, int oeuvreID, string statut)
    {
        var adherent = GetAdherentByID(adherentID);
        var oeuvre = _inventoryService.GetOeuvreById(oeuvreID);
        var statutReservation = Enum.Parse<StatutReservation>(statut, ignoreCase: true);

        var reservation = new Reservation
        {
            Adherent = adherent,
            Statut = statutReservation,
            Oeuvre = oeuvre
        };

        _repo.AddReservation(reservation);
    }

    public Reservation UpdateReservation(int id, int adherentID, int oeuvreID, string statut)
    {
        var reservationToUpdate = GetReservationById(id);

        var adherent = GetAdherentByID(adherentID);
        var oeuvre = _inventoryService.GetOeuvreById(oeuvreID);
        var statutReservation = Enum.Parse<StatutReservation>(statut, ignoreCase: true);

        reservationToUpdate.Adherent = adherent;   
        reservationToUpdate.Oeuvre = _inventoryService.GetOeuvreById(oeuvreID);
        reservationToUpdate.Statut = statutReservation;

        return reservationToUpdate;
    }

    public void DeleteReservation(int id)
    {
        var reservationToDelete = GetReservationById(id);
        _repo.RemoveReservation(reservationToDelete);
    }

}
