public class EmpruntService
{
    private const int LIMITE_EMPRUNTS_ACTIFS = 10;
    private const int DUREE_EMPRUNT_JOURS = 21;

    private readonly IAdherentRepository _adherentRepo;
    private readonly IEmpruntRepository _empruntRepo;
    private readonly IInventaireRepository _inventaireRepo;

    public EmpruntService(
        IAdherentRepository adherentRepo,
        IEmpruntRepository empruntRepo,
        IInventaireRepository inventaireRepo
    )
    {
        _adherentRepo = adherentRepo;
        _empruntRepo = empruntRepo;
        _inventaireRepo = inventaireRepo;
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

        Exemplaire exemplaire =
            _inventaireRepo
                .GetAllExemplaires()
                .FirstOrDefault(e =>
                    e.Oeuvre.Id == oeuvreId && e.Statut == StatutExemplaire.Disponible
                )
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

    public Emprunt ReturnEmprunt(int empruntId)
    {
        Emprunt emprunt =
            _empruntRepo.GetEmprunt(empruntId)
            ?? throw new NotFoundException("Emprunt introuvable.");

        if (emprunt.DateRetourReelle != null)
            throw new DomainValidationException("Cet emprunt a déjà été retourné.");

        emprunt.DateRetourReelle = DateOnly.FromDateTime(DateTime.Now);
        emprunt.Exemplaire.Statut = StatutExemplaire.Disponible;

        _empruntRepo.SaveChanges();
        return emprunt;
    }

    public void DeleteEmprunt(int empruntId)
    {
        Emprunt emprunt = GetEmpruntById(empruntId);
        emprunt.Exemplaire.Statut = StatutExemplaire.Disponible;
        _empruntRepo.RemoveEmprunt(emprunt);
        _empruntRepo.SaveChanges();
    }
}
