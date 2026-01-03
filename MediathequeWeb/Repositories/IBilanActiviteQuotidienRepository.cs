public interface IBilanActiviteQuotidienRepository
{
    public IEnumerable<BilanActiviteQuotidien> GetAllBilansActiviteQuotidiens();
    public BilanActiviteQuotidien? GetBilanActiviteQuotidienFromId(int id);
    public BilanActiviteQuotidien? GetBilanActiviteQuotidienFromDate(DateOnly date);
    public bool ExistsFromDate(DateOnly date);
    public void AddBilanActiviteQuotidien(BilanActiviteQuotidien bilanActiviteQuotidienQuotidien);
    public void DeleteBilanActiviteQuotidien(
        BilanActiviteQuotidien bilanActiviteQuotidienQuotidien
    );
    public void SaveChanges();
}
