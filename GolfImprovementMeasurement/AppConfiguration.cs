namespace GolfImprovementMeasurement;

internal sealed record AppConfiguration(DateTime ReferenceDate, string DataPath)
{
    public static AppConfiguration Default { get; } = new(
        new(2023, 5, 21),
        Path.Combine(AppContext.BaseDirectory, "Data", "golf_data.csv"));
}
