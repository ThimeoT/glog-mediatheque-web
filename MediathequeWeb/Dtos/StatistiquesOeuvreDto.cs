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
        StatutDisponibilite statutDisponibilite,
        int nombreCopies,
        int nombreEmpruntsTotal,
        int fileAttenteReservation
    )
    {
        return new StatistiquesOeuvreDto
        {
            Id = id,
            Titre = titre,
            AuteurNomComplet = auteurNomComplet,
            StatutDisponibilite = statutDisponibilite.ToString(),
            NombreCopies = nombreCopies,
            NombreEmpruntsTotal = nombreEmpruntsTotal,
            FileAttenteReservation = fileAttenteReservation,
        };
    }
}
