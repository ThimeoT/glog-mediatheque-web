namespace MediathequeWeb.UnitTests.Models;

[TestClass]
public class EmpruntTests
{
    [TestMethod]
    public void DateRetourPrevue_valide_est_acceptee()
    {
        var emprunt = new Emprunt
        {
            DateEmprunt = DateOnly.FromDateTime(DateTime.Now)
        };

        var dateRetour = emprunt.DateEmprunt.AddDays(10);
        emprunt.DateRetourPrevue = dateRetour;

        Assert.AreEqual(dateRetour, emprunt.DateRetourPrevue);
    }

    [TestMethod]
    public void DateRetourPrevue_avant_date_emprunt_lance_exception()
    {
        var emprunt = new Emprunt
        {
            DateEmprunt = DateOnly.FromDateTime(DateTime.Now)
        };

        var ex = Assert.ThrowsException<DomainValidationException>(() =>
            emprunt.DateRetourPrevue = emprunt.DateEmprunt.AddDays(-1)
        );

        Assert.AreEqual(
            "La date de retour prévue doit être après la date d’emprunt.",
            ex.Message
        );
    }

    [TestMethod]
    public void DateRetourReelle_valide_est_acceptee()
    {
        var emprunt = new Emprunt
        {
            DateEmprunt = DateOnly.FromDateTime(DateTime.Now)
        };

        var dateRetour = emprunt.DateEmprunt.AddDays(5);
        emprunt.DateRetourReelle = dateRetour;

        Assert.AreEqual(dateRetour, emprunt.DateRetourReelle);
    }

    [TestMethod]
    public void DateRetourReelle_avant_date_emprunt_lance_exception()
    {
        var emprunt = new Emprunt
        {
            DateEmprunt = DateOnly.FromDateTime(DateTime.Now)
        };

        var ex = Assert.ThrowsException<DomainValidationException>(() =>
            emprunt.DateRetourReelle = emprunt.DateEmprunt.AddDays(-3)
        );

        Assert.AreEqual(
            "La date de retour réelle ne peut pas être avant la date d’emprunt.",
            ex.Message
        );
    }
}
