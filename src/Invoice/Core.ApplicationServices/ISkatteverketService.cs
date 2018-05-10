using System.Threading.Tasks;

namespace Invoice.Core.ApplicationServices
{
    public interface ISkatteverketService
    {
        Task ApplyForReduction(string customerName, decimal reductionAmount);
    }

    class SkatteverketService : ISkatteverketService
    {
        public Task ApplyForReduction(string customerName, decimal reductionAmount)
        {
            return Task.CompletedTask;
        }
    }
}