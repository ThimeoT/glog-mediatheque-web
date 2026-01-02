public class StatistiquesOeuvreDto
{
    public int Id { get; set; }
    public string Titre { get; set; }
    public string AuteurNomComplet { get; set; }
    private string _statutDisponibilite { get; set; }
    public string StatutDisponibilite
    {
        get => _statutDisponibilite;
        set
        {
            if (value != "Disponible" && value != "Tous empruntés" && value != "En réservation")
                throw new DomainValidationException(
                    $"Statut de disponibilité '{value}' non valide. Autorisés : 'Disponible', 'Tous empruntés', 'En réservation'."
                );
        }
    }
    public int NombreCopies { get; set; }
    public int NombreEmpruntsTotal { get; set; }
    public int FileAttenteReservation { get; set; }
}
