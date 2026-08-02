namespace Ynab.Api.Client.Extensions.Exceptions;

public class PlanNotFoundException : Exception
{
    public PlanNotFoundException(string planName) : base($"Plan '{planName}' not found")
    {
    }

    public PlanNotFoundException() : base("No plans found")
    {
    }
}
