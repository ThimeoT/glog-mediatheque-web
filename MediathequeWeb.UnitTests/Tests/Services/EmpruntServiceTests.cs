using System.Resources;

namespace MediathequeWeb.UnitTests.Services;

[TestClass]
public class EmpruntServiceTests
{
    private EmpruntService _service;
    private MockAdherentRepository _mockAdherentRepo;
    private MockEmpruntRepository _mockEmpruntRepo;
    private MockInventaireRepository _mockInventaireRepo;

    [TestInitialize]
    public void Setup()
    {
        _mockEmpruntRepo = new();
        _mockAdherentRepo = new();
        _mockInventaireRepo = new();
        _service = new EmpruntService(_mockAdherentRepo, _mockEmpruntRepo, _mockInventaireRepo);
    }

    [TestMethod]
    public void GetAllEmprunts_retourne_tous_les_emprunts()
    {
        var emprunts = _service.GetAllEmprunts();

        Assert.AreEqual(2, emprunts.Count());
    }

    [TestMethod]
    public void GetEmpruntById_existant_retourne_emprunt()
    {
        var emprunt = _service.GetEmpruntById(1);

        Assert.IsNotNull(emprunt);
        Assert.IsInstanceOfType(emprunt, typeof(Emprunt));
        Assert.AreEqual(1, emprunt.Id);
    }

    [TestMethod]
    [ExpectedException(typeof(NotFoundException))]
    public void GetEmpruntById_inexistant_lance_exception()
    {
        _service.GetEmpruntById(99);
    }

    [TestMethod]
    public void CreateEmprunt_ajoute_un_emprunt()
    {
        var emprunt = _service.CreateEmprunt(adherentId: 1, oeuvreId: 1);
        Assert.IsTrue(emprunt.Id > 0);
        Assert.AreEqual(3, _mockEmpruntRepo.GetAllEmprunts().Count());
        Assert.AreEqual(StatutExemplaire.Emprunte, emprunt.Exemplaire.Statut);
    }

    [TestMethod]
    public void CreateEmprunt_avec_reservation_transforme_statut_reservation_en_termine()
    {
        int adherentId = 1;
        int oeuvreId = 1;
        int reservationId = 5;
        Adherent adherent = _mockAdherentRepo.GetAdherent(adherentId);
        Oeuvre oeuvre = _mockInventaireRepo.GetOeuvre(oeuvreId);
        adherent.Reservations.Add(
            new Reservation
            {
                Id = reservationId,
                Adherent = adherent,
                Oeuvre = oeuvre,
                Statut = StatutReservation.Active,
            }
        );
        var emprunt = _service.CreateEmprunt(adherentId: adherentId, oeuvreId: oeuvreId);
        var reservation = adherent.Reservations.FirstOrDefault(r =>
            r.Id == reservationId
        );
        Assert.AreEqual(StatutReservation.Terminee, reservation.Statut);
    }

    [TestMethod]
    public void CreateEmprunt_sans_exemplaire_lance_domain_validation()
    {
        int oeuvreId = 1;
        var exemplaires = _mockInventaireRepo.GetAllExemplairesFromOeuvreId(oeuvreId).ToList();
        int nombreExemplaires= exemplaires.Count();
        for(int i=0; i<nombreExemplaires;i++)
            _mockInventaireRepo.RemoveExemplaire(exemplaires[0]);
        Assert.ThrowsException<DomainValidationException>(() =>
            _service.CreateEmprunt(adherentId: 1, oeuvreId: oeuvreId)
        );
    }

    [TestMethod]
    public void CreateEmprunt_avec_limite_atteinte_lance_domain_validation()
    {
        var exemplaire = _mockInventaireRepo.GetExemplaire(1);
        var adherent = _mockAdherentRepo.GetAdherent(1);
        for (int i = 0; i < 10; i++)
        {
            adherent.Emprunts.Add(
                new Emprunt
                {
                    Id = 100 + i,
                    Adherent = adherent,
                    Exemplaire = exemplaire,
                    DateEmprunt = DateOnly.FromDateTime(DateTime.Now),
                    DateRetourPrevue = DateOnly.FromDateTime(DateTime.Now).AddDays(21),
                }
            );
        }
        Assert.ThrowsException<DomainValidationException>(() =>
            _service.CreateEmprunt(adherentId: adherent.Id, oeuvreId: 1)
        );
    }

    [TestMethod]
    public void ReturnEmprunt_retourne_un_emprunt_avec_une_date_retour_relle_et_un_exemplaire_disponible()
    {
        var emprunt = _service.ReturnEmprunt(1, DateOnly.FromDateTime(DateTime.Now));

        Assert.AreEqual(DateOnly.FromDateTime(DateTime.Now), emprunt.DateRetourReelle);
        Assert.AreEqual(StatutExemplaire.Disponible, emprunt.Exemplaire.Statut);
    }

    [TestMethod]
    public void DeleteEmprunt_existant_supprime_emprunt()
    {
        _service.DeleteEmprunt(1);

        Assert.AreEqual(1, _mockEmpruntRepo.GetAllEmprunts().Count());
    }
}
