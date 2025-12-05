using Microsoft.EntityFrameworkCore;

public static class SeedData
{
    public static void Initialize()
    {
        using var db = new DataContext();

        db.Database.Migrate();

        if (db.Auteurs.Any() || db.Oeuvres.Any() || db.Exemplaires.Any() || db.Adherents.Any() || db.Emprunts.Any() || db.Reservations.Any())
            return;

        // Auteurs
        var frankHerbert = new Auteur
        {
            Nom = "Herbert",
            Prenom = "Frank"
        };

        var christopherNolan = new Auteur
        {
            Nom = "Nolan",
            Prenom = "Christopher"
        };

        db.Auteurs.AddRange(frankHerbert, christopherNolan);
        db.SaveChanges();

        // Oeuvres
        var dune = new Oeuvre
        {
            Titre = "Dune",
            Type = OeuvreType.Livre,
            Auteur = frankHerbert
        };

        var interstellar = new Oeuvre
        {
            Titre = "Interstellar",
            Type = OeuvreType.DVD,
            Auteur = christopherNolan
        };

        db.Oeuvres.AddRange(dune, interstellar);
        db.SaveChanges();

        // Exemplaire
        var exemplaireDune = new Exemplaire
        {
            Oeuvre = dune,
            Statut = StatutExemplaire.Disponible
        };

        var exemplaireInterstellar = new Exemplaire
        {
            Oeuvre = interstellar,
            Statut = StatutExemplaire.Disponible
        };

        db.Exemplaires.AddRange(exemplaireDune, exemplaireInterstellar);
        db.SaveChanges();

        // Adhérents
        var jeanDupont = new Adherent
        {
            Nom = "Dupont",
            Prenom = "Jean",
            Email = "jean.dupont@email.com"
        };

        var marieCurie = new Adherent
        {
            Nom = "Curie",
            Prenom = "Marie",
            Email = "marie.curie@email.com"
        };

        db.Adherents.AddRange(jeanDupont, marieCurie);
        db.SaveChanges();

        // Emprunt pour Marie Curie
        var dateEmprunt = DateOnly.FromDateTime(DateTime.Now.AddDays(-10));
        var dateRetourPrevue = dateEmprunt.AddDays(21); // Si emprunt il y a 10 jours => J+21 = dans 11 jours 

        var empruntMarie = new Emprunt
        {
            Adherent = marieCurie,
            Exemplaire = exemplaireDune,
            DateEmprunt = dateEmprunt,
            DateRetourPrevue = dateRetourPrevue,
            DateRetourReelle = null
        };

        // Mise à jour du statut exemplaire (Dune emprunté)
        exemplaireDune.Statut = StatutExemplaire.Emprunte;

        db.Emprunts.Add(empruntMarie);
        db.SaveChanges();

        // Réservation pour Jean Dupont
        var reservationJean = new Reservation
        {
            Adherent = jeanDupont,
            Oeuvre = dune,
            DateReservation = DateOnly.FromDateTime(DateTime.Now),
            Statut = StatutReservation.Active
        };

        db.Reservations.Add(reservationJean);
        db.SaveChanges();
    }
}
