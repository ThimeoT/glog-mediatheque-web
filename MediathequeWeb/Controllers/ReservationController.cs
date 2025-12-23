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
    public ActionResult<IEnumerable<Reservation>> GetAllReservations()
    {
        var reservations = _reservationService.GetAllReservations();
        return Ok(reservations.Select(ReservationDto.FromModel));
    }

}