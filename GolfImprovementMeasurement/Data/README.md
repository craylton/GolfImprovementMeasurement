# Golf Data Format

## Overview
This directory contains golf round data used for improvement measurement and analysis.

## Data Structure

The CSV data file contains the following columns:

| Column | Description | Format/Values |
|--------|-------------|-----------------|
| **Date** | The date when the data was recorded | Date format (YYYY-MM-DD) |
| **Score** | The score achieved in the golf game | Numeric value (stroke play) |
| **Condition** | Playing conditions | `1` = Normal conditions<br>`2` = Adverse conditions (e.g., bad weather) |
| **Course** | Golf course identifier | `1` = Burgess Hill<br>`2` = Peacehaven |

## Course Codes

- **1**: Burgess Hill
- **2**: Peacehaven

## Condition Codes

- **1**: Normal conditions
- **2**: Adverse conditions (bad weather, etc.)