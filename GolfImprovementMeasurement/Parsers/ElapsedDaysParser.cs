using System.Globalization;

namespace GolfImprovementMeasurement.Parsers;

internal static class ElapsedDaysParser
{
    private static readonly string[] SupportedFormats =
    [
        "yyyy-MM-dd",     // 2025-05-04 (ISO 8601)
        "d MMM yyyy",      // 21 May 2023
        "dd MMM yyyy",     // 21 May 2023
        "d MMMM yyyy",     // 21 May 2023
        "dd MMMM yyyy",    // 21 May 2023
        "M/d/yyyy",        // 4/5/2025 or 04/05/2025
        "d/M/yyyy",        // 4/5/2025 or 04/05/2025
        "dd/MM/yyyy",      // 04/05/2025
    ];

    public static bool TryParse(string? dateStr, DateTime referenceDate, out int result)
    {
        result = default;
        if (string.IsNullOrWhiteSpace(dateStr))
        {
            return false;
        }

        if (!DateTime.TryParseExact(
            dateStr,
            SupportedFormats,
            CultureInfo.InvariantCulture,
            DateTimeStyles.AllowWhiteSpaces,
            out var dateTime))
        {
            return false;
        }

        result = (int)(dateTime.Date - referenceDate.Date).TotalDays;
        return true;
    }
}
