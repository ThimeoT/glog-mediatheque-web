public class StatistiquesAdherentDto
{
    public int Id { get; set; }
    private string _nomComplet;
    public string NomComplet
    {
        get => _nomComplet;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new DomainValidationException(
                    "L'adhérent ne peut pas avoir de nom complet vide"
                );
        }
    }
    public StatutAdherent Statut { get; set; }
    private int _empruntsEnCours;
    public int EmpruntsEnCours
    {
        get => _empruntsEnCours;
        set
        {
            if (int.IsNegative(value))
                throw new DomainValidationException(
                    "Le nombre d'emprunts en cours ne peut pas être négatif"
                );
        }
    }
    private int _reservationsActives;
    public int ReservationsActives
    {
        get => _reservationsActives;
        set
        {
            if (int.IsNegative(value))
                throw new DomainValidationException(
                    "Le nombre de réservations actives ne peut pas être négatif"
                );
        }
    }
    private int _historiqueEmprunts;
    public int HistoriqueEmprunts
    {
        get => _historiqueEmprunts;
        set
        {
            if (int.IsNegative(value))
                throw new DomainValidationException(
                    "Le cumul des emprunts en cours et passés ne peut pas être négatif"
                );
        }
    }
}
