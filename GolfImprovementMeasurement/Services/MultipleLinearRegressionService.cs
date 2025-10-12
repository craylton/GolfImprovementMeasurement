using GolfImprovementMeasurement.Models;
using MathNet.Numerics.LinearAlgebra;

namespace GolfImprovementMeasurement.Services;

public class MultipleLinearRegressionService
{
    private const int MinimumDataPoints = 4;
    private const int NumberOfCoefficients = 4;

    /// <summary>
    /// Performs multiple linear regression on golf rounds to find the best-fit hyperplane.
    /// z = β₀ + β₁x + β₂y + β₃w
    /// where x = DaysSinceReference, y = CourseCondition, w = CourseMultiplier, z = NumberOfShots
    /// </summary>
    public RegressionResult FitPlane(List<GolfRound> rounds)
    {
        ValidateInput(rounds);

        var designMatrix = BuildDesignMatrix(rounds);
        var responseVector = BuildResponseVector(rounds);
        var coefficients = CalculateCoefficients(designMatrix, responseVector);

        return CreateRegressionResult(coefficients, rounds.Count);
    }

    /// <summary>
    /// Predicts the number of shots based on the regression coefficients.
    /// </summary>
    public double Predict(RegressionResult result, int daysSinceReference, decimal courseCondition, decimal courseMultiplier)
    {
        ArgumentNullException.ThrowIfNull(result);

        return result.Beta0 +
               result.Beta1 * daysSinceReference +
               result.Beta2 * (double)courseCondition +
               result.Beta3 * (double)courseMultiplier;
    }

    /// <summary>
    /// Calculates the R² (coefficient of determination) for the regression model.
    /// R² indicates how well the model fits the data (0 to 1, where 1 is perfect fit).
    /// </summary>
    public double CalculateRSquared(List<GolfRound> rounds, RegressionResult result)
    {
        ArgumentNullException.ThrowIfNull(result);

        if (rounds == null || rounds.Count == 0)
        {
            return 0;
        }

        var meanObserved = CalculateMeanShots(rounds);
        var (totalSumOfSquares, residualSumOfSquares) = CalculateSumOfSquares(rounds, result, meanObserved);

        return CalculateRSquaredValue(totalSumOfSquares, residualSumOfSquares);
    }

    private static void ValidateInput(List<GolfRound> rounds)
    {
        if (rounds == null || rounds.Count < MinimumDataPoints)
        {
            throw new ArgumentException(
                $"At least {MinimumDataPoints} data points are required for multiple linear regression.",
                nameof(rounds));
        }
    }

    private static Matrix<double> BuildDesignMatrix(List<GolfRound> rounds)
    {
        var matrix = Matrix<double>.Build.Dense(rounds.Count, NumberOfCoefficients);

        for (int i = 0; i < rounds.Count; i++)
        {
            matrix[i, 0] = 1.0; // Intercept column
            matrix[i, 1] = rounds[i].DaysSinceReference;
            matrix[i, 2] = (double)rounds[i].CourseCondition;
            matrix[i, 3] = (double)rounds[i].CourseMultiplier;
        }

        return matrix;
    }

    private static Vector<double> BuildResponseVector(List<GolfRound> rounds)
    {
        var vector = Vector<double>.Build.Dense(rounds.Count);

        for (int i = 0; i < rounds.Count; i++)
        {
            vector[i] = rounds[i].NumberOfShots;
        }

        return vector;
    }

    private static Vector<double> CalculateCoefficients(Matrix<double> designMatrix, Vector<double> responseVector)
    {
        // Apply the Normal Equation: β̂ = (Xᵀ X)⁻¹ Xᵀ Y
        var xTranspose = designMatrix.Transpose();
        var xTransposeX = xTranspose.Multiply(designMatrix);
        var xTransposeXInverse = xTransposeX.Inverse();
        var xTransposeY = xTranspose.Multiply(responseVector);

        return xTransposeXInverse.Multiply(xTransposeY);
    }

    private static RegressionResult CreateRegressionResult(Vector<double> coefficients, int dataPointCount)
    {
        return new RegressionResult
        {
            Beta0 = coefficients[0],
            Beta1 = coefficients[1],
            Beta2 = coefficients[2],
            Beta3 = coefficients[3],
            DataPointCount = dataPointCount
        };
    }

    private static double CalculateMeanShots(List<GolfRound> rounds)
    {
        return rounds.Average(r => r.NumberOfShots);
    }

    private (double TotalSumOfSquares, double ResidualSumOfSquares) CalculateSumOfSquares(
        List<GolfRound> rounds,
        RegressionResult result,
        double meanObserved)
    {
        double totalSumOfSquares = 0;
        double residualSumOfSquares = 0;

        foreach (var round in rounds)
        {
            var observed = round.NumberOfShots;
            var predicted = Predict(result, round.DaysSinceReference, round.CourseCondition, round.CourseMultiplier);

            totalSumOfSquares += Math.Pow(observed - meanObserved, 2);
            residualSumOfSquares += Math.Pow(observed - predicted, 2);
        }

        return (totalSumOfSquares, residualSumOfSquares);
    }

    private static double CalculateRSquaredValue(double totalSumOfSquares, double residualSumOfSquares)
    {
        return totalSumOfSquares > 0
            ? 1 - (residualSumOfSquares / totalSumOfSquares)
            : 0;
    }
}
