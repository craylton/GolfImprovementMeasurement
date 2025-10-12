using GolfImprovementMeasurement.Models;

namespace GolfImprovementMeasurement.Parsers;

/// <summary>
/// Interface for parsing golf round data from files
/// </summary>
public interface IGolfDataParser
{
    /// <summary>
    /// Parses a file and returns a list of golf rounds
    /// </summary>
    List<GolfRound> ParseFile(string filePath);
}
