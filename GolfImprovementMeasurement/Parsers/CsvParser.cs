using System.Globalization;
using GolfImprovementMeasurement.Models;

namespace GolfImprovementMeasurement.Parsers;

internal sealed partial class CsvParser(DateTime referenceDate)
{
    private const int ExpectedFieldCount = 4;

    private const int DateIndex = 0;
    private const int ShotsIndex = 1;
    private const int ConditionIndex = 2;
    private const int CourseIndex = 3;

    public CsvParseResult ParseFile(string filePath)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(filePath);

        if (!File.Exists(filePath))
        {
            throw new FileNotFoundException($"CSV file not found: {filePath}");
        }

        var results = File.ReadLines(filePath)
            .Skip(1) // Skip header
            .Select((line, index) => (Line: line, Number: index + 2)) // Line numbers start at 2
            .Select(x => ParseLine(x.Line, x.Number))
            .ToList();

        var rounds = results
            .Where(parsedLine => parsedLine.IsSuccess)
            .Select(parsedLine => parsedLine.Round!)
            .ToList();

        var errors = results
            .Where(parsedLine => !parsedLine.IsSuccess)
            .Select(parsedLine => parsedLine.Error!)
            .ToList();

        return new CsvParseResult(rounds, errors);
    }

    private ParsedLine ParseLine(string line, int lineNumber)
    {
        if (string.IsNullOrWhiteSpace(line))
        {
            return ParsedLine.Failure($"Line {lineNumber}: Empty or whitespace line");
        }

        var parts = line.Split(',', StringSplitOptions.TrimEntries);
        if (parts.Length < ExpectedFieldCount)
        {
            return ParsedLine.Failure(
                $"Line {lineNumber}: Expected {ExpectedFieldCount} fields, found {parts.Length}");
        }

        var dateStr = parts[DateIndex];
        if (!ElapsedDaysParser.TryParse(dateStr, referenceDate, out var daysSinceReference))
        {
            return ParsedLine.Failure($"Line {lineNumber}: Invalid date format");
        }

        if (!int.TryParse(
            parts[ShotsIndex],
            NumberStyles.Integer,
            CultureInfo.InvariantCulture,
            out var shots))
        {
            return ParsedLine.Failure($"Line {lineNumber}: Invalid shots value");
        }

        if (!double.TryParse(
            parts[ConditionIndex],
            NumberStyles.Number,
            CultureInfo.InvariantCulture,
            out var courseCondition))
        {
            return ParsedLine.Failure($"Line {lineNumber}: Invalid course condition value");
        }

        if (!double.TryParse(
            parts[CourseIndex],
            NumberStyles.Number,
            CultureInfo.InvariantCulture,
            out var courseMultiplier))
        {
            return ParsedLine.Failure($"Line {lineNumber}: Invalid course multiplier value");
        }

        return ParsedLine.Success(new GolfRound(daysSinceReference, shots, courseCondition, courseMultiplier));
    }
}
