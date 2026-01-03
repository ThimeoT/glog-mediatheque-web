public class ReservationService
{
    private readonly IAdherentRepository _adherentRepo;
    private readonly IInventaireRepository _inventaireRepo;
    private readonly IReservationRepository _reservationRepo;
    private readonly IStatistiquesRepository _statistiquesRepo;

    public ReservationService(
        IAdherentRepository adherentRepo,
        IInventaireRepository inventaireRepo,
        IReservationRepository reservationRepo,
        IStatistiquesRepository statistiquesRepo
    )
    {
        _adherentRepo = adherentRepo;
        _inventaireRepo = inventaireRepo;
        _reservationRepo = reservationRepo;
        _statistiquesRepo = statistiquesRepo;
    }

    public IEnumerable<Reservation> GetAllReservations()
    {
        return _reservationRepo.GetAllReservations();
    }

    public IEnumerable<Reservation> GetAllReservationsFromAdherentId(int adherentId)
    {
        if (!_adherentRepo.AdherentExists(adherentId))
            throw new NotFoundException($"L’adhérent avec l’id {adherentId} n’existe pas.");
        return _reservationRepo.GetAllReservationsFromAdherentId(adherentId);
    }

    public IEnumerable<Reservation> GetAllReservationsFromOeuvreId(int oeuvreId)
    {
        if (!_inventaireRepo.OeuvreExists(oeuvreId))
            throw new NotFoundException($"L’œuvre avec l’id {oeuvreId} n’existe pas.");
        return _reservationRepo.GetAllReservationsFromOeuvreId(oeuvreId);
    }

    public Reservation CreateReservation(int adherentId, int oeuvreId)
    {
        Adherent adherent =
            _adherentRepo.GetAdherent(adherentId)
            ?? throw new NotFoundException("Adhérent introuvable.");

        Oeuvre oeuvre =
            _inventaireRepo.GetOeuvre(oeuvreId)
            ?? throw new NotFoundException("Œuvre introuvable.");

        if (_inventaireRepo.ExisteExemplaireDisponiblePourOeuvre(oeuvreId))
            throw new DomainValidationException(
                "Pas besoin de réserver, un exemplaire est déjà disponible !"
            );

        if (_reservationRepo.ReservationActiveExists(adherentId, oeuvreId))
            throw new DomainValidationException(
                "Une réservation active existe déjà pour cette œuvre."
            );

        Reservation reservation = new()
        {
            Adherent = adherent,
            Oeuvre = oeuvre,
            DateReservation = DateOnly.FromDateTime(DateTime.Now),
            Statut = StatutReservation.Active,
        };
        _reservationRepo.AddReservation(reservation);
        IncrementNouvellesReservationsStatistiques(reservation.DateReservation);
        _reservationRepo.SaveChanges();

        return reservation;
    }

    public void DeleteReservation(int reservationId)
    {
        Reservation reservation =
            _reservationRepo.GetReservation(reservationId)
            ?? throw new NotFoundException("Réservation introuvable.");
        DecrementNouvellesReservationsStatistiques(reservation.DateReservation);
        _reservationRepo.RemoveReservation(reservation);
        _reservationRepo.SaveChanges();
    }

    public void IncrementNouvellesReservationsStatistiques(DateOnly date)
    {
        Statistiques? statistiques = _statistiquesRepo.GetStatistiquesFromDate(date);
        if (statistiques == null)
            _statistiquesRepo.AddStatistiques(
                new Statistiques { Date = date, NouvellesReservations = 1 }
            );
        else
            statistiques.NouvellesReservations += 1;
        _statistiquesRepo.SaveChanges();
    }

    public void DecrementNouvellesReservationsStatistiques(DateOnly date)
    {
        var suivi =
            _statistiquesRepo.GetStatistiquesFromDate(date)
            ?? throw new NotFoundException($"Aucune activité trouvée pour la date {date}.");

        if (suivi.NouvellesReservations <= 0)
            throw new DomainValidationException(
                "Aucunne nouvelle réservation enregistrée pour cette date."
            );

        suivi.NouvellesReservations -= 1;
        _statistiquesRepo.SaveChanges();
    }
}
