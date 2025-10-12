using GolfImprovementMeasurement.Models;

namespace GolfImprovementMeasurement.Services;

/// <summary>
/// Interface for displaying golf data
/// </summary>
public interface IGolfDataDisplayer
{
    /// <summary>
    /// Displays golf round data for a player
    /// </summary>
    void Display(string playerName, IEnumerable<GolfRound> rounds);
}
