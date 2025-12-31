// namespace MediathequeWeb.UnitTests.Services;

// [TestClass]
// public class ReservationServiceTests
// {
//     private ReservationService _service;
//     private MockReservationRepository _repo;

//     [TestInitialize]
//     public void Setup()
//     {
//         _repo = new MockReservationRepository();
//         _service = new ReservationService(_repo);
//     }

//     [TestMethod]
//     public void GetAllReservations_retourne_tous_les_reservations()
//     {
//         var reservations = _service.GetAllReservations();

//         Assert.AreEqual(2, reservations.Count());
//     }

//     [TestMethod]
//     public void GetReservationById_existant_retourne_reservation()
//     {
//         var reservation = _service.GetReservationById(1);

//         Assert.AreEqual("Némard", reservation.Nom);
//     }

//     [TestMethod]
//     [ExpectedException(typeof(NotFoundException))]
//     public void GetReservationById_inexistant_lance_exception()
//     {
//         _service.GetReservationById(99);
//     }

//     [TestMethod]
//     public void CreateReservation_ajoute_un_reservation()
//     {
//         var reservation = _service.CreateReservation("Gonzola", "Igor", "igorgonzola@ensc.fr");

//         Assert.IsTrue(reservation.Id > 0);
//         Assert.AreEqual(3, _repo.GetAllReservations().Count());
//     }

//     [TestMethod]
//     public void UpdateReservation_modifie_les_donnees()
//     {
//         var reservation = _service.UpdateReservation(1, "Emarre", "Jean", "jeanemarre@ensc.fr");

//         Assert.AreEqual("Emarre", reservation.Nom);
//         Assert.AreEqual("jeanemarre@ensc.fr", reservation.Email);
//     }

//     [TestMethod]
//     public void DeleteReservation_existant_supprime_reservation()
//     {
//         _service.DeleteReservation(1);

//         Assert.AreEqual(1, _repo.GetAllReservations().Count());
//     }
// }
