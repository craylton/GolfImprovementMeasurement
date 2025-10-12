using GolfImprovementMeasurement.Parsers;
using GolfImprovementMeasurement.Services;

// Reference date for calculating days since
var referenceDate = new DateTime(2023, 5, 21);

// Create parser
var parser = new CsvParser(referenceDate);

// Parse CSV files
var bhillData = parser.ParseFile("data/bhill.csv");
var phavenData = parser.ParseFile("data/phaven.csv");

// Display results
GolfDataDisplayer.Display("Bhill", bhillData);
GolfDataDisplayer.Display("Phaven", phavenData);

// Perform multiple linear regression
var regressionService = new MultipleLinearRegressionService();

Console.WriteLine("\n" + new string('=', 80));
Console.WriteLine("MULTIPLE LINEAR REGRESSION ANALYSIS");
Console.WriteLine(new string('=', 80));

// Analyze Bhill data
if (bhillData.Count >= 3)
{
    Console.WriteLine("\n--- Bhill Regression ---");
    var bhillResult = regressionService.FitPlane(bhillData);
    Console.WriteLine(bhillResult);
    
    var bhillRSquared = regressionService.CalculateRSquared(bhillData, bhillResult);
    Console.WriteLine($"  R² (goodness of fit) = {bhillRSquared:F4}");
    
    // Example prediction
    if (bhillData.Count > 0)
    {
        var sampleRound = bhillData[0];
        var predicted = regressionService.Predict(bhillResult, sampleRound.DaysSinceReference, sampleRound.CourseCondition);
        Console.WriteLine($"\n  Example: For day {sampleRound.DaysSinceReference}, condition {sampleRound.CourseCondition}");
        Console.WriteLine($"    Predicted shots: {predicted:F2}");
        Console.WriteLine($"    Actual shots: {sampleRound.NumberOfShots}");
    }
}

// Analyze Phaven data
if (phavenData.Count >= 3)
{
    Console.WriteLine("\n--- Phaven Regression ---");
    var phavenResult = regressionService.FitPlane(phavenData);
    Console.WriteLine(phavenResult);
    
    var phavenRSquared = regressionService.CalculateRSquared(phavenData, phavenResult);
    Console.WriteLine($"  R² (goodness of fit) = {phavenRSquared:F4}");
    
    // Example prediction
    if (phavenData.Count > 0)
    {
        var sampleRound = phavenData[0];
        var predicted = regressionService.Predict(phavenResult, sampleRound.DaysSinceReference, sampleRound.CourseCondition);
        Console.WriteLine($"\n  Example: For day {sampleRound.DaysSinceReference}, condition {sampleRound.CourseCondition}");
        Console.WriteLine($"    Predicted shots: {predicted:F2}");
        Console.WriteLine($"    Actual shots: {sampleRound.NumberOfShots}");
    }
}

Console.WriteLine("\n" + new string('=', 80));
