public class AdherentService
{
    private readonly IAdherentRepository _adherentRepo;
    private readonly ISuiviActiviteRepository _suiviActiviteRepo;

    public AdherentService(IAdherentRepository adherentRepo,ISuiviActiviteRepository suiviActiviteRepo)
    {
        _adherentRepo = adherentRepo;
        _suiviActiviteRepo = suiviActiviteRepo;
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
        IncrementNouveauxAdherentsSuiviActivite(DateOnly.FromDateTime(DateTime.Now));

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
    public void IncrementNouveauxAdherentsSuiviActivite(DateOnly date)
    {
        SuiviActivite? suiviActivite = _suiviActiviteRepo.GetSuiviActiviteFromDate(date);
        if (suiviActivite == null)
            _suiviActiviteRepo.AddSuiviActivite(
                new SuiviActivite { Date = date, NouveauxAdherents = 1 }
            );
        else
            suiviActivite.NouveauxAdherents += 1;
        _suiviActiviteRepo.SaveChanges();
    }
}
