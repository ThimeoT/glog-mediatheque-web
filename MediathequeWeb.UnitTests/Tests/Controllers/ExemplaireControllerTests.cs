namespace MediathequeWeb.UnitTests.Tests.Controllers;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

[TestClass]
public class ExemplaireContollerTests
{
    private ExemplaireController _controller;
    private InventaireService _service;
    private MockInventaireRepository _mockRepo;

    [TestInitialize]
    public void Setup()
    {
        _mockRepo = new();
        _service = new(_mockRepo);
        _controller = new(_service);
    }

    [TestMethod]
    public void Recuperer_exemplaire_avec_id_valide_retourne_ok_avec_dto_exemplaire()
    {
        var result = _controller.RecupererExemplaire(1);

        var okResult = result.Result as OkObjectResult;
        Assert.IsNotNull(okResult);
        var dto = okResult.Value as ExemplaireDto;
        Assert.IsNotNull(dto);
        Assert.AreEqual("Dune", dto.OeuvreTitre);
        Assert.AreEqual("Disponible", dto.Statut);
    }

    [TestMethod]
    public void Recuperer_exemplaire_id_avec_invalide_retourne_not_found()
    {
        var result = _controller.RecupererExemplaire(99);

        var notFound = result.Result as NotFoundObjectResult;
        Assert.IsNotNull(notFound);
    }

    // ATTENTION : les tests de récupération ou d'ajout d'exemplaire
    // se trouvent dans OeuvreControllerTests

    [TestMethod]
    public void Supprimer_exemplaire_avec_id_valide_retourne_no_content()
    {
        var result = _controller.SupprimerExemplaire(1);

        var noContent = result as NoContentResult;
        Assert.IsNotNull(noContent);
        Assert.AreEqual(1, _mockRepo.GetAllExemplaires().Count());
    }

    [TestMethod]
    public void Supprimer_exemplaire_avec_id_invalide_retourne_not_found()
    {
        var result = _controller.SupprimerExemplaire(99);

        var notFoundResult = result as NotFoundObjectResult;
        Assert.IsNotNull(notFoundResult);
    }
}
