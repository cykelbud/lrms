using System.Threading.Tasks;
using Payout.Requests;

namespace Payout.Core.ApplicationServices
{
    public interface IPayoutService
    {
        Task PayEmployee(PayEmployeeRequest request);
    }
}
