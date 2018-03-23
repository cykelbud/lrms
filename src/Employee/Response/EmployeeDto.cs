using System;

namespace Employee.Response
{
    public class EmployeeDto
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string PersonalIdentificationNumber { get; set; }
        public string BankAccountNumber { get; set; }
    }
}