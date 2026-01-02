public class SuiviActiviteQuotidien
{
    public int Id {get; set; }
    public DateOnly Date { get; set; }
    private int _nouveauxEmprunts;
    public int NouveauxEmprunts
    {
        get => _nouveauxEmprunts;
        set
        {
            if (int.IsNegative(value))
                throw new DomainValidationException(
                    "Le nombre de nouveaux emprunts ne peut pas être négatif."
                );
        }
    }
    private int _retoursEffectues;
    public int RetoursEffectues
    {
        get => _retoursEffectues;
        set
        {
            if (int.IsNegative(value))
                throw new DomainValidationException(
                    "Le nombre de retours effectués ne peut pas être négatif."
                );
        }
    }
    private int _nouvellesReservations;
    public int NouvellesReservations
    {
        get => _nouvellesReservations;
        set
        {
            if (int.IsNegative(value))
                throw new DomainValidationException(
                    "Le nombre de nouvelles réservations ne peut pas être négatif."
                );
        }
    }
    private int _nouveauxAdherents;
    public int NouveauxAdherents
    {
        get => _nouveauxAdherents;
        set
        {
            if (int.IsNegative(value))
                throw new DomainValidationException(
                    "Le nombre total d'emprunts ne peut pas être négatif."
                );
        }
    }
}
