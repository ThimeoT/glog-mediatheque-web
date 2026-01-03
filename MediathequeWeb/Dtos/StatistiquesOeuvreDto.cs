public class StatistiquesOeuvreDto
{
    public int Id { get; set; }
    public string Titre { get; set; }
    public string AuteurNomComplet { get; set; }
    public string Statut { get; set; }
    public int NombreCopies { get; set; }
    public int NombreEmpruntsTotal { get; set; }
    public int FileAttenteReservation { get; set; }

    public static StatistiquesOeuvreDto FromData(
        int id,
        string titre,
        string auteurNomComplet,
        StatutDisponibilite statut,
        int nombreCopies,
        int nombreEmpruntsTotal,
        int fileAttenteReservation
    )
    {
        string statutDisponibilite;
        switch (statut)
        {
            case StatutDisponibilite.EnReservation:
                statutDisponibilite = "En Réservation";
                break;
            case StatutDisponibilite.TousEmpruntes:
                statutDisponibilite = "Tous Empruntés";
                break;
            default:
                statutDisponibilite = "Disponible";
                break;
        }

        return new StatistiquesOeuvreDto
        {
            Id = id,
            Titre = titre,
            AuteurNomComplet = auteurNomComplet,
            Statut = statutDisponibilite,
             NombreCopies = nombreCopies,
            NombreEmpruntsTotal = nombreEmpruntsTotal,
            FileAttenteReservation = fileAttenteReservation,
        };
    }
}
