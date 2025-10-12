using System.Globalization;
using GolfImprovementMeasurement.Models;

namespace GolfImprovementMeasurement.Parsers;

public class CsvParser(DateTime referenceDate)
{
    public List<GolfRound> ParseFile(string filePath)
    {
        if (string.IsNullOrWhiteSpace(filePath))
        {
            throw new ArgumentException("File path cannot be null or empty.", nameof(filePath));
        }

        if (!File.Exists(filePath))
        {
            throw new FileNotFoundException($"CSV file not found: {filePath}");
        }

        var rounds = new List<GolfRound>();
        var lines = File.ReadAllLines(filePath);

        if (lines.Length == 0)
        {
            return rounds;
        }

        // Skip header row
        for (int i = 1; i < lines.Length; i++)
        {
            var line = lines[i];
            if (string.IsNullOrWhiteSpace(line))
            {
                continue;
            }

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

        if (parts.Length < 4)
        {
            return null;
        }

        var dateStr = parts[0].Trim();
        var roundDate = DateParser.Parse(dateStr);
        var daysSince = CalculateDaysSinceReference(roundDate);

        var shots = int.Parse(parts[1].Trim(), CultureInfo.InvariantCulture);
        var condition = decimal.Parse(parts[2].Trim(), CultureInfo.InvariantCulture);
        var courseMultiplier = decimal.Parse(parts[3].Trim(), CultureInfo.InvariantCulture);

        return new GolfRound
        {
            DaysSinceReference = daysSince,
            NumberOfShots = shots,
            CourseCondition = condition,
            CourseMultiplier = courseMultiplier
        };
    }

    private int CalculateDaysSinceReference(DateTime date) =>
        (int)(date - referenceDate).TotalDays;
}
