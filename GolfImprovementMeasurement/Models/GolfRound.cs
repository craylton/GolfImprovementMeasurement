namespace GolfImprovementMeasurement.Models;

internal sealed record GolfRound(
    int DaysSinceReference,
    int NumberOfShots,
    decimal CourseCondition,
    decimal CourseMultiplier);
