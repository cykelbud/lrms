using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Payout.Requests;
using Payout.Response;

namespace Payout.Core.ApplicationServices
{
    public interface IPayoutService
    {
        Task PayEmployee(PayEmployeeRequest request);
        Task<IEnumerable<PayoutDto>> GetAll();
        Task<PayoutDto> GetPayout(Guid invoiceId);
    }
}
