public class StatistiquesService
{
    private readonly IAdherentRepository _adherentRepo;
    private readonly IInventaireRepository _inventaireRepo;
    private readonly IStatistiquesRepository _statistiquesRepo;

    public StatistiquesService(
        IAdherentRepository adherentRepo,
        IInventaireRepository inventaireRepo,
        IStatistiquesRepository statistiquesRepo
    )
    {
        _adherentRepo = adherentRepo;
        _inventaireRepo = inventaireRepo;
        _statistiquesRepo = statistiquesRepo;
    }

    public StatutAdherent DetermineStatutAdherent(Adherent adherent)
    {
        if (
            adherent.Emprunts.Any(e =>
                e.DateRetourReelle == null && DateOnly.FromDateTime(DateTime.Now) > e.DateRetourPrevue
            )
        )
        {
            return StatutAdherent.EnRetard;
        }
        return StatutAdherent.EnRegle;
    }

    private static StatutDisponibilite DetermineStatutDisponibilite(Oeuvre oeuvre)
    {
        int exemplairesDisponibles = oeuvre.Exemplaires.Count(e =>
            e.Statut == StatutExemplaire.Disponible
        );

        int reservationsActives = oeuvre.Reservations.Count(r =>
            r.Statut == StatutReservation.Active
        );

        // Disponible = au moins un exemplaire disponible
        if (exemplairesDisponibles > 0)
            return StatutDisponibilite.Disponible;

        // En réservation = aucun exemplaire disponible ET au moins une réservation active
        if (reservationsActives > 0)
            return StatutDisponibilite.EnReservation;

        // Tous empruntés = aucun exemplaire disponible ET aucune réservation active
        return StatutDisponibilite.TousEmpruntes;
    }

    public StatistiquesAdherentDto GetStatistiquesAdherentById(int adherentId)
    {
        Adherent adherent =
            _adherentRepo.GetAdherent(adherentId)
            ?? throw new NotFoundException($"L’adhérent avec l’id {adherentId} n’existe pas.");

        return StatistiquesAdherentDto.FromData(
            id: adherent.Id,
            nomComplet: $"{adherent.Prenom} {adherent.Nom}",
            statut: DetermineStatutAdherent(adherent),
            empruntsEnCours: adherent.Emprunts.Count(e => !e.DateRetourReelle.HasValue),
            reservationsActives: adherent.Reservations.Count(r =>
                r.Statut == StatutReservation.Active
            ),
            historiqueEmprunts: adherent.Emprunts.Count
        );
    }

    public IEnumerable<StatistiquesAdherentDto> GetAllStatistiquesAdherents()
    {
        List<StatistiquesAdherentDto> statistiquesAdherents = [];
        foreach (Adherent adherent in _adherentRepo.GetAllAdherents())
        {
            statistiquesAdherents.Add(
                StatistiquesAdherentDto.FromData(
                    id: adherent.Id,
                    nomComplet: $"{adherent.Prenom} {adherent.Nom}",
                    statut: DetermineStatutAdherent(adherent),
                    empruntsEnCours: adherent.Emprunts.Count(e => !e.DateRetourReelle.HasValue),
                    reservationsActives: adherent.Reservations.Count(r =>
                        r.Statut == StatutReservation.Active
                    ),
                    historiqueEmprunts: adherent.Emprunts.Count
                )
            );
        }
        return statistiquesAdherents;
    }

    public StatistiquesOeuvreDto GetStatistiquesOeuvreById(int oeuvreId)
    {
        Oeuvre oeuvre =
            _inventaireRepo.GetOeuvre(oeuvreId)
            ?? throw new NotFoundException($"L’œuvre avec l’id {oeuvreId} n’existe pas.");
        return StatistiquesOeuvreDto.FromData(
            id: oeuvre.Id,
            titre: oeuvre.Titre,
            auteurNomComplet: $"{oeuvre.Auteur.Prenom} {oeuvre.Auteur.Nom}",
            statut: DetermineStatutDisponibilite(oeuvre),
            nombreCopies: oeuvre.Exemplaires.Count,
            nombreEmpruntsTotal: oeuvre
                .Exemplaires.SelectMany(e => e.Emprunts)
                .Count(),
            fileAttenteReservation: oeuvre.Reservations.Count(r =>
                r.Statut == StatutReservation.Active
            )
        );
    }

    public IEnumerable<StatistiquesOeuvreDto> GetAllStatistiquesOeuvres()
    {
        List<StatistiquesOeuvreDto> statistiquesOeuvres = [];
        foreach (Oeuvre oeuvre in _inventaireRepo.GetAllOeuvres())
        {
            statistiquesOeuvres.Add(
                StatistiquesOeuvreDto.FromData(
                    id: oeuvre.Id,
                    titre: oeuvre.Titre,
                    auteurNomComplet: $"{oeuvre.Auteur.Prenom} {oeuvre.Auteur.Nom}",
                    statut: DetermineStatutDisponibilite(oeuvre),
                    nombreCopies: oeuvre.Exemplaires.Count,
                    nombreEmpruntsTotal: oeuvre
                        .Exemplaires.SelectMany(e => e.Emprunts)
                        .Count(),
                    fileAttenteReservation: oeuvre.Reservations.Count(r =>
                        r.Statut == StatutReservation.Active
                    )
                )
            );
        }
        return statistiquesOeuvres;
    }

    public Statistiques GetStatistiquesStatistiquesById(int activiteId)
    {
        Statistiques statistiques =
            _statistiquesRepo.GetStatistiquesFromId(activiteId)
            ?? throw new NotFoundException(
                $"Le suivi de l'activité avec l’id {activiteId} n’existe pas."
            );
        return statistiques;
    }

    public Statistiques GetStatistiquesActiviteByDate(DateOnly date)
    {
        Statistiques statistiques =
            _statistiquesRepo.GetStatistiquesFromDate(date)
            ?? throw new NotFoundException(
                $"Le suivi de l'activité de la date {date} n’existe pas."
            );
        return statistiques;
    }

    public IEnumerable<Statistiques> GetAllStatistiquesActivites()
    {
        List<Statistiques> bilansActiviteQuotidiens = [];
        foreach (Statistiques bilanActivite in _statistiquesRepo.GetAllStatistiques())
            bilansActiviteQuotidiens.Add(bilanActivite);
        return bilansActiviteQuotidiens;
    }
}
