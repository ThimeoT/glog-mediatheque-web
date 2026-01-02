public class StatistiquesOeuvreDto
{
    public int Id { get; set; }
    public string Titre { get; set; }
    public string AuteurNomComplet { get; set; }
    private string _statutDisponibilite { get; set; }
    public string StatutDisponibilite
    {
        get => _statutDisponibilite;
        set
        {
            if (value != "Disponible" && value != "Tous empruntés" && value != "En réservation")
                throw new DomainValidationException(
                    $"Statut de disponibilité '{value}' non valide. Autorisés : 'Disponible', 'Tous empruntés', 'En réservation'."
                );
            _statutDisponibilite = value;
        }
    }
    public int NombreCopies { get; set; }
    public int NombreEmpruntsTotal { get; set; }
    public int FileAttenteReservation { get; set; }

    public static StatistiquesOeuvreDto FromData(Oeuvre oeuvre)
    {
        return new StatistiquesOeuvreDto
        {
            Id = oeuvre.Id,
            Titre = oeuvre.Titre,
            AuteurNomComplet = $"{oeuvre.Auteur.Prenom} {oeuvre.Auteur.Nom}",
            StatutDisponibilite = DeterminerStatutOeuvre(oeuvre),
        };
    }

    private static string DeterminerStatutOeuvre(Oeuvre oeuvre)
    {
        int exemplairesDisponibles = oeuvre.Exemplaires.Count(e =>
            e.Statut == StatutExemplaire.Disponible
        );
        int nombreExemplaires = oeuvre.Exemplaires.Count();
        int reservationsActives = oeuvre.Reservations.Count(r =>
            r.Statut == StatutReservation.Active
        );

        if (
            reservationsActives > 0
            && reservationsActives >= exemplairesDisponibles
            && nombreExemplaires > 0
        )
            return "En réservation";
        if (nombreExemplaires > 0 && exemplairesDisponibles == 0)
            return "Tous empruntés";
        return "Disponible";
    }
}
