namespace GolfImprovementMeasurement;

internal sealed record AppConfiguration(DateTime ReferenceDate, string DataPath)
{
    public static AppConfiguration Default => new(new(2023, 5, 21), "Data/golf_data.csv");
}
