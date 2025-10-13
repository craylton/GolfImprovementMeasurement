using GolfImprovementMeasurement.Models;

namespace GolfImprovementMeasurement;

internal sealed class RegressionAnalyzer(MultipleLinearRegression regression)
{
    public void Analyze(IReadOnlyList<GolfRound> rounds)
    {
        Console.WriteLine();
        Console.WriteLine("--- Regression ---");

        var result = regression.FitPlane(rounds);
        Console.WriteLine(result);

        var rSquared = regression.CalculateRSquared(rounds, result);
        Console.WriteLine($"  R^2 (goodness of fit) = {rSquared:F4}");

        DisplayPredictionExample(rounds, result);
    }

    private void DisplayPredictionExample(IReadOnlyList<GolfRound> rounds, RegressionResult result)
    {
        if (rounds.Count == 0)
        {
            return;
        }

        var sampleRound = rounds[^1];
        var predicted = regression.Predict(
            result,
            sampleRound.DaysSinceReference,
            sampleRound.CourseCondition,
            sampleRound.CourseMultiplier);

        Console.WriteLine();
        Console.WriteLine(
            $"  Example: For day {sampleRound.DaysSinceReference}, condition {sampleRound.CourseCondition}, course {sampleRound.CourseMultiplier}");
        Console.WriteLine($"  Predicted shots: {predicted:F2}");
        Console.WriteLine($"  Actual shots: {sampleRound.NumberOfShots}");
    }
}
