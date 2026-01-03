public class StatistiquesAdherentDto
{
    public int Id { get; set; }
    public string NomComplet { get; set; }
    public string Statut { get; set; }
    public int EmpruntsEnCours { get; set; }
    public int ReservationsActives { get; set; }
    public int HistoriqueEmprunts { get; set; }

    public static StatistiquesAdherentDto FromData(Adherent adherent)
    {
        int empruntsEnCours = adherent.Emprunts.Count(e => !e.DateRetourReelle.HasValue);
        int reservationsActives = adherent.Reservations.Count(r =>
            r.Statut == StatutReservation.Active
        );
        int historiqueEmprunts = adherent.Emprunts.Count;
        bool isEnRetard = adherent.Emprunts.Any(e =>
            e.DateRetourReelle.HasValue && e.DateRetourReelle.Value > e.DateRetourPrevue
        );
        string statut = isEnRetard
            ? StatutAdherent.EnRetard.ToString()
            : StatutAdherent.EnRegle.ToString();
        return new StatistiquesAdherentDto
        {
            Id = adherent.Id,
            NomComplet = $"{adherent.Prenom} {adherent.Nom}",
            Statut = statut,
            EmpruntsEnCours = empruntsEnCours,
            ReservationsActives = reservationsActives,
            HistoriqueEmprunts = historiqueEmprunts,
        };
    }
}
