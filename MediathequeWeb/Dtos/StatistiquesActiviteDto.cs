public class StatistiquesActiviteDto
{
    public string Date { get; set; }
    public int NouveauxEmprunts { get; set; }
    public int RetoursEffectues { get; set; }
    public int NouvellesReservations { get; set; }
    public int NouveauxAdherents { get; set; }

    public static StatistiquesActiviteDto FromModel(Statistiques activite)
    {
        return new StatistiquesActiviteDto
        {
            Date = activite.Date.ToString("yyyy-MM-dd"),
            NouveauxEmprunts = activite.NouveauxEmprunts,
            RetoursEffectues = activite.RetoursEffectues,
            NouvellesReservations = activite.NouvellesReservations,
            NouveauxAdherents = activite.NouveauxAdherents,
        };
    }
}
