using System.Net.NetworkInformation;

public class MockInventaireRepository : IInventaireRepository
{
    private readonly List<Auteur> _auteurs;
    private readonly List<Oeuvre> _oeuvres;
    private readonly List<Exemplaire> _exemplaires;
    private int _nextIdAuteur = 3;
    private int _nextIdOeuvre = 3;
    private readonly int _nextIdExemplaire = 3;

    public MockInventaireRepository()
    {
        _auteurs =
        [
            new Auteur
            {
                Id = 1,
                Nom = "Williams",
                Prenom = "Robbie",
            },
            new Auteur
            {
                Id = 2,
                Nom = "King",
                Prenom = "Stephen",
            },
        ];

        _oeuvres =
        [
            new Oeuvre
            {
                Id = 1,
                Titre = "Kids",
                Type = OeuvreType.CD,
                Auteur = _auteurs[0],
            },
            new Oeuvre
            {
                Id = 2,
                Titre = "It",
                Type = OeuvreType.DVD,
                Auteur = _auteurs[1],
            },
        ];

        _exemplaires =
        [
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
                Statut = StatutExemplaire.Emprunte,
            },
        ];
    }

    public Auteur? GetAuteur(int id)
    {
        return _auteurs.FirstOrDefault(a => a.Id == id);
    }

    public IEnumerable<Auteur> GetAllAuteurs()
    {
        return _auteurs;
    }

    public void AddAuteur(Auteur auteur)
    {
        auteur.Id = _nextIdAuteur++;
        auteur.Oeuvres ??= [];
        _auteurs.Add(auteur);
    }

    public void RemoveAuteur(Auteur auteur)
    {
        _auteurs.Remove(auteur);
    }

    public Oeuvre? GetOeuvre(int oeuvreId)
    {
        return _oeuvres.FirstOrDefault(o => o.Id == oeuvreId);
    }

    public bool OeuvreExists(int oeuvreId)
    {
        return _oeuvres.Any(o => o.Id == oeuvreId);
    }

    public IEnumerable<Oeuvre> GetAllOeuvres()
    {
        return _oeuvres;
    }

    public void AddOeuvre(Oeuvre oeuvre)
    {
        oeuvre.Id = _nextIdOeuvre++;
        oeuvre.Exemplaires ??= [];
        oeuvre.Reservations ??= [];
        _oeuvres.Add(oeuvre);
    }

    public void RemoveOeuvre(Oeuvre oeuvre)
    {
        _oeuvres.Remove(oeuvre);
    }

    public Exemplaire? GetExemplaire(int id)
    {
        return _exemplaires.FirstOrDefault(e => e.Id == id);
    }

    public IEnumerable<Exemplaire> GetAllExemplaires()
    {
        return _exemplaires;
    }

    public IEnumerable<Exemplaire> GetAllExemplairesFromOeuvreId(int oeuvreId)
    {
        return _exemplaires.Where(e => e.Oeuvre.Id == oeuvreId);
    }

    public void AddExemplaire(Exemplaire exemplaire)
    {
        exemplaire.Id = _nextIdExemplaire;
        exemplaire.Emprunts ??= [];
        _exemplaires.Add(exemplaire);
    }

    public void RemoveExemplaire(Exemplaire exemplaire)
    {
        _exemplaires.Remove(exemplaire);
    }

    public bool ExisteExemplaireDisponiblePourOeuvre(int oeuvreId)
    {
        return _exemplaires.Any(e =>
            e.Oeuvre.Id == oeuvreId && e.Statut == StatutExemplaire.Disponible
        );
    }

    public void SaveChanges() { }
}
