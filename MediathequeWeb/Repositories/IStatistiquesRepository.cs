public interface IStatistiquesRepository
{
    public IEnumerable<Statistiques> GetAllStatistiques();
    public Statistiques? GetStatistiquesFromId(int id);
    public Statistiques? GetStatistiquesFromDate(DateOnly date);
    public bool ExistsFromDate(DateOnly date);
    public void AddStatistiques(Statistiques statistiques);
    public void RemoveStatistiques(Statistiques statistiques);
    public void SaveChanges();
}
