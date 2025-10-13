using System.Globalization;
using GolfImprovementMeasurement.Models;

namespace GolfImprovementMeasurement.Parsers;

internal sealed class CsvParser(DateTime referenceDate)
{
    private const int ExpectedFieldCount = 4;
    private const int DateIndex = 0;
    private const int ShotsIndex = 1;
    private const int ConditionIndex = 2;
    private const int CourseIndex = 3;

    public IReadOnlyList<GolfRound> ParseFile(string filePath)
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

        var isFirstLine = true;
        foreach (var line in File.ReadLines(filePath))
        {
            if (isFirstLine)
            {
                // Skip header row
                isFirstLine = false;
                continue;
            }

            if (TryParseLine(line, out var round))
            {
                rounds.Add(round);
            }
        }

        return rounds;
    }

    private bool TryParseLine(string line, out GolfRound round)
    {
        round = default!;

        if (string.IsNullOrWhiteSpace(line))
        {
            return false;
        }

        var parts = line.Split(',', StringSplitOptions.TrimEntries);
        if (parts.Length < ExpectedFieldCount)
        {
            return false;
        }

        var dateStr = parts[DateIndex];
        if (!ElapsedDaysParser.TryParse(dateStr, referenceDate, out var daysSinceReference))
        {
            return false;
        }

        if (!int.TryParse(
            parts[ShotsIndex],
            NumberStyles.Integer,
            CultureInfo.InvariantCulture,
            out var shots))
        {
            return false;
        }

        if (!decimal.TryParse(
            parts[ConditionIndex],
            NumberStyles.Number,
            CultureInfo.InvariantCulture,
            out var courseCondition))
        {
            return false;
        }

        if (!decimal.TryParse(
            parts[CourseIndex],
            NumberStyles.Number,
            CultureInfo.InvariantCulture,
            out var courseMultiplier))
        {
            return false;
        }

        round = new GolfRound(daysSinceReference, shots, courseCondition, courseMultiplier);
        return true;
    }
}
