namespace MediathequeWeb.UnitTests.Models;

[TestClass]
public class SuiviActiviteQuotidienTests
{
    [TestMethod]
    public void NouveauxEmprunts_valide_est_acceptee()
    {
        int nouveauxEmprunts = 1;
        SuiviActiviteQuotidien suiviActiviteQuotidien = new SuiviActiviteQuotidien { Date = DateOnly.FromDateTime(DateTime.Now) };
        suiviActiviteQuotidien.NouveauxEmprunts = nouveauxEmprunts;

        Assert.AreEqual(nouveauxEmprunts, suiviActiviteQuotidien.NouveauxEmprunts);
    }
    [TestMethod]
    public void NouveauxEmprunts_negatif_lance_exception()
    {
        int nouveauxEmprunts = -1;
        SuiviActiviteQuotidien suiviActiviteQuotidien = new SuiviActiviteQuotidien { Date = DateOnly.FromDateTime(DateTime.Now) };

        var ex = Assert.ThrowsException<DomainValidationException>(() =>
            suiviActiviteQuotidien.NouveauxEmprunts = nouveauxEmprunts
        );
        Assert.AreEqual("Le nombre de nouveaux emprunts ne peut pas être négatif.", ex.Message);
    }
    [TestMethod]
    public void RetoursEffectues_valide_est_acceptee()
    {
        int retoursEffectues = 1;
        SuiviActiviteQuotidien suiviActiviteQuotidien = new SuiviActiviteQuotidien { Date = DateOnly.FromDateTime(DateTime.Now) };
        suiviActiviteQuotidien.RetoursEffectues = retoursEffectues;
        
        Assert.AreEqual(retoursEffectues, suiviActiviteQuotidien.RetoursEffectues);
    }
    
    [TestMethod]
    public void RetoursEffectues_negatif_lance_exception()
    {
        int retoursEffectues = -1;
        SuiviActiviteQuotidien suiviActiviteQuotidien = new SuiviActiviteQuotidien { Date = DateOnly.FromDateTime(DateTime.Now) };

        var ex = Assert.ThrowsException<DomainValidationException>(() =>
            suiviActiviteQuotidien.RetoursEffectues = retoursEffectues
        );
        Assert.AreEqual("Le nombre de retours effectués ne peut pas être négatif.", ex.Message);
    }

    [TestMethod]
    public void NouvellesReservations_valide_est_acceptee()
    {
        int nouvellesReservations = 1;
        SuiviActiviteQuotidien suiviActiviteQuotidien = new SuiviActiviteQuotidien { Date = DateOnly.FromDateTime(DateTime.Now) };
        suiviActiviteQuotidien.NouvellesReservations = nouvellesReservations;

        Assert.AreEqual(nouvellesReservations, suiviActiviteQuotidien.NouvellesReservations);
    }
    [TestMethod]
    public void NouvellesReservations_negatif_lance_exception()
    {
        int nouvellesReservations = -1;
        SuiviActiviteQuotidien suiviActiviteQuotidien = new SuiviActiviteQuotidien { Date = DateOnly.FromDateTime(DateTime.Now) };

        var ex = Assert.ThrowsException<DomainValidationException>(() =>
            suiviActiviteQuotidien.NouvellesReservations = nouvellesReservations
        );
        Assert.AreEqual("Le nombre de nouvelles réservations ne peut pas être négatif.", ex.Message);
    }
    [TestMethod]
    public void NouveauxAdherents_valide_est_acceptee()
    {
        int nouveauxAdherents = 1;
        SuiviActiviteQuotidien suiviActiviteQuotidien = new SuiviActiviteQuotidien { Date = DateOnly.FromDateTime(DateTime.Now) };
        suiviActiviteQuotidien.NouveauxAdherents = nouveauxAdherents;

        Assert.AreEqual(nouveauxAdherents, suiviActiviteQuotidien.NouveauxAdherents);
    }
    [TestMethod]
    public void NouveauxAdherents_negatif_lance_exception()
    {
        int nouveauxAdherents = -1;
        SuiviActiviteQuotidien suiviActiviteQuotidien = new SuiviActiviteQuotidien { Date = DateOnly.FromDateTime(DateTime.Now) };

        var ex = Assert.ThrowsException<DomainValidationException>(() =>
            suiviActiviteQuotidien.NouveauxAdherents = nouveauxAdherents
        );
        Assert.AreEqual("Le nombre de nouveaux adhérents ne peut pas être négatif.", ex.Message);
    }

    
}
