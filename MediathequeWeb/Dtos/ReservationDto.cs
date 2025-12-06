public class ReservationDto
{
    public int Id { get; set; }
    public AdherentDto Adherent { get; set; }
    public OeuvreDto Oeuvre { get; set; }
    public DateOnly DateReservation { get; set; }
    public StatutReservation Statut { get; set; }

    public static ReservationDto FromModel(Reservation reservation)
    {
        var dto = new ReservationDto
        {
            Id = reservation.Id,
            Adherent = AdherentDto.FromModel(reservation.Adherent),
            Oeuvre = OeuvreDto.FromModel(reservation.Oeuvre),
            DateReservation = reservation.DateReservation,
            Statut = reservation.Statut,
        };
        return dto;
    }
}
