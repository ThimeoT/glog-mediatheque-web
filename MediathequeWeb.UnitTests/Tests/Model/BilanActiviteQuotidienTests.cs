namespace MediathequeWeb.UnitTests.Models;

[TestClass]
public class BilanActiviteQuotidienTests
{
    [TestMethod]
    public void NouveauxEmprunts_valide_est_acceptee()
    {
        int nouveauxEmprunts = 1;
        BilanActiviteQuotidien bilanActiviteQuotidien = new BilanActiviteQuotidien
        {
            Date = DateOnly.FromDateTime(DateTime.Now),
        };
        bilanActiviteQuotidien.NouveauxEmprunts = nouveauxEmprunts;

        Assert.AreEqual(nouveauxEmprunts, bilanActiviteQuotidien.NouveauxEmprunts);
    }

    [TestMethod]
    public void NouveauxEmprunts_negatif_lance_exception()
    {
        int nouveauxEmprunts = -1;
        BilanActiviteQuotidien bilanActiviteQuotidien = new BilanActiviteQuotidien
        {
            Date = DateOnly.FromDateTime(DateTime.Now),
        };

        var ex = Assert.ThrowsException<DomainValidationException>(() =>
            bilanActiviteQuotidien.NouveauxEmprunts = nouveauxEmprunts
        );
        Assert.AreEqual("Le nombre de nouveaux emprunts ne peut pas être négatif.", ex.Message);
    }

    [TestMethod]
    public void RetoursEffectues_valide_est_acceptee()
    {
        int retoursEffectues = 1;
        BilanActiviteQuotidien bilanActiviteQuotidien = new BilanActiviteQuotidien
        {
            Date = DateOnly.FromDateTime(DateTime.Now),
        };
        bilanActiviteQuotidien.RetoursEffectues = retoursEffectues;

        Assert.AreEqual(retoursEffectues, bilanActiviteQuotidien.RetoursEffectues);
    }

    [TestMethod]
    public void RetoursEffectues_negatif_lance_exception()
    {
        int retoursEffectues = -1;
        BilanActiviteQuotidien bilanActiviteQuotidien = new BilanActiviteQuotidien
        {
            Date = DateOnly.FromDateTime(DateTime.Now),
        };

        var ex = Assert.ThrowsException<DomainValidationException>(() =>
            bilanActiviteQuotidien.RetoursEffectues = retoursEffectues
        );
        Assert.AreEqual("Le nombre de retours effectués ne peut pas être négatif.", ex.Message);
    }

    [TestMethod]
    public void NouvellesReservations_valide_est_acceptee()
    {
        int nouvellesReservations = 1;
        BilanActiviteQuotidien bilanActiviteQuotidien = new BilanActiviteQuotidien
        {
            Date = DateOnly.FromDateTime(DateTime.Now),
        };
        bilanActiviteQuotidien.NouvellesReservations = nouvellesReservations;

        Assert.AreEqual(nouvellesReservations, bilanActiviteQuotidien.NouvellesReservations);
    }

    [TestMethod]
    public void NouvellesReservations_negatif_lance_exception()
    {
        int nouvellesReservations = -1;
        BilanActiviteQuotidien bilanActiviteQuotidien = new BilanActiviteQuotidien
        {
            Date = DateOnly.FromDateTime(DateTime.Now),
        };

        var ex = Assert.ThrowsException<DomainValidationException>(() =>
            bilanActiviteQuotidien.NouvellesReservations = nouvellesReservations
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
        BilanActiviteQuotidien bilanActiviteQuotidien = new BilanActiviteQuotidien
        {
            Date = DateOnly.FromDateTime(DateTime.Now),
        };
        bilanActiviteQuotidien.NouveauxAdherents = nouveauxAdherents;

        Assert.AreEqual(nouveauxAdherents, bilanActiviteQuotidien.NouveauxAdherents);
    }

    [TestMethod]
    public void NouveauxAdherents_negatif_lance_exception()
    {
        int nouveauxAdherents = -1;
        BilanActiviteQuotidien bilanActiviteQuotidien = new BilanActiviteQuotidien
        {
            Date = DateOnly.FromDateTime(DateTime.Now),
        };

        var ex = Assert.ThrowsException<DomainValidationException>(() =>
            bilanActiviteQuotidien.NouveauxAdherents = nouveauxAdherents
        );
        Assert.AreEqual("Le nombre de nouveaux adhérents ne peut pas être négatif.", ex.Message);
    }
}
