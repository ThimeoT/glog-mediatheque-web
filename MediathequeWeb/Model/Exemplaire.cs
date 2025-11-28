public class Exemplaire
{
    public int Id { get; set; }

    public StatutExemplaire Statut { get; set; } = StatutExemplaire.Disponible;

    public Oeuvre Oeuvre { get; set; }

    public List<Emprunt> Emprunts { get; set; } = [];
}