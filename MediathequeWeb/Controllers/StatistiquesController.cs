using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/gestion")]
public class StatistiquesController : ControllerBase
{
    private readonly StatistiquesService _statistiquesService;

    public StatistiquesController(StatistiquesService statistiquesService)
    {
        _statistiquesService = statistiquesService;
    }

    [HttpGet("adherent")]
    public ActionResult<IEnumerable<StatistiquesAdherentDto>> RecupererStatistiquesAdherents()
    {
        var statistiquesAdherentsDtos = _statistiquesService.GetAllStatistiquesAdherents();
        return Ok(statistiquesAdherentsDtos);
    }

    [HttpGet("adherent/{id}")]
    public ActionResult<StatistiquesAdherentDto> RecupererAdherent(int id)
    {
        try
        {
            var statistiquesAdherentDto = _statistiquesService.GetStatistiquesAdherentById(id);
            return Ok(statistiquesAdherentDto);
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpGet("oeuvre")]
    public ActionResult<IEnumerable<StatistiquesOeuvreDto>> RecupererStatistiquesOeuvres()
    {
        var statistiquesOeuvresDtos = _statistiquesService.GetAllStatistiquesOeuvres();
        return Ok(statistiquesOeuvresDtos);
    }

    [HttpGet("oeuvre/{id}")]
    public ActionResult<StatistiquesOeuvreDto> RecupererOeuvre(int id)
    {
        try
        {
            var statistiquesOeuvreDto = _statistiquesService.GetStatistiquesOeuvreById(id);
            return Ok(statistiquesOeuvreDto);
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpGet("activite")]
    public ActionResult<IEnumerable<StatistiquesActiviteDto>> RecupererStatistiquesActivite()
    {
        var bilansActiviteQuotidiens = _statistiquesService.GetAllStatistiquesActivites();
        return Ok(bilansActiviteQuotidiens.Select(StatistiquesActiviteDto.FromModel));
    }

    [HttpGet("activite/{apres}")]
    public ActionResult<StatistiquesActiviteDto> RecupererStatistiquesActivite(string apres)
    {
        try
        {
            if (!DateOnly.TryParse(apres, out DateOnly date))
            {
                return BadRequest(
                    $"Format de date invalide. Utilisez 'yyyy-MM-dd'. Format reçu : {apres}"
                );
            }
            var statistiquesAdherentDto = _statistiquesService.GetStatistiquesActiviteByDate(date);
            return Ok(statistiquesAdherentDto);
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }
}
