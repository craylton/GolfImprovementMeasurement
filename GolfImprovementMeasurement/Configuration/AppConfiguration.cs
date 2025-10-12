namespace GolfImprovementMeasurement.Configuration;

public class AppConfiguration
{
    public required DateTime ReferenceDate { get; init; }
    public required string CombinedDataPath { get; init; }

    public static AppConfiguration Default => new()
    {
        ReferenceDate = new DateTime(2023, 5, 21),
        CombinedDataPath = "data/combined.csv"
    };
}
