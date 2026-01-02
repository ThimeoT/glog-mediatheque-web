namespace MediathequeWeb.UnitTests.Tests.Controllers;

using Microsoft.AspNetCore.Mvc;

[TestClass]
public class ReservationControllerTests
{
    private ReservationController _controller;
    private MockReservationRepository _mockReservationRepo;

    [TestInitialize]
    public void Setup()
    {
        _mockReservationRepo = new();
        MockInventaireRepository _mockInventaireRepo = new();
        MockAdherentRepository _mockAdherentRepo = new();
        ReservationService _reservationService = new(
            _mockAdherentRepo,
            _mockInventaireRepo,
            _mockReservationRepo
        );
        _controller = new(_reservationService);
    }

    [TestMethod]
    public void Recuperer_reservations_retourne_ok_avec_la_liste_des_reservations()
    {
        var result = _controller.RecupererReservations();

        var okResult = result.Result as OkObjectResult;
        Assert.IsNotNull(okResult);

        var dtos = okResult.Value as IEnumerable<ReservationDto>;
        Assert.IsNotNull(dtos);
        Assert.AreEqual(2, dtos.Count());
    }

    [TestMethod]
    public void Ajouter_reservation_retourne_created_avec_reservation()
    {
        var result = _controller.AjouterReservation(adherentId: 1, oeuvreId: 2);

        var created = result.Result as CreatedAtActionResult;
        Assert.IsNotNull(created);

        var dto = created.Value as ReservationDto;
        Assert.IsNotNull(dto);
        Assert.AreEqual(1, dto.AdherentId);
        Assert.AreEqual(2, dto.OeuvreId);
        Assert.AreEqual("Active", dto.Statut);
    }

    [TestMethod]
    public void Ajouter_reservation_avec_exemplaire_disponible_retourne_bad_request()
    {
        var result = _controller.AjouterReservation(adherentId: 1, oeuvreId: 1);

        var badRequest = result.Result as BadRequestObjectResult;
        Assert.IsNotNull(badRequest);
        Assert.AreEqual("Pas besoin de réserver, un exemplaire est déjà disponible !", badRequest.Value);
    }

    [TestMethod]
    public void Ajouter_reservation_avec_adherentId_invalide_retourne_not_found()
    {
        int idInvalide = 999;
        var result = _controller.AjouterReservation(adherentId: idInvalide, oeuvreId: 1);

        var notFoundResult = result.Result as NotFoundObjectResult;
        Assert.IsNotNull(notFoundResult);
        Assert.AreEqual("Adhérent introuvable.", notFoundResult.Value);
    }

    [TestMethod]
    public void Ajouter_reservation_avec_oeuvreId_invalide_retourne_not_found()
    {
        int idInvalide = 999;
        var result = _controller.AjouterReservation(adherentId: 1, oeuvreId: idInvalide);

        var notFoundResult = result.Result as NotFoundObjectResult;
        Assert.IsNotNull(notFoundResult);
        Assert.AreEqual("Œuvre introuvable.", notFoundResult.Value);
    }

    [TestMethod]
    public void Supprimer_reservation_avec_id_valide_retourne_no_content()
    {
        var result = _controller.SupprimerReservation(1);

        var noContent = result as NoContentResult;
        Assert.IsNotNull(noContent);
        Assert.AreEqual(1, _mockReservationRepo.GetAllReservations().Count());
    }

    [TestMethod]
    public void Supprimer_reservation_avec_id_invalide_retourne_not_found()
    {
        var result = _controller.SupprimerReservation(999);

        var notFoundResult = result as NotFoundObjectResult;
        Assert.IsNotNull(notFoundResult);
    }
}
