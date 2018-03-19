using System;

namespace Employee.Requests
{
    public class AddEmployeeBankInfoRequest
    {
        public Guid EmployeeId { get; set; }
        public string BankAccountNumber { get; set; }
    }
}