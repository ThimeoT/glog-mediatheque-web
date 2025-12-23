using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/exemplaire")]
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
    public ActionResult<ExemplaireDto> RecupererEmprunt(int empruntId)
    {
        try
        {
            var emprunt = _empruntService.GetEmpruntById(empruntId);
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
        catch (DomainValidationException ex)
        {
            return BadRequest(ex.Message);
        }
    }
    [HttpPut("{id}")]
    public ActionResult<EmpruntDto> ModifierAdherent(int empruntId)
    {
        try
        {
            var emprunt = _empruntService.ReturnEmprunt(empruntId);
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
    public ActionResult SupprimerEmprunt(int empruntId)
    {
        try
        {
            _empruntService.DeleteEmprunt(empruntId);
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
}
