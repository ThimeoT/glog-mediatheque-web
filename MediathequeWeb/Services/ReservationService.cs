public class ReservationService
{
    private readonly IAdherentRepository _adherentRepo;
    private readonly IInventaireRepository _inventaireRepo;

    public ReservationService(
        IAdherentRepository adherentRepo,
        IInventaireRepository inventaireRepo)
    {
        _adherentRepo = adherentRepo;
        _inventaireRepo = inventaireRepo;
    }

    public Reservation CreerReservation(int adherentId, int oeuvreId)
    {
        Adherent adherent = _adherentRepo.GetAdherent(adherentId)
            ?? throw new NotFoundException("Adhérent introuvable.");

        Oeuvre oeuvre = _inventaireRepo.GetOeuvre(oeuvreId)
            ?? throw new NotFoundException("Oeuvre introuvable.");

        bool exemplaireDisponible = _inventaireRepo.GetAllExemplaires()
            .Any(e => e.Oeuvre.Id == oeuvreId && e.Statut == StatutExemplaire.Disponible);

        if (exemplaireDisponible)
            throw new DomainValidationException("Un exemplaire est disponible, réservation impossible.");

        bool dejaReserve = adherent.Reservations
            .Any(r => r.Oeuvre.Id == oeuvreId && r.Statut == StatutReservation.Active);

        if (dejaReserve)
            throw new DomainValidationException("Réservation déjà existante pour cette œuvre.");

        Reservation reservation = new()
        {
            Adherent = adherent,
            Oeuvre = oeuvre,
            DateReservation = DateOnly.FromDateTime(DateTime.Now),
            Statut = StatutReservation.Active
        };

        _adherentRepo.AddReservation(reservation);
        _adherentRepo.SaveChanges();

        return reservation;
    }
}
