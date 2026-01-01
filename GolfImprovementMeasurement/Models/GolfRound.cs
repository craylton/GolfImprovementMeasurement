namespace GolfImprovementMeasurement.Models;

internal sealed record GolfRound(
    int DaysSinceReference,
    int NumberOfShots,
    double CourseCondition,
    double CourseMultiplier);
