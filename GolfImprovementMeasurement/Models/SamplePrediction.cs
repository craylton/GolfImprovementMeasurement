namespace GolfImprovementMeasurement.Models;

internal sealed record SamplePrediction(
    int DaysSinceReference,
    decimal CourseCondition,
    decimal CourseMultiplier,
    double PredictedShots,
    int ActualShots);
