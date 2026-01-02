public class AuteurDto
{
    public int Id { get; set; }
    public string Nom { get; set; }
    public string Prenom { get; set; }
    public List<string> Oeuvres { get; set; } = [];

    public static AuteurDto FromModel(Auteur auteur)
    {
        var dto = new AuteurDto
        {
            Id = auteur.Id,
            Nom = auteur.Nom,
            Prenom = auteur.Prenom,
            Oeuvres = auteur.Oeuvres.Select(t => t.Titre).ToList(),
        };
        return dto;
    }
}
