using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/reservation")]
public class ReservationController : ControllerBase
{
    private readonly ReservationService _reservationService;

    public ReservationController(ReservationService reservationService)
    {
        _reservationService = reservationService;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Reservation>> RecupererReservations()
    {
        var reservations = _reservationService.GetAllReservations();
        return Ok(reservations.Select(ReservationDto.FromModel));
    }

    public ActionResult<IEnumerable<Reservation>> RecupererReservationsAdherent(int adherentId)
    {
        try
        {
            var reservations = _reservationService.GetAllReservationsFromAdherentId(adherentId);
            return Ok(reservations.Select(ReservationDto.FromModel));
        }
        catch (NotFoundException ex) // si on ne trouve pas l'adhérent
        {
            return NotFound(ex.Message);
        }
    }

    public ActionResult<IEnumerable<Reservation>> RecupererReservationsOeuvre(int oeuvreId)
    {
        try
        {
            var reservations = _reservationService.GetAllReservationsFromOeuvreId(oeuvreId);
            return Ok(reservations.Select(ReservationDto.FromModel));
        }
        catch (NotFoundException ex) // si on ne trouve pas l'oeuvre
        {
            return NotFound(ex.Message);
        }
    }
}
