namespace Lobster.Adventures.Domain.SeedWork
{

    public class BusinessRuleValidator : IBusinessRuleValidator
    {
        public async Task AssertRules(IEnumerable<IBusinessRule> rules)
        {
            foreach (var rule in rules)
            {
                await CheckRule(rule);
            }
        }

        protected async Task CheckRule(IBusinessRule rule)
        {
            var isBroken = await rule.IsBroken();
            if (isBroken)
            {
                throw new BusinessRuleValidationException(rule);
            }
        }
    }
}