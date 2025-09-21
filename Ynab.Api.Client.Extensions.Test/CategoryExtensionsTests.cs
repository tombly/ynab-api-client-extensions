namespace Ynab.Api.Client.Extensions.Test;

public class CategoryExtensionsTests
{
    [Fact]
    public void MonthlyNeed_NoTarget()
    {
        // Arrange.
        var category = new Category
        {
            Goal_cadence = null,
            Goal_cadence_frequency = null,
            Goal_target = 0,
            Goal_months_to_budget = null,
            Goal_overall_left = null
        };

        // Act.
        var actual = category.MonthlyNeed();

        // Assert.
        Assert.Equal(0L, actual);
    }

    [Fact]
    public void MonthlyNeed_Weekly_SetAside()
    {
        // Arrange.
        var category = new Category
        {
            Goal_cadence = 2,
            Goal_cadence_frequency = 1,
            Goal_target = 125000,
            Goal_months_to_budget = 1,
            Goal_overall_left = 125000
        };

        // Act.
        var actual = category.MonthlyNeed();

        // Assert.
        Assert.Equal(500000L, actual);
    }

    [Fact]
    public void MonthlyNeed_Monthly_SetAside()
    {
        // Arrange.
        var category = new Category
        {
            Goal_cadence = 1,
            Goal_cadence_frequency = 1,
            Goal_target = 480000,
            Goal_months_to_budget = 1,
            Goal_overall_left = 480000
        };

        // Act.
        var actual = category.MonthlyNeed();

        // Assert.
        Assert.Equal(480000L, actual);
    }

    [Fact]
    public void MonthlyNeed_Yearly_SetAside()
    {
        // Arrange.
        var category = new Category
        {
            Goal_cadence = 13,
            Goal_cadence_frequency = 1,
            Goal_target = 1250000,
            Goal_months_to_budget = 2,
            Goal_overall_left = 1250000
        };

        // Act.
        var actual = category.MonthlyNeed();

        // Assert.
        Assert.Equal(104166L, actual);
    }

    [Fact]
    public void MonthlyNeed_Weekly_RefillUpTo()
    {
        // Arrange.
        var category = new Category
        {
            Goal_cadence = 2,
            Goal_cadence_frequency = 1,
            Goal_target = 380000,
            Goal_months_to_budget = 1,
            Goal_overall_left = 380000
        };

        // Act.
        var actual = category.MonthlyNeed();

        // Assert.
        Assert.Equal(1520000L, actual);
    }

    [Fact]
    public void MonthlyNeed_Monthly_RefillUpTo()
    {
        // Arrange.
        var category = new Category
        {
            Goal_cadence = 1,
            Goal_cadence_frequency = 1,
            Goal_target = 900000,
            Goal_months_to_budget = 1,
            Goal_overall_left = 900000
        };

        // Act.
        var actual = category.MonthlyNeed();

        // Assert.
        Assert.Equal(900000L, actual);
    }

    [Fact]
    public void MonthlyNeed_Yearly_RefillUpTo()
    {
        // Arrange.
        var category = new Category
        {
            Goal_cadence = 13,
            Goal_cadence_frequency = 1,
            Goal_target = 1800000,
            Goal_months_to_budget = 2,
            Goal_overall_left = 1800000
        };

        // Act.
        var actual = category.MonthlyNeed();

        // Assert.
        Assert.Equal(150000L, actual);
    }

    [Fact]
    public void MonthlyNeed_Custom_SetAside_NoRepeat()
    {
        // Arrange.
        var category = new Category
        {
            Goal_cadence = 0,
            Goal_cadence_frequency = null,
            Goal_target = 675000,
            Goal_months_to_budget = 4,
            Goal_overall_left = 675000
        };

        // Act.
        var actual = category.MonthlyNeed();

        // Assert.
        Assert.Equal(168750L, actual);
    }

