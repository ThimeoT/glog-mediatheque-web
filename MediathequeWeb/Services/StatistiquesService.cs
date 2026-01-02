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
        {
            statistiquesAdherents.Add(GetStatistiquesAdherentById(adherent.Id));
        }
        return statistiquesAdherents;
    }
}
