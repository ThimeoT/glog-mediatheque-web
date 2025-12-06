public class ExemplaireDto
{
    public int Id { get; set; }
    public StatutExemplaire Statut { get; set; }
    public string Oeuvre { get; set; }
    public List<string> Emprunts { get; set; }

    public static ExemplaireDto FromModel(Exemplaire exemplaire)
    {
        var dto = new ExemplaireDto
        {
            Id = exemplaire.Id,
            Statut = exemplaire.Statut,
            Oeuvre = exemplaire.Oeuvre.Titre,
            Emprunts = exemplaire
                .Emprunts.Select(e => $"{e.Adherent.Prenom} {e.Adherent.Nom}")
                .ToList(),
        };
        return dto;
    }
}
