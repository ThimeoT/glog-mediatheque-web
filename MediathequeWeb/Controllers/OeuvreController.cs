using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/oeuvre")]
public class OeuvreController : ControllerBase
{
    private readonly InventaireService _inventaireService;
    private readonly ReservationService _reservationService;

    public OeuvreController(
        InventaireService inventaireService,
        ReservationService reservationService
    )
    {
        _inventaireService = inventaireService;
        _reservationService = reservationService;
    }

    [HttpGet]
    public ActionResult<IEnumerable<OeuvreDto>> RecupererOeuvres()
    {
        var oeuvres = _inventaireService.GetAllOeuvres();
        return Ok(oeuvres.Select(OeuvreDto.FromModel));
    }

    [HttpGet("{id}")]
    public ActionResult<OeuvreDto> RecupererOeuvre(int id)
    {
        try
        {
            var oeuvre = _inventaireService.GetOeuvreById(id);
            return Ok(OeuvreDto.FromModel(oeuvre));
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpGet("{id}/exemplaire")]
    public ActionResult<IEnumerable<ExemplaireDto>> RecupererExemplairesOeuvre(int id)
    {
        try
        {
            var exemplaires = _inventaireService.GetAllExemplairesFromOeuvreId(id);
            return Ok(exemplaires.Select(ExemplaireDto.FromModel));
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpPost]
    public ActionResult<OeuvreDto> AjouterOeuvre(string titre, string type, int auteurId)
    {
        try
        {
            var oeuvre = _inventaireService.CreateOeuvre(titre, type, auteurId);
            return CreatedAtAction(
                nameof(RecupererOeuvre),
                new { id = oeuvre.Id },
                OeuvreDto.FromModel(oeuvre)
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

    [HttpPost("{id}/exemplaire")]
    public ActionResult<ExemplaireDto> AjouterExemplaireOeuvre(int id)
    {
        try
        {
            var exemplaire = _inventaireService.CreateExemplaire(id);
            return CreatedAtAction(
                nameof(ExemplaireController.RecupererExemplaire),
                new { id = exemplaire.Id },
                ExemplaireDto.FromModel(exemplaire)
            );
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpPut("{id}")]
    public ActionResult<OeuvreDto> ModifierOeuvre(int id, string titre, string type, int auteurId)
    {
        try
        {
            var oeuvre = _inventaireService.UpdateOeuvre(id, titre, type, auteurId);
            return Ok(OeuvreDto.FromModel(oeuvre));
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
    public ActionResult SupprimerOeuvre(int id)
    {
        try
        {
            _inventaireService.DeleteOeuvre(id);
            return NoContent();
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpGet("{id}/reservation")]
    public ActionResult<IEnumerable<ReservationDto>> RecupererReservationsOeuvre(int id)
    {
        try
        {
            var reservations = _reservationService.GetAllReservationsFromOeuvreId(id);
            return Ok(reservations.Select(ReservationDto.FromModel));
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }
}
