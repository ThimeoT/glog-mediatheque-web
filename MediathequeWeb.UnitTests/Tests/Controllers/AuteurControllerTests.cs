namespace MediathequeWeb.UnitTests.Tests.Controllers;

using Microsoft.AspNetCore.Mvc;

[TestClass]
public class AuteurContollerTests
{
    private AuteurController _controller;
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
    public void Recuperer_auteurs_retourne_ok_avec_la_liste_des_dto_auteurs()
    {
        var result = _controller.RecupererAuteurs();

        var okResult = result.Result as OkObjectResult;
        Assert.IsNotNull(okResult);

        var dtos = okResult.Value as IEnumerable<AuteurDto>;
        Assert.IsNotNull(dtos);
        Assert.AreEqual(2, dtos.Count());
    }

    [TestMethod]
    public void Recuperer_auteur_avec_id_valide_retourne_ok_avec_dto_auteur()
    {
        var result = _controller.RecupererAuteur(1);

        var okResult = result.Result as OkObjectResult;
        Assert.IsNotNull(okResult);

        var dto = okResult.Value as AuteurDto;
        Assert.IsNotNull(dto);
        Assert.AreEqual("Herbert", dto.Nom);
        Assert.AreEqual("Frank", dto.Prenom);
    }

    [TestMethod]
    public void Recuperer_auteur_id_avec_invalide_retourne_not_found()
    {
        var result = _controller.RecupererAuteur(99);

        var notFound = result.Result as NotFoundObjectResult;
        Assert.IsNotNull(notFound);
    }

    [TestMethod]
    public void Ajouter_auteur_retourne_created_avec_dto_auteur()
    {
        var result = _controller.AjouterAuteur("Werber", "Bernard");

        var created = result.Result as CreatedAtActionResult;
        Assert.IsNotNull(created);

        var dto = created.Value as AuteurDto;
        Assert.IsNotNull(dto);
        Assert.AreEqual("Werber", dto.Nom);
        Assert.AreEqual("Bernard", dto.Prenom);
    }

    [TestMethod]
    public void Ajouter_auteur_avec_nom_vide_retourne_bad_request()
    {
        string nomVide = "";
        var result = _controller.AjouterAuteur(nomVide, "Bernard");

        var badRequestResult = result.Result as BadRequestObjectResult;
        Assert.IsNotNull(badRequestResult);
        Assert.AreEqual("Le nom ne peut pas être vide.", badRequestResult.Value);
    }

    [TestMethod]
    public void Ajouter_auteur_avec_prenom_vide_retourne_bad_request()
    {
        string prenomVide = "";
        var result = _controller.AjouterAuteur("Werber", prenomVide);

        var badRequestResult = result.Result as BadRequestObjectResult;
        Assert.IsNotNull(badRequestResult);
        Assert.AreEqual("Le prénom ne peut pas être vide.", badRequestResult.Value);
    }

    [TestMethod]
    public void Modifier_auteur_avec_id_valide_retourne_ok_avec_dto_auteur_modifie()
    {
        var result = _controller.ModifierAuteur(1, "Werber", "Bernard");

        var okResult = result.Result as OkObjectResult;
        Assert.IsNotNull(okResult);

        var dto = okResult.Value as AuteurDto;
        Assert.IsNotNull(dto);
        Assert.AreEqual("Werber", dto.Nom);
        Assert.AreEqual("Bernard", dto.Prenom);
    }

    [TestMethod]
    public void Modifier_auteur_avec_id_invalide_retourne_not_found()
    {
        var result = _controller.ModifierAuteur(99, "Werber", "Bernard");

        var notFoundResult = result.Result as NotFoundObjectResult;
        Assert.IsNotNull(notFoundResult);
    }

    [TestMethod]
    public void Modifier_auteur_avec_nom_vide_retourne_bad_request()
    {
        var result = _controller.ModifierAuteur(1, "", "Jean");

        var badRequestResult = result.Result as BadRequestObjectResult;
        Assert.IsNotNull(badRequestResult);
        Assert.AreEqual("Le nom ne peut pas être vide.", badRequestResult.Value);
    }
    [TestMethod]
    public void Modifier_auteur_avec_prenom_vide_retourne_bad_request()
    {
        var result = _controller.ModifierAuteur(1, "Werber", "");

        var badRequestResult = result.Result as BadRequestObjectResult;
        Assert.IsNotNull(badRequestResult);
        Assert.AreEqual("Le prénom ne peut pas être vide.", badRequestResult.Value);
    }

    [TestMethod]
    public void Supprimer_auteur_avec_id_valide_retourne_no_content()
    {
        var result = _controller.SupprimerAuteur(1);

        var noContent = result as NoContentResult;
        Assert.IsNotNull(noContent);
        Assert.AreEqual(1, _mockRepo.GetAllAuteurs().Count());
    }

    [TestMethod]
    public void Supprimer_auteur_avec_id_invalide_retourne_not_found()
    {
        var result = _controller.SupprimerAuteur(99);

        var notFoundResult = result as NotFoundObjectResult;
        Assert.IsNotNull(notFoundResult);
    }
}
