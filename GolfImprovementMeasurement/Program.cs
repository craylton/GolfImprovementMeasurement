using GolfImprovementMeasurement;
using GolfImprovementMeasurement.Parsers;

// Configure application
var configuration = AppConfiguration.Default;

// Create services
var parser = new CsvParser(configuration.ReferenceDate);
var regressionService = new MultipleLinearRegression();
var analyzer = new RegressionAnalyzer(regressionService);

// Create and run application
var application = new GolfAnalysisApplication(configuration, parser, analyzer);
application.Run();
