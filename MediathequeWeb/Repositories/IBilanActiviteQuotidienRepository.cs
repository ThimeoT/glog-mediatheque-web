public interface IBilanActiviteQuotidienRepository
{
    public IEnumerable<BilanActiviteQuotidien> GetAllBilansActiviteQuotidiens();
    public BilanActiviteQuotidien? GetBilanActiviteQuotidienFromId(int id);
    public BilanActiviteQuotidien? GetBilanActiviteQuotidienFromDate(DateOnly date);
    public bool ExistsFromDate(DateOnly date);
    public void AddBilanActiviteQuotidien(BilanActiviteQuotidien bilanActiviteQuotidien);
    public void RemoveBilanActiviteQuotidien(BilanActiviteQuotidien bilanActiviteQuotidien);
    public void SaveChanges();
}
