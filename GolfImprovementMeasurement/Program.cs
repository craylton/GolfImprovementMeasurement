using GolfImprovementMeasurement;
using GolfImprovementMeasurement.Parsers;

// Configure application
var configuration = AppConfiguration.Default;

// Create services
var parser = new CsvParser(configuration.ReferenceDate);
var regressionService = new MultipleLinearRegression();
var analyzer = new RegressionAnalyzer(regressionService);

// Run analysis
var rounds = parser.ParseFile(configuration.DataPath);

Console.WriteLine(new string('=', 80));
Console.WriteLine("MULTIPLE LINEAR REGRESSION ANALYSIS");
Console.WriteLine(new string('=', 80));

var analysis = analyzer.Analyze(rounds);

// Present results
Console.WriteLine(analysis.Regression);
Console.WriteLine($"  R^2 (goodness of fit) = {analysis.RSquared:F4}");
Console.WriteLine();
Console.WriteLine($"  Example: " +
    $"For day {analysis.Sample.DaysSinceReference}, " +
    $"condition {analysis.Sample.CourseCondition}, " +
    $"course {analysis.Sample.CourseMultiplier}");
Console.WriteLine($"  Predicted shots: {analysis.Sample.PredictedShots:F2}");
Console.WriteLine($"  Actual shots: {analysis.Sample.ActualShots}");
