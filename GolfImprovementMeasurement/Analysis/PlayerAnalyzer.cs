using GolfImprovementMeasurement.Models;
using GolfImprovementMeasurement.Parsers;
using GolfImprovementMeasurement.Services;

namespace GolfImprovementMeasurement.Analysis;

/// <summary>
/// Analyzes golf data for a player and displays regression results
/// </summary>
public class PlayerAnalyzer
{
    private readonly IRegressionService _regressionService;
    private readonly IGolfDataDisplayer _displayer;
    private const int MinimumRoundsForRegression = 3;

    public PlayerAnalyzer(IRegressionService regressionService, IGolfDataDisplayer displayer)
    {
        _regressionService = regressionService ?? throw new ArgumentNullException(nameof(regressionService));
        _displayer = displayer ?? throw new ArgumentNullException(nameof(displayer));
    }

    public void AnalyzePlayer(string playerName, List<GolfRound> data)
    {
        if (string.IsNullOrWhiteSpace(playerName))
        {
            throw new ArgumentException("Player name cannot be null or empty.", nameof(playerName));
        }

        ArgumentNullException.ThrowIfNull(data);

        _displayer.Display(playerName, data);

        if (data.Count < MinimumRoundsForRegression)
        {
            Console.WriteLine($"Insufficient data for {playerName}. At least {MinimumRoundsForRegression} rounds required.\n");
            return;
        }

        PerformRegressionAnalysis(playerName, data);
    }

    private void PerformRegressionAnalysis(string playerName, List<GolfRound> data)
    {
        Console.WriteLine($"\n--- {playerName} Regression ---");
        
        var result = _regressionService.FitPlane(data);
        Console.WriteLine(result);
        
        var rSquared = _regressionService.CalculateRSquared(data, result);
        Console.WriteLine($"  R² (goodness of fit) = {rSquared:F4}");
        
        DisplayPredictionExample(data, result);
    }

    private void DisplayPredictionExample(List<GolfRound> data, RegressionResult result)
    {
        if (data.Count == 0)
        {
            return;
        }

        var sampleRound = data[0];
        var predicted = _regressionService.Predict(result, sampleRound.DaysSinceReference, sampleRound.CourseCondition);
        
        Console.WriteLine($"\n  Example: For day {sampleRound.DaysSinceReference}, condition {sampleRound.CourseCondition}");
        Console.WriteLine($"    Predicted shots: {predicted:F2}");
        Console.WriteLine($"    Actual shots: {sampleRound.NumberOfShots}");
    }
}
