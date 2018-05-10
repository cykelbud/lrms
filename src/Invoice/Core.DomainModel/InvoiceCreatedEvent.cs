using System;
using EventFlow.Aggregates;

namespace Invoice.Core.DomainModel
{
    public class InvoiceCreatedEvent : AggregateEvent<InvoiceAggregate, InvoiceId>
    {
        public Guid EmployeeId { get; }
        public Guid CustomerId { get; }
        public DateTime StartDate { get;  }
        public DateTime EndDate { get;  }
        public string InvoiceDescription { get;  }
        public string Name { get;  }
        public decimal Vat { get;  }
        public InvoiceItem[] InvoiceItems { get;  }
        public bool PayInAdvance { get; }
        public bool HasTaxReduction { get; } // RUT / ROT


        public InvoiceCreatedEvent(Guid employeeId, Guid customerId, DateTime startDate, DateTime endDate, string invoiceDescription, string name, decimal vat, InvoiceItem[] invoiceItems, bool payInAdvance, bool hasTaxReduction)
        {
            EmployeeId = employeeId;
            CustomerId = customerId;
            StartDate = startDate;
            EndDate = endDate;
            InvoiceDescription = invoiceDescription;
            Name = name;
            Vat = vat;
            InvoiceItems = invoiceItems;
            PayInAdvance = payInAdvance;
            HasTaxReduction = hasTaxReduction;
        }
    }
}