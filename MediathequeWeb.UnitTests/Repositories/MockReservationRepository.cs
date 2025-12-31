public class MockReservationRepository : IReservationRepository
{
    private readonly List<Auteur> _auteurs;
    private readonly List<Adherent> _adherents;
    private readonly List<Reservation> _reservations;
    private readonly List<Oeuvre> _oeuvres;
    private int _nextId = 3;

    public MockReservationRepository()
    {
        _auteurs =
        [
            new Auteur
            {
                Id = 1,
                Nom = "Herbert",
                Prenom = "Frank",
            },
            new Auteur
            {
                Id = 2,
                Nom = "Nolan",
                Prenom = "Christopher",
            },
        ];

        _oeuvres =
        [
            new Oeuvre
            {
                Id = 1,
                Titre = "Dune",
                Type = OeuvreType.Livre,
                Auteur = _auteurs[0],
            },
            new Oeuvre
            {
                Id = 2,
                Titre = "Interstellar",
                Type = OeuvreType.DVD,
                Auteur = _auteurs[1],
            },
        ];

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
        _reservations =
        [
            new Reservation
            {
                Adherent = _adherents[0],
                Oeuvre = _oeuvres[0],
                DateReservation = DateOnly.FromDateTime(DateTime.Now),
                Statut = StatutReservation.Active,
            },
            new Reservation
            {
                Adherent = _adherents[1],
                Oeuvre = _oeuvres[1],
                DateReservation = DateOnly.FromDateTime(DateTime.Now),
                Statut = StatutReservation.Active,
            },
        ];
    }

    public Reservation? GetReservation(int id)
    {
        return _reservations.FirstOrDefault(a => a.Id == id);
    }

    public IEnumerable<Reservation> GetAllReservations()
    {
        return _reservations;
    }

    public IEnumerable<Reservation> GetAllReservationsFromAdherentId(int adherentId)
    {
        return _reservations.Where(r => r.Adherent.Id == adherentId);
    }

    public IEnumerable<Reservation> GetAllReservationsFromOeuvreId(int oeuvreId)
    {
        return _reservations.Where(r => r.Oeuvre.Id == oeuvreId);
    }

    public bool ReservationActiveExists(int adherentId, int oeuvreId)
    {
        return _reservations.Any(r => r.Adherent.Id == adherentId && r.Oeuvre.Id == oeuvreId);
    }

    public void AddReservation(Reservation reservation)
    {
        reservation.Id = _nextId++;
        reservation.DateReservation = DateOnly.FromDateTime(DateTime.Now);
        _reservations.Add(reservation);
    }

    public void RemoveReservation(Reservation reservation)
    {
        _reservations.Remove(reservation);
    }

    public void SaveChanges()
    {
        // Rien à faire
    }
}
