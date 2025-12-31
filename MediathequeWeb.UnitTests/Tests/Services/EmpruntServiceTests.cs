namespace MediathequeWeb.UnitTests.Services;

[TestClass]
public class EmpruntServiceTests
{
    private EmpruntService _service;
    private MockEmpruntRepository _mockEmpruntRepo;

    [TestInitialize]
    public void Setup()
    {
        _mockEmpruntRepo = new MockEmpruntRepository();
        MockAdherentRepository _mockAdherentRepo = new();
        MockInventaireRepository _mockInventaireRepo = new();
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
        var emprunt = _service.CreateEmprunt(adherentId: 1,oeuvreId: 1);
        Assert.IsTrue(emprunt.Id > 0);
        Assert.AreEqual(3, _mockEmpruntRepo.GetAllEmprunts().Count());
    }


    [TestMethod]
    public void ReturnEmprunt_retourne_un_emprunt_avec_une_date_retour_relle_et_un_exemplaire_disponible()
    {
        var emprunt = _service.ReturnEmprunt(1,DateOnly.FromDateTime(DateTime.Now));

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
