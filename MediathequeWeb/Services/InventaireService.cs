public class InventaireService
{
    private readonly IInventaireRepository _repo;

    public InventaireService(IInventaireRepository repo)
    {
        _repo = repo;
    }

    public Auteur GetAuteurById(int id)
    {
        return _repo.GetAuteur(id)
            ?? throw new NotFoundException($"L’auteur avec l’id {id} n’existe pas.");
    }

    public IEnumerable<Auteur> GetAllAuteurs()
    {
        return _repo.GetAllAuteurs();
    }

    public Auteur CreateAuteur(string nom, string prenom)
    {
        Auteur auteur = new() { Nom = nom, Prenom = prenom };

        _repo.AddAuteur(auteur);
        _repo.SaveChanges();
        return auteur;
    }

    public Auteur UpdateAuteur(int id, string nom, string prenom)
    {
        Auteur auteur = GetAuteurById(id);

        auteur.Nom = nom;
        auteur.Prenom = prenom;

        _repo.SaveChanges();
        return auteur;
    }

    public void DeleteAuteur(int id)
    {
        Auteur auteur = GetAuteurById(id);
        _repo.RemoveAuteur(auteur);
        _repo.SaveChanges();
    }

    public Oeuvre GetOeuvreById(int id)
    {
        return _repo.GetOeuvre(id)
            ?? throw new NotFoundException($"L’œuvre avec l’id {id} n’existe pas.");
    }

    public IEnumerable<Oeuvre> GetAllOeuvres()
    {
        return _repo.GetAllOeuvres();
    }

    private static OeuvreType ParseOeuvreType(string type)
    {
        if (!Enum.TryParse<OeuvreType>(type, true, out var oeuvreType))
            throw new DomainValidationException(
                $"Type d’œuvre invalide. Valeurs possibles : {string.Join(", ", Enum.GetNames<OeuvreType>())}"
            );

        return oeuvreType;
    }

    public Oeuvre CreateOeuvre(string titre, string type, int auteurId)
    {
        Oeuvre oeuvre = new()
        {
            Titre = titre,
            Type = ParseOeuvreType(type),
            Auteur = GetAuteurById(auteurId),
        };

        _repo.AddOeuvre(oeuvre);
        _repo.SaveChanges();
        return oeuvre;
    }

    public Oeuvre UpdateOeuvre(int id, string titre, string type, int auteurId)
    {
        Oeuvre oeuvre = GetOeuvreById(id);

        oeuvre.Titre = titre;
        oeuvre.Type = ParseOeuvreType(type);
        oeuvre.Auteur = GetAuteurById(auteurId);

        _repo.SaveChanges();
        return oeuvre;
    }

    public void DeleteOeuvre(int id)
    {
        var oeuvre = GetOeuvreById(id);
        _repo.RemoveOeuvre(oeuvre);
        _repo.SaveChanges();
    }

    public Exemplaire GetExemplaireById(int id)
    {
        return _repo.GetExemplaire(id)
            ?? throw new NotFoundException($"L’exemplaire avec l’id {id} n’existe pas.");
    }

    public IEnumerable<Exemplaire> GetAllExemplaires()
    {
        return _repo.GetAllExemplaires();
    }

    public IEnumerable<Exemplaire> GetAllExemplairesFromOeuvreId(int oeuvreId)
    {
        if (!_repo.OeuvreExists(oeuvreId))
            throw new NotFoundException($"L'oeuvre avec l’id {oeuvreId} n’existe pas.");
        return _repo.GetAllExemplairesByOeuvreId(oeuvreId);
    }

    public Exemplaire CreateExemplaire(int oeuvreId)
    {
        Exemplaire exemplaire = new()
        {
            Oeuvre = GetOeuvreById(oeuvreId),
            Statut = StatutExemplaire.Disponible,
        };

        _repo.AddExemplaire(exemplaire);
        _repo.SaveChanges();
        return exemplaire;
    }

    public void DeleteExemplaire(int id)
    {
        Exemplaire exemplaire = GetExemplaireById(id);
        _repo.RemoveExemplaire(exemplaire);
        _repo.SaveChanges();
    }
}
