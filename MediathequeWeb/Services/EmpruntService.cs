public class EmpruntService
{
    private const int LIMITE_EMPRUNTS_ACTIFS = 10;
    private const int DUREE_EMPRUNT_JOURS = 21;

    private readonly IAdherentRepository _adherentRepo;
    private readonly IEmpruntRepository _empruntRepo;
    private readonly IInventaireRepository _inventaireRepo;
    private readonly IStatistiquesRepository _statistiquesRepo;

    public EmpruntService(
        IAdherentRepository adherentRepo,
        IEmpruntRepository empruntRepo,
        IInventaireRepository inventaireRepo,
        IStatistiquesRepository statistiquesRepo
    )
    {
        _adherentRepo = adherentRepo;
        _empruntRepo = empruntRepo;
        _inventaireRepo = inventaireRepo;
        _statistiquesRepo = statistiquesRepo;
    }

    public Emprunt CreateEmprunt(int adherentId, int oeuvreId)
    {
        Adherent adherent =
            _adherentRepo.GetAdherent(adherentId)
            ?? throw new NotFoundException("Adhérent introuvable.");

        int empruntsActifs = adherent.Emprunts.Count(e => e.DateRetourReelle == null);

        if (empruntsActifs >= LIMITE_EMPRUNTS_ACTIFS)
            throw new DomainValidationException(
                $"Limite de {LIMITE_EMPRUNTS_ACTIFS} emprunts atteinte."
            );

        if (!_inventaireRepo.OeuvreExists(oeuvreId))
            throw new NotFoundException($"L’œuvre avec l’id {oeuvreId} n’existe pas.");

        Exemplaire exemplaire =
            _inventaireRepo
                .GetAllExemplairesFromOeuvreId(oeuvreId)
                .FirstOrDefault(e => e.Statut == StatutExemplaire.Disponible)
            ?? throw new DomainValidationException("Aucun exemplaire disponible.");

        DateOnly dateEmprunt = DateOnly.FromDateTime(DateTime.Now);

        Emprunt emprunt = new()
        {
            Adherent = adherent,
            Exemplaire = exemplaire,
            DateEmprunt = dateEmprunt,
            DateRetourPrevue = dateEmprunt.AddDays(DUREE_EMPRUNT_JOURS),
            DateRetourReelle = null,
        };

        exemplaire.Statut = StatutExemplaire.Emprunte;

        _empruntRepo.AddEmprunt(emprunt);

        Reservation? reservation = adherent.Reservations.FirstOrDefault(r =>
            r.Oeuvre.Id == oeuvreId && r.Statut == StatutReservation.Active
        );

        if (reservation != null)
        {
            reservation.Statut = StatutReservation.Terminee;
        }
        IncrementNouveauxEmpruntsStatistiques(emprunt.DateEmprunt);
        _empruntRepo.SaveChanges();

        return emprunt;
    }

    public Emprunt GetEmpruntById(int empruntId)
    {
        Emprunt emprunt =
            _empruntRepo.GetEmprunt(empruntId)
            ?? throw new NotFoundException("Emprunt introuvable.");
        return emprunt;
    }

    public IEnumerable<Emprunt> GetAllEmprunts()
    {
        IEnumerable<Emprunt> emprunts = _empruntRepo.GetAllEmprunts();
        return emprunts;
    }

    public Emprunt ReturnEmprunt(int empruntId, DateOnly dateRetourReelle)
    {
        Emprunt emprunt =
            _empruntRepo.GetEmprunt(empruntId)
            ?? throw new NotFoundException("Emprunt introuvable.");

        if (emprunt.DateRetourReelle != null)
            throw new DomainValidationException("Cet emprunt a déjà été retourné.");

        emprunt.DateRetourReelle = dateRetourReelle;
        emprunt.Exemplaire.Statut = StatutExemplaire.Disponible;
        IncrementRetoursEffectuesStatistiques(dateRetourReelle);

        _empruntRepo.SaveChanges();

        return emprunt;
    }

    public void DeleteEmprunt(int empruntId)
    {
        Emprunt emprunt = GetEmpruntById(empruntId);
        emprunt.Exemplaire.Statut = StatutExemplaire.Disponible;
        DecrementNouveauxEmpruntsStatistiques(emprunt.DateEmprunt);
        _empruntRepo.RemoveEmprunt(emprunt);
        _empruntRepo.SaveChanges();
    }

    public void IncrementNouveauxEmpruntsStatistiques(DateOnly date)
    {
        Statistiques? statistiques = _statistiquesRepo.GetStatistiquesFromDate(date);
        if (statistiques == null)
            _statistiquesRepo.AddStatistiques(
                new Statistiques { Date = date, NouveauxEmprunts = 1 }
            );
        else
            statistiques.NouveauxEmprunts += 1;
        _statistiquesRepo.SaveChanges();
    }

    public void DecrementNouveauxEmpruntsStatistiques(DateOnly date)
    {
        var suivi =
            _statistiquesRepo.GetStatistiquesFromDate(date)
            ?? throw new NotFoundException($"Aucune activité trouvée pour la date {date}.");

        if (suivi.NouveauxEmprunts <= 0)
            throw new DomainValidationException("Aucun nouvel emprunt enregistré pour cette date.");

        suivi.NouveauxEmprunts -= 1;
        _statistiquesRepo.SaveChanges();
    }

    public void IncrementRetoursEffectuesStatistiques(DateOnly date)
    {
        Statistiques? statistiques = _statistiquesRepo.GetStatistiquesFromDate(date);
        if (statistiques == null)
            _statistiquesRepo.AddStatistiques(
                new Statistiques { Date = date, RetoursEffectues = 1 }
            );
        else
            statistiques.RetoursEffectues += 1;
        _statistiquesRepo.SaveChanges();
    }
}
