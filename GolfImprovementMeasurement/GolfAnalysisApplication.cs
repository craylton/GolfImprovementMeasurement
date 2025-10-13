using GolfImprovementMeasurement.Parsers;

namespace GolfImprovementMeasurement;

internal sealed class GolfAnalysisApplication(
    AppConfiguration configuration,
    CsvParser parser,
    RegressionAnalyzer analyzer)
{
    public void Run()
    {
        var combinedData = parser.ParseFile(configuration.DataPath);

        Console.WriteLine(new string('=', 80));
        Console.WriteLine("MULTIPLE LINEAR REGRESSION ANALYSIS");
        Console.WriteLine(new string('=', 80));

        analyzer.Analyze(combinedData);
    }
}
