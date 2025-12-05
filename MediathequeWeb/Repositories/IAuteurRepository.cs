public interface IAuteurRepository
{
    Auteur? Get(int id);
    IEnumerable<Auteur> GetAll();
    void Add(Auteur auteur);
    void Remove(Auteur auteur);
}
