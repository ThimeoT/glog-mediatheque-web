namespace MediathequeWeb.UnitTests.Models;

[TestClass]
public class StatistiquesTests
{
    [TestMethod]
    public void NouveauxEmprunts_valide_est_acceptee()
    {
        int nouveauxEmprunts = 1;
        Statistiques statistiques = new Statistiques { Date = DateOnly.FromDateTime(DateTime.Now) };
        statistiques.NouveauxEmprunts = nouveauxEmprunts;

        Assert.AreEqual(nouveauxEmprunts, statistiques.NouveauxEmprunts);
    }

    [TestMethod]
    public void NouveauxEmprunts_negatif_lance_exception()
    {
        int nouveauxEmprunts = -1;
        Statistiques statistiques = new Statistiques { Date = DateOnly.FromDateTime(DateTime.Now) };

        var ex = Assert.ThrowsException<DomainValidationException>(() =>
            statistiques.NouveauxEmprunts = nouveauxEmprunts
        );
        Assert.AreEqual("Le nombre de nouveaux emprunts ne peut pas être négatif.", ex.Message);
    }

    [TestMethod]
    public void RetoursEffectues_valide_est_acceptee()
    {
        int retoursEffectues = 1;
        Statistiques statistiques = new Statistiques { Date = DateOnly.FromDateTime(DateTime.Now) };
        statistiques.RetoursEffectues = retoursEffectues;

        Assert.AreEqual(retoursEffectues, statistiques.RetoursEffectues);
    }

    [TestMethod]
    public void RetoursEffectues_negatif_lance_exception()
    {
        int retoursEffectues = -1;
        Statistiques statistiques = new Statistiques { Date = DateOnly.FromDateTime(DateTime.Now) };

        var ex = Assert.ThrowsException<DomainValidationException>(() =>
            statistiques.RetoursEffectues = retoursEffectues
        );
        Assert.AreEqual("Le nombre de retours effectués ne peut pas être négatif.", ex.Message);
    }

    [TestMethod]
    public void NouvellesReservations_valide_est_acceptee()
    {
        int nouvellesReservations = 1;
        Statistiques statistiques = new Statistiques { Date = DateOnly.FromDateTime(DateTime.Now) };
        statistiques.NouvellesReservations = nouvellesReservations;

        Assert.AreEqual(nouvellesReservations, statistiques.NouvellesReservations);
    }

    [TestMethod]
    public void NouvellesReservations_negatif_lance_exception()
    {
        int nouvellesReservations = -1;
        Statistiques statistiques = new Statistiques { Date = DateOnly.FromDateTime(DateTime.Now) };

        var ex = Assert.ThrowsException<DomainValidationException>(() =>
            statistiques.NouvellesReservations = nouvellesReservations
        );
        Assert.AreEqual(
            "Le nombre de nouvelles réservations ne peut pas être négatif.",
            ex.Message
        );
    }

    [TestMethod]
    public void NouveauxAdherents_valide_est_acceptee()
    {
        int nouveauxAdherents = 1;
        Statistiques statistiques = new Statistiques { Date = DateOnly.FromDateTime(DateTime.Now) };
        statistiques.NouveauxAdherents = nouveauxAdherents;

        Assert.AreEqual(nouveauxAdherents, statistiques.NouveauxAdherents);
    }

    [TestMethod]
    public void NouveauxAdherents_negatif_lance_exception()
    {
        int nouveauxAdherents = -1;
        Statistiques statistiques = new Statistiques { Date = DateOnly.FromDateTime(DateTime.Now) };

        var ex = Assert.ThrowsException<DomainValidationException>(() =>
            statistiques.NouveauxAdherents = nouveauxAdherents
        );
        Assert.AreEqual("Le nombre de nouveaux adhérents ne peut pas être négatif.", ex.Message);
    }
}
