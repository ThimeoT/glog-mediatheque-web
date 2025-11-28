public class Oeuvre
{
    public int Id { get; set; }

    private string _titre;
    public string Titre
    {
        get => _titre;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new DomainValidationException("Le titre de l’œuvre ne peut pas être vide.");
            _titre = value;
        }
    }

    public OeuvreType Type { get; set; }

    public Auteur? Auteur { get; set; } = null;

    public List<Exemplaire> Exemplaires { get; set; } = [];
    
    public List<Reservation> Reservations { get; set; } = [];
}