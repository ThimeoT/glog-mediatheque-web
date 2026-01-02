public interface ISSuiviActiviteRepository
{
    public IEnumerable<SuiviActivite> GetAllSuiviActivite();
    public SuiviActivite? GetSuiviActiviteFromId(int id);
    public SuiviActivite? GetSuiviActiviteFromDate(DateOnly date);
    public bool ExistsFromDate(DateOnly date);
    public void AddSuiviActivite(SuiviActivite suiviActiviteQuotidien);
    public void DeleteSuiviActivite(SuiviActivite suiviActiviteQuotidien);
    public void SaveChanges();

}