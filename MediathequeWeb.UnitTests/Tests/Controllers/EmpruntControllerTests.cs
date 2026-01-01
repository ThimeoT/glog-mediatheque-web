namespace MediathequeWeb.UnitTests.Tests.Controllers;

using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

[TestClass]
public class EmpruntContollerTests
{
    private EmpruntController _controller;
    private EmpruntService _service;
    private MockEmpruntRepository _mockEmpruntRepo;

    [TestInitialize]
    public void Setup()
    {
        _mockEmpruntRepo = new();
        MockAdherentRepository _mockAdherentRepo = new();
        MockInventaireRepository _mockInventaireRepo = new();
        _service = new(_mockAdherentRepo, _mockEmpruntRepo, _mockInventaireRepo);
        _controller = new(_service);
    }

    [TestMethod]
    public void Recuperer_emprunts_retourne_ok_avec_la_liste_des_dto_emprunts()
    {
        var result = _controller.RecupererEmprunts();

        var okResult = result.Result as OkObjectResult;
        Assert.IsNotNull(okResult);

        var dtos = okResult.Value as IEnumerable<EmpruntDto>;
        Assert.IsNotNull(dtos);
        Assert.AreEqual(2, dtos.Count());
    }

    [TestMethod]
    public void Recuperer_emprunt_avec_id_valide_retourne_ok_avec_dto()
    {
        var result = _controller.RecupererEmprunt(1);

        var okResult = result.Result as OkObjectResult;
        Assert.IsNotNull(okResult);

        var dto = okResult.Value as EmpruntDto;
        Assert.IsNotNull(dto);
        Assert.AreEqual("Jean Némard", dto.Adherent);
        Assert.AreEqual("Kids", dto.Oeuvre);
        Assert.AreEqual(dto.DateEmprunt.AddDays(21), dto.DateRetourPrevue);
    }

    [TestMethod]
    public void Recuperer_emprunt_id_avec_invalide_retourne_not_found()
    {
        var result = _controller.RecupererEmprunt(99);

        var notFound = result.Result as NotFoundObjectResult;
        Assert.IsNotNull(notFound);
    }

    [TestMethod]
    public void Ajouter_emprunt_retourne_created_avec_dto_emprunt()
    {
        var result = _controller.AjouterEmprunt(adherentId: 1, oeuvreId: 1);

        var created = result.Result as CreatedAtActionResult;
        Assert.IsNotNull(created);

        var dto = created.Value as EmpruntDto;
        Assert.IsNotNull(dto);
        Assert.AreEqual("Jean Némard", dto.Adherent);
        Assert.AreEqual("Kids", dto.Oeuvre);
        Assert.AreEqual(DateOnly.FromDateTime(DateTime.Now), dto.DateEmprunt);
        Assert.AreEqual(dto.DateEmprunt.AddDays(21), dto.DateRetourPrevue);
    }

    [TestMethod]
    public void Ajouter_reservation_avec_adherentId_invalide_retourne_not_found()
    {
        int idInvalide = 999;
        var result = _controller.AjouterEmprunt(adherentId: idInvalide, oeuvreId: 1);

        var notFoundResult = result.Result as NotFoundObjectResult;
        Assert.IsNotNull(notFoundResult);
        Assert.AreEqual("Adhérent introuvable.", notFoundResult.Value);
    }

    [TestMethod]
    public void Ajouter_emprunt_avec_oeuvreId_invalide_retourne_not_found()
    {
        int idInvalide = 999;
        var result = _controller.AjouterEmprunt(adherentId: 1, oeuvreId: idInvalide);

        var notFoundResult = result.Result as NotFoundObjectResult;
        Assert.IsNotNull(notFoundResult);
        Assert.AreEqual($"L’œuvre avec l’id {idInvalide} n’existe pas.", notFoundResult.Value);
    }

    // TODO : ajouter test pour vérifier le cas où tous les exemplaires sont indisponibles

    [TestMethod]
    public void Retourner_emprunt_avec_id_valide_retourne_ok_avec_dto_emprunt_modifie()
    {
        var result = _controller.RetournerEmprunt(
            id: 1,
            dateRetourReelle: DateOnly.FromDateTime(DateTime.Now)
        );

        var okResult = result.Result as OkObjectResult;
        Assert.IsNotNull(okResult);

        var dto = okResult.Value as EmpruntDto;
        Assert.IsNotNull(dto);
        Assert.AreEqual("Jean Némard", dto.Adherent);
        Assert.AreEqual("Kids", dto.Oeuvre);
    }

    [TestMethod]
    public void Retourner_emprunt_avec_id_invalide_retourne_not_found()
    {
        int idInvalide = 999;
        var result = _controller.RetournerEmprunt(id: idInvalide,
            dateRetourReelle: DateOnly.FromDateTime(DateTime.Now));

        var notFoundResult = result.Result as NotFoundObjectResult;
        Assert.IsNotNull(notFoundResult);
        Assert.AreEqual("Emprunt introuvable.", notFoundResult.Value);
    }

    [TestMethod]
    public void Retourner_emprunt_avec_date_invalide_retourne_bad_request()
    {
        DateOnly dateInvalide = DateOnly.FromDateTime(DateTime.Now).AddDays(-120);
        var result = _controller.RetournerEmprunt(id: 1,
            dateRetourReelle: dateInvalide );

        var badRequestResult = result.Result as BadRequestObjectResult;
        Assert.IsNotNull(badRequestResult);
        Assert.AreEqual("La date de retour réelle ne peut pas être avant la date d’emprunt.", badRequestResult.Value);
        
    }
    

    [TestMethod]
    public void Supprimer_emprunt_avec_id_valide_retourne_no_content()
    {
        var result = _controller.SupprimerEmprunt(1);

        var noContent = result as NoContentResult;
        Assert.IsNotNull(noContent);
        Assert.AreEqual(1, _mockEmpruntRepo.GetAllEmprunts().Count());
    }

    [TestMethod]
    public void Supprimer_emprunt_avec_id_invalide_retourne_not_found()
    {
        var result = _controller.SupprimerEmprunt(99);

        var notFoundResult = result as NotFoundObjectResult;
        Assert.IsNotNull(notFoundResult);
    }
}
