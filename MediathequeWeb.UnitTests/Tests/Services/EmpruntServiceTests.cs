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
        MockBilanActiviteQuotidienRepository _mockBilanActiviteQuotidienRepo = new();
        _service = new EmpruntService(_mockAdherentRepo, _mockEmpruntRepo, _mockInventaireRepo,_mockBilanActiviteQuotidienRepo);
    }

    [TestMethod]
    public void GetAllEmprunts_retourne_tous_les_emprunts()
    {
        var emprunts = _service.GetAllEmprunts();

        Assert.AreEqual(2, emprunts.Count());
    }

    [TestMethod]
    public void GetEmpruntById_avec_id_existant_retourne_emprunt()
    {
        var emprunt = _service.GetEmpruntById(1);

        Assert.IsNotNull(emprunt);
        Assert.IsInstanceOfType<Emprunt>(emprunt);
        Assert.AreEqual(1, emprunt.Id);
    }

    [TestMethod]
    public void GetEmpruntById_avec_id_inexistant_lance_exception()
    {
        var ex = Assert.ThrowsException<NotFoundException>(() => _service.GetEmpruntById(99));

        Assert.AreEqual("Emprunt introuvable.", ex.Message);
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

        Adherent? adherent = _mockAdherentRepo.GetAdherent(adherentId);
        Oeuvre? oeuvre = _mockInventaireRepo.GetOeuvre(oeuvreId);

        Assert.IsNotNull(adherent);
        Assert.IsNotNull(oeuvre);

        Reservation reservation = new()
        {
            Id = 5,
            Adherent = adherent,
            Oeuvre = oeuvre,
            Statut = StatutReservation.Active,
        };

        adherent.Reservations.Add(reservation);

        _service.CreateEmprunt(adherentId, oeuvreId);

        Assert.AreEqual(StatutReservation.Terminee, reservation.Statut);
    }

    [TestMethod]
    public void CreateEmprunt_sans_exemplaire_disponible_lance_domain_validation_exception()
    {
        int oeuvreId = 1;

        var exemplaires = _mockInventaireRepo.GetAllExemplairesFromOeuvreId(oeuvreId).ToList();

        foreach (var exemplaire in exemplaires)
        {
            _mockInventaireRepo.RemoveExemplaire(exemplaire);
        }

        var ex = Assert.ThrowsException<DomainValidationException>(() =>
            _service.CreateEmprunt(adherentId: 1, oeuvreId: oeuvreId)
        );

        Assert.AreEqual("Aucun exemplaire disponible.", ex.Message);
    }

    [TestMethod]
    public void CreateEmprunt_avec_limite_atteinte_lance_domain_validation_exception()
    {
        Exemplaire? exemplaire = _mockInventaireRepo.GetExemplaire(1);
        Adherent? adherent = _mockAdherentRepo.GetAdherent(1);

        Assert.IsNotNull(exemplaire);
        Assert.IsNotNull(adherent);

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

        var ex = Assert.ThrowsException<DomainValidationException>(() =>
            _service.CreateEmprunt(adherent.Id, oeuvreId: 1)
        );

        Assert.AreEqual("Limite de 10 emprunts atteinte.", ex.Message);
    }

    [TestMethod]
    public void ReturnEmprunt_retourne_un_emprunt_avec_une_date_retour_relle_et_remet_exemplaire_disponible()
    {
        DateOnly dateRetour = DateOnly.FromDateTime(DateTime.Now);

        var emprunt = _service.ReturnEmprunt(1, dateRetour);

        Assert.AreEqual(dateRetour, emprunt.DateRetourReelle);
        Assert.AreEqual(StatutExemplaire.Disponible, emprunt.Exemplaire.Statut);
    }

    [TestMethod]
    public void ReturnEmprunt_deja_retourne_lance_domain_validation_exception()
    {
        var emprunt = _service.GetEmpruntById(1);
        emprunt.DateRetourReelle = DateOnly.FromDateTime(DateTime.Now);

        var ex = Assert.ThrowsException<DomainValidationException>(() =>
            _service.ReturnEmprunt(1, DateOnly.FromDateTime(DateTime.Now))
        );

        Assert.AreEqual("Cet emprunt a déjà été retourné.", ex.Message);
    }

    [TestMethod]
    public void ReturnEmprunt_inexistant_lance_not_found_exception()
    {
        var ex = Assert.ThrowsException<NotFoundException>(() =>
            _service.ReturnEmprunt(999, DateOnly.FromDateTime(DateTime.Now))
        );

        Assert.AreEqual("Emprunt introuvable.", ex.Message);
    }

    [TestMethod]
    public void DeleteEmprunt_existant_supprime_emprunt_et_remet_exemplaire_disponible()
    {
        Adherent? adherent = _mockAdherentRepo.GetAdherent(1);
        Exemplaire? exemplaire = _mockInventaireRepo.GetExemplaire(1);

        Assert.IsNotNull(adherent);
        Assert.IsNotNull(exemplaire);

        Emprunt emprunt = new() { Adherent = adherent, Exemplaire = exemplaire };

        _mockEmpruntRepo.AddEmprunt(emprunt);
        int empruntId = emprunt.Id;

        _service.DeleteEmprunt(empruntId);

        Assert.ThrowsException<NotFoundException>(() => _service.GetEmpruntById(empruntId));
        Assert.AreEqual(StatutExemplaire.Disponible, exemplaire.Statut);
    }
}
