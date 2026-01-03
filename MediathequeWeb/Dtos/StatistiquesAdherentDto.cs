public class StatistiquesAdherentDto
{
    public int Id { get; set; }
    public string NomComplet { get; set; }
    public string Statut { get; set; }
    public int EmpruntsEnCours { get; set; }
    public int ReservationsActives { get; set; }
    public int HistoriqueEmprunts { get; set; }

    public static StatistiquesAdherentDto FromData(int id, string nomComplet, StatutAdherent statut, int empruntsEnCours, int reservationsActives, int historiqueEmprunts)
    {
        return new StatistiquesAdherentDto
        {
            Id = id,
            NomComplet = nomComplet,
            Statut = statut.ToString(),
            EmpruntsEnCours = empruntsEnCours,
            ReservationsActives = reservationsActives,
            HistoriqueEmprunts = historiqueEmprunts,
        };
    }
}
