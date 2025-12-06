public class ReservationDto
{
    public int Id { get; set; }
    public string Adherent { get; set; }
    public string Oeuvre { get; set; }
    public DateOnly DateReservation { get; set;}
    public StatutReservation Statut { get; set; }
    public static ReservationDto FromModel(Reservation reservation)
    {
        var dto = new ReservationDto
        {
            Id = reservation.Id,
            Adherent = $"{reservation.Adherent.Nom} {reservation.Adherent.Prenom}",
            Oeuvre = reservation.Oeuvre.Titre,
            DateReservation = reservation.DateReservation,
            Statut = reservation.Statut
        };
        return dto;
    }
}