public class ExemplaireDto
{
    public int Id { get; set; }
    public string OeuvreTitre { get; set; }
    public string Statut { get; set; }

    public static ExemplaireDto FromModel(Exemplaire exemplaire)
    {
        var dto = new ExemplaireDto
        {
            Id = exemplaire.Id,
            OeuvreTitre = exemplaire.Oeuvre.Titre,
            Statut = exemplaire.Statut.ToString(),
        };
        return dto;
    }
}
