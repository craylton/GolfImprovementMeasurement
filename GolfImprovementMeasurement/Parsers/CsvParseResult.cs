using GolfImprovementMeasurement.Models;

namespace GolfImprovementMeasurement.Parsers;

internal sealed record CsvParseResult(
    IReadOnlyList<GolfRound> Rounds,
    IReadOnlyList<string> Errors
);
