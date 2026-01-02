public class AdherentService
{
    private readonly IAdherentRepository _repo;

    public AdherentService(IAdherentRepository repo)
    {
        _repo = repo;
    }

    public Adherent GetAdherentById(int id)
    {
        return _repo.GetAdherent(id)
            ?? throw new NotFoundException($"L’adhérent avec l’id {id} n’existe pas.");
    }

    public IEnumerable<Adherent> GetAllAdherents()
    {
        return _repo.GetAllAdherents();
    }

    public Adherent CreateAdherent(string nom, string prenom, string email)
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

    public Adherent UpdateAdherent(int id, string nom, string prenom, string email)
    {
        Adherent adherent = GetAdherentById(id);

        adherent.Nom = nom;
        adherent.Prenom = prenom;
        adherent.Email = email;

        _repo.SaveChanges();
        return adherent;
    }

    public void DeleteAdherent(int id)
    {
        Adherent adherent = GetAdherentById(id);
        _repo.RemoveAdherent(adherent);
        _repo.SaveChanges();
    }
}
