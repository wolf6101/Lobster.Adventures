namespace Lobster.Adventures.Domain.SeedWork
{
    public interface IBusinessRule
    {
        Task<bool> IsBroken();
        string Name { get; }
        string Message { get; }
    }
}