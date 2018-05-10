using System;

namespace Invoice.Requests
{
    public class CreateInvoiceRequest
    {
        public Guid EmployeeId { get; set; }
        public Guid CustomerId { get; set; }

        public bool PayInAdvance { get; set; }
        public bool HasTaxReduction { get; set; } // RUT / ROT
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string InvoiceDescription { get; set; }
        public string Name { get; set; }
        public decimal Vat { get; set; }
        public InvoiceItemDto[] InvoiceItems { get; set; }
    }
}
