namespace MediathequeWeb.UnitTests.Tests.Controllers;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

[TestClass]
public class OeuvreContollerTests
{
    private OeuvreController _controller;
    private InventaireService _service;
    private MockInventaireRepository _mockRepo;

    [TestInitialize]
    public void Setup()
    {
        _mockRepo = new();
        _service = new(_mockRepo);
        _controller = new(_service, null!);
    }

    [TestMethod]
    public void Recuperer_oeuvres_retourne_ok_avec_la_liste_des_dto_oeuvres()
    {
        var result = _controller.RecupererOeuvres();

        var okResult = result.Result as OkObjectResult;
        Assert.IsNotNull(okResult);

        var dtos = okResult.Value as IEnumerable<OeuvreDto>;
        Assert.IsNotNull(dtos);
        Assert.AreEqual(2, dtos.Count());
    }

    [TestMethod]
    public void Recuperer_oeuvre_avec_id_valide_retourne_ok_avec_dto_oeuvre()
    {
        var result = _controller.RecupererOeuvre(1);

        var okResult = result.Result as OkObjectResult;
        Assert.IsNotNull(okResult);
        var dto = okResult.Value as OeuvreDto;
        Assert.IsNotNull(dto);
        Assert.AreEqual("Dune", dto.Titre);
    }

    [TestMethod]
    public void Recuperer_oeuvre_id_avec_invalide_retourne_not_found()
    {
        var result = _controller.RecupererOeuvre(99);

        var notFound = result.Result as NotFoundObjectResult;
        Assert.IsNotNull(notFound);
    }

    [TestMethod]
    public void Ajouter_oeuvre_retourne_created_avec_dto_oeuvre()
    {
        var result = _controller.AjouterOeuvre("Oppenheimer", "DVD", 1);

        var created = result.Result as CreatedAtActionResult;
        Assert.IsNotNull(created);

        var dto = created.Value as OeuvreDto;
        Assert.IsNotNull(dto);
        Assert.AreEqual("Oppenheimer", dto.Titre);
        Assert.AreEqual("DVD", dto.Type);
        Assert.AreEqual("Christopher Nolan", dto.Auteur);
    }

    [TestMethod]
    public void Modifier_oeuvre_avec_id_valide_retourne_ok_avec_dto_oeuvre_modifie()
    {
        var result = _controller.ModifierOeuvre(1, "Inception", "CD", 1);

        var okResult = result.Result as OkObjectResult;
        Assert.IsNotNull(okResult);

        var dto = okResult.Value as OeuvreDto;
        Assert.IsNotNull(dto);
        Assert.AreEqual("Inception", dto.Titre);
        Assert.AreEqual("CD", dto.Type);
        Assert.AreEqual("Christopher Nolan", dto.Auteur);
    }

    [TestMethod]
    public void Modifier_oeuvre_avec_id_invalide_retourne_not_found()
    {
        var result = _controller.ModifierOeuvre(999, "Tenet", "CD", 1);

        var notFoundResult = result.Result as NotFoundObjectResult;
        Assert.IsNotNull(notFoundResult);
    }

    [TestMethod]
    public void Modifier_oeuvre_avec_nom_vide_retourne_bad_request()
    {
        var result = _controller.ModifierOeuvre(1, "Tenet", "CD", 1);

        var badRequestResult = result.Result as BadRequestObjectResult;
        Assert.IsNotNull(badRequestResult);
        Assert.AreEqual("Le nom ne peut pas être vide.", badRequestResult.Value);
    }

    [TestMethod]
    public void Modifier_oeuvre_avec_type_vide_retourne_bad_request()
    {
        var result = _controller.ModifierOeuvre(1, "Tenet", "",1);

        var badRequestResult = result.Result as BadRequestObjectResult;
        Assert.IsNotNull(badRequestResult);
    }
    [TestMethod]
    public void Modifier_oeuvre_avec_auteurId_invalide_retourne_bad_request()
    {
        var result = _controller.ModifierOeuvre(1, "Tenet", "CD",999);

        var badRequestResult = result.Result as BadRequestObjectResult;
        Assert.IsNotNull(badRequestResult);
    }

    [TestMethod]
    public void Supprimer_oeuvre_avec_id_valide_retourne_no_content()
    {
        var result = _controller.SupprimerOeuvre(1);

        var noContent = result as NoContentResult;
        Assert.IsNotNull(noContent);
        Assert.AreEqual(1, _mockRepo.GetAllOeuvres().Count());
    }

    [TestMethod]
    public void Supprimer_oeuvre_avec_id_invalide_retourne_not_found()
    {
        var result = _controller.SupprimerOeuvre(99);

        var notFoundResult = result as NotFoundObjectResult;
        Assert.IsNotNull(notFoundResult);
    }
}