    [Fact]
    public void MonthlyNeed_Custom_SetAside_Repeat_1_Month()
    {
        // Arrange.
        var category = new Category
        {
            Goal_cadence = 1,
            Goal_cadence_frequency = 1,
            Goal_target = 675000,
            Goal_months_to_budget = 3,
            Goal_overall_left = 675000
        };

        // Act.
        var actual = category.MonthlyNeed();

        // Assert.
        Assert.Equal(675000L, actual);
    }

    [Fact]
    public void MonthlyNeed_Custom_SetAside_Repeat_3_Month()
    {
        // Arrange.
        var category = new Category
        {
            Goal_cadence = 1,
            Goal_cadence_frequency = 3,
            Goal_target = 675000,
            Goal_months_to_budget = 3,
            Goal_overall_left = 675000
        };

        // Act.
        var actual = category.MonthlyNeed();

        // Assert.
        Assert.Equal(225000L, actual);
    }

    [Fact]
    public void MonthlyNeed_Custom_SetAside_Repeat_2_Year()
    {
        // Arrange.
        var category = new Category
        {
            Goal_cadence = 13,
            Goal_cadence_frequency = 2,
            Goal_target = 675000,
            Goal_months_to_budget = 3,
            Goal_overall_left = 675000
        };

        // Act.
        var actual = category.MonthlyNeed();

        // Assert.
        Assert.Equal(28125L, actual);
    }

    [Fact]
    public void MonthlyNeed_Custom_RefillUpTo_NoRepeat()
    {
        // Arrange.
        var category = new Category
        {
            Goal_cadence = 13,
            Goal_cadence_frequency = 2,
            Goal_target = 675000,
            Goal_months_to_budget = 3,
            Goal_overall_left = 675000
        };

        // Act.
        var actual = category.MonthlyNeed();

        // Assert.
        Assert.Equal(28125L, actual);
    }

    [Fact]
    public void MonthlyNeed_Custom_RefillUpTo_Repeat_1_Month()
    {
        // Arrange.
        var category = new Category
        {
            Goal_cadence = 1,
            Goal_cadence_frequency = 1,
            Goal_target = 575000,
            Goal_months_to_budget = 3,
            Goal_overall_left = 575000
        };

        // Act.
        var actual = category.MonthlyNeed();

        // Assert.
        Assert.Equal(575000L, actual);
    }

    [Fact]
    public void MonthlyNeed_Custom_RefillUpTo_Repeat_3_Month()
    {
        // Arrange.
        var category = new Category
        {
            Goal_cadence = 1,
            Goal_cadence_frequency = 3,
            Goal_target = 575000,
            Goal_months_to_budget = 3,
            Goal_overall_left = 575000
        };

        // Act.
        var actual = category.MonthlyNeed();

        // Assert.
        Assert.Equal(191666L, actual);
    }

    [Fact]
    public void MonthlyNeed_Custom_RefillUpTo_Repeat_2_Year()
    {
        // Arrange.
        var category = new Category
        {
            Goal_cadence = 13,
            Goal_cadence_frequency = 2,
            Goal_target = 575000,
            Goal_months_to_budget = 3,
            Goal_overall_left = 575000
        };

        // Act.
        var actual = category.MonthlyNeed();

        // Assert.
        Assert.Equal(23958L, actual);
    }

    [Fact]
    public void MonthlyNeed_Custom_HaveABalance_NoDueDate()
    {
        // Arrange.
        var category = new Category
        {
            Goal_cadence = null,
            Goal_cadence_frequency = null,
            Goal_target = 1200000,
            Goal_percentage_complete = 0,
            Goal_overall_left = 1200000
        };

        // Act.
        var actual = category.MonthlyNeed();

        // Assert.
        Assert.Equal(0L, actual);
    }

    [Fact]
    public void MonthlyNeed_Custom_HaveABalance_DueDate()
    {
        // Arrange.
        var category = new Category
        {
            Goal_cadence = null,
            Goal_cadence_frequency = null,
            Goal_target = 1345000,
            Goal_percentage_complete = 0,
            Goal_overall_left = 1345000
        };

        // Act.
        var actual = category.MonthlyNeed();

        // Assert.
        Assert.Equal(0L, actual);
    }
}