public class StatistiquesOeuvreDto
{
    public int Id { get; set; }
    public string Titre { get; set; }
    public string AuteurNomComplet { get; set; }
    public string StatutDisponibilite { get; set; }
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
        string statutDisponibilite = statut switch
        {
            global::StatutDisponibilite.EnReservation => "En Réservation",
            global::StatutDisponibilite.TousEmpruntes => "Tous Empruntés",
            _ => "Disponible",
        };
        
        return new StatistiquesOeuvreDto
        {
            Id = id,
            Titre = titre,
            AuteurNomComplet = auteurNomComplet,
            StatutDisponibilite = statutDisponibilite,
             NombreCopies = nombreCopies,
            NombreEmpruntsTotal = nombreEmpruntsTotal,
            FileAttenteReservation = fileAttenteReservation,
        };
    }
}
