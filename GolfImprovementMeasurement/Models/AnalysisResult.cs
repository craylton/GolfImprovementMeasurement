namespace GolfImprovementMeasurement.Models;

internal sealed record AnalysisResult(
    RegressionResult Regression,
    double RSquared,
    SamplePrediction Sample);
