public class AdherentService
{
    private readonly IAdherentRepository _adherentRepo;

    public AdherentService(
        IAdherentRepository adherentRepo
    )
    {
        _adherentRepo = adherentRepo;
    }

    public Adherent GetAdherentById(int id)
    {
        return _adherentRepo.GetAdherent(id)
            ?? throw new NotFoundException($"L’adhérent avec l’id {id} n’existe pas.");
    }

    public IEnumerable<Adherent> GetAllAdherents()
    {
        return _adherentRepo.GetAllAdherents();
    }

    public Adherent CreateAdherent(string nom, string prenom, string email)
    {
        Adherent adherent = new()
        {
            Nom = nom,
            Prenom = prenom,
            Email = email,
        };

        _adherentRepo.AddAdherent(adherent);
        _adherentRepo.SaveChanges();

        return adherent;
    }

    public Adherent UpdateAdherent(int id, string nom, string prenom, string email)
    {
        Adherent adherent = GetAdherentById(id);

        adherent.Nom = nom;
        adherent.Prenom = prenom;
        adherent.Email = email;

        _adherentRepo.SaveChanges();
        return adherent;
    }

    public void DeleteAdherent(int id)
    {
        Adherent adherent = GetAdherentById(id);
        _adherentRepo.RemoveAdherent(adherent);
        _adherentRepo.SaveChanges();
    }
}
