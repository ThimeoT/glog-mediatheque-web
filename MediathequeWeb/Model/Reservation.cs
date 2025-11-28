public class Reservation
{
    public int Id { get; set; }

    public Adherent Adherent { get; set; } = null!;

    public Oeuvre Oeuvre { get; set; } = null!;

    public DateOnly DateReservation { get; set; } = DateOnly.FromDateTime(DateTime.Now);

    public StatutReservation Statut { get; set; } = StatutReservation.Active;
}