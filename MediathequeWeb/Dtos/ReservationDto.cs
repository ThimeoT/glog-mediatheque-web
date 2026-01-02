public class ReservationDto
{
    public int Id { get; set; }
    public int AdherentId { get; set; }
    public int OeuvreId { get; set; }
    public DateOnly DateReservation { get; set; }
    public string Statut { get; set; }

    public static ReservationDto FromModel(Reservation reservation)
    {
        return new ReservationDto
        {
            Id = reservation.Id,
            AdherentId = reservation.Adherent.Id,
            OeuvreId = reservation.Oeuvre.Id,
            DateReservation = reservation.DateReservation,
            Statut = reservation.Statut.ToString(),
        };
    }
}
