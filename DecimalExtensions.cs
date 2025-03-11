namespace Ynab.Api.Client.Extensions;

public static class DecimalExtensions
{
    /// <summary>
    /// Converts a milliunits amount to a currency amount. Implementation
    /// matches the official YNAB JS SDK.
    /// </summary>
    /// <param name="milliunits"></param>
    /// <param name="currencyDecimalDigits"></param>
    /// <returns></returns>
    public static decimal FromMilliunits(this long milliunits, int currencyDecimalDigits = 2)
    {
        var numberToRoundTo = Math.Pow(10, 3 - Math.Min(3, currencyDecimalDigits));
        numberToRoundTo = 1 / numberToRoundTo;
        var rounded = Math.Round(milliunits * numberToRoundTo) / numberToRoundTo;
        var currencyAmount = rounded * (0.1 / Math.Pow(10, 2));
        return (decimal)currencyAmount;
    }
}