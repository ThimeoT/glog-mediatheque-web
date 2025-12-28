using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/adherent")]
public class AdherentController : ControllerBase
{
    private readonly AdherentService _adherentService;
    private readonly ReservationService _reservationService;

    public AdherentController(AdherentService adherentService, ReservationService reservationService)
    {
        _adherentService = adherentService;
        _reservationService = reservationService;
    }

    [HttpGet]
    public ActionResult<IEnumerable<AdherentDto>> RecupererAdherents()
    {
        var adherents = _adherentService.GetAllAdherents();
        return Ok(adherents.Select(AdherentDto.FromModel));
    }

    [HttpGet("{id}")]
    public ActionResult<AdherentDto> RecupererAdherent(int id)
    {
        try
        {
            var adherent = _adherentService.GetAdherentById(id);
            return Ok(AdherentDto.FromModel(adherent));
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpPost]
    public ActionResult<AdherentDto> AjouterAdherent(string nom, string prenom, string email)
    {
        try
        {
            var adherent = _adherentService.CreateAdherent(nom, prenom, email);
            return CreatedAtAction(
                nameof(RecupererAdherent),
                new { id = adherent.Id },
                AdherentDto.FromModel(adherent)
            );
        }
        catch (DomainValidationException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id}")]
    public ActionResult<AdherentDto> ModifierAdherent(
        int id,
        string nom,
        string prenom,
        string email
    )
    {
        try
        {
            var adherent = _adherentService.UpdateAdherent(id, nom, prenom, email);
            return Ok(AdherentDto.FromModel(adherent));
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

    [HttpDelete("{id}")]
    public ActionResult SupprimerAdherent(int id)
    {
        try
        {
            _adherentService.DeleteAdherent(id);
            return NoContent();
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

    [HttpGet("{id}/reservation")]
    public ActionResult<IEnumerable<ReservationDto>> RecupererReservationsAdherent(int id)
    {
        try
        {
            var reservations = _reservationService.GetAllReservationsFromAdherentId(id);
            return Ok(reservations.Select(ReservationDto.FromModel));
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }
}
