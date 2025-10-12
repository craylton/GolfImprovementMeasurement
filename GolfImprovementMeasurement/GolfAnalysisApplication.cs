using GolfImprovementMeasurement.Analysis;
using GolfImprovementMeasurement.Configuration;
using GolfImprovementMeasurement.Parsers;

namespace GolfImprovementMeasurement;

public class GolfAnalysisApplication(
    AppConfiguration configuration,
    CsvParser parser,
    CourseAnalyzer analyzer)
{
    public void Run()
    {
        DisplayHeader();

        var bhillData = parser.ParseFile(configuration.BhillDataPath);
        var phavenData = parser.ParseFile(configuration.PhavenDataPath);

        DisplayRegressionAnalysisHeader();

        analyzer.AnalyzeCourse("Bhill", bhillData);
        analyzer.AnalyzeCourse("Phaven", phavenData);

        DisplayFooter();
    }

    private static void DisplayHeader()
    {
        Console.WriteLine(new string('=', 80));
        Console.WriteLine("GOLF IMPROVEMENT MEASUREMENT ANALYSIS");
        Console.WriteLine(new string('=', 80));
        Console.WriteLine();
    }

    private static void DisplayRegressionAnalysisHeader()
    {
        Console.WriteLine(new string('=', 80));
        Console.WriteLine("MULTIPLE LINEAR REGRESSION ANALYSIS");
        Console.WriteLine(new string('=', 80));
    }

    private static void DisplayFooter()
    {
        Console.WriteLine("\n" + new string('=', 80));
    }
}
