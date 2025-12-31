using MediathequeWeb.UnitTests.Tests.Controllers;

public class MockEmpruntRepository : IEmpruntRepository
{
    private List<Adherent> _adherents;
    private List<Auteur> _auteurs;
    private List<Oeuvre> _oeuvres;
    private List<Exemplaire> _exemplaires;
    private List<Reservation> _reservations;
    private List<Emprunt> _emprunts;
    private int _nextId = 3;
    public MockEmpruntRepository()
    {
        _adherents =
        [
            new Adherent
            {
                Id = 1,
                Nom = "Curie",
                Prenom = "Marie",
                Email = "marie.curie@email.com",
            },
            new Adherent
            {
                Id = 2,
                Nom = "Dupont",
                Prenom = "Jean",
                Email = "jean.dupont@email.com",
            },
        ];
        _auteurs =
        [
            new Auteur
            {
                Id = 1,
                Nom = "Herbert",
                Prenom = "Frank",
            },
            new Auteur
            {
                Id = 2,
                Nom = "Nolan",
                Prenom = "Christopher",
            },
        ];
        _oeuvres =
        [
            new Oeuvre
            {
                Id = 1,
                Titre = "Dune",
                Type = OeuvreType.Livre,
                Auteur = _auteurs[0],
            },
            new Oeuvre
            {
                Id = 2,
                Titre = "Interstellar",
                Type = OeuvreType.DVD,
                Auteur = _auteurs[1],
            },
        ];
        _exemplaires = [
            new Exemplaire
            {
                Id = 1,
                Oeuvre = _oeuvres[0],
                Statut = StatutExemplaire.Disponible,
            },
            new Exemplaire
            {
                Id = 2,
                Oeuvre = _oeuvres[1],
                Statut = StatutExemplaire.Disponible,
            }
        ];
        _reservations = [
             new Reservation
            {
                Id = 1,
                Adherent = _adherents[0],
                Oeuvre = _oeuvres[0],
                DateReservation = DateOnly.FromDateTime(DateTime.Now),
                Statut = StatutReservation.Active,
            },
            new Reservation
            {
                Id = 2,
                Adherent = _adherents[1],
                Oeuvre = _oeuvres[1],
                DateReservation = DateOnly.FromDateTime(DateTime.Now),
                Statut = StatutReservation.Active,
            },
        ];

        _emprunts =
        [
            new Emprunt
            {
                Id = 1,
                Adherent = _adherents[0],
                Exemplaire = _exemplaires[0],
                DateEmprunt = DateOnly.FromDateTime(DateTime.Now.AddDays(-10)),
                DateRetourPrevue = DateOnly.FromDateTime(DateTime.Now.AddDays(-10+21)),
                DateRetourReelle = null,
            },
            new Emprunt
            {
                Id = 2,
                Adherent = _adherents[1],
                Exemplaire = _exemplaires[1],
                DateEmprunt = DateOnly.FromDateTime(DateTime.Now.AddDays(-4)),
                DateRetourPrevue = DateOnly.FromDateTime(DateTime.Now.AddDays(-4+21)),
                DateRetourReelle = null,
            }
        ];
    }

    public Emprunt? GetEmprunt(int id)
    {
        return _emprunts.FirstOrDefault(e => e.Id == id);
    }

    public IEnumerable<Emprunt> GetAllEmprunts()
    {
        return _emprunts;
    }

    public void AddEmprunt(Emprunt emprunt)
    {
        emprunt.Id = _nextId++;
        _emprunts.Add(emprunt);
    }


    public void RemoveEmprunt(Emprunt emprunt)
    {
        _emprunts.Remove(emprunt);
    }

    public void SaveChanges()
    {
        // rien à faire ici
    }
}
