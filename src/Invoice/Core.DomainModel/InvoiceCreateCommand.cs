using System;
using System.Threading;
using System.Threading.Tasks;
using EventFlow.Commands;

namespace Invoice.Core.DomainModel
{
    public class InvoiceCreateCommand : Command<InvoiceAggregate,InvoiceId>
    {
        public Guid EmployeeId { get; }
        public Guid CustomerId { get; }
        public bool PayInAdvance { get; }
        public bool HasTaxReduction { get; } // RUT / ROT
        public DateTime StartDate { get; }
        public DateTime EndDate { get; }
        public string InvoiceDescription { get; }
        public string Name { get; }
        public decimal Vat { get; }
        public InvoiceItem[] InvoiceItems { get; }

        public InvoiceCreateCommand(InvoiceId aggregateId, Guid employeeId, Guid customerId, DateTime startDate, DateTime endDate, string invoiceDescription, string name, decimal vat, InvoiceItem[] invoiceItems, bool payInAdvance, bool hasTaxReduction) : base(aggregateId)
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

    public class InvoiceCreateCommandHandler : CommandHandler<InvoiceAggregate, InvoiceId, InvoiceCreateCommand>
    {
        public override Task ExecuteAsync(InvoiceAggregate aggregate, InvoiceCreateCommand command, CancellationToken cancellationToken)
        {
            aggregate.CreateInvoice(command);
            return Task.CompletedTask;
        }
    }
}