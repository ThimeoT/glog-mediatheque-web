[TestClass]
public class InventaireServiceTests
{
    private InventaireService _service;
    private MockInventaireRepository _repo;

    [TestInitialize]
    public void Setup()
    {
        _repo = new();
        _service = new(_repo);
    }
    public void GetAllAuteurs_retourne_tous_les_auteurs()
    {
        var auteurs = _service.GetAllAuteurs();
        Assert.AreEqual(2, auteurs.Count());
    }
}
