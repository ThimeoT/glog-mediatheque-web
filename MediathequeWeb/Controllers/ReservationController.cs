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

    [HttpPost]
    public ActionResult<ReservationDto> AjouterReservation(int adherentId, int oeuvreId)
    {
        try
        {
            var reservation = _reservationService.CreerReservation(adherentId, oeuvreId);

            return CreatedAtAction(
                nameof(RecupererReservations),
                new { id = reservation.Id },
                ReservationDto.FromModel(reservation)
            );
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (DomainValidationException ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
