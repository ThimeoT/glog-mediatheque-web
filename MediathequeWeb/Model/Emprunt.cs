public class Emprunt
{
    public int Id { get; set; }

    public Adherent Adherent { get; set; }

    public Exemplaire Exemplaire { get; set; }

    public DateOnly DateEmprunt { get; } = DateOnly.FromDateTime(DateTime.Now);

    private DateOnly _dateRetourPrevue;
    public DateOnly DateRetourPrevue
    {
        get => _dateRetourPrevue;
        set
        {
            if (value < DateEmprunt)
                throw new DomainValidationException(
                    "La date de retour prévue doit être après la date d’emprunt."
                );
            _dateRetourPrevue = value;
        }
    }

    private DateOnly? _dateRetourReelle;
    public DateOnly? DateRetourReelle
    {
        get => _dateRetourReelle;
        set
        {
            if (value != null && value < DateEmprunt)
                throw new DomainValidationException(
                    "La date de retour réelle ne peut pas être avant la date d’emprunt."
                );
            _dateRetourReelle = value;
        }
    }
}