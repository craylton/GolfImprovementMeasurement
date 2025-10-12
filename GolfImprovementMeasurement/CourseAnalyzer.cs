using GolfImprovementMeasurement.Models;

namespace GolfImprovementMeasurement;

public class CourseAnalyzer(MultipleLinearRegression regressionService)
{
    private const int MinimumRoundsForRegression = 4;

    public void AnalyzeCourse(string courseName, List<GolfRound> data)
    {
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

        var sampleRound = data.Last();
        var predicted = regressionService.Predict(
            result,
            sampleRound.DaysSinceReference,
            sampleRound.CourseCondition,
            sampleRound.CourseMultiplier);

        Console.WriteLine(Environment.NewLine);
        Console.WriteLine($"\tExample: " +
            $"For day {sampleRound.DaysSinceReference}, " +
            $"condition {sampleRound.CourseCondition}, " +
            $"course {sampleRound.CourseMultiplier}");
        Console.WriteLine($"\tPredicted shots: {predicted:F2}");
        Console.WriteLine($"\tActual shots: {sampleRound.NumberOfShots}");
    }
}
