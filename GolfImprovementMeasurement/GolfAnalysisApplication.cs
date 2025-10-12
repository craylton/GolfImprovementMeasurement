using GolfImprovementMeasurement.Analysis;
using GolfImprovementMeasurement.Configuration;
using GolfImprovementMeasurement.Parsers;
using GolfImprovementMeasurement.Services;

namespace GolfImprovementMeasurement;

/// <summary>
/// Main application orchestrator for golf improvement measurement analysis
/// </summary>
public class GolfAnalysisApplication
{
    private readonly AppConfiguration _configuration;
    private readonly IGolfDataParser _parser;
    private readonly PlayerAnalyzer _analyzer;

    public GolfAnalysisApplication(
        AppConfiguration configuration,
        IGolfDataParser parser,
        PlayerAnalyzer analyzer)
    {
        _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        _parser = parser ?? throw new ArgumentNullException(nameof(parser));
        _analyzer = analyzer ?? throw new ArgumentNullException(nameof(analyzer));
    }

    public void Run()
    {
        DisplayHeader();
        
        var bhillData = _parser.ParseFile(_configuration.BhillDataPath);
        var phavenData = _parser.ParseFile(_configuration.PhavenDataPath);

        DisplayRegressionAnalysisHeader();
        
        _analyzer.AnalyzePlayer("Bhill", bhillData);
        _analyzer.AnalyzePlayer("Phaven", phavenData);

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
