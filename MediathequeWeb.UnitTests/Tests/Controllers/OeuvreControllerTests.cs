namespace MediathequeWeb.UnitTests.Tests.Controllers;

using Microsoft.AspNetCore.Mvc;

[TestClass]
public class OeuvreControllerTests
{
    private OeuvreController _controller;
    private MockInventaireRepository _mockInventaireRepo;

    [TestInitialize]
    public void Setup()
    {
        _mockInventaireRepo = new();
        MockReservationRepository _mockReservationRepo = new();
        InventaireService _inventaireService = new(_mockInventaireRepo);
        ReservationService _reservationService = new(
            null!,
            _mockInventaireRepo,
            _mockReservationRepo
        );
        _controller = new(_inventaireService, _reservationService);
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
        Assert.AreEqual("Kids", dto.Titre);
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
        var result = _controller.AjouterOeuvre("Carrie", "DVD", 2);

        var created = result.Result as CreatedAtActionResult;
        Assert.IsNotNull(created);

        var dto = created.Value as OeuvreDto;
        Assert.IsNotNull(dto);
        Assert.AreEqual("Carrie", dto.Titre);
        Assert.AreEqual("DVD", dto.Type);
        Assert.AreEqual("Stephen King", dto.Auteur);
    }

    [TestMethod]
    public void Ajouter_oeuvre_avec_titre_vide_retourne_bad_request()
    {
        string prenomVide = "";
        var result = _controller.AjouterOeuvre(prenomVide, "DVD", 1);

        var badRequestResult = result.Result as BadRequestObjectResult;
        Assert.IsNotNull(badRequestResult);
        Assert.AreEqual("Le titre de l’œuvre ne peut pas être vide.", badRequestResult.Value);
    }

    [TestMethod]
    public void Ajouter_oeuvre_avec_type_vide_retourne_bad_request()
    {
        string typeVide = "";
        var result = _controller.AjouterOeuvre("Tenet", typeVide, 1);

        var badRequestResult = result.Result as BadRequestObjectResult;
        Assert.IsNotNull(badRequestResult);
    }

    [TestMethod]
    public void Ajouter_oeuvre_avec_auteurId_invalide_retourne_not_found()
    {
        int idInvalide = 999;
        var result = _controller.AjouterOeuvre("Tenet", "CD", idInvalide);

        var notFoundResult = result.Result as NotFoundObjectResult;
        Assert.IsNotNull(notFoundResult);
        Assert.AreEqual($"L’auteur avec l’id {idInvalide} n’existe pas.", notFoundResult.Value);
    }

    [TestMethod]
    public void Modifier_oeuvre_avec_id_valide_retourne_ok_avec_dto_oeuvre_modifie()
    {
        var result = _controller.ModifierOeuvre(1, "Inception", "CD", 2);

        var okResult = result.Result as OkObjectResult;
        Assert.IsNotNull(okResult);

        var dto = okResult.Value as OeuvreDto;
        Assert.IsNotNull(dto);
        Assert.AreEqual("Inception", dto.Titre);
        Assert.AreEqual("CD", dto.Type);
        Assert.AreEqual("Stephen King", dto.Auteur);
    }

    [TestMethod]
    public void Modifier_oeuvre_avec_id_invalide_retourne_not_found()
    {
        int idInvalide = 999;
        var result = _controller.ModifierOeuvre(idInvalide, "Tenet", "CD", 1);

        var notFoundResult = result.Result as NotFoundObjectResult;
        Assert.IsNotNull(notFoundResult);
        Assert.AreEqual($"L’œuvre avec l’id {idInvalide} n’existe pas.", notFoundResult.Value);
    }

    [TestMethod]
    public void Modifier_oeuvre_avec_titre_vide_retourne_bad_request()
    {
        var result = _controller.ModifierOeuvre(1, "", "CD", 1);

        var badRequestResult = result.Result as BadRequestObjectResult;
        Assert.IsNotNull(badRequestResult);
        Assert.AreEqual("Le titre de l’œuvre ne peut pas être vide.", badRequestResult.Value);
    }

