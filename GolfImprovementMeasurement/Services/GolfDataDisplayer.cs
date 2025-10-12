using GolfImprovementMeasurement.Models;

namespace GolfImprovementMeasurement.Services;

public static class GolfDataDisplayer
{
    public static void Display(string playerName, IEnumerable<GolfRound> rounds)
    {
        Console.WriteLine($"{playerName} Data:");

        foreach (var round in rounds)
        {
            Console.WriteLine(
                $"Days: {round.DaysSinceReference}, " +
                $"Shots: {round.NumberOfShots}, " +
                $"Condition: {round.CourseCondition}");
        }

        Console.WriteLine();
    }
}
