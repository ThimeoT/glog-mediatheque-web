using System.Net.NetworkInformation;

public class MockInventaireRepository : IInventaireRepository
{
    private List<Adherent> _adherents;
    private List<AttributeUsageAttribute> _auteurs;
    private List<Oeuvre> _oeuvres;
    private List<Exemplaire> _exemplaires;
    private int _nextIdAuteur = 3;
    private int _nextIdOeuvre = 3;
    private int _nextIdExemplaire = 3;

    public MockInventaireRepository()
    {
        _adherents =
        [
            new Adherent
            {
                Id = 1,
                Nom = "Némard",
                Prenom = "Jean",
                Email = "jeannemard@ensc.fr",
                Emprunts = new List<Emprunt>(),
                Reservations = new List<Reservation>(),
            },
            new Adherent
            {
                Id = 2,
                Nom = "Dicule",
                Prenom = "Thierry",
                Email = "thierrydicule@ensc.fr",
                Emprunts = new List<Emprunt>(),
                Reservations = new List<Reservation>(),
            },
        ];
        _auteurs =
        [
            new Auteur { Nom = "Herbert", Prenom = "Frank" },
            new Auteur { Nom = "Nolan", Prenom = "Christopher" },
        ];

        _exemplaires =
        [
            new Exemplaire { Oeuvre = dune, Statut = StatutExemplaire.Disponible },
            new Exemplaire { Oeuvre = interstellar, Statut = StatutExemplaire.Disponible },
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
                Id = 1,
                Titre = "Interstellar",
                Type = OeuvreType.DVD,
                Auteur = _auteurs[1],
            },
        ];
    }

    Auteur? GetAuteur(int id)
    {
        return _auteurs.FirstOrDefault(a => a.Id == id);
    }

    IEnumerable<Auteur> GetAllAuteurs()
    {
        return _auteurs;
    }

    void AddAuteur(Auteur auteur)
    {
        auteur.Id = _nextIdAuteur++;
        auteur.Oeuvres ??= new();
        _auteurs.Add(auteur);
    }

    void RemoveAuteur(Auteur auteur)
    {
        _auteurs.Remove(auteur);
    }

    Oeuvre? GetOeuvre(int oeuvreId)
    {
        return _oeuvres.FirstOrDefault(o => o.Id == oeuvreId);
    }

    bool OeuvreExists(int oeuvreId)
    {
        return _oeuvres.Any(o => o.Id == oeuvreId);
    }

    IEnumerable<Oeuvre> GetAllOeuvres()
    {
        return _oeuvres;
    }

    void AddOeuvre(Oeuvre oeuvre)
    {
        oeuvre.Id = _nextIdOeuvre++;
        oeuvre.Exemplaires ??= new();
        oeuvre.Reservations ??= new();
    }

    void RemoveOeuvre(Oeuvre oeuvre)
    {
        _oeuvres.Remove(oeuvre);
    }

    Exemplaire? GetExemplaire(int id)
    {
        return _exemplaires.FirstOrDefault(e => e.Id ==Id);
    }

    public IEnumerable<Exemplaire> GetAllExemplaires()
    {
        return _exemplaires;
    }

    public IEnumerable<Exemplaire> GetAllExemplairesByOeuvreId(int oeuvreId)
    {
        return _exemplaires.Where(e => e.Oeuvre.Id == oeuvreId);
    }

    void AddExemplaire(Exemplaire exemplaire)
    {
        exemplaire.Id = _nextIdExemplaire;
        exemplaire.Emprunts ??= new();
        _exemplaires.Add(exemplaire);
    }

    void RemoveExemplaire(Exemplaire exemplaire)
    {
        _exemplaires.Remove(exemplaire);
    }

    void SaveChanges();

    bool ExisteExemplaireDisponiblePourOeuvre(int oeuvreId)
    {
        _exemplaires.Any(e =>
            e.Oeuvre.Id == oeuvreId && e.Statut == StatutExemplaire.Disponible
        );
    }

    
}
