using GolfImprovementMeasurement;
using GolfImprovementMeasurement.Analysis;
using GolfImprovementMeasurement.Configuration;
using GolfImprovementMeasurement.Parsers;
using GolfImprovementMeasurement.Services;

// Configure application
var configuration = AppConfiguration.Default;

// Create services
var parser = new CsvParser(configuration.ReferenceDate);
var regressionService = new MultipleLinearRegressionService();
var displayer = new GolfDataDisplayer();
var analyzer = new CourseAnalyzer(regressionService, displayer);

// Create and run application
var application = new GolfAnalysisApplication(configuration, parser, analyzer);
application.Run();
