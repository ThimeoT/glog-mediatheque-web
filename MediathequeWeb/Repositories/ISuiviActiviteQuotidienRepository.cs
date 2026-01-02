public interface ISSuiviActiviteQuotidienRepository
{
    public void GetAllSuiviActiviteQuotidien();
    public void GetSuiviActiviteQuotidienFromId(int id);
    public void GetSuiviActiviteQuotidienFromDate(DateOnly date);
    public void AddSuiviActiviteQuotidien(SuiviActiviteQuotidien suiviActiviteQuotidien);
    public void DeleteSuiviActiviteQuotidien(SuiviActiviteQuotidien suiviActiviteQuotidien);
    public bool ExistsFromDate(DateOnly date);
}
