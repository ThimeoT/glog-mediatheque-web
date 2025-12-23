public class AdherentService
{
    private readonly IAdherentRepository _repo;

    public AdherentService(IAdherentRepository repo)
    {
        _repo = repo;
    }

    public Adherent GetById(int id)
    {
        return _repo.GetAdherent(id)
            ?? throw new NotFoundException($"L’adhérent avec l’id {id} n’existe pas.");
    }

    public IEnumerable<Adherent> GetAll()
    {
        return _repo.GetAllAdherents();
    }

    public Adherent Create(string nom, string prenom, string email)
    {
        Adherent adherent = new()
        {
            Nom = nom,
            Prenom = prenom,
            Email = email,
        };

        _repo.AddAdherent(adherent);
        _repo.SaveChanges();

        return adherent;
    }

    public Adherent Update(int id, string nom, string prenom, string email)
    {
        Adherent adherent = GetById(id);

        adherent.Nom = nom;
        adherent.Prenom = prenom;
        adherent.Email = email;

        _repo.SaveChanges();
        return adherent;
    }

    public void Delete(int id)
    {
        Adherent adherent = GetById(id);
        _repo.RemoveAdherent(adherent);
        _repo.SaveChanges();
    }
}
