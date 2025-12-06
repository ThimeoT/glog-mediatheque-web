public class EmpruntDto
{
    public int Id { get; set; }
    public string Adherent { get; set; }
    public ExemplaireDto Exemplaire { get; set; }

    public DateOnly DateEmprunt { get; set; }
    public DateOnly DateRetourPrevue { get; set; }
    public DateOnly? DateRetourReelle;

    public static EmpruntDto FromModel(Emprunt emprunt)
    {
        var dto = new EmpruntDto
        {
            Id = emprunt.Id,
            Adherent = $"{emprunt.Adherent.Prenom} {emprunt.Adherent.Nom}",
            Exemplaire = ExemplaireDto.FromModel(emprunt.Exemplaire),
            DateEmprunt = emprunt.DateEmprunt,
            DateRetourPrevue = emprunt.DateRetourPrevue,
            DateRetourReelle = emprunt.DateRetourReelle,
        };
        return dto;
    }
}
