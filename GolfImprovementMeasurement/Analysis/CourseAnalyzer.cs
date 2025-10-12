using GolfImprovementMeasurement.Models;
using GolfImprovementMeasurement.Services;

namespace GolfImprovementMeasurement.Analysis;

public class CourseAnalyzer(MultipleLinearRegressionService regressionService, GolfDataDisplayer displayer)
{
    private const int MinimumRoundsForRegression = 4;

    public void AnalyzeCourse(string courseName, List<GolfRound> data)
    {
        if (string.IsNullOrWhiteSpace(courseName))
        {
            throw new ArgumentException("Course name cannot be null or empty.", nameof(courseName));
        }

        ArgumentNullException.ThrowIfNull(data);

        displayer.Display(courseName, data);

        if (data.Count < MinimumRoundsForRegression)
        {
            Console.WriteLine($"Insufficient data for {courseName}. At least {MinimumRoundsForRegression} rounds required.\n");
            return;
        }

        PerformRegressionAnalysis(courseName, data);
    }

    private void PerformRegressionAnalysis(string courseName, List<GolfRound> data)
    {
        Console.WriteLine($"\n--- {courseName} Regression ---");

        var result = regressionService.FitPlane(data);
        Console.WriteLine(result);

        var rSquared = regressionService.CalculateRSquared(data, result);
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
        var predicted = regressionService.Predict(result, sampleRound.DaysSinceReference, sampleRound.CourseCondition, sampleRound.CourseMultiplier);

        Console.WriteLine($"\n  Example: For day {sampleRound.DaysSinceReference}, condition {sampleRound.CourseCondition}, course {sampleRound.CourseMultiplier}");
        Console.WriteLine($"    Predicted shots: {predicted:F2}");
        Console.WriteLine($"    Actual shots: {sampleRound.NumberOfShots}");
    }
}
