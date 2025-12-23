using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/adherent")]
public class AdherentController : ControllerBase
{
    private readonly AdherentService _adherentService;

    public AdherentController(AdherentService adherentService)
    {
        _adherentService = adherentService;
    }

    [HttpGet]
    public ActionResult<IEnumerable<AdherentDto>> RecupererAdherents()
    {
        var adherents = _adherentService.GetAll();
        return Ok(adherents.Select(AdherentDto.FromModel));
    }

    [HttpGet("{id}")]
    public ActionResult<AdherentDto> RecupererAdherent(int id)
    {
        try
        {
            var adherent = _adherentService.GetById(id);
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
            var adherent = _adherentService.Create(nom, prenom, email);
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
    public ActionResult<AdherentDto> ModifierAdherent(int id, string nom, string prenom, string email)
    {
        try
        {
            var adherent = _adherentService.Update(id, nom, prenom, email);
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
            _adherentService.Delete(id);
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