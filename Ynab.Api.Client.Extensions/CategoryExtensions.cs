namespace Ynab.Api.Client.Extensions;

public static class CategoryExtensions
{
    /// <summary>
    /// Calculates how much money is needed each month to reach a category's
    /// target. This is intended for planning purposes so the calculated amount
    /// reflects the monthly amount needed for each month of the goal period,
    /// not how much is needed per month to reach the next goal.  
    /// </summary>
    public static long MonthlyNeed(this Category category)
    {
        // Targets are either eventual, one-time, or recurring. Eventual targets
        // have no specific due date and have a goal cadence of null.
        // 
        // One-time targets have a goal cadence of 0, in which case we consider
        // the monthly need to be the overall amount left divided by the number
        // of months left in the goal period (includes the current month).
        //
        // The cadence is null if the want is "Have a balance of ..." (type
        // "TB" or "TBD"). If the other two (type "NEED") then the cadence is
        // 0 to indicate that the target is a one-time target.
        if (category.Goal_cadence == null)
            return 0;
        
        if (category.Goal_cadence == 0)
        {
            if (category.Goal_overall_left > 0 && category.Goal_months_to_budget > 0)
                return (long)(category.Goal_overall_left / category.Goal_months_to_budget);
            else
                return 0;
        }

        // Recurring targets are either weekly (2), monthly (1), yearly (13),
        // or custom (3-12, 14). Weekly, monthly, and yearly targets have a
        // cadence frequency that determines how often the target is due. For
        // example, a monthly target with a cadence frequency of 2 means the
        // target is due every other month. For custom targets, the cadence
        // frequency is ignored and the target is due every goal_cadence, where
        // 3 = Every 2 Months, 4 = Every 3 Months, ..., 12 = Every 11 Months,
        // and 14 = Every 2 Years.

        // Figure out how many months until the next goal is due.
        var monthsToTarget = category.Goal_cadence switch
        {
            1 => category.Goal_cadence_frequency,           // Monthly
            2 => category.Goal_cadence_frequency / 4d,      // Weekly
            13 => category.Goal_cadence_frequency * 12d,    // Yearly
            14 => 24,                                       // Every 2 years
            3 => 2,                                         // Every 2 months
            4 => 3,                                         // Every 3 months
            5 => 4,                                         // Every 4 months
            6 => 5,                                         // Every 5 months
            7 => 6,                                         // Every 6 months
            8 => 7,                                         // Every 7 months
            9 => 8,                                         // Every 8 months
            10 => 9,                                        // Every 9 months
            11 => 10,                                       // Every 10 months
            12 => 11,                                       // Every 11 months
            _ => throw new Exception($"Unrecognized goal cadence: '{category.Goal_cadence}'")
        };

        return (long)((category.Goal_target ?? 0) * (1d / monthsToTarget!));
    }
}