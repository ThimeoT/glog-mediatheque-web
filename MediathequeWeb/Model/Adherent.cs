public class Adherent
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

    private string _email;
    public string Email
    {
        get => _email;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new DomainValidationException("L'adresse email ne peut pas être vide.");
            }
            _email = value;
        }
    }

    public DateOnly DateInscription { get; } = DateOnly.FromDateTime(DateTime.Now);
}
