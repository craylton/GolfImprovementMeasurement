using GolfImprovementMeasurement;
using GolfImprovementMeasurement.Parsers;

var configuration = AppConfiguration.Default;

Console.WriteLine(new string('=', 80));
Console.WriteLine("MULTIPLE LINEAR REGRESSION ANALYSIS");
Console.WriteLine(new string('=', 80));

// Parse data
var parser = new CsvParser(configuration.ReferenceDate);
var rounds = parser.ParseFile(configuration.DataPath);

// Fit regression plane
var regressionService = new MultipleLinearRegression();
var result = regressionService.FitPlane(rounds);
Console.WriteLine();
Console.WriteLine(result);

// Calculate R^2 (goodness of fit)
var rSquared = result.CalculateRSquared(rounds);
Console.WriteLine();
Console.WriteLine($"  R^2 (goodness of fit) = {rSquared:F4}");

// Predict sample using the most recent round
var sampleRound = rounds[^1];
var predicted = result.Predict(
    sampleRound.DaysSinceReference,
    sampleRound.CourseCondition,
    sampleRound.CourseMultiplier);

Console.WriteLine();
Console.WriteLine($"  Example: " +
    $"For day {sampleRound.DaysSinceReference}, " +
    $"condition {sampleRound.CourseCondition}, " +
    $"course {sampleRound.CourseMultiplier}");
Console.WriteLine($"  Predicted shots: {predicted:F2}");
Console.WriteLine($"  Actual shots: {sampleRound.NumberOfShots}");
