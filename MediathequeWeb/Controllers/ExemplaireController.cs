using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/exemplaire")]
public class ExemplaireController : ControllerBase
{
    private readonly InventaireService _inventaireService;

    public ExemplaireController(InventaireService inventaireService)
    {
        _inventaireService = inventaireService;
    }

    [HttpGet("{id}")]
    public ActionResult<ExemplaireDto> RecupererExemplaire(int id)
    {
        try
        {
            var exemplaire = _inventaireService.GetExemplaireById(id);
            return Ok(ExemplaireDto.FromModel(exemplaire));
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    public ActionResult SupprimerExemplaire(int id)
    {
        try
        {
            _inventaireService.DeleteExemplaire(id);
            return NoContent();
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }
}
