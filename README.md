# Golf Improvement Measurement

A small .NET console app that helps track how golf scores change over time. It fits a simple multiple linear regression to your rounds, so you can see the overall trend while accounting for playing conditions and the course you played.

What it does
- Reads your rounds from a CSV file
- Fits a regression model using:
  - Time (days since a reference date)
  - Condition multiplier (e.g., 1 = normal, 2 = adverse)
  - Course multiplier (e.g., 1 = Course A, 2 = Course B)
- Prints the regression equation, the R² (goodness of fit), and an example prediction based on your most recent round

Getting started
1) Install .NET 9 SDK
2) Put your data in `GolfImprovementMeasurement/Data/golf_data.csv`
   - The file already contains example data you can replace or extend
3) Run the app from the repository root or the project folder:
   - From repo root: `dotnet run --project GolfImprovementMeasurement`
   - From project folder: `dotnet run`

Data format
The CSV must have a header row and four columns in this order:

```
date,numberOfShots,conditionMultiplier,courseMultiplier
```

- `date`: The date of the round. Common formats are supported (e.g., `YYYY-MM-DD`, `21 May 2023`, `4/5/2025`, `dd/MM/yyyy`).
- `numberOfShots`: Total strokes for the round (integer).
- `conditionMultiplier`: Numeric indicator for conditions. Typical values:
  - 1 = Normal conditions
  - 2 = Adverse conditions (e.g., bad weather)
- `courseMultiplier`: Numeric indicator for course. Keep the values consistent (e.g., 1 = Burgess Hill, 2 = Peacehaven). You can use other numbers if you track more courses.

Configuration
- Reference date: The model uses “days since reference” as the time variable. You can change the reference date in `GolfImprovementMeasurement/AppConfiguration.cs` (default is 21 May 2023).
- Data path: Also configured in `AppConfiguration.cs` (defaults to `Data/golf_data.csv`). The file is copied to the output on build, so the default path works when running from the project folder.

Output
When the app runs, it prints:
- The regression coefficients and a readable equation
- R² (how well the model fits your data)
- A sample prediction using the most recent round’s inputs

Notes
- A minimum of 4 rounds is required for the analysis.
- This is a simple linear model. Treat the results as a guide to overall trend rather than a definitive performance rating.

Project
- Target framework: .NET 9
- Package used: MathNet.Numerics

## For my own future reference
Running this app for my own recorded results tells me:
- My game improves by about 0.01 strokes per day (about 3.6 strokes per year).
- Playing in adverse conditions adds about 3.2 strokes to my score.
- Playing at Peacehaven adds about 11.0 strokes compared to Burgess Hill.