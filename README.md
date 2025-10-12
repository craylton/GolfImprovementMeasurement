# Golf Improvement Measurement

A tiny .NET console app that looks at your past golf rounds and spots simple patterns:

- Are you getting better over time?
- How much do tough conditions (wind, mud, etc.) affect your score?
- Which course tends to play harder for you?

It reads your rounds from a CSV, runs a simple statistical fit, and prints easy-to-read results.


## What you’ll see
The app prints:
- Your overall model (how time, conditions, and course relate to your score)
- An R² value (how well the model explains your scores)
- A quick prediction example using one of your rounds (predicted vs actual)

This gives you a sense of trend (improvement over time), penalty for rough conditions, and how much harder one course feels than another.


## Data format (CSV)
Put your data in `GolfImprovementMeasurement/Data/golf_data.csv` (or change the path in `AppConfiguration`). The CSV should have a header row and the columns below, in this order:

1) Date
2) Score
3) Condition
4) Course

Example:

```
Date,Score,Condition,Course
21 May 2023,114,1,1
04/06/2023,110,2,1
2023-07-15,108,1,2
```

Notes:
- Dates are flexible. The app understands things like `21 May 2023`, `04/06/2023`, or `2023-07-15`.
- Condition is a number. You can use `1` for normal and `2` for adverse, or use your own scale (e.g., `1.0` normal, `1.3` very tough).
- Course is a number. You can use `1`, `2`, etc., or assign a difficulty-like multiplier if you prefer (e.g., `1.0` easier course, `1.1` slightly harder). The app will treat whatever you use as a numeric factor.

Tip: Keep your scale consistent over time. For example, if you start using `1.2` for a windy day, use that same meaning in future entries.


## How to run
Prerequisite: .NET 9 SDK installed.

- Put your CSV at `GolfImprovementMeasurement/Data/golf_data.csv` (or update `AppConfiguration`).
- From the repo root, run the project:
  - `dotnet run --project GolfImprovementMeasurement`
- The app will print the analysis to the console.


## Customizing
- Configuration lives in `GolfImprovementMeasurement/AppConfiguration.cs`:
  - `ReferenceDate`: the baseline date used to calculate “days since” (used for the improvement-over-time signal)
  - `DataPath`: where your CSV is
- The main entry point is `GolfImprovementMeasurement/Program.cs`.
- The analysis is done by `CourseAnalyzer` and `MultipleLinearRegression`.
- Date handling is in `Parsers/DateParser.cs`, CSV reading is in `Parsers/CsvParser.cs`.

Right now the app runs one combined analysis. If you want separate results per course, you can split/filter your data and call `AnalyzeCourse` for each subset.


## What the output means (quick read)
- Coefficients: Show how each factor (time, condition, course) nudges your score up/down on average.
- R²: 0 to 1. Higher means the model explains more of the ups and downs in your scores.
- Example prediction: A sanity check against a real round from your data.

Remember, this is a simple model. It’s great for quick insights, not a full-on performance lab.


## Why this can help
- Encouragement: See if you’re trending in the right direction.
- Planning: Notice how much the weather or a specific course adds to your score.
- Focus: Decide where practice could have the biggest payoff.

Have fun and good luck lowering those scores!


## For my own future reference
Running this app for my own recorded results tells me:
- My game improves by about 0.01 strokes per day (about 4 strokes per year).
- Playing in adverse conditions adds about 3.2 strokes to my score.
- Playing at Peacehaven adds about 11.0 strokes compared to Burgess Hill.