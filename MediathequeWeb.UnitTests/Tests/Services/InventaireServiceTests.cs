[TestClass]
public class InventaireServiceTests
{
    private InventaireService _service;
    private MockInventaireRepository _repo;

    [TestInitialize]
    public void Setup()
    {
        _repo = new();
        _service = new InventaireService(_repo);
    }

    [TestMethod]
    public void GetAllAuteurs_retourne_tous_les_auteurs()
    {
        var auteurs = _service.GetAllAuteurs();
        Assert.AreEqual(2, auteurs.Count());
    }

    [TestMethod]
    [ExpectedException(typeof(NotFoundException))]
    public void GetAuteurById_inexistant_lance_exception()
    {
        _service.GetAuteurById(99);
    }
    [TestMethod]
    public void CreateAdherent_ajoute_un_adherent()
    {
        var auteur = _service.CreateAuteur("Zola","Emile");

        Assert.IsTrue(auteur.Id > 0);
        Assert.AreEqual(3, _repo.GetAllAuteurs().Count());
    }
    [TestMethod]
    public void UpdateAdherent_modifie_les_donnees()
    {
        var auteur = _service.UpdateAuteur(1, "Mapped", "Kylian");

        Assert.AreEqual("Mapped", auteur.Nom);
        Assert.AreEqual("Kylian", auteur.Prenom);
    }
    [TestMethod]
    public void DeleteAdherent_existant_supprime_adherent()
    {
        _service.DeleteAuteur(1);

        Assert.AreEqual(1, _repo.GetAllAuteurs().Count());
    }
}
