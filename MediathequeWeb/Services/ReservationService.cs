public class ReservationService
{
    private readonly IAdherentRepository _adherentRepo;
    private readonly IInventaireRepository _inventaireRepo;
    private readonly IReservationRepository _reservationRepo;

    public ReservationService(
        IAdherentRepository adherentRepo,
        IInventaireRepository inventaireRepo,
        IReservationRepository reservationRepo
    )
    {
        _adherentRepo = adherentRepo;
        _inventaireRepo = inventaireRepo;
        _reservationRepo = reservationRepo;
    }

    public IEnumerable<Reservation> GetAllReservations()
    {
        return _reservationRepo.GetAllReservations();
    }

    public IEnumerable<Reservation> GetAllReservationsFromAdherentId(int adherentId)
    {
        if (!_adherentRepo.AdherentExists(adherentId))
            throw new NotFoundException($"L'adhérent avec l'id {adherentId} n'existe pas");
        return _reservationRepo.GetAllReservationsFromAdherentId(adherentId);
    }

    public IEnumerable<Reservation> GetAllReservationsFromOeuvreId(int oeuvreId)
    {
        if (!_inventaireRepo.OeuvreExists(oeuvreId))
            throw new NotFoundException($"L'oeuvre avec l'id {oeuvreId} n'existe pas");
        return _reservationRepo.GetAllReservationsFromOeuvreId(oeuvreId);
    }

    public Reservation CreerReservation(int adherentId, int oeuvreId)
    {
        Adherent adherent =
            _adherentRepo.GetAdherent(adherentId)
            ?? throw new NotFoundException("Adhérent introuvable.");

        Oeuvre oeuvre =
            _inventaireRepo.GetOeuvre(oeuvreId)
            ?? throw new NotFoundException("Oeuvre introuvable.");

        bool exemplaireDisponible = _inventaireRepo
            .GetAllExemplaires()
            .Any(e => e.Oeuvre.Id == oeuvreId && e.Statut == StatutExemplaire.Disponible);

        if (exemplaireDisponible)
            throw new DomainValidationException(
                "Un exemplaire est disponible, réservation impossible."
            );

        bool dejaReserve = adherent.Reservations.Any(r =>
            r.Oeuvre.Id == oeuvreId && r.Statut == StatutReservation.Active
        );

        if (dejaReserve)
            throw new DomainValidationException("Réservation déjà existante pour cette œuvre.");

        Reservation reservation = new()
        {
            Adherent = adherent,
            Oeuvre = oeuvre,
            DateReservation = DateOnly.FromDateTime(DateTime.Now),
            Statut = StatutReservation.Active,
        };

        _reservationRepo.AddReservation(reservation);
        _adherentRepo.SaveChanges();

        return reservation;
    }
}
