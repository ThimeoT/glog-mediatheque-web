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
            throw new NotFoundException($"L'adhérent avec l'id {adherentId} n'existe pas.");
        return _reservationRepo.GetAllReservationsFromAdherentId(adherentId);
    }

    public IEnumerable<Reservation> GetAllReservationsFromOeuvreId(int oeuvreId)
    {
        if (!_inventaireRepo.OeuvreExists(oeuvreId))
            throw new NotFoundException($"L'oeuvre avec l'id {oeuvreId} n'existe pas.");
        return _reservationRepo.GetAllReservationsFromOeuvreId(oeuvreId);
    }

    public Reservation CreateReservation(int adherentId, int oeuvreId)
    {
        Adherent adherent =
            _adherentRepo.GetAdherent(adherentId)
            ?? throw new NotFoundException("Adhérent introuvable.");

        Oeuvre oeuvre =
            _inventaireRepo.GetOeuvre(oeuvreId)
            ?? throw new NotFoundException("Oeuvre introuvable.");

        if (_inventaireRepo.ExisteExemplaireDisponiblePourOeuvre(oeuvreId))
            throw new DomainValidationException(
                "Un exemplaire est disponible, réservation impossible."
            );

        if (_reservationRepo.ReservationActiveExiste(adherentId, oeuvreId))
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
        _adherentRepo.SaveChanges();

        return reservation;
    }

    public void DeleteReservation(int reservationId)
    {
        Reservation reservation =
            _reservationRepo.GetReservation(reservationId)
            ?? throw new NotFoundException("Réservation introuvable.");

        _reservationRepo.RemoveReservation(reservation);
        _reservationRepo.SaveChanges();
    }
}
