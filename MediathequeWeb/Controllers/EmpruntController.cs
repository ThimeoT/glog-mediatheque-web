using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/emprunt")]
public class EmpruntController : ControllerBase
{
    private readonly EmpruntService _empruntService;

    public EmpruntController(EmpruntService empruntService)
    {
        _empruntService = empruntService;
    }

    [HttpGet]
    public ActionResult<IEnumerable<EmpruntDto>> RecupererEmprunts()
    {
        var emprunts = _empruntService.GetAllEmprunts();
        return Ok(emprunts.Select(EmpruntDto.FromModel));
    }

    [HttpGet("{id}")]
    public ActionResult<EmpruntDto> RecupererEmprunt(int id)
    {
        try
        {
            var emprunt = _empruntService.GetEmpruntById(id);
            return Ok(EmpruntDto.FromModel(emprunt));
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpPost]
    public ActionResult<EmpruntDto> AjouterEmprunt(int adherentId, int oeuvreId)
    {
        try
        {
            var emprunt = _empruntService.CreateEmprunt(adherentId, oeuvreId);
            return CreatedAtAction(
                nameof(RecupererEmprunt),
                new { id = emprunt.Id },
                EmpruntDto.FromModel(emprunt)
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

    [HttpPut("{id}")]
    public ActionResult<EmpruntDto> RetournerEmprunt(int id, DateOnly dateRetourReelle)
    {
        try
        {
            var emprunt = _empruntService.ReturnEmprunt(id, dateRetourReelle);
            return Ok(EmpruntDto.FromModel(emprunt));
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
    public ActionResult SupprimerEmprunt(int id)
    {
        try
        {
            _empruntService.DeleteEmprunt(id);
            return NoContent();
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }
}
