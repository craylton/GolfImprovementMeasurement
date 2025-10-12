using System.Globalization;

namespace GolfImprovementMeasurement.Parsers;

public static class DateParser
{
    private static readonly string[] SupportedFormats =
    [
        "d MMM yyyy",      // 21 May 2023
        "dd MMM yyyy",     // 21 May 2023
        "d MMMM yyyy",     // 21 May 2023
        "dd MMMM yyyy",    // 21 May 2023
        "M/d/yyyy",        // 4/5/2025
        "d/M/yyyy",        // 4/5/2025
        "dd/MM/yyyy",      // 04/05/2025
    ];

    public static DateTime Parse(string dateStr)
    {
        if (DateTime.TryParseExact(dateStr, SupportedFormats, CultureInfo.InvariantCulture,
            DateTimeStyles.AllowWhiteSpaces, out DateTime result))
        {
            return result;
        }

        // Fallback to general parsing
        if (DateTime.TryParse(dateStr, out result))
        {
            return result;
        }

        throw new FormatException($"Unable to parse date: {dateStr}");
    }
}
