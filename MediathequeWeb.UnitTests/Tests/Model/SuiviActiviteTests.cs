namespace MediathequeWeb.UnitTests.Models;

[TestClass]
public class SuiviActiviteTests
{
    [TestMethod]
    public void NouveauxEmprunts_valide_est_acceptee()
    {
        int nouveauxEmprunts = 1;
        SuiviActivite suiviActivite = new SuiviActivite { Date = DateOnly.FromDateTime(DateTime.Now) };
        suiviActivite.NouveauxEmprunts = nouveauxEmprunts;

        Assert.AreEqual(nouveauxEmprunts, suiviActivite.NouveauxEmprunts);
    }
    [TestMethod]
    public void NouveauxEmprunts_negatif_lance_exception()
    {
        int nouveauxEmprunts = -1;
        SuiviActivite suiviActivite = new SuiviActivite { Date = DateOnly.FromDateTime(DateTime.Now) };

        var ex = Assert.ThrowsException<DomainValidationException>(() =>
            suiviActivite.NouveauxEmprunts = nouveauxEmprunts
        );
        Assert.AreEqual("Le nombre de nouveaux emprunts ne peut pas être négatif.", ex.Message);
    }
    [TestMethod]
    public void RetoursEffectues_valide_est_acceptee()
    {
        int retoursEffectues = 1;
        SuiviActivite suiviActivite = new SuiviActivite { Date = DateOnly.FromDateTime(DateTime.Now) };
        suiviActivite.RetoursEffectues = retoursEffectues;
        
        Assert.AreEqual(retoursEffectues, suiviActivite.RetoursEffectues);
    }
    
    [TestMethod]
    public void RetoursEffectues_negatif_lance_exception()
    {
        int retoursEffectues = -1;
        SuiviActivite suiviActivite = new SuiviActivite { Date = DateOnly.FromDateTime(DateTime.Now) };

        var ex = Assert.ThrowsException<DomainValidationException>(() =>
            suiviActivite.RetoursEffectues = retoursEffectues
        );
        Assert.AreEqual("Le nombre de retours effectués ne peut pas être négatif.", ex.Message);
    }

    [TestMethod]
    public void NouvellesReservations_valide_est_acceptee()
    {
        int nouvellesReservations = 1;
        SuiviActivite suiviActivite = new SuiviActivite { Date = DateOnly.FromDateTime(DateTime.Now) };
        suiviActivite.NouvellesReservations = nouvellesReservations;

        Assert.AreEqual(nouvellesReservations, suiviActivite.NouvellesReservations);
    }
    [TestMethod]
    public void NouvellesReservations_negatif_lance_exception()
    {
        int nouvellesReservations = -1;
        SuiviActivite suiviActivite = new SuiviActivite { Date = DateOnly.FromDateTime(DateTime.Now) };

        var ex = Assert.ThrowsException<DomainValidationException>(() =>
            suiviActivite.NouvellesReservations = nouvellesReservations
        );
        Assert.AreEqual("Le nombre de nouvelles réservations ne peut pas être négatif.", ex.Message);
    }
    [TestMethod]
    public void NouveauxAdherents_valide_est_acceptee()
    {
        int nouveauxAdherents = 1;
        SuiviActivite suiviActivite = new SuiviActivite { Date = DateOnly.FromDateTime(DateTime.Now) };
        suiviActivite.NouveauxAdherents = nouveauxAdherents;

        Assert.AreEqual(nouveauxAdherents, suiviActivite.NouveauxAdherents);
    }
    [TestMethod]
    public void NouveauxAdherents_negatif_lance_exception()
    {
        int nouveauxAdherents = -1;
        SuiviActivite suiviActivite = new SuiviActivite { Date = DateOnly.FromDateTime(DateTime.Now) };

        var ex = Assert.ThrowsException<DomainValidationException>(() =>
            suiviActivite.NouveauxAdherents = nouveauxAdherents
        );
        Assert.AreEqual("Le nombre de nouveaux adhérents ne peut pas être négatif.", ex.Message);
    }

    
}
