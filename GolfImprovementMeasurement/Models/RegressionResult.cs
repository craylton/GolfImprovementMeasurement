namespace GolfImprovementMeasurement.Models;

internal sealed record RegressionResult(
    double Intercept,
    double DaysCoefficient,
    double ConditionCoefficient,
    double CourseCoefficient,
    int DataPointCount)
{
    public string GetEquation() =>
        $"shots = " +
        $"{Intercept:F4} + " +
        $"{DaysCoefficient:F6}*days + " +
        $"{ConditionCoefficient:F4}*condition + " +
        $"{CourseCoefficient:F4}*course";

    public double CalculateRSquared(IReadOnlyList<GolfRound> rounds)
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
            var predicted = Predict(round.DaysSinceReference, round.CourseCondition, round.CourseMultiplier);

            totalSumOfSquares += Math.Pow(observed - meanObserved, 2);
            residualSumOfSquares += Math.Pow(observed - predicted, 2);
        }

        return totalSumOfSquares > 0
            ? 1 - residualSumOfSquares / totalSumOfSquares
            : 0;
    }

    public double Predict(int daysSinceReference, decimal courseCondition, decimal courseMultiplier) =>
        Intercept +
        DaysCoefficient * daysSinceReference +
        ConditionCoefficient * (double)courseCondition +
        CourseCoefficient * (double)courseMultiplier;

    public override string ToString() =>
        $"Regression Result (n={DataPointCount}):{Environment.NewLine}" +
        $"  Intercept = {Intercept:F4}{Environment.NewLine}" +
        $"  Days coefficient = {DaysCoefficient:F6}{Environment.NewLine}" +
        $"  Condition coefficient = {ConditionCoefficient:F4}{Environment.NewLine}" +
        $"  Course coefficient = {CourseCoefficient:F4}{Environment.NewLine}" +
        $"  Equation: {GetEquation()}";
}