    [TestMethod]
    public void Modifier_oeuvre_avec_type_vide_retourne_bad_request()
    {
        var result = _controller.ModifierOeuvre(1, "Tenet", "", 1);

        var badRequestResult = result.Result as BadRequestObjectResult;
        Assert.IsNotNull(badRequestResult);
    }

    [TestMethod]
    public void Modifier_oeuvre_avec_auteurId_invalide_retourne_not_found()
    {
        int idInvalide = 999;
        var result = _controller.ModifierOeuvre(1, "Tenet", "CD", idInvalide);

        var notFoundResult = result.Result as NotFoundObjectResult;
        Assert.IsNotNull(notFoundResult);
        Assert.AreEqual($"L’auteur avec l’id {idInvalide} n’existe pas.", notFoundResult.Value);
    }

    [TestMethod]
    public void Supprimer_oeuvre_avec_id_valide_retourne_no_content()
    {
        var result = _controller.SupprimerOeuvre(1);

        var noContent = result as NoContentResult;
        Assert.IsNotNull(noContent);
        Assert.AreEqual(1, _mockInventaireRepo.GetAllOeuvres().Count());
    }

    [TestMethod]
    public void Supprimer_oeuvre_avec_id_invalide_retourne_not_found()
    {
        var result = _controller.SupprimerOeuvre(999);

        var notFoundResult = result as NotFoundObjectResult;
        Assert.IsNotNull(notFoundResult);
    }

    [TestMethod]
    public void Recuperer_exemplaires_oeuvre_retourne_ok_avec_la_liste_des_dto_exemplaires()
    {
        var result = _controller.RecupererExemplairesOeuvre(1);

        var okResult = result.Result as OkObjectResult;
        Assert.IsNotNull(okResult);

        var dtos = okResult.Value as IEnumerable<ExemplaireDto>;
        Assert.IsNotNull(dtos);
        Assert.AreEqual(1, dtos.Count());
    }

    [TestMethod]
    public void Recuperer_exemplaires_oeuvre_avec_id_invalide_retourne_not_found()
    {
        int idInvalide = 999;
        var result = _controller.RecupererExemplairesOeuvre(id: idInvalide);

        var notFound = result.Result as NotFoundObjectResult;
        Assert.IsNotNull(notFound);
    }

    [TestMethod]
    public void Ajouter_exemplaire_retourne_created_avec_dto_oeuvre()
    {
        var result = _controller.AjouterExemplaireOeuvre(1);

        var created = result.Result as CreatedAtActionResult;
        Assert.IsNotNull(created);

        var dto = created.Value as ExemplaireDto;
        Assert.IsNotNull(dto);
        Assert.AreEqual("Kids", dto.OeuvreTitre);
        Assert.AreEqual("Disponible", dto.Statut);
    }

    [TestMethod]
    public void Ajouter_exemplaire_avec_oeuvreId_invalide_retourne_bad_request()
    {
        int idInvalide = 999;
        var result = _controller.AjouterExemplaireOeuvre(idInvalide);

        var notFoundResult = result.Result as NotFoundObjectResult;
        Assert.IsNotNull(notFoundResult);
        Assert.AreEqual($"L’œuvre avec l’id {idInvalide} n’existe pas.", notFoundResult.Value);
    }

    [TestMethod]
    public void Recuperer_reservations_oeuvre_retourne_ok_avec_la_liste_des_dto_exemplaires()
    {
        var result = _controller.RecupererReservationsOeuvre(1);

        var okResult = result.Result as OkObjectResult;
        Assert.IsNotNull(okResult);

        var dtos = okResult.Value as IEnumerable<ReservationDto>;
        Assert.IsNotNull(dtos);
        Assert.AreEqual(1, dtos.Count());
    }

    [TestMethod]
    public void Recuperer_reservations_oeuvre_avec_id_invalide_retourne_not_found()
    {
        int idInvalide = 999;
        var result = _controller.RecupererReservationsOeuvre(id: idInvalide);

        var notFound = result.Result as NotFoundObjectResult;
        Assert.IsNotNull(notFound);
    }
}
