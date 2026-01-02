public class Auteur
{
    public int Id { get; set; }

    private string _nom;
    public string Nom
    {
        get => _nom;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new DomainValidationException("Le nom ne peut pas être vide.");
            }
            _nom = value;
        }
    }

    private string _prenom;
    public string Prenom
    {
        get => _prenom;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new DomainValidationException("Le prénom ne peut pas être vide.");
            }
            _prenom = value;
        }
    }

    public List<Oeuvre> Oeuvres { get; set; } = [];
}
