public class AdherentService
{
    private readonly IAdherentRepository _adherentRepo;
    private readonly IStatistiquesRepository _statistiquesRepo;

    public AdherentService(
        IAdherentRepository adherentRepo,
        IStatistiquesRepository statistiquesRepo
    )
    {
        _adherentRepo = adherentRepo;
        _statistiquesRepo = statistiquesRepo;
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
        IncrementNouveauxAdherentsStatistiques(DateOnly.FromDateTime(DateTime.Now));

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

    public void IncrementNouveauxAdherentsStatistiques(DateOnly date)
    {
        Statistiques? statistiques = _statistiquesRepo.GetStatistiquesFromDate(date);
        if (statistiques == null)
            _statistiquesRepo.AddStatistiques(
                new Statistiques { Date = date, NouveauxAdherents = 1 }
            );
        else
            statistiques.NouveauxAdherents += 1;
        _statistiquesRepo.SaveChanges();
    }
}
