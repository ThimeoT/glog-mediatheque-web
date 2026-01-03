public class MockBilanActiviteQuotidienRepository : IBilanActiviteQuotidienRepository
{
    private readonly List<Auteur> _auteurs;
    private readonly List<Adherent> _adherents;
    private readonly List<Reservation> _reservations;
    private readonly List<Oeuvre> _oeuvres;
    private readonly List<Exemplaire> _exemplaires;
    private readonly List<Emprunt> _emprunts;
    private readonly List<BilanActiviteQuotidien> _bilansActiviteQuotidiens;
    private int _nextId = 3;

    public MockBilanActiviteQuotidienRepository()
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

        _auteurs =
        [
            new Auteur
            {
                Id = 1,
                Nom = "Williams",
                Prenom = "Robbie",
            },
            new Auteur
            {
                Id = 2,
                Nom = "King",
                Prenom = "Stephen",
            },
            new Auteur
            {
                Id = 2,
                Nom = "Cameron",
                Prenom = "James",
            },
        ];

        _oeuvres =
        [
            new Oeuvre
            {
                Id = 1,
                Titre = "Kids",
                Type = OeuvreType.CD,
                Auteur = _auteurs[0],
            },
            new Oeuvre
            {
                Id = 2,
                Titre = "It",
                Type = OeuvreType.DVD,
                Auteur = _auteurs[1],
            },
            new Oeuvre
            {
                Id = 3,
                Titre = "Avatar",
                Type = OeuvreType.DVD,
                Auteur = _auteurs[2],
            },
        ];
        _exemplaires =
        [
            new Exemplaire
            {
                Id = 1,
                Oeuvre = _oeuvres[0],
                Statut = StatutExemplaire.Disponible,
            },
            new Exemplaire
            {
                Id = 2,
                Oeuvre = _oeuvres[1],
                Statut = StatutExemplaire.Disponible,
            },
        ];

        _reservations =
        [
            new Reservation
            {
                Id = 1,
                Adherent = _adherents[0],
                Oeuvre = _oeuvres[0],
                DateReservation = DateOnly.FromDateTime(DateTime.Now),
                Statut = StatutReservation.Active,
            },
            new Reservation
            {
                Id = 2,
                Adherent = _adherents[1],
                Oeuvre = _oeuvres[1],
                DateReservation = DateOnly.FromDateTime(DateTime.Now),
                Statut = StatutReservation.Active,
            },
        ];

        _exemplaires[1].Statut = StatutExemplaire.Emprunte;

        _emprunts =
        [
            new Emprunt
            {
                Id = 1,
                Adherent = _adherents[0],
                Exemplaire = _exemplaires[0],
                DateEmprunt = DateOnly.FromDateTime(DateTime.Now.AddDays(-10)),
                DateRetourPrevue = DateOnly.FromDateTime(DateTime.Now.AddDays(-10 + 21)),
                DateRetourReelle = null,
            },
            new Emprunt
            {
                Id = 2,
                Adherent = _adherents[1],
                Exemplaire = _exemplaires[1],
                DateEmprunt = DateOnly.FromDateTime(DateTime.Now.AddDays(-4)),
                DateRetourPrevue = DateOnly.FromDateTime(DateTime.Now.AddDays(-4 + 21)),
                DateRetourReelle = null,
            },
        ];
        _adherents[0].Emprunts.Add(_emprunts[0]);
        _adherents[1].Emprunts.Add(_emprunts[1]);

        _bilansActiviteQuotidiens = [new BilanActiviteQuotidien { Id = 1 }];
    }

    public BilanActiviteQuotidien? GetBilanActiviteQuotidienFromId(int id)
    {
        return _bilansActiviteQuotidiens.FirstOrDefault(a => a.Id == id);
    }

    public BilanActiviteQuotidien? GetBilanActiviteQuotidienFromDate(DateOnly date)
    {
        return _bilansActiviteQuotidiens.FirstOrDefault(a => a.Date == date);
    }

    public IEnumerable<BilanActiviteQuotidien> GetAllBilansActiviteQuotidiens()
    {
        return _bilansActiviteQuotidiens;
    }

    public bool ExistsFromDate(DateOnly date)
    {
        return _bilansActiviteQuotidiens.Any(b => b.Date == date);
    }

    public void AddBilanActiviteQuotidien(BilanActiviteQuotidien bilanActiviteQuotidienQuotidien)
    {
        bilanActiviteQuotidienQuotidien.Id = _nextId++;
        _bilansActiviteQuotidiens.Add(bilanActiviteQuotidienQuotidien);
    }

    public void RemoveBilanActiviteQuotidien(BilanActiviteQuotidien bilanActiviteQuotidien)
    {
        _bilansActiviteQuotidiens.Remove(bilanActiviteQuotidien);
    }

    public void SaveChanges() { }
}
