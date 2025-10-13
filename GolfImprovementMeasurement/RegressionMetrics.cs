using GolfImprovementMeasurement.Models;

namespace GolfImprovementMeasurement;

internal static class RegressionMetrics
{
    public static double CalculateRSquared(IReadOnlyList<GolfRound> rounds, RegressionResult result)
    {
        if (rounds.Count == 0)
        {
            return 0;
        }

        var meanObserved = rounds.Average(r => r.NumberOfShots);

        double totalSumOfSquares = 0;
        double residualSumOfSquares = 0;

        foreach (var round in rounds)
        {
            var observed = round.NumberOfShots;
            var predicted = result.Predict(round.DaysSinceReference, round.CourseCondition, round.CourseMultiplier);

            totalSumOfSquares += Math.Pow(observed - meanObserved, 2);
            residualSumOfSquares += Math.Pow(observed - predicted, 2);
        }

        return totalSumOfSquares > 0
            ? 1 - residualSumOfSquares / totalSumOfSquares
            : 0;
    }
}
