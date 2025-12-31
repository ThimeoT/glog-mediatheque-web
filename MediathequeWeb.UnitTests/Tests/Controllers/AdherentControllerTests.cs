using Microsoft.AspNetCore.Mvc;

namespace MediathequeWeb.UnitTests.Tests.Controllers;

[TestClass]
public class AdherentControllerTests
{
    private AdherentController _controller;
    private MockAdherentRepository _mockAdherentRepo;

    [TestInitialize]
    public void Setup()
    {
        _mockAdherentRepo = new();
        MockInventaireRepository _mockInventaireRepo = new();
        MockReservationRepository _mockReservationRepo = new();
        var adherentService = new AdherentService(_mockAdherentRepo);
        ReservationService reservationService = new(
            _mockAdherentRepo,
            _mockInventaireRepo,
            _mockReservationRepo
        );

        _controller = new AdherentController(adherentService, reservationService);
    }

    [TestMethod]
    public void Recuperer_adherents_retourne_ok_avec_la_liste_des_adherents()
    {
        var result = _controller.RecupererAdherents();

        var okResult = result.Result as OkObjectResult;
        Assert.IsNotNull(okResult);

        var dtos = okResult.Value as IEnumerable<AdherentDto>;
        Assert.IsNotNull(dtos);
        Assert.AreEqual(2, dtos.Count());
    }

    [TestMethod]
    public void Recuperer_adherent_avec_id_valide_retourne_ok_avec_adherent()
    {
        var result = _controller.RecupererAdherent(1);

        var okResult = result.Result as OkObjectResult;
        Assert.IsNotNull(okResult);

        var dto = okResult.Value as AdherentDto;
        Assert.IsNotNull(dto);
        Assert.AreEqual("Némard", dto.Nom);
    }

    [TestMethod]
    public void Recuperer_adherent_id_avec_invalide_retourne_not_found()
    {
        var result = _controller.RecupererAdherent(99);

        var notFound = result.Result as NotFoundObjectResult;
        Assert.IsNotNull(notFound);
    }

    [TestMethod]
    public void Ajouter_adherent_retourne_created_avec_adherent()
    {
        var result = _controller.AjouterAdherent("Gonzola", "Igor", "igorgonzola@ensc.fr");

        var created = result.Result as CreatedAtActionResult;
        Assert.IsNotNull(created);

        var dto = created.Value as AdherentDto;
        Assert.IsNotNull(dto);
        Assert.AreEqual("Gonzola", dto.Nom);
    }

    [TestMethod]
    public void Ajouter_adherent_avec_nom_vide_retourne_bad_request()
    {
        string nomVide = "";
        var result = _controller.AjouterAdherent(nomVide, "Igor", "igorgonzola@ensc.fr");

        var badRequestResult = result.Result as BadRequestObjectResult;
        Assert.IsNotNull(badRequestResult);
        Assert.AreEqual("Le nom ne peut pas être vide.", badRequestResult.Value);
    }

    [TestMethod]
    public void Ajouter_adherent_avec_prenom_vide_retourne_bad_request()
    {
        string prenomVide = "";
        var result = _controller.AjouterAdherent("Gonzola", prenomVide, "igorgonzola@ensc.fr");

        var badRequestResult = result.Result as BadRequestObjectResult;
        Assert.IsNotNull(badRequestResult);
        Assert.AreEqual("Le prénom ne peut pas être vide.", badRequestResult.Value);
    }

    [TestMethod]
    public void Ajouter_adherent_avec_mail_vide_retourne_bad_request()
    {
        string mailVide = "";
        var result = _controller.AjouterAdherent("Gonzola", "Igor", mailVide);

        var badRequestResult = result.Result as BadRequestObjectResult;
        Assert.IsNotNull(badRequestResult);
        Assert.AreEqual("L’adresse email ne peut pas être vide.", badRequestResult.Value);
    }

    [TestMethod]
    public void Modifier_adherent_avec_id_valide_retourne_ok_avec_adherent_modifie()
    {
        var result = _controller.ModifierAdherent(1, "Emarre", "Jean", "jeanemarre@ensc.fr");

        var okResult = result.Result as OkObjectResult;
        Assert.IsNotNull(okResult);

        var dto = okResult.Value as AdherentDto;
        Assert.IsNotNull(dto);
        Assert.AreEqual("Emarre", dto.Nom);
        Assert.AreEqual("Jean", dto.Prenom);
        Assert.AreEqual("jeanemarre@ensc.fr", dto.Email);
    }

    [TestMethod]
    public void Modifier_adherent_avec_id_invalide_retourne_not_found()
    {
        var result = _controller.ModifierAdherent(99, "Emarre", "Jean", "jeanemarre@ensc.fr");

        var notFoundResult = result.Result as NotFoundObjectResult;
        Assert.IsNotNull(notFoundResult);
    }

    [TestMethod]
    public void Modifier_adherent_avec_nom_vide_retourne_bad_request()
    {
        var result = _controller.ModifierAdherent(1, "", "Jean", "jeanemarre@ensc.fr");

        var badRequestResult = result.Result as BadRequestObjectResult;
        Assert.IsNotNull(badRequestResult);
        Assert.AreEqual("Le nom ne peut pas être vide.", badRequestResult.Value);
    }

    [TestMethod]
    public void Supprimer_adherent_avec_id_valide_retourne_no_content()
    {
        var result = _controller.SupprimerAdherent(1);

        var noContent = result as NoContentResult;
        Assert.IsNotNull(noContent);
        Assert.AreEqual(1, _mockAdherentRepo.GetAllAdherents().Count());
    }

    [TestMethod]
    public void Supprimer_adherent_avec_id_invalide_retourne_not_found()
    {
        var result = _controller.SupprimerAdherent(99);

        var notFoundResult = result as NotFoundObjectResult;
        Assert.IsNotNull(notFoundResult);
    }

    [TestMethod]
    public void Recuperer_reservations_adherent_retourne_ok_avec_la_liste_des_dto_exemplaires()
    {
        var result = _controller.RecupererReservationsAdherent(1);

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
        var result = _controller.RecupererReservationsAdherent(id: idInvalide);

        var notFound = result.Result as NotFoundObjectResult;
        Assert.IsNotNull(notFound);
    }
}
