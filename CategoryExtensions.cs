namespace Ynab.Api.Client.Extensions;

public static class CategoryExtensions
{
    /// <summary>
    /// Figures out what the monthly need of a category is (shown as "Needed This
    /// Month" in the YNAB website). It first calculates a multiplier based on
    /// the repeat frequency and then uses it to calculate the monthly need based
    /// on the remaining amount to reach the target. For non-recurring goals it
    /// simply divides the remaining amount by the remaining months.
    /// </summary>
    public static decimal MonthlyNeed(this Category category)
    {
        var multiplier = default(decimal?);
        switch (category.Goal_cadence)
        {
            case 0: // No repeat
                break;
            case 1: // Monthly
                multiplier = 1 / category.Goal_cadence_frequency;
                break;
            case 2: // Weekly
                multiplier = 4 * category.Goal_cadence_frequency;
                break;
            case 3: // Every 2 months
            case 4: // Every 3 months
            case 5: // Every 4 months
            case 6: // Every 5 months
            case 7: // Every 6 months
            case 8: // Every 7 months
            case 9: // Every 8 months
            case 10: // Every 9 months
            case 11: // Every 10 months
            case 12: // Every 11 months
                multiplier = 1 / (category.Goal_cadence - 1);
                break;
            case 13: // Yearly
                multiplier = 1 / (12 * category.Goal_cadence_frequency);
                break;
            case 14: // Every 2 years
                multiplier = 1 / 24;
                break;
        }

        // If we have a multiplier then it's recurring.
        if (multiplier != null)
        {
            return category.Goal_target * multiplier ?? 0;
        }
        else
        {
            if (category.Goal_overall_left > 0)
                return category.Goal_overall_left / category.Goal_months_to_budget ?? 0;
            else
                return 0;
        }
    }
}