public class EmpruntDto
{
    public int Id { get; set; }
    public string Oeuvre { get; set; }
    public string Adherent { get; set; }
    public DateOnly DateEmprunt { get; set; }
    public DateOnly DateRetourPrevue { get; set; }
    public DateOnly? DateRetourReelle;

    public static EmpruntDto FromModel(Emprunt emprunt)
    {
        var dto = new EmpruntDto
        {
            Id = emprunt.Id,
            Oeuvre = emprunt.Exemplaire.Oeuvre.Titre,
            Adherent = $"{emprunt.Adherent.Prenom} {emprunt.Adherent.Nom}",
            DateEmprunt = emprunt.DateEmprunt,
            DateRetourPrevue = emprunt.DateRetourPrevue,
            DateRetourReelle = emprunt.DateRetourReelle,
        };
        return dto;
    }
}
