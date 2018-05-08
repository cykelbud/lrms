using System.Threading.Tasks;

namespace Payout.Core.ApplicationServices
{
    internal interface ISkatteverket
    {
        Task Pay(string ocr, decimal totalTax);
    }

    internal class Skatteverket : ISkatteverket
    {
        public Task Pay(string ocr, decimal totalTax)
        {
            return Task.CompletedTask;
        }
    }
}