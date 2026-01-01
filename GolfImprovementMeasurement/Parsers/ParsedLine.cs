using GolfImprovementMeasurement.Models;

namespace GolfImprovementMeasurement.Parsers;

internal readonly record struct ParsedLine
{
    public GolfRound? Round { get; init; }
    public string? Error { get; init; }
    public bool IsSuccess => Round is not null;

    public static ParsedLine Success(GolfRound round) => new() { Round = round };
    public static ParsedLine Failure(string error) => new() { Error = error };
}
