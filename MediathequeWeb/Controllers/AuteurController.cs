using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/auteur")]
public class AuteurController : ControllerBase
{
    private readonly InventaireService _inventaireService;

    public AuteurController(InventaireService inventaireService)
    {
        _inventaireService = inventaireService;
    }

    [HttpGet]
    public ActionResult<IEnumerable<AuteurDto>> RecupererAuteurs()
    {
        var auteurs = _inventaireService.GetAllAuteurs();
        return Ok(auteurs.Select(AuteurDto.FromModel));
    }

    [HttpGet("{id}")]
    public ActionResult<AuteurDto> RecupererAuteur(int id)
    {
        try
        {
            var auteur = _inventaireService.GetAuteurById(id);
            return Ok(AuteurDto.FromModel(auteur));
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpPost]
    public ActionResult<AuteurDto> AjouterAuteur(string nom, string prenom)
    {
        try
        {
            var auteur = _inventaireService.CreateAuteur(nom, prenom);
            return CreatedAtAction(
                nameof(RecupererAuteur), 
                new { id = auteur.Id }, 
                AuteurDto.FromModel(auteur)
            );
        }
        catch (DomainValidationException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id}")]
    public ActionResult<AuteurDto> ModifierAuteur(int id, string nom, string prenom)
    {
        try
        {
            var auteur = _inventaireService.UpdateAuteur(id, nom, prenom);
            return Ok(AuteurDto.FromModel(auteur));
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
    public ActionResult SupprimerAuteur(int id)
    {
        try
        {
            _inventaireService.DeleteAuteur(id);
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