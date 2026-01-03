public class StatistiquesService
{
    private readonly IAdherentRepository _adherentRepo;
    private readonly IEmpruntRepository _empruntRepo;
    private readonly IInventaireRepository _inventaireRepo;
    private readonly IStatistiquesRepository _statistiquesRepo;

    public StatistiquesService(
        IAdherentRepository adherentRepo,
        IEmpruntRepository empruntRepo,
        IInventaireRepository inventaireRepo,
        IStatistiquesRepository statistiquesRepo
    )
    {
        _adherentRepo = adherentRepo;
        _empruntRepo = empruntRepo;
        _inventaireRepo = inventaireRepo;
        _statistiquesRepo = statistiquesRepo;
    }

    public StatistiquesAdherentDto GetStatistiquesAdherentById(int adherentId)
    {
        Adherent adherent =
            _adherentRepo.GetAdherent(adherentId)
            ?? throw new NotFoundException($"L’adhérent avec l’id {adherentId} n’existe pas.");

        return StatistiquesAdherentDto.FromData(adherent);
    }

    public IEnumerable<StatistiquesAdherentDto> GetAllStatistiquesAdherents()
    {
        List<StatistiquesAdherentDto> statistiquesAdherents = [];
        foreach (Adherent adherent in _adherentRepo.GetAllAdherents())
            statistiquesAdherents.Add(StatistiquesAdherentDto.FromData(adherent));
        return statistiquesAdherents;
    }

    public StatistiquesOeuvreDto GetStatistiquesOeuvreById(int oeuvreId)
    {
        Oeuvre oeuvre =
            _inventaireRepo.GetOeuvre(oeuvreId)
            ?? throw new NotFoundException($"L’œuvre avec l’id {oeuvreId} n’existe pas.");
        return StatistiquesOeuvreDto.FromModel(oeuvre);
    }

    public IEnumerable<StatistiquesOeuvreDto> GetAllStatistiquesOeuvres()
    {
        List<StatistiquesOeuvreDto> statistiquesOeuvres = [];
        foreach (Oeuvre oeuvre in _inventaireRepo.GetAllOeuvres())
            statistiquesOeuvres.Add(StatistiquesOeuvreDto.FromModel(oeuvre));
        return statistiquesOeuvres;
    }

    public Statistiques GetStatistiquesStatistiquesById(int activiteId)
    {
        Statistiques statistiques =
            _statistiquesRepo.GetStatistiquesFromId(activiteId)
            ?? throw new NotFoundException(
                $"Le suivi de l'activité avec l’id {activiteId} n’existe pas."
            );
        return statistiques;
    }

    public Statistiques GetStatistiquesActiviteByDate(DateOnly date)
    {
        Statistiques statistiques =
            _statistiquesRepo.GetStatistiquesFromDate(date)
            ?? throw new NotFoundException(
                $"Le suivi de l'activité de la date {date} n’existe pas."
            );
        return statistiques;
    }

    public IEnumerable<Statistiques> GetAllStatistiquesActivites()
    {
        List<Statistiques> bilansActiviteQuotidiens = [];
        foreach (Statistiques bilanActivite in _statistiquesRepo.GetAllStatistiques())
            bilansActiviteQuotidiens.Add(bilanActivite);
        return bilansActiviteQuotidiens;
    }
}
