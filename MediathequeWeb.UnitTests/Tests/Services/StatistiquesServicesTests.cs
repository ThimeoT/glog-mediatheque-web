// using Microsoft.Extensions.Logging.Abstractions;

// namespace MediathequeWeb.UnitTests.Services;

// [TestClass]
// public class StatistiquesServiceTests
// {
//     private StatistiquesService _service;
//     private MockAdherentRepository _mockAdherentRepo;
//     private MockEmpruntRepository _mockEmpruntRepo;
//     private MockInventaireRepository _mockInventaireRepo;
//     private MockStatistiquesRepository _mockStatistiquesRepo;

//     [TestInitialize]
//     public void Setup()
//     {
//         _mockAdherentRepo = new();
//         _mockEmpruntRepo = new();
//         _mockInventaireRepo = new();
//         _mockStatistiquesRepo = new();
//         _service = new StatistiquesService(
//             _mockAdherentRepo,
//             _mockEmpruntRepo,
//             _mockInventaireRepo,
//             _mockStatistiquesRepo
//         );
//     }

//     [TestMethod]
//     public void GetAllStatistiquesAdherents_retourne_tous_les_adherents()
//     {
//         var statistiquesAdherents = _service.GetAllStatistiquesAdherents();
//         Assert.IsInstanceOfType<IEnumerable<StatistiquesAdherentDto>>(statistiquesAdherents);
//         Assert.AreEqual(2, statistiquesAdherents.Count());
//     }

//     [TestMethod]
//     public void GetStatistqiuesAdherentById_avec_id_existant_retourne_statistiquesAdherentDto()
//     {
//         int adherentId = 10;
//         Adherent adherent = new()
//         {
//             Nom = "Bruel",
//             Prenom = "Patrick",
//             Emprunts =
//             [
//                 new(),
//                 new(),
//                 new()
//                 {
//                     DateEmprunt = DateOnly.FromDateTime(DateTime.Now).AddDays(-10),
//                     DateRetourPrevue = DateOnly.FromDateTime(DateTime.Now).AddDays(-1),
//                     DateRetourReelle = DateOnly.FromDateTime(DateTime.Now).AddDays(-2),
//                 },
//             ],
//             Reservations =
//             [
//                 new() { Statut = StatutReservation.Active },
//                 new() { Statut = StatutReservation.Annulee },
//                 new() { Statut = StatutReservation.Annulee },
//                 new() { Statut = StatutReservation.Disponible },
//                 new() { Statut = StatutReservation.Disponible },
//                 new() { Statut = StatutReservation.Disponible },
//                 new() { Statut = StatutReservation.Terminee },
//                 new() { Statut = StatutReservation.Terminee },
//                 new() { Statut = StatutReservation.Terminee },
//                 new() { Statut = StatutReservation.Terminee },
//             ],
//         };
//         _mockAdherentRepo.AddAdherent(adherent);
//         adherent.Id = adherentId;

//         var statistiquesAdherentDto = _service.GetStatistiquesAdherentById(adherentId);

//         Assert.IsInstanceOfType<StatistiquesAdherentDto>(statistiquesAdherentDto);
//         Assert.AreEqual("Patrick Bruel", statistiquesAdherentDto.NomComplet);
//         Assert.AreEqual("En règle", statistiquesAdherentDto.Statut);
//         Assert.AreEqual(2, statistiquesAdherentDto.EmpruntsEnCours);
//         Assert.AreEqual(1, statistiquesAdherentDto.ReservationsActives);
//         Assert.AreEqual(3, statistiquesAdherentDto.HistoriqueEmprunts);
//     }

