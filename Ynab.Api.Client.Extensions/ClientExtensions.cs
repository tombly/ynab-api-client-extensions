using Ynab.Api.Client.Extensions.Exceptions;
using Ynab.Api.Client.Models;

namespace Ynab.Api.Client.Extensions;

public static class YnabApiClientExtensions
{
    /// <summary>
    /// Retrieves the plan detail for the specified plan name. If no plan
    /// name is provided, the first plan is returned.
    /// </summary>
    public async static Task<PlanDetail> GetPlanDetailAsync(this IYnabApiClient client, string? planName = null)
    {
        // Retrieve the plan summary.
        var planSummary = (planName != null ?
            (await client.GetPlansAsync(false)).Plans.FirstOrDefault(p => p.Name == planName) ?? throw new PlanNotFoundException(planName) :
            (await client.GetPlansAsync(false)).Plans.First()) ?? throw new PlanNotFoundException();

        // Retrieve the plan detail from the API.
        return (await client.GetPlanByIdAsync(planSummary.Id.ToString(), null)).Plan;
    }
}