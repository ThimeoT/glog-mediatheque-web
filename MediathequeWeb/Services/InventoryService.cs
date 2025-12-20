public class InventoryService
{
    private readonly IInventoryRepository _repo;

    public InventoryService(IInventoryRepository repo)
    {
        _repo = repo;
    }

    public Auteur? GetAuteurByID(int id)
    {
        var auteur = _repo.GetAuteur(id);
        if (auteur == null)
        {
            throw new NotFoundException($"Le auteur avec l'Id {id} n'existe pas.");
        }
        return auteur;
    }

    public IEnumerable<Auteur> GetAllAuteurs()
    {
        return _repo.GetAllAuteurs();
    }

    public void CreateAuteur(string nom, string prenom)
    {
        var newAuteur = new Auteur
        {
            Nom = nom,
            Prenom = prenom
        };

        _repo.AddAuteur(newAuteur);
    }

    public Auteur UpdateAuteur(int id, string nom, string prenom)
    {
        var auteurToUpdate = GetAuteurByID(id);

        auteurToUpdate.Nom = nom;
        auteurToUpdate.Prenom = prenom;

        return auteurToUpdate;
    }

    public void DeleteAuteur(int id)
    {
        var auteurToDelete = GetAuteurByID(id);
        _repo.RemoveAuteur(auteurToDelete);

    }

    public Oeuvre? GetOeuvreById(int id)
    {
        var oeuvre = _repo.GetOeuvre(id);
        if (oeuvre == null)
        {
            throw new NotFoundException($"Le oeuvre avec l'Id {id} n'existe pas.");
        }
        return oeuvre;
    }

    public IEnumerable<Oeuvre> GetAllOeuvres()
    {
        return _repo.GetAllOeuvres();
    }

    public void CreateOeuvre(string titre, string type, int auteurID)
    {
        var auteur = GetAuteurByID(auteurID);

        var oeuvreType = Enum.Parse<OeuvreType>(type, ignoreCase: true);

        var newOeuvre = new Oeuvre
        {
            Titre = titre,
            Type = oeuvreType,
            Auteur = auteur
        };

        _repo.AddOeuvre(newOeuvre);
    }

    public Oeuvre UpdateOeuvre(int id, string titre, string type, int auteurID)
    {
        var oeuvreToUpdate = GetOeuvreById(id);

        var oeuvreType = Enum.Parse<OeuvreType>(type, ignoreCase: true);

        oeuvreToUpdate.Titre = titre;
        oeuvreToUpdate.Type = oeuvreType;
        oeuvreToUpdate.Auteur = GetAuteurByID(auteurID);

        return oeuvreToUpdate;
    }

    public void DeleteOeuvre(int id)
    {
        var oeuvreToDelete = GetOeuvreById(id);
        _repo.RemoveOeuvre(oeuvreToDelete);
    }

    public Exemplaire? GetExemplaireById(int id)
    {
        var exemplaire = _repo.GetExemplaire(id);

        if (exemplaire == null)
        {
            throw new NotFoundException($"Le exemplaire avec l'Id {id} n'existe pas.");
        }

        return exemplaire;
    }

    public IEnumerable<Exemplaire> GetAllExemplaires()
    {
        return _repo.GetAllExemplaires();
    }

    public void CreateExemplaire(string statut, int oeuvreID)
    {
        var statutExemplaire = Enum.Parse<StatutExemplaire>(statut, ignoreCase: true);

        var oeuvre = GetOeuvreById(oeuvreID);

        var exemplaire = new Exemplaire
        {
            Statut = statutExemplaire,
            Oeuvre = oeuvre
        };

        _repo.AddExemplaire(exemplaire);
    }

    public Exemplaire UpdateExemplaire(int id, string statut, int oeuvreID)
    {
        var exemplaireToUpdate = GetExemplaireById(id);

        var statutExemplaire = Enum.Parse<StatutExemplaire>(statut, ignoreCase: true);

        exemplaireToUpdate.Statut = statutExemplaire;
        exemplaireToUpdate.Oeuvre = GetOeuvreById(oeuvreID);

        return exemplaireToUpdate;
    }

    public void DeleteExemplaire(int id)
    {
        var exemplaireToDelete = GetExemplaireById(id);
        _repo.RemoveExemplaire(exemplaireToDelete);
    }

}
