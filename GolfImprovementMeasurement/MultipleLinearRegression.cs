using GolfImprovementMeasurement.Models;
using MathNet.Numerics.LinearAlgebra;

namespace GolfImprovementMeasurement;

internal sealed class MultipleLinearRegression
{
    private const int MinimumDataPoints = 4;
    private const int NumberOfCoefficients = 4;

    private const int InterceptIndex = 0;
    private const int DaysIndex = 1;
    private const int ConditionIndex = 2;
    private const int CourseIndex = 3;

    /// <summary>
    /// Performs multiple linear regression on golf rounds to find the best-fit hyperplane.
    /// NumberOfShots = β₀ + β₁*DaysSinceReference + β₂*CourseCondition + β₃*CourseMultiplier
    /// </summary>
    public RegressionResult FitPlane(IReadOnlyList<GolfRound> rounds)
    {
        ValidateInput(rounds);

        var designMatrix = BuildDesignMatrix(rounds);
        var responseVector = BuildResponseVector(rounds);

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
            matrix[i, InterceptIndex] = 1.0;
            matrix[i, DaysIndex] = rounds[i].DaysSinceReference;
            matrix[i, ConditionIndex] = rounds[i].CourseCondition;
            matrix[i, CourseIndex] = rounds[i].CourseMultiplier;
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
            coefficients[InterceptIndex],
            coefficients[DaysIndex],
            coefficients[ConditionIndex],
            coefficients[CourseIndex],
            dataPointCount);
}
