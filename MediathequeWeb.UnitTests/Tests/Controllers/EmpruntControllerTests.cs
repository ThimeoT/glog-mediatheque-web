namespace MediathequeWeb.UnitTests.Tests.Controllers;

using Microsoft.AspNetCore.Mvc;

[TestClass]
public class EmpruntControllerTests
{
    private EmpruntController _controller;
    private EmpruntService _service;
    private MockEmpruntRepository _mockEmpruntRepo;
    private MockAdherentRepository _mockAdherentRepo;
    private MockInventaireRepository _mockInventaireRepo;

    [TestInitialize]
    public void Setup()
    {
        _mockEmpruntRepo = new();
        _mockAdherentRepo = new();
        _mockInventaireRepo = new();
        _service = new(_mockAdherentRepo, _mockEmpruntRepo, _mockInventaireRepo);
        _controller = new(_service);
    }

    [TestMethod]
    public void Recuperer_emprunts_retourne_ok_avec_la_liste_des_emprunts()
    {
        var result = _controller.RecupererEmprunts();

        var okResult = result.Result as OkObjectResult;
        Assert.IsNotNull(okResult);

        var dtos = okResult.Value as IEnumerable<EmpruntDto>;
        Assert.IsNotNull(dtos);
        Assert.AreEqual(2, dtos.Count());
    }

    [TestMethod]
    public void Recuperer_emprunt_avec_id_valide_retourne_ok_avec_emprunt()
    {
        var result = _controller.RecupererEmprunt(1);

        var okResult = result.Result as OkObjectResult;
        Assert.IsNotNull(okResult);

        var dto = okResult.Value as EmpruntDto;
        Assert.IsNotNull(dto);
        Assert.AreEqual("Jean Némard", dto.Adherent);
        Assert.AreEqual("Kids", dto.Oeuvre);
        Assert.AreEqual(dto.DateEmprunt.AddDays(21), dto.DateRetourPrevue);
    }

    [TestMethod]
    public void Recuperer_emprunt_avec_id_invalide_retourne_not_found()
    {
        var result = _controller.RecupererEmprunt(99);

        var notFound = result.Result as NotFoundObjectResult;
        Assert.IsNotNull(notFound);
    }

    [TestMethod]
    public void Ajouter_emprunt_retourne_created_avec_emprunt()
    {
        var result = _controller.AjouterEmprunt(adherentId: 1, oeuvreId: 1);

        var created = result.Result as CreatedAtActionResult;
        Assert.IsNotNull(created);

        var dto = created.Value as EmpruntDto;
        Assert.IsNotNull(dto);
        Assert.AreEqual("Jean Némard", dto.Adherent);
        Assert.AreEqual("Kids", dto.Oeuvre);
        Assert.AreEqual(DateOnly.FromDateTime(DateTime.Now), dto.DateEmprunt);
        Assert.AreEqual(dto.DateEmprunt.AddDays(21), dto.DateRetourPrevue);
        Assert.IsNull(dto.DateRetourReelle);
        
    }

    [TestMethod]
    public void Ajouter_emprunt_avec_adherentId_invalide_retourne_not_found()
    {
        int idInvalide = 999;
        var result = _controller.AjouterEmprunt(adherentId: idInvalide, oeuvreId: 1);

        var notFoundResult = result.Result as NotFoundObjectResult;
        Assert.IsNotNull(notFoundResult);
        Assert.AreEqual("Adhérent introuvable.", notFoundResult.Value);
    }

    [TestMethod]
    public void Ajouter_emprunt_avec_oeuvreId_invalide_retourne_not_found()
    {
        int idInvalide = 999;
        var result = _controller.AjouterEmprunt(adherentId: 1, oeuvreId: idInvalide);

        var notFoundResult = result.Result as NotFoundObjectResult;
        Assert.IsNotNull(notFoundResult);
        Assert.AreEqual($"L’œuvre avec l’id {idInvalide} n’existe pas.", notFoundResult.Value);
    }

    [TestMethod]
    public void Ajouter_emprunt_sans_exemplaire_disponible_retourne_bad_request()
    {
        int oeuvreId = 1;

        var inventaireRepo = new MockInventaireRepository();
        var exemplairesOeuvre = inventaireRepo.GetAllExemplairesFromOeuvreId(oeuvreId).ToList();

        foreach (var ex in exemplairesOeuvre)
            inventaireRepo.RemoveExemplaire(ex);

        EmpruntController controller = new(
            new EmpruntService(
                new MockAdherentRepository(),
                new MockEmpruntRepository(),
                inventaireRepo
            )
        );

        var result = controller.AjouterEmprunt(adherentId: 1, oeuvreId: oeuvreId);

        var badRequest = result.Result as BadRequestObjectResult;
        Assert.IsNotNull(badRequest);
        Assert.AreEqual("Aucun exemplaire disponible.", badRequest.Value);
    }

