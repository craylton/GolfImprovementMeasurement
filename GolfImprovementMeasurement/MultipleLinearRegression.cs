using GolfImprovementMeasurement.Models;
using MathNet.Numerics.LinearAlgebra;

namespace GolfImprovementMeasurement;

internal sealed class MultipleLinearRegression
{
    private const int MinimumDataPoints = 4;
    private const int NumberOfCoefficients = 4;

    // Expose the required data points internally so callers can validate without exceptions
    internal static int RequiredDataPoints => MinimumDataPoints;

    /// <summary>
    /// Performs multiple linear regression on golf rounds to find the best-fit hyperplane.
    /// shots = β₀ + β₁*days + β₂*condition + β₃*course
    /// where days = DaysSinceReference, condition = CourseCondition, course = CourseMultiplier, shots = NumberOfShots
    /// </summary>
    public RegressionResult FitPlane(IReadOnlyList<GolfRound> rounds)
    {
        ValidateInput(rounds);

        var designMatrix = BuildDesignMatrix(rounds);
        var responseVector = BuildResponseVector(rounds);

        // Solve least squares using QR decomposition for numerical stability
        var decomposition = designMatrix.QR();
        var coefficients = decomposition.Solve(responseVector);

        return CreateRegressionResult(coefficients, rounds.Count);
    }

    private static void ValidateInput(IReadOnlyList<GolfRound> rounds)
    {
        if (rounds.Count < MinimumDataPoints)
        {
            throw new ArgumentException(
                $"At least {MinimumDataPoints} data points are required for multiple linear regression.",
                nameof(rounds));
        }
    }

    private static Matrix<double> BuildDesignMatrix(IReadOnlyList<GolfRound> rounds)
    {
        var matrix = Matrix<double>.Build.Dense(rounds.Count, NumberOfCoefficients);

        for (int i = 0; i < rounds.Count; i++)
        {
            matrix[i, 0] = 1.0; // Intercept
            matrix[i, 1] = rounds[i].DaysSinceReference;
            matrix[i, 2] = (double)rounds[i].CourseCondition;
            matrix[i, 3] = (double)rounds[i].CourseMultiplier;
        }

        return matrix;
    }

    private static Vector<double> BuildResponseVector(IReadOnlyList<GolfRound> rounds)
    {
        var vector = Vector<double>.Build.Dense(rounds.Count);

        for (int i = 0; i < rounds.Count; i++)
        {
            vector[i] = rounds[i].NumberOfShots;
        }

        return vector;
    }

    private static RegressionResult CreateRegressionResult(
        Vector<double> coefficients,
        int dataPointCount) => new(
            coefficients[0],
            coefficients[1],
            coefficients[2],
            coefficients[3],
            dataPointCount);
}
