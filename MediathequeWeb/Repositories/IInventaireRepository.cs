public interface IInventaireRepository
{
    Auteur? GetAuteur(int id);
    IEnumerable<Auteur> GetAllAuteurs();
    void AddAuteur(Auteur auteur);
    void RemoveAuteur(Auteur auteur);

    Oeuvre? GetOeuvre(int id);
    IEnumerable<Oeuvre> GetAllOeuvres();
    void AddOeuvre(Oeuvre oeuvre);
    void RemoveOeuvre(Oeuvre oeuvre);

    Exemplaire? GetExemplaire(int id);
    void AddExemplaire(Exemplaire exemplaire);
    void RemoveExemplaire(Exemplaire exemplaire);
}
