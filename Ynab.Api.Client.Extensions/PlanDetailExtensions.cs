using Ynab.Api.Client.Models;

namespace Ynab.Api.Client.Extensions;

public static class PlanDetailExtensions
{
    /// <summary>
    /// Finds a category or category group with the given name. It first looks
    /// for an exact match on the category or group name and if not found then
    /// it looks for a partial match. Only active and visible categories and
    /// groups are searched. A category or group is returned, never both. If
    /// no match is found then null is returned for both.
    /// </summary>
    public static (Category?, CategoryGroup?) FindCategoryOrGroup(this PlanDetail planDetail, string categoryOrGroupName)
    {
        var categories = planDetail.Categories!.Where(c => c.Hidden == false && c.Deleted == false).ToList();
        var categoryGroups = planDetail.CategoryGroups!.Where(g => g.Hidden == false && g.Deleted == false).ToList();

        // See if the name term matches a category exactly.
        var category = categories.FirstOrDefault(c => c.Name.Equals(categoryOrGroupName, StringComparison.InvariantCultureIgnoreCase));
        if (category != null)
            return (category, null);

        // See if the name matches a category group exactly.
        var categoryGroup = categoryGroups.FirstOrDefault(g => g.Name.Equals(categoryOrGroupName, StringComparison.InvariantCultureIgnoreCase));
        if (categoryGroup != null)
            return (null, categoryGroup);

        // See if the name matches a category partially.
        category = categories.FirstOrDefault(c => c.Name.Contains(categoryOrGroupName, StringComparison.InvariantCultureIgnoreCase));
        if (category != null)
            return (category, null);

        // See if the name matches a category group partially.
        categoryGroup = categoryGroups.FirstOrDefault(g => g.Name.Contains(categoryOrGroupName, StringComparison.InvariantCultureIgnoreCase));
        if (categoryGroup != null)
            return (null, categoryGroup);

        return (null, null);
    }
}
