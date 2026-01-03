public class AdherentDto
{
    public int Id { get; set; }
    public string Nom { get; set; }
    public string Prenom { get; set; }
    public string Email { get; set; }
    public DateOnly DateInscription { get; set; } = DateOnly.FromDateTime(DateTime.Now);
    public List<string> Emprunts { get; set; } = [];

    public static AdherentDto FromModel(Adherent adherent)
    {
        var dto = new AdherentDto
        {
            Id = adherent.Id,
            Nom = adherent.Nom,
            Prenom = adherent.Prenom,
            Email = adherent.Email,
            DateInscription = adherent.DateInscription,
            Emprunts =
                adherent
                    .Emprunts?.Where(e => e.Exemplaire != null && e.Exemplaire.Oeuvre != null)
                    .Select(e => e.Exemplaire.Oeuvre.Titre)
                    .ToList() ?? [],
        };
        return dto;
    }
}
