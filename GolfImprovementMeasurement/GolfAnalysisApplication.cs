using GolfImprovementMeasurement.Parsers;

namespace GolfImprovementMeasurement;

public class GolfAnalysisApplication(
    AppConfiguration configuration,
    CsvParser parser,
    CourseAnalyzer analyzer)
{
    public void Run()
    {
        var combinedData = parser.ParseFile(configuration.DataPath);

        DisplayRegressionAnalysisHeader();

        analyzer.AnalyzeCourse("Combined Courses", combinedData);
    }

    private static void DisplayRegressionAnalysisHeader()
    {
        Console.WriteLine(new string('=', 80));
        Console.WriteLine("MULTIPLE LINEAR REGRESSION ANALYSIS");
        Console.WriteLine(new string('=', 80));
    }
}