    [TestMethod]
    public void Ajouter_emprunt_avec_limite_emprunts_atteinte_retourne_bad_request()
    {
        var adherent = _mockAdherentRepo.GetAdherent(1);
        Assert.IsNotNull(adherent);

        var exemplaire = _mockInventaireRepo.GetExemplaire(1);
        Assert.IsNotNull(exemplaire);

        for (int i = 0; i < 10; i++)
        {
            adherent.Emprunts.Add(new Emprunt
            {
                Id = 100 + i,
                Adherent = adherent,
                Exemplaire = exemplaire,
                DateEmprunt = DateOnly.FromDateTime(DateTime.Now),
                DateRetourPrevue = DateOnly.FromDateTime(DateTime.Now).AddDays(21)
            });
        }

        var result = _controller.AjouterEmprunt(adherentId: 1, oeuvreId: 1);

        var badRequest = result.Result as BadRequestObjectResult;
        Assert.IsNotNull(badRequest);
        Assert.AreEqual("Limite de 10 emprunts atteinte.", badRequest.Value);
    }

    [TestMethod]
    public void Retourner_emprunt_avec_id_valide_retourne_ok_avec_emprunt_modifie()
    {
        var result = _controller.RetournerEmprunt(
            id: 1,
            dateRetourReelle: DateOnly.FromDateTime(DateTime.Now)
        );

        var okResult = result.Result as OkObjectResult;
        Assert.IsNotNull(okResult);

        var dto = okResult.Value as EmpruntDto;
        Assert.IsNotNull(dto);
        Assert.AreEqual("Jean Némard", dto.Adherent);
        Assert.AreEqual("Kids", dto.Oeuvre);
    }

    [TestMethod]
    public void Retourner_emprunt_avec_id_invalide_retourne_not_found()
    {
        int idInvalide = 999;
        var result = _controller.RetournerEmprunt(id: idInvalide,
            dateRetourReelle: DateOnly.FromDateTime(DateTime.Now));

        var notFoundResult = result.Result as NotFoundObjectResult;
        Assert.IsNotNull(notFoundResult);
        Assert.AreEqual("Emprunt introuvable.", notFoundResult.Value);
    }

    [TestMethod]
    public void Retourner_emprunt_avec_date_invalide_retourne_bad_request()
    {
        DateOnly dateInvalide = DateOnly.FromDateTime(DateTime.Now).AddDays(-120);
        var result = _controller.RetournerEmprunt(id: 1,
            dateRetourReelle: dateInvalide );

        var badRequestResult = result.Result as BadRequestObjectResult;
        Assert.IsNotNull(badRequestResult);
        Assert.AreEqual("La date de retour réelle ne peut pas être avant la date d’emprunt.", badRequestResult.Value);
    }

    [TestMethod]
    public void Retourner_emprunt_deja_retourne_retourne_bad_request()
    {
        var emprunt = _mockEmpruntRepo.GetEmprunt(1);
        Assert.IsNotNull(emprunt);
        
        emprunt.DateRetourReelle = DateOnly.FromDateTime(DateTime.Now);

        var result = _controller.RetournerEmprunt(
            id: 1,
            dateRetourReelle: DateOnly.FromDateTime(DateTime.Now)
        );

        var badRequest = result.Result as BadRequestObjectResult;
        Assert.IsNotNull(badRequest);
        Assert.AreEqual("Cet emprunt a déjà été retourné.", badRequest.Value);
    }
    
    [TestMethod]
    public void Supprimer_emprunt_avec_id_valide_retourne_no_content()
    {
        var result = _controller.SupprimerEmprunt(1);

        var noContent = result as NoContentResult;
        Assert.IsNotNull(noContent);
        Assert.AreEqual(1, _mockEmpruntRepo.GetAllEmprunts().Count());
    }

    [TestMethod]
    public void Supprimer_emprunt_avec_id_invalide_retourne_not_found()
    {
        var result = _controller.SupprimerEmprunt(99);

        var notFoundResult = result as NotFoundObjectResult;
        Assert.IsNotNull(notFoundResult);
    }
}
