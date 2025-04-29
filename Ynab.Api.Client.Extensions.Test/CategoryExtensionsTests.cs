namespace Ynab.Api.Client.Extensions.Test;

public class CategoryExtensionsTests
{
    [Theory]
    [InlineData(null, null, 0L, null, null, 0L)]              // Target: None
    [InlineData(1, 1, 1000000L, 1, 1000000L, 1000000L)]       // Target: Monthly/$1000/Last Day of Month/Set aside (or refill)
    [InlineData(2, 1, 1000000L, 1, 4000000L, 4000000L)]       // Target: Weekly/$1000/Saturday/Set aside
    [InlineData(13, 1, 1000000L, 2, 1000000L, 83333L)]        // Target: Yearly/$1000/April-to-May/Set aside
    [InlineData(13, 1, 1000000L, 4, 1000000L, 83333L)]        // Target: Custom/$1000/Set aside/April-to-July/Repeat/1/Year
    [InlineData(14, 2, 1000000L, 4, 1000000L, 41666L)]        // Target: Custom/$1000/Set aside/April-to-July/Repeat/2/Year
    [InlineData(0, null, 1000000L, 4, 1000000L, 250000L)]     // Target: Custom/$1000/Set aside/April-to-July/No repeat
    [InlineData(null, null, 1000000L, 0, 1000000L, 0L)]       // Target: Custom/$1000/Have a balance/No due date
    [InlineData(null, null, 1000000L, 8, 1000000L, 125000L)]  // Target: Custom/$1000/Have a balance/April-to-November
    public void TestMonthlyNeed(int? cadence, int? frequency, long? target, int? months, long? left, long expected)
    {
        // Arrange.
        var category = new Category
        {
            Goal_cadence = cadence,
            Goal_cadence_frequency = frequency,
            Goal_target = target,
            Goal_overall_left = left,
            Goal_months_to_budget = months
        };

        // Act.
        var actual = category.MonthlyNeed();

        // Assert.
        Assert.Equal(expected, actual);
    }
}