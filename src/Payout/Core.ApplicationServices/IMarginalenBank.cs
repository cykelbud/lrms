using System.Threading.Tasks;

namespace Payout.Core.ApplicationServices
{
    internal interface IMarginalenBank
    {
        Task Pay(string accountNo, decimal afterTax);
    }

    internal class MarginalenBank : IMarginalenBank
    {
        public Task Pay(string accountNo, decimal afterTax)
        {
            return Task.CompletedTask;
        }
    }
}