namespace GolfImprovementMeasurement;

public class AppConfiguration
{
    public required DateTime ReferenceDate { get; init; }
    public required string DataPath { get; init; }

    public static AppConfiguration Default => new()
    {
        ReferenceDate = new DateTime(2023, 5, 21),
        DataPath = "data/golf_data.csv"
    };
}
