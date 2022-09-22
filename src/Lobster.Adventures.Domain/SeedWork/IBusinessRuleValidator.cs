namespace Lobster.Adventures.Domain.SeedWork
{
    public interface IBusinessRuleValidator
    {
        Task AssertRules(IEnumerable<IBusinessRule> rules);
    }
}