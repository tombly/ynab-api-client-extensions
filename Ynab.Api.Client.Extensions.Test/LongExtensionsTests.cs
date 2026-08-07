namespace Ynab.Api.Client.Extensions.Test;

public class LongExtensionsTests
{
    [Theory]
    [InlineData(1234L, 2, 1.23)]
    [InlineData(-1234L, 2, -1.23)]
    [InlineData(1235L, 2, 1.24)]
    [InlineData(1000L, 2, 1.00)]
    [InlineData(1500L, 0, 2)]
    [InlineData(1234L, 0, 1)]
    [InlineData(1234L, 3, 1.234)]
    [InlineData(1234L, 4, 1.234)]
    [InlineData(0L, 2, 0)]
    public void FromMilliunits_RoundsToCurrencyDecimalDigits(long milliunits, int currencyDecimalDigits, decimal expected)
    {
        // Act.
        var actual = milliunits.FromMilliunits(currencyDecimalDigits);

        // Assert.
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void FromMilliunits_DefaultsToTwoDecimalDigits()
    {
        // Act.
        var actual = 1234L.FromMilliunits();

        // Assert.
        Assert.Equal(1.23m, actual);
    }
}
