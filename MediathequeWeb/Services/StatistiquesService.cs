public class StatistiquesService
{
    private readonly IAdherentRepository _adherentRepo;
    private readonly IEmpruntRepository _empruntRepo;
    private readonly IInventaireRepository _inventaireRepo;
    private readonly ISuiviActiviteRepository _suiviActiviteRepo;

    public StatistiquesService(
        IAdherentRepository adherentRepo,
        IEmpruntRepository empruntRepo,
        IInventaireRepository inventaireRepo,
        ISuiviActiviteRepository suiviActiviteRepo
    )
    {
        _adherentRepo = adherentRepo;
        _empruntRepo = empruntRepo;
        _inventaireRepo = inventaireRepo;
        _suiviActiviteRepo = suiviActiviteRepo;
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

    public StatistiquesOeuvreDto GetStatistiquesOeuvreDtoById(int oeuvreId)
    {
        Oeuvre oeuvre =
            _inventaireRepo.GetOeuvre(oeuvreId)
            ?? throw new NotFoundException($"L’œuvre avec l’id {oeuvreId} n’existe pas.");
        return StatistiquesOeuvreDto.FromModel(oeuvre);
    }

    public IEnumerable<StatistiquesOeuvreDto> GetAllStatitstiquesOeuvres()
    {
        List<StatistiquesOeuvreDto> statistiquesOeuvres = [];
        foreach (Oeuvre oeuvre in _inventaireRepo.GetAllOeuvres())
            statistiquesOeuvres.Add(StatistiquesOeuvreDto.FromModel(oeuvre));
        return statistiquesOeuvres;
    }

    public StatistiquesActiviteDto GetStatistiquesSuiviActiviteById(int activiteId)
    {
        SuiviActivite suiviActivite =
            _suiviActiviteRepo.GetSuiviActiviteFromId(activiteId)
            ?? throw new NotFoundException($"Le suivi de l'activité avec l’id {activiteId} n’existe pas.");
        return StatistiquesActiviteDto.FromModel(suiviActivite);
    }
    public StatistiquesActiviteDto GetStatistiquesSuiviActiviteByDate(DateOnly date)
    {
        SuiviActivite suiviActivite =
            _suiviActiviteRepo.GetSuiviActiviteFromDate(date)
            ?? throw new NotFoundException($"Le suivi de l'activité de la date {date} n’existe pas.");
        return StatistiquesActiviteDto.FromModel(suiviActivite);
    }

    public IEnumerable<StatistiquesActiviteDto> GetAllSuivisActivite()
    {
        List<StatistiquesActiviteDto> statistiquesActivites = [];
        foreach (SuiviActivite activite in _suiviActiviteRepo.GetAllSuiviActivite())
            statistiquesActivites.Add(StatistiquesActiviteDto.FromModel(activite));
        return statistiquesActivites;
    }
}
