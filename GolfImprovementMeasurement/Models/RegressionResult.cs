namespace GolfImprovementMeasurement.Models;

/// <summary>
/// Represents the result of a multiple linear regression analysis.
/// </summary>
public class RegressionResult
{
    /// <summary>
    /// Intercept (??) - baseline number of shots
    /// </summary>
    public required double Beta0 { get; init; }

    /// <summary>
    /// Coefficient for DaysSinceReference (??) - change in shots per day
    /// </summary>
    public required double Beta1 { get; init; }

    /// <summary>
    /// Coefficient for CourseCondition (??) - change in shots per unit of course condition
    /// </summary>
    public required double Beta2 { get; init; }

    /// <summary>
    /// Coefficient for CourseMultiplier (??) - change in shots per unit of course multiplier
    /// </summary>
    public required double Beta3 { get; init; }

    /// <summary>
    /// Number of data points used in the regression
    /// </summary>
    public required int DataPointCount { get; init; }

    /// <summary>
    /// Gets the equation as a formatted string
    /// </summary>
    public string GetEquation()
    {
        return $"z = {Beta0:F4} + {Beta1:F6}x + {Beta2:F4}y + {Beta3:F4}w";
    }

    public override string ToString()
    {
        return $"Regression Result (n={DataPointCount}):\n" +
               $"  ?? (Intercept) = {Beta0:F4}\n" +
               $"  ?? (Days coefficient) = {Beta1:F6}\n" +
               $"  ?? (Condition coefficient) = {Beta2:F4}\n" +
               $"  ?? (Course coefficient) = {Beta3:F4}\n" +
               $"  Equation: {GetEquation()}";
    }
}
