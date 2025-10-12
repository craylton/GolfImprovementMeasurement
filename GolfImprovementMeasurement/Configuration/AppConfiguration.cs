namespace GolfImprovementMeasurement.Configuration;

public class AppConfiguration
{
    public required DateTime ReferenceDate { get; init; }
    public required string BhillDataPath { get; init; }
    public required string PhavenDataPath { get; init; }

    public static AppConfiguration Default => new()
    {
        ReferenceDate = new DateTime(2023, 5, 21),
        BhillDataPath = "data/bhill.csv",
        PhavenDataPath = "data/phaven.csv"
    };
}
