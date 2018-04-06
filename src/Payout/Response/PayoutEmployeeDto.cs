using System;

namespace Payout.Response
{
    public class PayoutEmployeeDto
    {
        public Guid EmployeeId { get; set; }
        public string BankAccountNumber { get; set; }
    }
}