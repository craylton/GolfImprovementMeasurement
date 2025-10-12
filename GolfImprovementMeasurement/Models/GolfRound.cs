namespace GolfImprovementMeasurement.Models;

public class GolfRound
{
    public int DaysSinceReference { get; set; }
    public int NumberOfShots { get; set; }
    public decimal CourseCondition { get; set; }
    public decimal CourseMultiplier { get; set; }
}
