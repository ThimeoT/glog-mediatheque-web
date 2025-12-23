public class EmpruntService
{
    private const int LIMITE_EMPRUNTS_ACTIFS = 10;
    private const int DUREE_EMPRUNT_JOURS = 21;

    private readonly IAdherentRepository _adherentRepo;
    private readonly IInventaireRepository _inventaireRepo;

    public EmpruntService(
        IAdherentRepository adherentRepo,
        IInventaireRepository inventaireRepo)
    {
        _adherentRepo = adherentRepo;
        _inventaireRepo = inventaireRepo;
    }

    public Emprunt CreerEmprunt(int adherentId, int oeuvreId)
    {
        Adherent adherent = _adherentRepo.GetAdherent(adherentId)
            ?? throw new NotFoundException("Adhérent introuvable.");

        int empruntsActifs = adherent.Emprunts
            .Count(e => e.DateRetourReelle == null);

        if (empruntsActifs >= LIMITE_EMPRUNTS_ACTIFS)
            throw new DomainValidationException($"Limite de {LIMITE_EMPRUNTS_ACTIFS} emprunts atteinte.");

        Exemplaire exemplaire = _inventaireRepo.GetAllExemplaires()
            .FirstOrDefault(e =>
                e.Oeuvre.Id == oeuvreId &&
                e.Statut == StatutExemplaire.Disponible)
            ?? throw new DomainValidationException("Aucun exemplaire disponible.");

        DateOnly dateEmprunt = DateOnly.FromDateTime(DateTime.Now);

        Emprunt emprunt = new()
        {
            Adherent = adherent,
            Exemplaire = exemplaire,
            DateEmprunt = dateEmprunt,
            DateRetourPrevue = dateEmprunt.AddDays(DUREE_EMPRUNT_JOURS),
            DateRetourReelle = null
        };

        exemplaire.Statut = StatutExemplaire.Emprunte;

        _adherentRepo.AddEmprunt(emprunt);
        _adherentRepo.SaveChanges();

        return emprunt;
    }

    public Emprunt RetournerEmprunt(int empruntId)
    {
        Emprunt emprunt = _adherentRepo.GetEmprunt(empruntId)
            ?? throw new NotFoundException("Emprunt introuvable.");

        if (emprunt.DateRetourReelle != null)
            throw new DomainValidationException("Cet emprunt a déjà été retourné.");

        emprunt.DateRetourReelle = DateOnly.FromDateTime(DateTime.Now);
        emprunt.Exemplaire.Statut = StatutExemplaire.Disponible;

        _adherentRepo.SaveChanges();
        return emprunt;
    }
}
