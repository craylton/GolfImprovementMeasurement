namespace GolfImprovementMeasurement.Models;

/// <summary>
/// Represents the result of a multiple linear regression analysis.
/// </summary>
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

    public override string ToString() =>
        $"Regression Result (n={DataPointCount}):{Environment.NewLine}" +
        $"  Intercept = {Intercept:F4}{Environment.NewLine}" +
        $"  Days coefficient = {DaysCoefficient:F6}{Environment.NewLine}" +
        $"  Condition coefficient = {ConditionCoefficient:F4}{Environment.NewLine}" +
        $"  Course coefficient = {CourseCoefficient:F4}{Environment.NewLine}" +
        $"  Equation: {GetEquation()}";
}
