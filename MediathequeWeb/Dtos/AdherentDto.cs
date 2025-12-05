public class AdherentDto
{
    public int Id { get; set; }
    public string Nom { get; set; }
    public string Prenom { get; set; }
    public string Email { get; set; }
    public DateOnly DateInscription { get; set; }
    public List<Emprunt> Emprunts { get; set; } = [] ;
    public List<Reservation> Reservations { get; set; } = [];
    public static AdherentDto FromModel(AdherentDto adherent)
    {
        var dto = new AdherentDto
        {
            Id = adherent.Id,
            Nom = adherent.Nom,
            
        };
        return dto;
    }

}
