using GolfImprovementMeasurement.Models;

namespace GolfImprovementMeasurement;

public class CourseAnalyzer(MultipleLinearRegression regressionService)
{
    public void AnalyzeCourse(string courseName, List<GolfRound> data)
    {
        try
        {
            PerformRegressionAnalysis(courseName, data);
        }
        catch (ArgumentException ex) when (ex.ParamName == "rounds")
        {
            Console.WriteLine($"Insufficient data for {courseName}. {ex.Message}\n");
        }
    }

    private void PerformRegressionAnalysis(string courseName, List<GolfRound> data)
    {
        Console.WriteLine($"\n--- {courseName} Regression ---");

        var result = regressionService.FitPlane(data);
        Console.WriteLine(result);

        var rSquared = regressionService.CalculateRSquared(data, result);
        Console.WriteLine($"  R^2 (goodness of fit) = {rSquared:F4}");

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
