public class StatistiquesAdherentDto
{
    public int Id { get; set; }
    public string NomComplet { get; set; }
    public StatutAdherent Statut { get; set; }
    public int EmpruntsEnCours { get; set; }
    public int ReservationsActives  { get; set; }
    public int HistoriqueEmprunts { get; set; }
}
