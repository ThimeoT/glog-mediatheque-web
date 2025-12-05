public class OeuvreDto
{
    public int Id { get; set; }
    public string Titre { get; set; }
    public string Type { get; set; }
    public string Auteur { get; set; }

    public static OeuvreDto FromModel(Oeuvre oeuvre)
    {
        return new OeuvreDto
        {
            Id = oeuvre.Id,
            Titre = oeuvre.Titre,
            Type = oeuvre.Type.ToString(), 
            Auteur = $"{oeuvre.Auteur.Prenom} {oeuvre.Auteur.Nom}"
        };
    }
}
