using Ynab.Api.Client.Enums;
using Ynab.Api.Client.Models;

namespace Ynab.Api.Client.Extensions.Test;

public class CategoryExtensionsTests
{
    private static Category NewCategory() => new()
    {
        Id = Guid.NewGuid(),
        CategoryGroupId = Guid.NewGuid(),
        Name = "Test Category",
        Hidden = false,
        Internal = false,
        Budgeted = 0,
        Activity = 0,
        Balance = 0,
        Deleted = false
    };

    [Fact]
    public void MonthlyNeed_NoTarget()
    {
        // Arrange.
        var category = NewCategory() with
        {
            GoalCadence = null,
            GoalCadenceFrequency = null,
            GoalTarget = 0,
            GoalMonthsToBudget = null,
            GoalOverallLeft = null
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
        var category = NewCategory() with
        {
            GoalCadence = 2,
            GoalCadenceFrequency = 1,
            GoalTarget = 125000,
            GoalMonthsToBudget = 1,
            GoalOverallLeft = 125000
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
        var category = NewCategory() with
        {
            GoalCadence = 1,
            GoalCadenceFrequency = 1,
            GoalTarget = 480000,
            GoalMonthsToBudget = 1,
            GoalOverallLeft = 480000
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
        var category = NewCategory() with
        {
            GoalCadence = 13,
            GoalCadenceFrequency = 1,
            GoalTarget = 1250000,
            GoalMonthsToBudget = 2,
            GoalOverallLeft = 1250000
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
        var category = NewCategory() with
        {
            GoalCadence = 2,
            GoalCadenceFrequency = 1,
            GoalTarget = 380000,
            GoalMonthsToBudget = 1,
            GoalOverallLeft = 380000
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
        var category = NewCategory() with
        {
            GoalCadence = 1,
            GoalCadenceFrequency = 1,
            GoalTarget = 900000,
            GoalMonthsToBudget = 1,
            GoalOverallLeft = 900000
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
        var category = NewCategory() with
        {
            GoalCadence = 13,
            GoalCadenceFrequency = 1,
            GoalTarget = 1800000,
            GoalMonthsToBudget = 2,
            GoalOverallLeft = 1800000
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
        var category = NewCategory() with
        {
            GoalCadence = 0,
            GoalCadenceFrequency = null,
            GoalTarget = 675000,
            GoalMonthsToBudget = 4,
            GoalOverallLeft = 675000,
            Budgeted = 0
        };

        // Act.
        var actual = category.MonthlyNeed();

        // Assert.
        Assert.Equal(168750L, actual);
    }

    [Fact]
    public void MonthlyNeed_Custom_SetAside_NoRepeat_WithBudgeted()
    {
        // Arrange.
        var category = NewCategory() with
        {
            GoalCadence = 0,
            GoalCadenceFrequency = null,
            GoalTarget = 675000,
            GoalMonthsToBudget = 4,
            GoalOverallLeft = 675000,
            Budgeted = 125000
        };

        // Act.
        var actual = category.MonthlyNeed();

        // Assert.
        Assert.Equal(200000L, actual);
    }

    [Fact]
    public void MonthlyNeed_Custom_SetAside_Repeat_1_Month()
    {
        // Arrange.
        var category = NewCategory() with
        {
            GoalCadence = 1,
            GoalCadenceFrequency = 1,
            GoalTarget = 675000,
            GoalMonthsToBudget = 3,
            GoalOverallLeft = 675000
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
        var category = NewCategory() with
        {
            GoalCadence = 1,
            GoalCadenceFrequency = 3,
            GoalTarget = 675000,
            GoalMonthsToBudget = 3,
            GoalOverallLeft = 675000
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
        var category = NewCategory() with
        {
            GoalCadence = 13,
            GoalCadenceFrequency = 2,
            GoalTarget = 675000,
            GoalMonthsToBudget = 3,
            GoalOverallLeft = 675000
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
        var category = NewCategory() with
        {
            GoalCadence = 13,
            GoalCadenceFrequency = 2,
            GoalTarget = 675000,
            GoalMonthsToBudget = 3,
            GoalOverallLeft = 675000
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
        var category = NewCategory() with
        {
            GoalCadence = 1,
            GoalCadenceFrequency = 1,
            GoalTarget = 575000,
            GoalMonthsToBudget = 3,
            GoalOverallLeft = 575000
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
        var category = NewCategory() with
        {
            GoalCadence = 1,
            GoalCadenceFrequency = 3,
            GoalTarget = 575000,
            GoalMonthsToBudget = 3,
            GoalOverallLeft = 575000
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
        var category = NewCategory() with
        {
            GoalCadence = 13,
            GoalCadenceFrequency = 2,
            GoalTarget = 575000,
            GoalMonthsToBudget = 3,
            GoalOverallLeft = 575000
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
        var category = NewCategory() with
        {
            GoalType = CategoryGoalType.TB,
            GoalCadence = null,
            GoalCadenceFrequency = null,
            GoalTarget = 1200000,
            GoalPercentageComplete = 0,
            GoalOverallLeft = 1200000
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
        var category = NewCategory() with
        {
            GoalType = CategoryGoalType.TBD,
            GoalCadence = null,
            GoalCadenceFrequency = null,
            GoalTarget = 1345000,
            GoalMonthsToBudget = 4,
            GoalPercentageComplete = 0,
            GoalOverallLeft = 1345000
        };

        // Act.
        var actual = category.MonthlyNeed();

        // Assert.
        Assert.Equal(336250L, actual);
    }
}
