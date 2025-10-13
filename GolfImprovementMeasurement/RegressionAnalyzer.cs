using GolfImprovementMeasurement.Models;

namespace GolfImprovementMeasurement;

internal sealed class RegressionAnalyzer(MultipleLinearRegression regression)
{
    public AnalysisResult Analyze(IReadOnlyList<GolfRound> rounds)
    {
        var result = regression.FitPlane(rounds);
        var rSquared = RegressionMetrics.CalculateRSquared(rounds, result);

        var sampleRound = rounds[^1];

        var predicted = result.Predict(
            sampleRound.DaysSinceReference,
            sampleRound.CourseCondition,
            sampleRound.CourseMultiplier);

        var sample = new SamplePrediction(
            sampleRound.DaysSinceReference,
            sampleRound.CourseCondition,
            sampleRound.CourseMultiplier,
            predicted,
            sampleRound.NumberOfShots);

        return new AnalysisResult(result, rSquared, sample);
    }
}
