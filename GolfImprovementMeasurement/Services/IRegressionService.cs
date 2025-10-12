using GolfImprovementMeasurement.Models;

namespace GolfImprovementMeasurement.Services;

/// <summary>
/// Interface for multiple linear regression analysis
/// </summary>
public interface IRegressionService
{
    /// <summary>
    /// Performs multiple linear regression on golf rounds
    /// </summary>
    RegressionResult FitPlane(List<GolfRound> rounds);
    
    /// <summary>
    /// Predicts the number of shots based on regression coefficients
    /// </summary>
    double Predict(RegressionResult result, int daysSinceReference, decimal courseCondition);
    
    /// <summary>
    /// Calculates the R² (coefficient of determination) for the regression model
    /// </summary>
    double CalculateRSquared(List<GolfRound> rounds, RegressionResult result);
}
