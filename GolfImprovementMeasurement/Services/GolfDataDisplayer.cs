using GolfImprovementMeasurement.Models;

namespace GolfImprovementMeasurement.Services;

public class GolfDataDisplayer(TextWriter output)
{
    public GolfDataDisplayer() : this(Console.Out)
    {
    }

    public void Display(string courseName, IEnumerable<GolfRound> rounds)
    {
        if (string.IsNullOrWhiteSpace(courseName))
        {
            throw new ArgumentException("Course name cannot be null or empty.", nameof(courseName));
        }

        ArgumentNullException.ThrowIfNull(rounds);

        output.WriteLine($"{courseName} Data:");

        foreach (var round in rounds)
        {
            output.WriteLine(
                $"Days: {round.DaysSinceReference}, " +
                $"Shots: {round.NumberOfShots}, " +
                $"Condition: {round.CourseCondition}");
        }

        output.WriteLine();
    }
}
