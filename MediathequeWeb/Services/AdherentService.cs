public class AdherentService
{
    private readonly IAdherentRepository _repo;
    private readonly InventoryService _inventoryService;

    public AdherentService(IAdherentRepository repo)
    {
        _repo = repo;
    }

    public Adherent? GetAdherentByID(int id)
    {
        var adherent =
            _repo.GetAdherent(id)
            ?? throw new NotFoundException($"L'adhérent avec l'Id {id} n'existe pas.");
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
            Email = email,
        };

        _repo.AddAdherent(newAdherent);
    }

    public Adherent UpdateAdherent(int id, string nom, string prenom, string email)
    {
        var adherentToUpdate =
            GetAdherentByID(id)
            ?? throw new NotFoundException($"L'adhérent avec l'Id {id} n'existe pas.");
        adherentToUpdate.Nom = nom;
        adherentToUpdate.Prenom = prenom;
        adherentToUpdate.Email = email;

        return adherentToUpdate;
    }

    public void DeleteAdherent(int id)
    {
        var adherentToDelete =
            GetAdherentByID(id)
            ?? throw new NotFoundException($"L'adhérent avec l'Id {id} n'existe pas.");
        _repo.RemoveAdherent(adherentToDelete);
    }

    public Emprunt? GetEmpruntById(int id)
    {
        var emprunt =
            _repo.GetEmprunt(id)
            ?? throw new NotFoundException($"L'emprunt avec l'Id {id} n'existe pas.");
        return emprunt;
    }

    public IEnumerable<Emprunt> GetAllEmprunts()
    {
        return _repo.GetAllEmprunts();
    }

    public void CreateEmprunt(int exemplaireID, int adherentID, string DateRetourPrevue)
    {
        var exemplaire =
            _inventoryService.GetExemplaireById(exemplaireID)
            ?? throw new NotFoundException($"L'exemplaire avec l'Id {exemplaireID} n'existe pas.");
        var adherent =
            GetAdherentByID(adherentID)
            ?? throw new NotFoundException($"L'adhérent avec l'Id {adherentID} n'existe pas.");

        var dt = DateTime.Parse(DateRetourPrevue);
        var dateRetourPrevue = DateOnly.FromDateTime(dt);

        var newEmprunt = new Emprunt
        {
            Exemplaire = exemplaire,
            Adherent = adherent,
            DateRetourPrevue = dateRetourPrevue,
        };

        _repo.AddEmprunt(newEmprunt);
    }

    public Emprunt UpdateEmprunt(
        int id,
        int exemplaireID,
        int adherentID,
        string DateRetourPrevue,
        string DateRetourReelle
    )
    {
        var empruntToUpdate =
            GetEmpruntById(id)
            ?? throw new NotFoundException($"L'emprunt avec l'Id {id} n'existe pas.");

        var dt = DateTime.Parse(DateRetourPrevue);
        var dateRetourPrevue = DateOnly.FromDateTime(dt);

        var dtr = DateTime.Parse(DateRetourReelle);
        var dateRetourReelle = DateOnly.FromDateTime(dtr);

        empruntToUpdate.Exemplaire =
            _inventoryService.GetExemplaireById(exemplaireID)
            ?? throw new NotFoundException($"L'emprunt avec l'Id {exemplaireID} n'existe pas.");
        empruntToUpdate.Adherent =
            GetAdherentByID(adherentID)
            ?? throw new NotFoundException($"L'adhérent avec l'Id {adherentID} n'existe pas.");
            
        empruntToUpdate.DateRetourPrevue = dateRetourPrevue;
        empruntToUpdate.DateRetourReelle = dateRetourReelle;

        return empruntToUpdate;
    }

    public void DeleteEmprunt(int id)
    {
        var empruntToDelete =
            GetEmpruntById(id)
            ?? throw new NotFoundException($"L'emprunt avec l'Id {id} n'existe pas.");
        _repo.RemoveEmprunt(empruntToDelete);
    }

    public Reservation? GetReservationById(int id)
    {
        var reservation =
            _repo.GetReservation(id)
            ?? throw new NotFoundException($"La réservation avec l'Id {id} n'existe pas.");
        return reservation;
    }

    public IEnumerable<Reservation> GetAllReservations()
    {
        return _repo.GetAllReservations();
    }

    public void CreateReservation(int adherentID, int oeuvreID, string statut)
    {
        var adherent =
            GetAdherentByID(adherentID)
            ?? throw new NotFoundException($"L'adhérent avec l'Id {adherentID} n'existe pas.");
        var oeuvre =
            _inventoryService.GetOeuvreById(oeuvreID)
            ?? throw new NotFoundException($"L'oeuvre avec l'Id {oeuvreID} n'existe pas.");
        var statutReservation = Enum.Parse<StatutReservation>(statut, ignoreCase: true);

        var reservation = new Reservation
        {
            Adherent = adherent,
            Statut = statutReservation,
            Oeuvre = oeuvre,
        };

        _repo.AddReservation(reservation);
    }

    public Reservation UpdateReservation(
        int reservationId,
        int adherentId,
        int oeuvreId,
        string statut
    )
    {
        var reservationToUpdate =
            GetReservationById(reservationId)
            ?? throw new NotFoundException(
                $"La réservation avec l'Id {reservationId} n'existe pas."
            );
        var adherent =
            GetAdherentByID(adherentId)
            ?? throw new NotFoundException($"L'adhérent avec l'Id {adherentId} n'existe pas.");
        var oeuvre =
            _inventoryService.GetOeuvreById(oeuvreId)
            ?? throw new NotFoundException($"L'oeuvre avec l'Id {oeuvreId} n'existe pas.");
        var statutReservation = Enum.Parse<StatutReservation>(statut, ignoreCase: true);

        reservationToUpdate.Adherent = adherent;
        reservationToUpdate.Oeuvre = oeuvre;
        reservationToUpdate.Statut = statutReservation;

        return reservationToUpdate;
    }

    public void DeleteReservation(int id)
    {
        var reservationToDelete =
            GetReservationById(id)
            ?? throw new NotFoundException($"La réservation avec l'Id {id} n'existe pas.");
        _repo.RemoveReservation(reservationToDelete);
    }
}
