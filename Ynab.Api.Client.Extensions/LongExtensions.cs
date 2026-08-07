namespace Ynab.Api.Client.Extensions;

public static class LongExtensions
{
    /// <summary>
    /// Converts a milliunits amount to a currency amount, rounded to the
    /// currency's decimal digits (matching the output of the official YNAB
    /// JS SDK's convertMilliUnitsToCurrencyAmount).
    /// </summary>
    /// <param name="milliunits"></param>
    /// <param name="currencyDecimalDigits"></param>
    /// <returns></returns>
    public static decimal FromMilliunits(this long milliunits, int currencyDecimalDigits = 2)
    {
        var decimalDigits = Math.Min(3, currencyDecimalDigits);
        return Math.Round(milliunits / 1000m, decimalDigits);
    }
}