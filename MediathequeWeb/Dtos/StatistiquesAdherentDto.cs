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
}
