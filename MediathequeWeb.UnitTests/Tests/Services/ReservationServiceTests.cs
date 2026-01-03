namespace MediathequeWeb.UnitTests.Services;

[TestClass]
public class ReservationServiceTests
{
    private ReservationService _service;
    private MockReservationRepository _mockReservationRepo;
    private MockStatistiquesRepository _mockStatistiquesRepo;

    [TestInitialize]
    public void Setup()
    {
        _mockReservationRepo = new MockReservationRepository();
        MockInventaireRepository _mockInventaireRepo = new();
        _mockStatistiquesRepo = new();
        MockAdherentRepository _mockAdherentRepo = new();
        _service = new ReservationService(
            _mockAdherentRepo,
            _mockInventaireRepo,
            _mockReservationRepo,
            _mockStatistiquesRepo
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
    public void GetAllReservationsFromAdherentId_avec_adherentId_inexistant_lance_not_found_exception()
    {
        var ex = Assert.ThrowsException<NotFoundException>(() =>
            _service.GetAllReservationsFromAdherentId(99)
        );

        Assert.AreEqual($"L’adhérent avec l’id 99 n’existe pas.", ex.Message);
    }

    [TestMethod]
    public void GetAllReservationsFromOeuvreId_avec_oeuvreId_inexistant_lance_not_found_exception()
    {
        var ex = Assert.ThrowsException<NotFoundException>(() =>
            _service.GetAllReservationsFromOeuvreId(99)
        );

        Assert.AreEqual("L’œuvre avec l’id 99 n’existe pas.", ex.Message);
    }

    [TestMethod]
    public void GetAllReservationsFromOeuvreId_retourne_toutes_les_reservations_associees_a_une_oeuvre()
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
    public void CreateReservation_ajoute_une_reservation()
    {
        var reservation = _service.CreateReservation(adherentId: 1, oeuvreId: 2);

        Assert.IsTrue(reservation.Id > 0);
        Assert.AreEqual(3, _mockReservationRepo.GetAllReservations().Count());
    }

    [TestMethod]
    public void CreateReservation_avec_exemplaire_disponible_lance_domain_validation_exception()
    {
        var ex = Assert.ThrowsException<DomainValidationException>(() =>
            _service.CreateReservation(adherentId: 1, oeuvreId: 1)
        );

        Assert.AreEqual("Pas besoin de réserver, un exemplaire est déjà disponible !", ex.Message);
    }

    [TestMethod]
    public void CreateReservation_avec_reservation_active_existante_lance_domain_validation_exception()
    {
        int adherentId = 1;
        int oeuvreId = 2;

        var reservationExistante = new Reservation
        {
            Id = 99,
            Adherent = new Adherent { Id = adherentId },
            Oeuvre = new Oeuvre { Id = oeuvreId },
            DateReservation = DateOnly.FromDateTime(DateTime.Now.AddDays(-1)),
            Statut = StatutReservation.Active,
        };

        _mockReservationRepo.AddReservation(reservationExistante);

        var ex = Assert.ThrowsException<DomainValidationException>(() =>
            _service.CreateReservation(adherentId, oeuvreId)
        );

        Assert.AreEqual("Une réservation active existe déjà pour cette œuvre.", ex.Message);
    }

    [TestMethod]
    public void DeleteReservation_existant_supprime_reservation_et_retire_activite()
    {
        int reservationId = 50;
        DateOnly dateReservation = DateOnly.FromDateTime(DateTime.Now);
        Reservation reservation = new() { DateReservation = dateReservation };
        _mockReservationRepo.AddReservation(reservation);
        reservation.Id = 50;
        Assert.IsNotNull(_mockReservationRepo.GetReservation(reservationId));
        _mockStatistiquesRepo.AddStatistiques(
            new()
            {
                Id = 5,
                Date = dateReservation,
                NouvellesReservations = 1,
            }
        );

        _service.DeleteReservation(reservationId);

        Assert.AreEqual(2, _mockReservationRepo.GetAllReservations().Count());
        Assert.IsNull(_mockReservationRepo.GetReservation(reservationId));
    }

    [TestMethod]
    public void DeleteReservation_inexistante_lance_not_found_exception()
    {
        var ex = Assert.ThrowsException<NotFoundException>(() => _service.DeleteReservation(999));

        Assert.AreEqual("Réservation introuvable.", ex.Message);
    }
}
