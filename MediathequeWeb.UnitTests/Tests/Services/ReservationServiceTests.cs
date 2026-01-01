namespace MediathequeWeb.UnitTests.Services;

[TestClass]
public class ReservationServiceTests
{
    private ReservationService _service;
    private MockReservationRepository _mockReservationRepo;

    [TestInitialize]
    public void Setup()
    {
        _mockReservationRepo = new MockReservationRepository();
        MockInventaireRepository _mockInventaireRepo = new();
        MockAdherentRepository _mockAdherentRepo = new();
        _service = new ReservationService(
            _mockAdherentRepo,
            _mockInventaireRepo,
            _mockReservationRepo
        );
    }

    [TestMethod]
    public void GetAllReservations_retourne_toutes_les_reservations()
    {
        var reservations = _service.GetAllReservations();

        Assert.AreEqual(2, reservations.Count());
    }

    [TestMethod]
    public void GetAllReservationsFromAdherentId_retourne_toutes_les_reservations_associees_a_un_adherent()
    {
        int idAdherent = 1;
        var reservations = _service.GetAllReservationsFromAdherentId(idAdherent);
        Assert.AreEqual(1, reservations.Count());
        foreach (Reservation reservation in reservations)
        {
            Assert.AreEqual(idAdherent, reservation.Adherent.Id);
        }
    }

    [TestMethod]
    public void GetAllReservationsFromOeuvreId_avec_oeuvreId_inexistant_lance_exception()
    {
        var ex = Assert.ThrowsException<NotFoundException>(
            () => _service.GetAllReservationsFromOeuvreId(99)
        );

        Assert.AreEqual(
            "L’œuvre avec l’id 99 n’existe pas.",
            ex.Message
        );
    }

    [TestMethod]
    public void GetAllReservationsFromOeuvreId_retourne_toutes_les_reservations_associees_a_une_Oeuvre()
    {
        int idOeuvre = 1;
        var reservations = _service.GetAllReservationsFromOeuvreId(idOeuvre);
        Assert.AreEqual(1, reservations.Count());
        foreach (Reservation reservation in reservations)
        {
            Assert.AreEqual(idOeuvre, reservation.Oeuvre.Id);
        }
    }

    [TestMethod]
    public void GetAllReservationsFromOeuvreId_inexistant_lance_exception()
    {
        var ex = Assert.ThrowsException<NotFoundException>(
            () => _service.GetAllReservationsFromOeuvreId(99)
        );

        Assert.AreEqual(
            $"L’œuvre avec l’id 99 n’existe pas.",
            ex.Message
        );
    }

    [TestMethod]
    public void CreateReservation_ajoute_une_reservation()
    {
        var reservation = _service.CreateReservation(adherentId: 1, oeuvreId: 2);

        Assert.IsTrue(reservation.Id > 0);
        Assert.AreEqual(3, _mockReservationRepo.GetAllReservations().Count());
    }

    [TestMethod]
    public void CreateReservation_avec_exemplaire_disponible_lance_exception()
    {
        var ex = Assert.ThrowsException<DomainValidationException>(
            () => _service.CreateReservation(adherentId: 1, oeuvreId: 1)
        );

        Assert.AreEqual(
            "Pas besoin de réserver, un exemplaire est déjà disponible !",
            ex.Message
        );
    }

    [TestMethod]
    public void DeleteReservation_existant_supprime_reservation()
    {
        _service.DeleteReservation(1);

        Assert.AreEqual(1, _mockReservationRepo.GetAllReservations().Count());
    }
}
