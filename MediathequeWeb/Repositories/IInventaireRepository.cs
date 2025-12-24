public interface IInventaireRepository
{
    Auteur? GetAuteur(int id);
    IEnumerable<Auteur> GetAllAuteurs();
    void AddAuteur(Auteur auteur);
    void RemoveAuteur(Auteur auteur);

    Oeuvre? GetOeuvre(int oeuvreId);
    bool OeuvreExists(int oeuvreId);
    IEnumerable<Oeuvre> GetAllOeuvres();
    void AddOeuvre(Oeuvre oeuvre);
    void RemoveOeuvre(Oeuvre oeuvre);

    Exemplaire? GetExemplaire(int id);
    public IEnumerable<Exemplaire> GetAllExemplaires();
    public IEnumerable<Exemplaire> GetAllExemplairesByOeuvreId(int oeuvreId);
    void AddExemplaire(Exemplaire exemplaire);
    void RemoveExemplaire(Exemplaire exemplaire);

    void SaveChanges();
}
