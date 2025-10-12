using GolfImprovementMeasurement.Models;
using MathNet.Numerics.LinearAlgebra;

namespace GolfImprovementMeasurement.Services;

public class MultipleLinearRegressionService
{
    /// <summary>
    /// Performs multiple linear regression on golf rounds to find the best-fit plane.
    /// z = ?? + ??x + ??y
    /// where x = DaysSinceReference, y = CourseCondition, z = NumberOfShots
    /// </summary>
    public RegressionResult FitPlane(List<GolfRound> rounds)
    {
        if (rounds == null || rounds.Count < 3)
        {
            throw new ArgumentException("At least 3 data points are required for multiple linear regression.", nameof(rounds));
        }

        int n = rounds.Count;

        // Create design matrix X (n × 3)
        // Column 0: all ones (for intercept ??)
        // Column 1: x values (DaysSinceReference)
        // Column 2: y values (CourseCondition)
        var designMatrix = Matrix<double>.Build.Dense(n, 3);
        
        // Create response vector Y (n × 1)
        var responseVector = Vector<double>.Build.Dense(n);

        for (int i = 0; i < n; i++)
        {
            designMatrix[i, 0] = 1.0; // Intercept column
            designMatrix[i, 1] = rounds[i].DaysSinceReference;
            designMatrix[i, 2] = (double)rounds[i].CourseCondition;
            responseVector[i] = rounds[i].NumberOfShots;
        }

        // Apply the Normal Equation: ?? = (X? X)?¹ X? Y
        var xTranspose = designMatrix.Transpose();
        var xTx = xTranspose.Multiply(designMatrix);
        var xTxInverse = xTx.Inverse();
        var xTy = xTranspose.Multiply(responseVector);
        var coefficients = xTxInverse.Multiply(xTy);

        return new RegressionResult
        {
            Beta0 = coefficients[0], // Intercept
            Beta1 = coefficients[1], // Coefficient for DaysSinceReference
            Beta2 = coefficients[2], // Coefficient for CourseCondition
            DataPointCount = n
        };
    }

    /// <summary>
    /// Predicts the number of shots based on the regression coefficients.
    /// </summary>
    public double Predict(RegressionResult result, int daysSinceReference, decimal courseCondition)
    {
        return result.Beta0 + 
               result.Beta1 * daysSinceReference + 
               result.Beta2 * (double)courseCondition;
    }

    /// <summary>
    /// Calculates the R² (coefficient of determination) for the regression model.
    /// R² indicates how well the model fits the data (0 to 1, where 1 is perfect fit).
    /// </summary>
    public double CalculateRSquared(List<GolfRound> rounds, RegressionResult result)
    {
        if (rounds == null || rounds.Count == 0)
        {
            return 0;
        }

        // Calculate mean of observed values
        double meanObserved = rounds.Average(r => r.NumberOfShots);

        // Calculate total sum of squares (SS_tot) and residual sum of squares (SS_res)
        double ssTot = 0;
        double ssRes = 0;

        foreach (var round in rounds)
        {
            double observed = round.NumberOfShots;
            double predicted = Predict(result, round.DaysSinceReference, round.CourseCondition);
            
            ssTot += Math.Pow(observed - meanObserved, 2);
            ssRes += Math.Pow(observed - predicted, 2);
        }

        // R² = 1 - (SS_res / SS_tot)
        return ssTot > 0 ? 1 - (ssRes / ssTot) : 0;
    }
}

public class RegressionResult
{
    /// <summary>
    /// Intercept (??) - baseline number of shots
    /// </summary>
    public double Beta0 { get; set; }

    /// <summary>
    /// Coefficient for DaysSinceReference (??) - change in shots per day
    /// </summary>
    public double Beta1 { get; set; }

    /// <summary>
    /// Coefficient for CourseCondition (??) - change in shots per unit of course condition
    /// </summary>
    public double Beta2 { get; set; }

    /// <summary>
    /// Number of data points used in the regression
    /// </summary>
    public int DataPointCount { get; set; }

    /// <summary>
    /// Gets the equation as a formatted string
    /// </summary>
    public string GetEquation()
    {
        return $"z = {Beta0:F4} + {Beta1:F6}x + {Beta2:F4}y";
    }

    public override string ToString()
    {
        return $"Regression Result (n={DataPointCount}):\n" +
               $"  ?? (Intercept) = {Beta0:F4}\n" +
               $"  ?? (Days coefficient) = {Beta1:F6}\n" +
               $"  ?? (Condition coefficient) = {Beta2:F4}\n" +
               $"  Equation: {GetEquation()}";
    }
}
