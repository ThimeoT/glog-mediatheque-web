namespace MediathequeWeb.UnitTests.Services;

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
    [ExpectedException(typeof(NotFoundException))]
    public void GetAuteurById_inexistant_lance_exception()
    {
        _service.GetAuteurById(999);
    }

    [TestMethod]
    public void GetAllAuteurs_retourne_tous_les_auteurs()
    {
        var auteurs = _service.GetAllAuteurs();
        Assert.AreEqual(2, auteurs.Count());
    }

    [TestMethod]
    public void CreateAuteur_ajoute_un_auteur()
    {
        var auteur = _service.CreateAuteur("Zola", "Emile");

        Assert.IsTrue(auteur.Id > 0);
        Assert.AreEqual(3, _repo.GetAllAuteurs().Count());
    }

    [TestMethod]
    public void UpdateAuteur_modifie_les_donnees()
    {
        var auteur = _service.UpdateAuteur(1, "Mapped", "Kylian");

        Assert.AreEqual("Mapped", auteur.Nom);
        Assert.AreEqual("Kylian", auteur.Prenom);
    }

    [TestMethod]
    public void DeleteAuteur_existant_supprime_auteur()
    {
        _service.DeleteAuteur(1);

        Assert.AreEqual(1, _repo.GetAllAuteurs().Count());
    }

    [TestMethod]
    [ExpectedException(typeof(NotFoundException))]
    public void GetOeuvreById_inexistant_lance_exception()
    {
        _service.GetOeuvreById(999);
    }

    [TestMethod]
    public void GetAllOeuvres_retourne_toutes_les_oeuvres()
    {
        var oeuvres = _service.GetAllOeuvres();
        Assert.AreEqual(2, oeuvres.Count());
    }

    [TestMethod]
    public void CreateOeuvre_ajoute_une_oeuvre()
    {
        var oeuvre = _service.CreateOeuvre("Feel", "Livre", 1);

        Assert.IsTrue(oeuvre.Id > 0);
        Assert.AreEqual(3, _repo.GetAllOeuvres().Count());
    }

    [TestMethod]
    public void UpdateOeuvre_modifie_les_donnees()
    {
        var oeuvre = _service.UpdateOeuvre(1, "Feel", "Livre", 1);
        Assert.AreEqual("Feel", oeuvre.Titre);
        Assert.AreEqual(OeuvreType.Livre, oeuvre.Type);
    }

    [TestMethod]
    public void DeleteOeuvre_existant_supprime_oeuvre()
    {
        _service.DeleteOeuvre(1);

        Assert.AreEqual(1, _repo.GetAllOeuvres().Count());
    }

    [TestMethod]
    [ExpectedException(typeof(NotFoundException))]
    public void GetExemplaireById_inexistant_lance_exception()
    {
        _service.GetExemplaireById(999);
    }

    [TestMethod]
    public void GetAllExemplaires_retourne_tous_les_exemplaires()
    {
        var exemplaire = _service.GetAllExemplaires();
        Assert.AreEqual(2, exemplaire.Count());
    }

    [TestMethod]
    public void GetAllExemplairesFromOeuvreId_retourne_tous_les_exemplaires_de_la_meme_oeuvre()
    {
        var exemplaires = _service.GetAllExemplairesFromOeuvreId(1);
        Assert.AreEqual(1, exemplaires.Count());
    }

    [TestMethod]
    public void CreateExemplaire_ajoute_un_exemplaire()
    {
        var exemplaire = _service.CreateExemplaire(1);

        Assert.IsTrue(exemplaire.Id > 0);
        Assert.AreEqual(3, _repo.GetAllExemplaires().Count());
    }

    [TestMethod]
    public void DeleteExemplaire_existant_supprime_exemplaire()
    {
        _service.DeleteExemplaire(1);
        Assert.AreEqual(1, _repo.GetAllExemplaires().Count());
    }
}
