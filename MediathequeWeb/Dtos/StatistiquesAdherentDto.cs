public class StatistiquesAdherentDto
{
    public int Id { get; set; }
    public string NomComplet { get; set; }
    private string _statut;
    public string Statut
    {
        get => _statut;
        set
        {
            if (value != "En règle" && value != "En retard")
                throw new DomainValidationException(
                    $"Nom de statut d'adhérent '{value}' non valide. Autorisés :  'En règle' ou 'En retard' "
                );
            _statut = value;
        }
    }

    public int EmpruntsEnCours { get; set; }
    public int ReservationsActives { get; set; }
    public int HistoriqueEmprunts { get; set; }

    public static StatistiquesAdherentDto FromData(Adherent adherent)
    {
        bool isEnRetard = adherent.Emprunts.Any(e =>
            e.DateRetourReelle.HasValue && e.DateRetourReelle.Value > e.DateRetourPrevue
        );
        int empruntsEnCours = adherent.Emprunts.Count(e => !e.DateRetourReelle.HasValue);
        int reservationsActives = adherent.Reservations.Count(r =>
            r.Statut == StatutReservation.Active
        );
        int historiqueEmprunts = adherent.Emprunts.Count;

        return new StatistiquesAdherentDto
        {
            Id = adherent.Id,
            NomComplet = $"{adherent.Prenom} {adherent.Nom}",
            Statut = isEnRetard ? "En retard" : "En règle",
            EmpruntsEnCours = empruntsEnCours,
            ReservationsActives = reservationsActives,
            HistoriqueEmprunts = historiqueEmprunts,
        };
    }
}
