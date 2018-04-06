using System;
using Invoice.Core.DomainModel;

namespace Invoice.Response
{
    public class InvoiceDto
    {
        public Guid InvoiceId { get; set; }
        public Guid EmployeeId { get; set; }
        public Guid CustomerId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string InvoiceDescription { get; set; }
        public string Name { get; set; }
        public decimal Vat { get; set; }
        public InvoiceItem[] InvoiceItems { get; set; }
    }


    public class InvoiceCustomerDto
    {
        public Guid CustomerId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
    }

    public class InvoiceEmployeeDto
    {
        public Guid EmployeeId { get; set; }
        public string Name { get; set; }
    }
}
