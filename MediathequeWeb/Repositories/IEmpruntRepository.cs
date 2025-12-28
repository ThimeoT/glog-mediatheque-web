public interface IEmpruntRepository
{
    Emprunt? GetEmprunt(int id);
    IEnumerable<Emprunt> GetAllEmprunts();
    void AddEmprunt(Emprunt emprunt);
    void RemoveEmprunt(Emprunt emprunt);
    void SaveChanges();
}
