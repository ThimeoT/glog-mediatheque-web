public class StatistiquesOeuvre
{
    public int Id { get; set; }
    private string _titre;
    public string Titre
    {
        get => _titre;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new DomainValidationException(
                    "Le titre de l’œuvre ne peut pas être vide"
                );
        }
    }
    private string _auteurNomComplet;
    public string AuteurNomComplet
    {
        get => _auteurNomComplet;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new DomainValidationException(
                    "Le titre de l’œuvre ne peut pas être vide"
                );
        }
    }
    public StatutDisponibilite StatutDisponibilite { get; set; }
    private int _nombreCopies;
    public int NombreCopies
    {
        get => _nombreCopies;
        set
        {
            if (int.IsNegative(value))
                throw new DomainValidationException(
                    "Le nombre de copies ne peut pas être négatif"
                );
        }
    }
    private int _nombreEmpruntsTotal;
    public int NombreEmpruntsTotal
    {
        get => _nombreEmpruntsTotal;
        set
        {
            if (int.IsNegative(value))
                throw new DomainValidationException(
                    "Le nombre total d'emprunts ne peut pas être négatif"
                );
        }
    }
    private int _fileAttenteReservation;
    public int FileAttenteReservation
    {
        get => _fileAttenteReservation;
        set
        {
            if (int.IsNegative(value))
                throw new DomainValidationException(
                    "Le cumul des emprunts en cours et passés ne peut pas être négatif"
                );
        }
    }
}
