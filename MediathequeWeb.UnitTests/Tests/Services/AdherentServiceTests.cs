using Microsoft.Extensions.Logging.Abstractions;

namespace MediathequeWeb.UnitTests.Services;

[TestClass]
public class AdherentServiceTests
{
    private AdherentService _service;
    private MockAdherentRepository _adherentRepo;

    [TestInitialize]
    public void Setup()
    {
        _adherentRepo = new MockAdherentRepository();
        _service = new AdherentService(_adherentRepo);
    }

    [TestMethod]
    public void GetAllAdherents_retourne_tous_les_adherents()
    {
        var adherents = _service.GetAllAdherents();

        Assert.AreEqual(2, adherents.Count());
    }

    [TestMethod]
    public void GetAdherentById_avec_id_existant_retourne_adherent()
    {
        var adherent = _service.GetAdherentById(1);

        Assert.AreEqual("Némard", adherent.Nom);
        Assert.AreEqual("Jean", adherent.Prenom);
    }

    [TestMethod]
    public void GetAdherentById_avec_id_inexistant_lance_not_found_exception()
    {
        var ex = Assert.ThrowsException<NotFoundException>(() => _service.GetAdherentById(99));

        Assert.AreEqual("L’adhérent avec l’id 99 n’existe pas.", ex.Message);
    }

    [TestMethod]
    public void CreateAdherent_ajoute_un_adherent()
    {
        var adherent = _service.CreateAdherent("Gonzola", "Igor", "igorgonzola@ensc.fr");

        Assert.IsTrue(adherent.Id > 0);
        Assert.AreEqual(3, _adherentRepo.GetAllAdherents().Count());
    }

    [TestMethod]
    public void UpdateAdherent_modifie_les_donnees_d_un_adherent()
    {
        var adherent = _service.UpdateAdherent(1, "Emarre", "Jean", "jeanemarre@ensc.fr");

        Assert.AreEqual("Emarre", adherent.Nom);
        Assert.AreEqual("jeanemarre@ensc.fr", adherent.Email);
    }

    [TestMethod]
    public void DeleteAdherent_existant_supprime_adherent()
    {
        _service.DeleteAdherent(1);

        Assert.AreEqual(1, _adherentRepo.GetAllAdherents().Count());
    }
}
