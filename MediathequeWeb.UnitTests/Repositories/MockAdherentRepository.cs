public class MockAdherentRepository : IAdherentRepository
{
    private readonly List<Adherent> _adherents;
    private int _nextId = 3;

    public MockAdherentRepository()
    {
        _adherents =
        [
            new Adherent
            {
                Id = 1,
                Nom = "Némard",
                Prenom = "Jean",
                Email = "jeannemard@ensc.fr",
                Emprunts = [],
                Reservations = [],
            },
            new Adherent
            {
                Id = 2,
                Nom = "Dicule",
                Prenom = "Thierry",
                Email = "thierrydicule@ensc.fr",
                Emprunts = [],
                Reservations = [],
            },
        ];
    }

    public Adherent? GetAdherent(int id)
    {
        return _adherents.FirstOrDefault(a => a.Id == id);
    }

    public IEnumerable<Adherent> GetAllAdherents()
    {
        return _adherents;
    }

    public bool AdherentExists(int adherentId)
    {
        return _adherents.Any(a => a.Id == adherentId);
    }

    public void AddAdherent(Adherent adherent)
    {
        adherent.Id = _nextId++;
        adherent.Emprunts ??= [];
        adherent.Reservations ??= [];
        _adherents.Add(adherent);
    }

    public void RemoveAdherent(Adherent adherent)
    {
        _adherents.Remove(adherent);
    }

    public void SaveChanges() { }
}