//     [TestMethod]
//     public void GetStatistqiuesAdherentById_avec_adherent_enretard_retourne_statut_en_retard()
//     {
//         int adherentId = 10;
//         Adherent adherent = new()
//         {
//             Nom = "Bruel",
//             Prenom = "Patrick",
//             Emprunts =
//             [
//                 new(),
//                 new(),
//                 new()
//                 {
//                     DateEmprunt = DateOnly.FromDateTime(DateTime.Now).AddDays(-10),
//                     DateRetourPrevue = DateOnly.FromDateTime(DateTime.Now).AddDays(-1),
//                     DateRetourReelle = DateOnly.FromDateTime(DateTime.Now),
//                 },
//             ],
//         };
//         _mockAdherentRepo.AddAdherent(adherent);
//         adherent.Id = adherentId;

//         StatistiquesAdherentDto? statistiquesAdherentDto = _service.GetStatistiquesAdherentById(
//             adherentId
//         );
//         Assert.AreEqual("En retard", statistiquesAdherentDto.Statut);
//     }

//     [TestMethod]
//     public void GetStatistiquesAdherentById_avec_id_inexistant_lance_not_found_exception()
//     {
//         int adherentId = 999;
//         var ex = Assert.ThrowsException<NotFoundException>(() =>
//             _service.GetStatistiquesAdherentById(adherentId)
//         );
//         Assert.AreEqual($"L’adhérent avec l’id {adherentId} n’existe pas.", ex.Message);
//     }

//     [TestMethod]
//     public void GetAllStatistiquesOeuvres_retourne_toutes_les_statistiques_oeuvres()
//     {
//         var statistiquesOeuvres = _service.GetAllStatistiquesOeuvres();
//         Assert.IsInstanceOfType<IEnumerable<StatistiquesOeuvreDto>>(statistiquesOeuvres);
//         Assert.AreEqual(2, statistiquesOeuvres.Count());
//     }

//     [TestMethod]
//     public void GetStatistiquesOeuvreById_avec_id_valide_retourne_statistique_oeuvre_dto_valide()
//     {
//         int oeuvreId = 20;
//         Oeuvre oeuvre = new()
//         {
//             Titre = "Shining",
//             Auteur = new() { Nom = "DeMarco", Prenom = "Marc" },
//             Reservations =
//             [
//                 new() { Statut = StatutReservation.Active },
//                 new() { Statut = StatutReservation.Annulee },
//                 new() { Statut = StatutReservation.Annulee },
//                 new() { Statut = StatutReservation.Disponible },
//                 new() { Statut = StatutReservation.Disponible },
//                 new() { Statut = StatutReservation.Disponible },
//                 new() { Statut = StatutReservation.Terminee },
//                 new() { Statut = StatutReservation.Terminee },
//                 new() { Statut = StatutReservation.Terminee },
//                 new() { Statut = StatutReservation.Terminee },
//             ],
//             Exemplaires =
//             [
//                 new(),
//                 new(),
//                 new() { Emprunts = [new(), new()] },
//                 new() { Emprunts = [new()] },
//             ],
//         };
//         _mockInventaireRepo.AddOeuvre(oeuvre);
//         oeuvre.Id = oeuvreId;
//         var statistiquesOeuvre = _service.GetStatistiquesOeuvreById(oeuvreId);

//         Assert.IsInstanceOfType<StatistiquesOeuvreDto>(statistiquesOeuvre);
//         Assert.AreEqual("Shining", statistiquesOeuvre.Titre);
//         Assert.AreEqual("Marc DeMarco", statistiquesOeuvre.AuteurNomComplet);
//         Assert.AreEqual("Disponible", statistiquesOeuvre.StatutDisponibilite);
//         Assert.IsTrue(
//             Enum.TryParse<StatutDisponibilite>(statistiquesOeuvre.StatutDisponibilite, true, out var statut)
//         );
//         Assert.AreEqual(4, statistiquesOeuvre.NombreCopies);
//         Assert.AreEqual(3, statistiquesOeuvre.NombreEmpruntsTotal);
//         Assert.AreEqual(1, statistiquesOeuvre.FileAttenteReservation);
//     }
// }
