using GolfImprovementMeasurement.Models;

namespace GolfImprovementMeasurement.Parsers;

public class CsvParser(DateTime referenceDate)
{
    public List<GolfRound> ParseFile(string filePath)
    {
        var rounds = new List<GolfRound>();
        var lines = File.ReadAllLines(filePath);

        // Skip header row
        for (int i = 1; i < lines.Length; i++)
        {
            var line = lines[i];
            var round = ParseLine(line);
            if (round != null)
            {
                rounds.Add(round);
            }
        }

        return rounds;
    }

    private GolfRound? ParseLine(string line)
    {
        var parts = line.Split(',');

        if (parts.Length < 3)
        {
            return null;
        }

        var dateStr = parts[0].Trim();
        var roundDate = DateParser.Parse(dateStr);
        var daysSince = CalculateDaysSinceReference(roundDate);

        var shots = int.Parse(parts[1].Trim());
        var condition = ParseConditionMultiplier(parts[2].Trim());

        return new GolfRound
        {
            DaysSinceReference = daysSince,
            NumberOfShots = shots,
            CourseCondition = condition
        };
    }

    private int CalculateDaysSinceReference(DateTime date)
    {
        return (int)(date - referenceDate).TotalDays;
    }

    private static decimal ParseConditionMultiplier(string conditionStr)
    {
        // Convert 'x' to 2.0, otherwise parse as decimal
        return conditionStr.Equals("x", StringComparison.OrdinalIgnoreCase)
            ? 2.0m
            : decimal.Parse(conditionStr);
    }
}
