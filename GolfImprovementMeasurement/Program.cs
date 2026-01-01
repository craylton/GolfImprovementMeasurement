using GolfImprovementMeasurement;
using GolfImprovementMeasurement.Parsers;

var configuration = AppConfiguration.Default;

Console.WriteLine(new string('=', 80));
Console.WriteLine("MULTIPLE LINEAR REGRESSION ANALYSIS");
Console.WriteLine(new string('=', 80));

// Parse data
var parser = new CsvParser(configuration.ReferenceDate);
var parseResult = parser.ParseFile(configuration.DataPath);

if (parseResult.Errors.Count > 0)
{
    Console.Error.WriteLine($"Warning: Skipped {parseResult.Errors.Count} invalid row(s).");
    foreach (var error in parseResult.Errors)
    {
        Console.Error.WriteLine($"  {error}");
    }
}

if (parseResult.Rounds.Count == 0)
{
    Console.Error.WriteLine("Error: No valid data found in CSV file.");
    return 1;
}

// Fit regression plane
var regressionService = new MultipleLinearRegression();
var result = regressionService.FitPlane(parseResult.Rounds);
Console.WriteLine();
Console.WriteLine(result);

// Calculate R^2 (goodness of fit)
var rSquared = result.CalculateRSquared(parseResult.Rounds);
Console.WriteLine();
Console.WriteLine($"  R^2 (goodness of fit) = {rSquared:F4}");

// Predict sample using the most recent round
var sampleRound = parseResult.Rounds[^1];
var predicted = result.Predict(
    sampleRound.DaysSinceReference,
    sampleRound.CourseCondition,
    sampleRound.CourseMultiplier);

Console.WriteLine();
Console.WriteLine($"  Example: For day {sampleRound.DaysSinceReference}, condition {sampleRound.CourseCondition}, course {sampleRound.CourseMultiplier}");
Console.WriteLine($"  Predicted shots: {predicted:F2}");
Console.WriteLine($"  Actual shots: {sampleRound.NumberOfShots}");

return 0;
