public class StatistiquesService
{
    private readonly IAdherentRepository _adherentRepo;
    private readonly IInventaireRepository _inventaireRepo;
    private readonly IEmpruntRepository _empruntRepo;
    private readonly IReservationRepository _reservationRepo;

    public StatistiquesService(
        IAdherentRepository adherentRepo,
        IInventaireRepository inventaireRepo,
        IEmpruntRepository empruntRepo,
        IReservationRepository reservationRepo
    )
    {
        _adherentRepo = adherentRepo;
        _inventaireRepo = inventaireRepo;
        _empruntRepo = empruntRepo;
        _reservationRepo = reservationRepo;
    }

    public StatutAdherent DetermineStatutAdherent(Adherent adherent)
    {
        if (
            adherent.Emprunts.Any(e =>
                e.DateRetourReelle == null
                && DateOnly.FromDateTime(DateTime.Now) > e.DateRetourPrevue
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
                    empruntsEnCours: _adherentRepo.GetNombreEmpruntsEnCours(adherent.Id),
                    reservationsActives: _adherentRepo.GetNombreReservationsActives(adherent.Id),
                    historiqueEmprunts: _adherentRepo.GetNombreEmpruntsTotal(adherent.Id)
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
            nombreEmpruntsTotal: _inventaireRepo.GetNombreEmpruntsTotalPourOeuvre(oeuvre.Id),
            fileAttenteReservation: _inventaireRepo.GetReservationsActivesPourOeuvre(oeuvre.Id)
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
                    nombreEmpruntsTotal: _inventaireRepo.GetNombreEmpruntsTotalPourOeuvre(
                        oeuvre.Id
                    ),
                    fileAttenteReservation: _inventaireRepo.GetReservationsActivesPourOeuvre(
                        oeuvre.Id
                    )
                )
            );
        }
        return statistiquesOeuvres;
    }

    public StatistiquesActiviteDto GetStatistiquesActivite(DateOnly? apres)
    {
        var emprunts = _empruntRepo.GetAllEmprunts();
        var reservations = _reservationRepo.GetAllReservations();
        var adherents = _adherentRepo.GetAllAdherents();

        if (apres.HasValue)
        {
            emprunts = emprunts.Where(e => e.DateEmprunt >= apres.Value);
            reservations = reservations.Where(r => r.DateReservation >= apres.Value);
            adherents = adherents.Where(a => a.DateInscription >= apres.Value);
        }

        return new StatistiquesActiviteDto
        {
            Date = DateOnly.FromDateTime(DateTime.Now).ToString("yyyy-MM-dd"),
            NouveauxEmprunts = emprunts.Count(),
            RetoursEffectues = emprunts.Count(e => e.DateRetourReelle != null),
            NouvellesReservations = reservations.Count(),
            NouveauxAdherents = adherents.Count(),
        };
    }
}
